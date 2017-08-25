using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OsuMimi.MVVM
{
    abstract class NotifyBase : INotifyPropertyChanged
    {
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
