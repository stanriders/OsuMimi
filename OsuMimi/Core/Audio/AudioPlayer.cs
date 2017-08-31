// Copyright (c) 2016-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ManagedBass;
using ManagedBass.Fx;
using ManagedBass.DirectX8;

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
        // эффекты
        private int fx1, fx2, fx3;

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
            get { return nightcore; }
            set
            {
                nightcore = value;
                ApplyNightcore();
            }
        }

        public bool Bassboost
        {
            get { return bassboost; }
            set
            {
                bassboost = value;
                ApplyBassboost();
            }
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
            ApplyEffects();

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

        private void ApplyEffects()
        {
            // TODO: fix overlaps
            ApplyDoubletime();
            ApplyNightcore();
            ApplyBassboost();
        }

        private void ApplyDoubletime()
        {
            Bass.ChannelSetAttribute(activeHandle, ChannelAttribute.Tempo, doubletime ? 50 : 0);
        }

        // TODO: rewrite this shit 
        private void ApplyNightcore()
        {
            // tempo
            Bass.ChannelSetAttribute(activeHandle, ChannelAttribute.Tempo, nightcore ? 30 : 0);
            // pitch
            Bass.ChannelSetAttribute(activeHandle, ChannelAttribute.Pitch, nightcore ? 3.5d : 0d);
            // bass
            Bass.ChannelRemoveFX(activeHandle, fx3);
            if (nightcore)
            {
                var parameters = new DXParamEQParameters
                {
                    fBandwidth = 24f,
                    fCenter = 80f,
                    fGain = 6f
                };
                fx3 = Bass.ChannelSetFX(activeHandle, EffectType.DXParamEQ, -3);
                Bass.FXSetParameters(fx3, parameters);
            }            
        }

        private void ApplyBassboost()
        {
            Bass.ChannelRemoveFX(activeHandle, fx1);
            Bass.ChannelRemoveFX(activeHandle, fx2);
            Bass.FXReset(activeHandle);

            if (bassboost)
            {
                var parameters = new DXParamEQParameters
                {
                    fBandwidth = 24f,
                    fCenter = 80f,
                    fGain = 15f
                };

                fx1 = Bass.ChannelSetFX(activeHandle, EffectType.DXParamEQ, -1);
                Bass.FXSetParameters(fx1, parameters);

                parameters.fCenter = 100f;
                fx2 = Bass.ChannelSetFX(activeHandle, EffectType.DXParamEQ, -1);
                Bass.FXSetParameters(fx2, parameters);
            }
        }
    }
}
