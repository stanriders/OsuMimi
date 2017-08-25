// Copyright (c) 2017-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

namespace OsuMimi.Core.Configuration
{
    /// <summary>
    /// Определяет настройки программы
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Путь к папке с игрой
        /// </summary>
        public string OsuDirectory { get; set; }

        /// <summary>
        /// Последний воспроизводимый файл
        /// </summary>
        public int LastPlayed { get; set; }

        /// <summary>
        /// Статус рандома
        /// </summary>
        public bool Random { get; set; }

        /// <summary>
        /// Статус даблтайма
        /// </summary>
        public bool DoubleTime { get; set; }

        /// <summary>
        /// Статус найткора
        /// </summary>
        public bool Nightcore { get; set; }

        /// <summary>
        /// Статус басбуста
        /// </summary>
        public bool Bassboost { get; set; }
    }
}
