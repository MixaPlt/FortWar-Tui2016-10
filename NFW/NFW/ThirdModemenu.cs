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
    class ThirdModeMenu
    {
        public Canvas mainCanvas;
        public Window mainWindow;
        private Button ContinueButton = new Button() { Content = "Продолжить" };
        private Button SaveButton = new Button() { Content = "Сохранить" };
        private Button ReturnButton = new Button() { Content = "В главное меню" };
        public void Build()
        {
            mainCanvas.Children.Clear();
            mainWindow.SizeChanged += WindowSizeChanged;
            mainCanvas.Children.Add(ContinueButton);
            mainCanvas.Children.Add(SaveButton);
            mainCanvas.Children.Add(ReturnButton);
            ContinueButton.Click += Continue;
            SaveButton.Click += Save;
            ReturnButton.Click += Return;
            WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Height = mainWindow.ActualHeight - 30;
            mainCanvas.Width = mainWindow.ActualWidth - 4;
            int buttonHeight = (Math.Min((int)(mainCanvas.Height * 3 / 16), (int)(mainCanvas.Width - 4) / 3));
            Thickness margin = new Thickness() { Left = (mainCanvas.Width - buttonHeight * 3) / 2, Top = mainCanvas.Height / 8 };
            ContinueButton.Margin = margin;
            ContinueButton.Height = buttonHeight;
            ContinueButton.Width = buttonHeight * 3;
            ContinueButton.FontSize = buttonHeight / 5;
            margin.Top += buttonHeight;
            SaveButton.Margin = margin;
            SaveButton.Height = buttonHeight;
            SaveButton.Width = buttonHeight * 3;
            SaveButton.FontSize = buttonHeight / 5;
            margin.Top += buttonHeight;
            ReturnButton.Margin = margin;
            ReturnButton.Height = buttonHeight;
            ReturnButton.Width = buttonHeight * 3;
            ReturnButton.FontSize = buttonHeight / 5;
        }
        private void Continue(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            mainCanvas.Children.Remove(SaveButton);
        }
        private void Return(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            MainMenu mainMenu = new MainMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            mainMenu.Build();
        }
    }
}
