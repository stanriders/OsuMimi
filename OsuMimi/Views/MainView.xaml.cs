// Copyright (c) 2016-2017 nyan [http://github.com/nyawk]
// Licensed under the MIT Licence - https://raw.githubusercontent.com/nyawk/OsuMimi/master/LICENSE

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OsuMimi.ViewModels;
using System.Windows.Threading;
using OsuMimi.Extensions;

namespace OsuMimi
{
    public partial class MainWindow : Window
    {
        private bool isPlaylistShown = false;
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            this.DataContext = new MainViewModel();
        }

        void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show
            (
               "Fatal error: {0}".Fmt(e.Exception.Message),
               "FATAL ERROR",
               MessageBoxButton.OK,
               MessageBoxImage.Error
            );

            e.Handled = true;

            Logger.LogFatal(e.Exception);
            Environment.Exit(0);
        }

        private void Maximize()
        {
            isPlaylistShown = false;
            if (WindowState == WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                playlistRow.Height = new GridLength(0, GridUnitType.Star);
                topRow.Height = new GridLength(1, GridUnitType.Star);
                playlistMaximizeRow.Height = new GridLength(10, GridUnitType.Pixel);
                bottomRow.Height = new GridLength(0, GridUnitType.Star);
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
                playlistRow.Height = new GridLength(1, GridUnitType.Star);
                topRow.Height = new GridLength(280, GridUnitType.Pixel);
                playlistMaximizeRow.Height = new GridLength(0, GridUnitType.Pixel);
                bottomRow.Height = new GridLength(30, GridUnitType.Pixel);
            }
        }

        private void ShowPlaylist(object sender, RoutedEventArgs e)
        {
            if (isPlaylistShown)
            {
                playlistRow.Height = new GridLength(0, GridUnitType.Star);
                topRow.Height = new GridLength(1, GridUnitType.Star);
                bottomRow.Height = new GridLength(0, GridUnitType.Star);
                isPlaylistShown = false;
            }
            else
            {
                playlistRow.Height = new GridLength(40, GridUnitType.Star);
                topRow.Height = new GridLength(60, GridUnitType.Star);
                bottomRow.Height = new GridLength(30, GridUnitType.Pixel);
                isPlaylistShown = true;
            }
        }
        private void DockPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                Maximize();

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
            this.WindowState = WindowState.Minimized;
        }

        private void SearchButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as OsuMimi.Controls.MimiButton;
            if (button != null)
            {
                searchGrid.Visibility = button.IsDown ? Visibility.Visible : Visibility.Hidden;
                searchTextBox.IsEnabled = button.IsDown;
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchLabel.Visibility = searchTextBox.Text.Length > 0 ? Visibility.Hidden : Visibility.Visible;
        }

        private void ListBox_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            var model = (MainViewModel)this.DataContext;
            var listbox = (ListBox)sender;

            if (model == null || listbox.SelectedItem == null)
                return;
            if (model.TrackSelectedCommand.CanExecute(listbox.SelectedItem))
                model.TrackSelectedCommand.Execute(listbox.SelectedItem);
        }
    }
}
