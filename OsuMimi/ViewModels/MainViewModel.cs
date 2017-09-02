// Copyright (c) 2016-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

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
        private double currentPosition;
        private ImageSource playButtonImage;
        private ImageSource shuffleButtonImage;
        private ImageSource repeatButtonImage;
        private ImageSource backgroundImage;

        private bool isShuffling = false;
        private bool isRepeating = false;

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
        /// Текущая позиция
        /// </summary>
        public double CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                currentPosition = value;
                OnPropertyChanged("CurrentPosition");
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

        public PlaylistItem CurrentSong { get; set; }

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
        /// Клик по трекбару
        /// </summary>
        public RelayCommand TrackbarCommand { get; set; }

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

        public RelayCommand ShuffleCommand { get; set; }
        public RelayCommand RepeatCommand { get; set; }
        public ImageSource ShuffleButtonImage
        {
            get { return shuffleButtonImage; }
            set
            {
                shuffleButtonImage = value;
                OnPropertyChanged("ShuffleButtonImage");
            }
        }
        public ImageSource RepeatButtonImage
        {
            get { return repeatButtonImage; }
            set
            {
                repeatButtonImage = value;
                OnPropertyChanged("RepeatButtonImage");
            }
        }
        private void ShuffleAction(object obj)
        {
            if (isShuffling)
            {
                ShuffleButtonImage = ImageHelper.LoadFromAssembly("random.png");
                isShuffling = false;
            }
            else
            {
                ShuffleButtonImage = ImageHelper.LoadFromAssembly("pause.png");
                isShuffling = true;
            }
        }
        private void RepeatAction(object obj)
        {
            if (isRepeating)
            {
                RepeatButtonImage = ImageHelper.LoadFromAssembly("play.png");
                isRepeating = false;
            }
            else
            {
                RepeatButtonImage = ImageHelper.LoadFromAssembly("pause.png");
                isRepeating = true;
            }
        }

        public MainViewModel()
        {
            LoadCommands();
            LoadPlaylist();
            audioplayer = new AudioPlayer();
            audioplayer.OnTrackEnded += audioplayer_OnTrackEnd;
            audioplayer.Initialize();
            SetUpdateTimer();
        }

        private void SetUpdateTimer()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(250);
            timer.Tick += (e, s) =>
            {
                if (audioplayer.Status != PlayerStatus.Play)
                    return;

                var pos = audioplayer.Position;
                var length = audioplayer.Duration;

                CurrentTime = pos.ToString(@"hh\:mm\:ss");
                double proc = pos.TotalMilliseconds / length.TotalMilliseconds * 100d;

                CurrentPosition = proc;
            };
            timer.Start();
        }

        private void LoadCommands()
        {
            SearchCommand = new RelayCommand((a) => { });
            ShuffleCommand = new RelayCommand(ShuffleAction);
            RepeatCommand = new RelayCommand(RepeatAction);
            PreviousSongCommand = new RelayCommand(PreviousSongAction);
            NextSongCommand = new RelayCommand(NextSongAction);
            PlaySongCommand = new RelayCommand(PlaySongAction);
            TrackSelectedCommand = new RelayCommand(TrackSelected);
            TrackbarCommand = new RelayCommand(TrackbarAction);
            RandomCommand = new RelayCommand(RandomAction);
            DoubleTimeCommand = new RelayCommand(DoubleTimeAction);
            NightcoreCommand = new RelayCommand(NightcoreAction);
            BassboostCommand = new RelayCommand(BassboostAction);

            PlayButtonImage = ImageHelper.LoadFromAssembly("play.png");
            ShuffleButtonImage = ImageHelper.LoadFromAssembly("random.png");
            RepeatButtonImage = ImageHelper.LoadFromAssembly("play.png");
        }

        private void PreviousSongAction(object obj)
        {
            PreviousSong();
        }

        private void NextSongAction(object obj)
        {
            NextSong();
        }

        private void PlaySongAction(object obj)
        {
            if (audioplayer.Status == PlayerStatus.NoFile)
            {
                PlaySong(PlaylistItems[0]);
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
            PlaySong(item);
        }

        private void TrackbarAction(object obj)
        {
            var length = audioplayer.Duration.TotalMilliseconds;
            var pos = (double)obj;

            var newpos = length / 100d * pos;
            audioplayer.Position = TimeSpan.FromMilliseconds(newpos);
        }

        private void BassboostAction(object obj)
        {
            var isdown = (bool)obj;
            audioplayer.Bassboost = isdown;
        }

        private void NightcoreAction(object obj)
        {
            var isdown = (bool)obj;
            audioplayer.Nightcore = isdown;
        }

        private void DoubleTimeAction(object obj)
        {
            var isdown = (bool)obj;
            audioplayer.DoubleTime = isdown;
        }

        private void RandomAction(object obj)
        {
            throw new NotImplementedException();
        }

        void audioplayer_OnTrackEnd(object sender, EventArgs e)
        {
            if (isRepeating)
                PlaySong(CurrentSong);
            else
                NextSong();
        }

        private void PreviousSong()
        {
            int prevTrack = CurrentSong.Index - 1;
            PlaySong(PlaylistItems[prevTrack]);
        }

        private void NextSong()
        {
            int nextTrack = CurrentSong.Index + 1;
            if (isShuffling)
                nextTrack = new Random().Next(PlaylistItems.Count);

            PlaySong(PlaylistItems[nextTrack]);
        }

        private void PlaySong(PlaylistItem song)
        {
            Artist = song.Artist;
            Title = song.Title;
            audioplayer.OpenFile(Helpers.PathHelper.SCombine(song.Directory, song.Audiofile));
            audioplayer.Play();
            PlayButtonImage = ImageHelper.LoadFromAssembly("pause.png");
            string bgFile = ImageHelper.GetBackgroundName(PathHelper.SCombine(song.Directory, song.OsuFile));
            if (System.IO.File.Exists(PathHelper.SCombine(song.Directory, bgFile)))
                BackgroundImage = ImageHelper.LoadFromFile(PathHelper.SCombine(song.Directory, bgFile));
            else
                BackgroundImage = null;
            TotalTime = audioplayer.Duration.ToString(@"hh\:mm\:ss");
            song.IsCurrent = true;
            CurrentSong = song;
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
