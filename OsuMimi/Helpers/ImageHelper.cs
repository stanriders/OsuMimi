using System;
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
    }
}
