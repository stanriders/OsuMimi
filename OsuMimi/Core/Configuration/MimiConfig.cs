// Copyright (c) 2016-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System.Xml.Serialization;
using System.IO;
using OsuMimi.Helpers;

namespace OsuMimi.Core.Configuration
{
    public static class MimiConfig
    {
        /// <summary>
        /// Параметры программы
        /// </summary>
        public static Settings Settings { get; set; }

        private const string ConfigFileName = "config.xml";

        static MimiConfig()
        {
            Settings = LoadSettings();
        }

        private static Settings LoadSettings()
        {
            var path = Path.Combine(PathHelper.GetMimiPath(), ConfigFileName);

            if (File.Exists(path))
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var serializer = new XmlSerializer(typeof(Settings));
                    try
                    {
                        var result = serializer.Deserialize(stream) as Settings;
                        return result ?? new Settings();
                    }
                    catch
                    {
                        return new Settings();
                    }
                }
            }
            else
            {
                return new Settings();
            }
        }

        /// <summary>
        /// Сохраняет настройки программы в файл
        /// </summary>
        public static void SaveSettings()
        {
            var path = Path.Combine(PathHelper.GetMimiPath(), ConfigFileName);

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                var serializer = new XmlSerializer(typeof(Settings));
                try
                {
                    serializer.Serialize(stream, Settings);
                }
                catch { }
            }
        }
    }
}
