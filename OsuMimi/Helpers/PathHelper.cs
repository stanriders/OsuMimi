// Copyright (c) 2016-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace OsuMimi.Helpers
{
    static class PathHelper
    {
        private static string osuPath = "";
        private static string songsPath = "Songs";

        static PathHelper()
        {
            osuPath = FindOsuDir();
        }

        /// <summary>
        /// Путь к папке osu!
        /// </summary>
        public static string OsuDir
        {
            get
            {
                return FindOsuDir();
            }
        }

        /// <summary>
        /// Склеивает указанный путь с путём к папке osu!/Songs
        /// </summary>
        /// <param name="paths">Путь, который требуется присоединить</param>
        /// <returns>Результат соединения</returns>
        public static string SCombine(params string[] paths)
        {
            string songs = Path.Combine(osuPath, songsPath);
            return CombineWith(songs, paths);
        }

        /// <summary>
        /// Склеивает указанный путь с путём к папке osu!
        /// </summary>
        /// <param name="paths">Пути,которые требуется присоединить</param>
        /// <returns>Результат соединения</returns>
        public static string Combine(params string[] paths)
        {
            return CombineWith(osuPath, paths);
        }

        /// <summary>
        /// Получает путь к директории, в которой можно хранить
        /// разные полезные штуки для плеера (типа конфигов, плейлистов и остальных мелочей)
        /// </summary>
        /// <returns>Путь к директории osu!mimi</returns>
        public static string GetMimiPath()
        {
            string applicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string result = Path.Combine(applicationData, "OsuMimi");

            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }

            return result;
        }

        private static string CombineWith(string basePath, params string[] paths)
        {
            char separator = Path.DirectorySeparatorChar;
            var sb = new StringBuilder();

            sb.Append(basePath);
            foreach (var path in paths)
            {
                sb.Append(separator);
                sb.Append(path);
            }

            return sb.ToString();
        }

        private static string FindOsuDir()
        {
            string result = null;

            using (var osuKey = Registry.ClassesRoot.OpenSubKey("osu!"))
            {
                using (var shellKey = osuKey.OpenSubKey("shell"))
                {
                    using (var openKey = shellKey.OpenSubKey("open"))
                    {
                        using (var commandKey = openKey.OpenSubKey("command"))
                        {
                            var val = commandKey.GetValue("");
                            if (val != null)
                            {
                                result = val.ToString().Split('"')[1];
                            }
                        }
                    }
                }
            }

            return (result == null) ? string.Empty : Path.GetDirectoryName(result);
        }
    }
}
