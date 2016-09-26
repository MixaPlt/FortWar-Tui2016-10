using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NFW
{
    class SecondModeLoad
    {
        public Canvas mainCanvas;
        public Window mainWindow;
        private Label InfoLabel = new Label() { Content = "Выберите сохранение", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private Button LoadButton = new Button() { Content = "Загрузить" };
        private ScrollViewer LoadListBar = new ScrollViewer() { Background = Brushes.Gray };
        private ListBox LoadList = new ListBox() { };
        private Button BackButton = new Button() { Content = "Отмена" };
        public void Build()
        {
            mainCanvas.Children.Clear();
            mainWindow.SizeChanged += WindowSizeChanged;
            mainCanvas.Children.Add(InfoLabel);
            mainCanvas.Children.Add(LoadListBar);
            mainCanvas.Children.Add(LoadButton);
            mainCanvas.Children.Add(BackButton);
            LoadListBar.Content = LoadList;
            BackButton.Click += Back;
            LoadButton.Click += Agree;
            try
            {
                LoadList.ItemsSource = System.IO.File.ReadAllLines("SecondModeSaves.list");
            }
            catch { }
            WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Height = mainWindow.ActualHeight - 30;
            mainCanvas.Width = mainWindow.ActualWidth - 6;
            int myHeight = (Math.Min((int)(mainCanvas.Height * 3 / 16), (int)(mainCanvas.Width - 4) / 3));
            Thickness margin = new Thickness { Left = 0, Top = 0 };
            InfoLabel.Width = mainCanvas.Width;
            InfoLabel.Height = mainCanvas.Height / 8;
            InfoLabel.Margin = margin;
            InfoLabel.FontSize = myHeight / 4;
            margin.Top = mainCanvas.Height / 8;
            margin.Left = (mainCanvas.Width - myHeight * 3) / 2;
            LoadListBar.Margin = margin;
            LoadListBar.Width = myHeight * 3;
            LoadListBar.Height = myHeight * 3.5;
            margin.Top += myHeight * 3.5;
            LoadButton.Margin = margin;
            LoadButton.Height = myHeight / 2.5;
            LoadButton.Width = myHeight * 3 / 2;
            LoadButton.FontSize = myHeight / 5;
            margin.Left += myHeight * 3 / 2;
            BackButton.Margin = margin;
            BackButton.Height = myHeight / 2.5;
            BackButton.Width = myHeight * 3 / 2;
            BackButton.FontSize = myHeight / 5;
            LoadList.FontSize = myHeight / 6;
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            Load load = new Load() { mainWindow = mainWindow, mainCanvas = mainCanvas };
            load.Build();
        }
        private void Agree(object sender, RoutedEventArgs e)
        {
            if (LoadList.SelectedItem != null)
            {
                mainWindow.SizeChanged -= WindowSizeChanged;
                SecondMode secondMode = new SecondMode() { mainCanvas = mainCanvas, mainWindow = mainWindow, LoadMode = 1, GameMode = 1, LoadWay = "Saves/2" + LoadList.SelectedItem + ".save" };
                secondMode.Build();
            }
        }
    }
}
