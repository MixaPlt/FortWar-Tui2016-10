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
    class MainMenu
    {
        public Window mainWindow;
        public Canvas mainCanvas;
        static private bool isStarted = false;
        public void Build()
        {
            mainCanvas.Children.Clear();
            if(isStarted)
                WindowSizeChanged(null, null);
            mainWindow.SizeChanged += WindowSizeChanged;
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
            Environment.Exit(0);
        }

    }
}
