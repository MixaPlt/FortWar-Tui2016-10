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
    class Load
    {
        public Window mainWindow;
        public Canvas mainCanvas;
        private Button SecondModeButton = new Button() { Content = "Первый и второй режимы" };
        private Button ThirdModeButton = new Button() { Content = "Третий режим" };
        private Button BackButton = new Button() { Content = "Назад" };
        public void Build()
        {
            mainCanvas.Children.Clear();
            mainCanvas.Children.Add(SecondModeButton);
            mainCanvas.Children.Add(ThirdModeButton);
            mainCanvas.Children.Add(BackButton);
            SecondModeButton.Click += SecondMode;
            ThirdModeButton.Click += ThirdMode;
            BackButton.Click += Back;
            mainWindow.SizeChanged += WindowSizeChanged;
            WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged (object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Width = mainWindow.ActualWidth - 10;
            mainCanvas.Height = mainWindow.ActualHeight - 30;
            int buttonHeight = (Math.Min((int)(mainCanvas.Height * 3 / 16), (int)(mainCanvas.Width - 4) / 3));
            Thickness buttonMargin = new Thickness { Left = (mainCanvas.Width - buttonHeight * 3) / 2, Top = mainCanvas.Height / 8 };
            SecondModeButton.Margin = buttonMargin;
            SecondModeButton.Height = buttonHeight;
            SecondModeButton.Width = buttonHeight * 3;
            SecondModeButton.FontSize = buttonHeight / 5;
            buttonMargin.Top += buttonHeight;
            ThirdModeButton.Margin = buttonMargin;
            ThirdModeButton.Height = buttonHeight;
            ThirdModeButton.Width = buttonHeight * 3;
            ThirdModeButton.FontSize = buttonHeight / 5;
            buttonMargin.Top += buttonHeight;
            BackButton.Margin = buttonMargin;
            BackButton.Height = buttonHeight;
            BackButton.Width = buttonHeight * 3;
            BackButton.FontSize = buttonHeight / 5;
            buttonMargin.Top += buttonHeight;
        }
        private void FirstMode(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
        }
        private void SecondMode(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            SecondModeLoad secondModeLoad = new SecondModeLoad() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            secondModeLoad.Build();
        }
        private void ThirdMode(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            ThirdModeLoad thirdModeLoad = new ThirdModeLoad() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            thirdModeLoad.Build();
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            MainMenu mainMenu = new MainMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            mainMenu.Build();
        }
    }
}
