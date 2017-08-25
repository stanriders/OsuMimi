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
        public TimeSpan Duration
        {
            get;
        }

        public TimeSpan Position
        {
            get;
            set;
        }

        public PlayerStatus Status
        {
            get;
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

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void OpenFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public event EventHandler OnTrackEnd;
    }
}
