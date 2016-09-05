using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
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
    class MainMenu
    {
        public Window mainWindow;
        public Canvas mainCanvas;
        static private bool isStarted = false;
        public void Build()
        {
            if (File.Exists("screenresolution.txt") == true)
            {
                StreamReader file = new StreamReader("screenresolution.txt");
                mainWindow.Height = double.Parse(file.ReadLine());
                mainWindow.Width = double.Parse(file.ReadLine());
                file.Close();
            }
            else
            {
                    string[] arr = {mainWindow.ActualHeight.ToString(), mainWindow.ActualWidth.ToString()};
                    File.WriteAllLines("screenresolution.txt", arr);
            }
            mainWindow.SizeChanged += WindowSizeChanged;
            mainCanvas.HorizontalAlignment = HorizontalAlignment.Center;
            mainCanvas.VerticalAlignment = VerticalAlignment.Center;
            mainCanvas.Children.Clear();
            if (isStarted)
                WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged (object sender, SizeChangedEventArgs e)
        {
            isStarted = true;
            mainCanvas.Children.Clear();
            mainCanvas.Width = mainWindow.ActualWidth - 10;
            mainCanvas.Height = mainWindow.ActualHeight - 30;
            int buttonHeight = (Math.Min((int)(mainCanvas.Height * 3 / 16), (int)(mainCanvas.Width - 4) / 3));
            int buttonWidth = buttonHeight * 3;
            Thickness buttonMargin = new Thickness { Left = (mainCanvas.Width - buttonWidth) / 2, Top = mainCanvas.Height / 8 };
            Button startGameButton = new Button() { Height = buttonHeight, Width = buttonWidth, Margin = buttonMargin, Content = "Начать новую игру", FontSize = buttonHeight / 5 };
            startGameButton.Click += StartNewGame;
            mainCanvas.Children.Add(startGameButton);
            buttonMargin.Top += buttonHeight;
            Button continueGameButton = new Button() { Height = buttonHeight, Width = buttonWidth, Margin = buttonMargin, Content = "Продолжить сохранeние", FontSize = buttonHeight / 5 };
            continueGameButton.Click += ContinueGame;
            mainCanvas.Children.Add(continueGameButton);
            buttonMargin.Top += buttonHeight;
            Button settingsButton = new Button() { Height = buttonHeight, Width = buttonWidth, Margin = buttonMargin, Content = "Глобальные настройки", FontSize = buttonHeight / 5 };
            settingsButton.Click += StartSettings;
            mainCanvas.Children.Add(settingsButton);
            buttonMargin.Top += buttonHeight;
            Button exitButton = new Button() { Height = buttonHeight, Width = buttonWidth, Margin = buttonMargin, Content = "Выйти", FontSize = buttonHeight / 5 };
            exitButton.Click += ExitGame;
            mainCanvas.Children.Add(exitButton);
        }
        private void StartNewGame (object sender, RoutedEventArgs e)
        {
            StartGameMenu startGameMenu = new StartGameMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            startGameMenu.Build();
        }
        private void ContinueGame(object sender, RoutedEventArgs e)
        {

        }
        private void StartSettings(object sender, RoutedEventArgs e)
        {

        }
        private void ExitGame(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            StreamReader file = new StreamReader("screenresolution.txt");
            if (mainWindow.Height != double.Parse(file.ReadLine()) || mainWindow.Width != double.Parse(file.ReadLine()))
            {
                MessageBoxResult rez = MessageBox.Show("Вы хотите сохранить изменения в разрешении экрана?", "FortWar", MessageBoxButton.YesNoCancel);
                if (rez == MessageBoxResult.Yes)
                {
                    file.Close();
                    string[] arr = {mainWindow.Height.ToString(), mainWindow.Width.ToString()};
                    File.WriteAllLines("screenresolution.txt", arr);
                    Environment.Exit(0);
                }
                else if(rez == MessageBoxResult.No)
                {
                    file.Close();
                    Environment.Exit(0);
                }
                else if(rez == MessageBoxResult.Cancel)
                {
                    file.Close();
                }
            }
            else
            {
                MessageBoxResult rez = MessageBox.Show("Вы действительно хотите выйти?", "FortWar", MessageBoxButton.YesNo);
                if(rez == MessageBoxResult.Yes)
                {
                    Environment.Exit(0);
                }
            }
        }

    }
}
