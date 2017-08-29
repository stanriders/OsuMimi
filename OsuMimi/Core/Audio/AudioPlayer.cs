// Copyright (c) 2016-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

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
        // the handle for this track
        private int activeHandle;

        // the handle for applying effects
        private int streamHandle;

        // статус плеера
        private PlayerStatus status;

        // режимы
        private bool doubletime;
        private bool nightcore;
        private bool bassboost;

        public TimeSpan Duration
        {
            get
            {
                var seconds = Bass.ChannelBytes2Seconds(activeHandle, Bass.ChannelGetLength(activeHandle));
                return TimeSpan.FromSeconds(seconds);
            }
        }

        public TimeSpan Position
        {
            get
            {
                var seconds = Bass.ChannelBytes2Seconds(activeHandle, Bass.ChannelGetPosition(activeHandle));
                return TimeSpan.FromSeconds(seconds);
            }
            set
            {
                var position = Bass.ChannelSeconds2Bytes(activeHandle, value.TotalSeconds);
                Bass.ChannelSetPosition(activeHandle, (long)position);
            }
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
            get { return doubletime; }
            set
            {
                doubletime = value;
                ApplyDoubletime();
            }
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
            Unload();
            Bass.Free();
        }

        public void OpenFile(string filePath)
        {
            Unload();

            streamHandle = Bass.CreateStream(filePath, Flags: BassFlags.Decode | BassFlags.Prescan);
            activeHandle = BassFx.TempoCreate(streamHandle, BassFlags.FxFreeSource);

            Bass.ChannelSetAttribute(activeHandle, ChannelAttribute.TempoUseQuickAlgorithm, 1);

            status = PlayerStatus.FileLoaded;
        }

        public void Play()
        {
            switch (Status)
            {
                case PlayerStatus.NoFile:
                    break;
                case PlayerStatus.FileLoaded:
                    Bass.ChannelPlay(activeHandle, true);
                    status = PlayerStatus.Play;
                    break;
                case PlayerStatus.Pause:
                    Bass.ChannelPlay(activeHandle, false);
                    status = PlayerStatus.Play;
                    break;
            }
        }

        public void Stop()
        {
            Bass.ChannelStop(activeHandle);
            status = PlayerStatus.FileLoaded;
        }

        public void Pause()
        {
            if (Status == PlayerStatus.Play)
            {
                Bass.ChannelPause(activeHandle);
                status = PlayerStatus.Pause;
            }
        }

        public event EventHandler OnTrackEnd;

        private void Unload()
        {
            if (activeHandle != 0)
            {
                Bass.ChannelStop(activeHandle);
                Bass.StreamFree(activeHandle);
                activeHandle = 0;
            }
        }

        private void ApplyDoubletime()
        {
            Bass.ChannelSetAttribute(activeHandle, ChannelAttribute.Tempo, doubletime ? 50 : 0);
        }

        private void ApplyBassboost()
        {

        }
    }
}
