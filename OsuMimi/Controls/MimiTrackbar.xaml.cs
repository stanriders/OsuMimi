using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OsuMimi.Controls
{
    public partial class MimiTrackbar : UserControl
    {
        public MimiTrackbar()
        {
            InitializeComponent();
        }

        static DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(double), typeof(MimiTrackbar));

        static RoutedEvent ProgressChangedEvent = EventManager.RegisterRoutedEvent("ProgressChanged", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(MimiTrackbar));

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set
            {
                SetValue(ProgressProperty, value);
                SetProgress(value);
            }
        }

        public event RoutedEventHandler ProgressChanged
        {
            add
            {
                AddHandler(ProgressChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ProgressChangedEvent, value);
            }
        }

        void RaiseProgressChangedEvent()
        {
            var args = new RoutedEventArgs(ProgressChangedEvent);
            RaiseEvent(args);
        }

        private void SetProgress(double progress)
        {
            double perc = this.ActualWidth / 100d;
            double prog = progress * perc;
            this.progressRectangle.Width = prog;
        }

        private void layoutGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Progress = e.GetPosition(this).X / this.ActualWidth * 100d;
                RaiseProgressChangedEvent();
            }
        }

        private void layoutGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Progress = e.GetPosition(this).X / this.ActualWidth * 100d;
            RaiseProgressChangedEvent();
        }
    }
}
