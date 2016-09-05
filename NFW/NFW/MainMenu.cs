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
        public void Build()
        {
            StreamReader file = new System.IO.StreamReader("screenresolution.txt");
            mainWindow.Height = double.Parse(file.ReadLine());
            mainWindow.Width = double.Parse(file.ReadLine());
            file.Close();
            mainWindow.SizeChanged += WindowSizeChanged;
            mainCanvas.HorizontalAlignment = HorizontalAlignment.Center;
            mainCanvas.VerticalAlignment = VerticalAlignment.Center;
        }
        private void WindowSizeChanged (object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Children.Clear();
            int buttonHeight = (Math.Min((int)(mainWindow.ActualHeight / 5 * 4), (int)((mainWindow.ActualWidth - 2) * 4 / 3)) / 4);
            int buttonWidth = (int)(buttonHeight * 3);
            mainCanvas.Width = buttonWidth;
            mainCanvas.Height = buttonHeight * 4;
            Thickness buttonMargin = new Thickness { Left = 0, Top = mainWindow.ActualHeight / 30 };
            Button startGameButton = new Button() { Height = buttonHeight, Width = buttonWidth, Margin = buttonMargin, Content = "Начать новую игру", FontSize = buttonHeight / 4};
            startGameButton.Click += StartNewGame;
            mainCanvas.Children.Add(startGameButton);
            buttonMargin.Top += buttonHeight;
            Button continueGameButton = new Button() { Height = buttonHeight, Width = buttonWidth, Margin = buttonMargin, Content = "Продолжить сохранeние", FontSize = buttonHeight / 4 };
            continueGameButton.Click += ContinueGame;
            mainCanvas.Children.Add(continueGameButton);
            buttonMargin.Top += buttonHeight;
            Button settingsButton = new Button() { Height = buttonHeight, Width = buttonWidth, Margin = buttonMargin, Content = "Глобальные настройки", FontSize = buttonHeight / 4 };
            settingsButton.Click += StartSettings;
            mainCanvas.Children.Add(settingsButton);
            buttonMargin.Top += buttonHeight;
            Button exitButton = new Button() { Height = buttonHeight, Width = buttonWidth, Margin = buttonMargin, Content = "Выйти", FontSize = buttonHeight / 4 };
            exitButton.Click += ExitGame;
            mainCanvas.Children.Add(exitButton);
        }
        private void StartNewGame (object sender, RoutedEventArgs e)
        {

        }
        private void ContinueGame(object sender, RoutedEventArgs e)
        {

        }
        private void StartSettings(object sender, RoutedEventArgs e)
        {

        }
        private void ExitGame(object sender, RoutedEventArgs e)
        {
            StreamReader file = new StreamReader("screenresolution.txt");
            if (mainWindow.Height != double.Parse(file.ReadLine()) || mainWindow.Width != double.Parse(file.ReadLine()))
            {
                if(MessageBox.Show("Вы хотите сохранить изменения в разрешении экрана?", "FortWar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    file.Close();
                    string[] arr = {mainWindow.Height.ToString(), mainWindow.Width.ToString()};
                    File.WriteAllLines("screenresolution.txt", arr);
                    Environment.Exit(0);
                }
                else 
                {
                    file.Close();
                    Environment.Exit(0);
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

    }
}
