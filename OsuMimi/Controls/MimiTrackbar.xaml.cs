// Copyright (c) 2016-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

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

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
            "Position",
            typeof(double),
            typeof(MimiTrackbar),
            new PropertyMetadata
            {
                DefaultValue = 0d,
                PropertyChangedCallback = new PropertyChangedCallback(PositionChangedCallback)
            }
        );

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MimiTrackbar), new PropertyMetadata(null));

        public double Position
        {
            get { return (double)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        private static void PositionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (MimiTrackbar)d;

            double maxValue = obj.ActualWidth;
            double value = (double)e.NewValue;
            value = (value <= 100d) ? ((value >= 0d) ? value : 0d) : 100d;

            var width = maxValue / 100d * value;
            obj.progressRectangle.Width = width;
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }        

        private void layoutGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double position = e.GetPosition(this).X / this.ActualWidth * 100d;
                Command.Execute(position);
            }
        }

        private void layoutGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double position = e.GetPosition(this).X / this.ActualWidth * 100d;
            Command.Execute(position);
        }
    }
}
