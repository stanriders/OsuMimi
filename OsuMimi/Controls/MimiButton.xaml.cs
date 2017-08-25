// Copyright (c) 2016-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OsuMimi.Controls
{
    public partial class MimiButton : UserControl
    {
        private bool isDown = false;
        static DependencyProperty PictureProperty = DependencyProperty.Register("Picture", typeof(ImageSource), typeof(MimiButton));
        static DependencyProperty SaveStateProperty = DependencyProperty.Register("SaveState", typeof(bool), typeof(MimiButton));
        static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(MimiButton));

        /// <summary>
        /// Команда, которая выполняется при нажатии на кнопку
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        
        /// <summary>
        /// Зажата ли кнопка
        /// </summary>
        public bool IsDown
        {
            get { return isDown; }
            set
            {
                isDown = value;
                backgroundRectangle.Fill = (isDown) ? new SolidColorBrush(Color.FromArgb(0xFF, 0xBD, 0xBD, 0xBD)) : Brushes.White;
            }
        }

        /// <summary>
        /// Изображение на кнопке
        /// </summary>
        public ImageSource Picture
        {
            get { return (ImageSource)GetValue(PictureProperty); }
            set { SetValue(PictureProperty, value); }
        }

        /// <summary>
        /// Нужно ли сохранять статус нажатия
        /// </summary>
        public bool SaveState
        {
            get { return (bool)GetValue(SaveStateProperty); }
            set { SetValue(SaveStateProperty, value); }
        }

        public MimiButton()
        {
            InitializeComponent();
        }

        private void layoutGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            hightlightRectangle.Fill = new SolidColorBrush(Color.FromArgb(0xFF, 0xCC, 0xCC, 0xCC));
        }

        private void layoutGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            hightlightRectangle.Fill = Brushes.Transparent;
        }

        private void layoutGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Command != null && Command.CanExecute(null))
            {
                Command.Execute(null);
                if (SaveState)
                {
                    isDown = !isDown;
                    backgroundRectangle.Fill = (isDown) ? new SolidColorBrush(Color.FromArgb(0xFF, 0xBD, 0xBD, 0xBD)) : Brushes.White;
                }
            }
        }
    }
}
