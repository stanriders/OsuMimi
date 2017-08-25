using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OsuMimi.ViewModels;

namespace OsuMimi
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void DockPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            base.OnMouseLeftButtonDown(e);
        }

        private void Image_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Native.WindowResize(sender, e);
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void SearchButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as OsuMimi.Controls.MimiButton;
            if (button != null)
            {
                searchGrid.Visibility = button.IsDown ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
                searchTextBox.IsEnabled = button.IsDown;
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchLabel.Visibility = searchTextBox.Text.Length > 0 ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
        }
    }
}
