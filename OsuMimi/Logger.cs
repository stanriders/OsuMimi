// Copyright (c) 2016-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System;
using OsuMimi.Helpers;
using System.IO;

namespace OsuMimi
{
    public static class Logger
    {
        public static void LogFatal(object data)
        {
            var appPath = PathHelper.GetLogsPath();
            var logPath = Path.Combine(appPath, GetErrorLogName());

            try
            {
                File.AppendAllText(logPath, data.ToString());
            }
            catch { }
        }

        private static string GetErrorLogName()
        {
            var prefix = "Crash";
            var time = DateTime.Now.ToString(@"ddMMyyyy_HHmmss");

            return string.Format("{0}_{1}.txt", prefix, time);
        }
    }
}
