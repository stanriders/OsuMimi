// Copyright (c) 2017-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace OsuMimi.Helpers
{
    class ImageHelper
    {
        const string AssemblyPath = @"pack://application:,,,/osuMimi;component/Images/";

        /// <summary>
        /// Загружает картинку из сборки
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Загруженная картинка</returns>
        public static BitmapImage LoadFromAssembly(string fileName)
        {
            return Load(AssemblyPath + fileName);
        }

        /// <summary>
        /// Загружает картинку из файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Загруженная картинка</returns>
        public static BitmapImage LoadFromFile(string path)
        {
            return Load(path);
        }

        private static BitmapImage Load(string fullPath)
        {
            var image = new BitmapImage();
            var uri = new Uri(fullPath, UriKind.Absolute);

            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = uri;
            image.EndInit();

            return image;
        }

        public static string GetBackgroundName(string osuFile)
        {
            if (!File.Exists(osuFile))
            {
                return string.Empty;
            }

            using (var stream = new FileStream(osuFile, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(stream);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("0,"))
                    {
                        if (line.Contains(@".jpg""") || line.Contains(@".png"""))
                        {
                            var split = line.Split('"');
                            return split[1].Trim();
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            // default value
            return string.Empty;
        }
    }
}
