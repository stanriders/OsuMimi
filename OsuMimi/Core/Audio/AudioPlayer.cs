using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ManagedBass;
using ManagedBass.Fx;

namespace OsuMimi.Core.Audio
{
    class AudioPlayer : IDisposable
    {
        private int handle;

        private PlayerStatus status;

        public TimeSpan Duration
        {
            get
            {
                return TimeSpan.Zero;
            }
        }

        public TimeSpan Position
        {
            get;
            set;
        }

        public PlayerStatus Status
        {
            get
            {
                return status;
            }
        }

        public bool DoubleTime
        {
            get;
            set;
        }

        public bool Nightcore
        {
            get;
            set;
        }

        public bool Bassboost
        {
            get;
            set;
        }

        public void Initialize()
        {
            Bass.Init();
        }

        public void Dispose()
        {
            Bass.Free();
        }

        public void OpenFile(string filePath)
        {
            Bass.StreamFree(handle);
            handle = Bass.CreateStream(filePath);
            status = PlayerStatus.FileLoaded;
        }

        public void Play()
        {
            switch (Status)
            {
                case PlayerStatus.NoFile:
                    break;
                case PlayerStatus.FileLoaded:
                    Bass.ChannelPlay(handle, true);
                    status = PlayerStatus.Play;
                    break;
                case PlayerStatus.Pause:
                    Bass.ChannelPlay(handle, false);
                    status = PlayerStatus.Play;
                    break;
            }
        }

        public void Stop()
        {
            Bass.ChannelStop(handle);
            status = PlayerStatus.FileLoaded;
        }

        public void Pause()
        {
            if (Status == PlayerStatus.Play)
            {
                Bass.ChannelPause(handle);
                status = PlayerStatus.Pause;
            }
        }

        public event EventHandler OnTrackEnd;
    }
}
