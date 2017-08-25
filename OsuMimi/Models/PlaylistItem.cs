using OsuMimi.MVVM;

namespace OsuMimi.Models
{
    class PlaylistItem : NotifyBase
    {
        private string artist;

        /// <summary>
        /// Исполнитель
        /// </summary>
        public string Artist
        {
            get { return artist; }
            set { artist = value; }
        }

        private string title;

        /// <summary>
        /// Название
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// Индекс
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Директория
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Аудиофайл
        /// </summary>
        public string Audiofile { get; set; }

        /// <summary>
        /// Осу файл
        /// </summary>
        public string OsuFile { get; set; }

        /// <summary>
        /// Текущий ли трек
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}
