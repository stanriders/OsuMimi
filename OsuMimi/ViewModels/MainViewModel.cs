using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuMimi.MVVM;
using System.Windows.Media;
using System.Collections.ObjectModel;
using OsuMimi.Core.Audio;
using OsuMimi.Helpers;
using OsuMimi.Core.OsuDatabase;
using OsuMimi.Models;
using System.Windows;

namespace OsuMimi.ViewModels
{
    class MainViewModel : NotifyBase
    {
        private AudioPlayer audioplayer;

        // приватные поля
        private string artist;
        private string title;
        private string currentTime;
        private string totalTime;
        private ImageSource playButtonImage;
        private ImageSource backgroundImage;

        /// <summary>
        /// Текущий исполнитель
        /// </summary>
        public string Artist
        {
            get { return artist; }
            set
            {
                artist = value;
                OnPropertyChanged("Artist");
            }
        }

        /// <summary>
        /// Название текущей песни
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Текущее время
        /// </summary>
        public string CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                OnPropertyChanged("CurrentTime");
            }
        }

        /// <summary>
        /// Общее время
        /// </summary>
        public string TotalTime
        {
            get { return totalTime; }
            set
            {
                totalTime = value;
                OnPropertyChanged("TotalTime");
            }
        }

        /// <summary>
        /// Изображение на кнопке воспроизведения
        /// </summary>
        public ImageSource PlayButtonImage
        {
            get { return playButtonImage; }
            set
            {
                playButtonImage = value;
                OnPropertyChanged("PlayButtonImage");
            }
        }

        /// <summary>
        /// Фонова картинка
        /// </summary>
        public ImageSource BackgroundImage
        {
            get { return backgroundImage; }
            set
            {
                backgroundImage = value;
                OnPropertyChanged("BackgroundImage");
            }
        }

        /// <summary>
        /// Коллекция с плейлистом
        /// </summary>
        public ObservableCollection<PlaylistItem> PlaylistItems { get; set; }

        /// <summary>
        /// Включить предыдущую песню
        /// </summary>
        public RelayCommand PreviousSongCommand { get; set; }

        /// <summary>
        /// Включить следующую песню
        /// </summary>
        public RelayCommand NextSongCommand { get; set; }

        /// <summary>
        /// Воспроизведение/пауза
        /// </summary>
        public RelayCommand PlaySongCommand { get; set; }

        /// <summary>
        /// Выбран трек (двойным кликом)
        /// </summary>
        public RelayCommand TrackSelectedCommand { get; set; }

        /// <summary>
        /// Поиск
        /// </summary>
        public RelayCommand SearchCommand { get; set; }
        
        /// <summary>
        /// Случайная песня
        /// </summary>
        public RelayCommand RandomCommand { get; set; }

        /// <summary>
        /// Даблтайм
        /// </summary>
        public RelayCommand DoubleTimeCommand { get; set; }

        /// <summary>
        /// Найткор
        /// </summary>
        public RelayCommand NightcoreCommand { get; set; }

        /// <summary>
        /// Басбуст
        /// </summary>
        public RelayCommand BassboostCommand { get; set; }

        /// <summary>
        /// Информация
        /// </summary>
        public RelayCommand InfoCommand { get; set; }

        public MainViewModel()
        {
            LoadCommands();
            LoadPlaylist();
            audioplayer = new AudioPlayer();
            audioplayer.Initialize();

            BackgroundImage = ImageHelper.LoadFromFile(@"C:\Users\nyan\Desktop\photo_2017-08-16_16-48-42.jpg");
        }

        private void LoadCommands()
        {
            SearchCommand = new RelayCommand((a) => { });
            PreviousSongCommand = new RelayCommand(PreviousSongAction);
            NextSongCommand = new RelayCommand(NextSongAction);
            PlaySongCommand = new RelayCommand(PlaySongAction);
            TrackSelectedCommand = new RelayCommand(TrackSelected);
            RandomCommand = new RelayCommand(RandomAction);
            DoubleTimeCommand = new RelayCommand(DoubleTimeAction);
            NightcoreCommand = new RelayCommand(NightcoreAction);
            BassboostCommand = new RelayCommand(BassboostAction);
        }

        private void PreviousSongAction(object obj)
        {
            throw new NotImplementedException();
        }

        private void NextSongAction(object obj)
        {
            throw new NotImplementedException();
        }

        private void PlaySongAction(object obj)
        {
            if (audioplayer.Status == PlayerStatus.NoFile)
            {
                return;
            }
            else if (audioplayer.Status == PlayerStatus.Pause)
            {
                audioplayer.Play();
                PlayButtonImage = ImageHelper.LoadFromAssembly("pause.png");
            }
            else if (audioplayer.Status == PlayerStatus.Play)
            {
                audioplayer.Pause();
                PlayButtonImage = ImageHelper.LoadFromAssembly("play.png");
            }
        }

        private void TrackSelected(object obj)
        {
            var item = (PlaylistItem)obj;

            Artist = item.Artist;
            Title = item.Title;
            audioplayer.OpenFile(Helpers.PathHelper.SCombine(item.Directory, item.Audiofile));
            audioplayer.Play();
        }

        private void BassboostAction(object obj)
        {
            throw new NotImplementedException();
        }

        private void NightcoreAction(object obj)
        {
            throw new NotImplementedException();
        }

        private void DoubleTimeAction(object obj)
        {
            throw new NotImplementedException();
        }

        private void RandomAction(object obj)
        {
            throw new NotImplementedException();
        }

        private void LoadPlaylist()
        {
            OsuDatabase db = new OsuDatabase(PathHelper.Combine("osu!.db"));
            var result = new List<PlaylistItem>();

            int id = 0;
            foreach (var i in db.Beatmaps)
            {
                bool shouldBeAdded = result.Any(x => x.Directory == i.FolderName && x.Audiofile == i.AudioFileName);

                if (!shouldBeAdded)
                {
                    result.Add(new PlaylistItem
                    {
                        Index = id++,
                        Audiofile = i.AudioFileName,
                        Artist = i.ArtistName,
                        Title = i.SongTitle,
                        Directory = i.FolderName,
                        OsuFile = i.NameOfOsuFile,
                        IsCurrent = false
                    });
                }
            }

            PlaylistItems = new ObservableCollection<PlaylistItem>(result);
        }
    }
}
