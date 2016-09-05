using System.Windows;
using System.Windows.Controls;
using System;

namespace NFW
{
    class StartGameMenu
    {
        public Window mainWindow;
        public Canvas mainCanvas;
        public void Build()
        {
            mainWindow.SizeChanged += WindowSizeChanged;
            WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Children.Clear();
            mainCanvas.Width = mainWindow.ActualWidth - 10;
            mainCanvas.Height = mainWindow.ActualHeight - 30;
            Thickness margin = new Thickness() {Top = 0 };
            Label chooseModeInfoLabel = new Label() { Margin = margin, Width = mainCanvas.Width, Height = mainCanvas.Height / 8, FontSize = mainCanvas.Height / 30, HorizontalContentAlignment = HorizontalAlignment.Center, Content = "Выберите режим игры" };
            mainCanvas.Children.Add(chooseModeInfoLabel);
            margin.Top = mainCanvas.Height / 8;
            int buttonHeight = Math.Min((int)(mainCanvas.Width) / 3, (int) (mainCanvas.Height) * 3 / 16);
            margin.Left = (mainCanvas.Width - buttonHeight * 3) / 2;
            Button firstModeButton = new Button() { Height = buttonHeight, Width = buttonHeight * 3, FontSize = buttonHeight / 5, Margin = margin, Content = "I'm too young to die", IsDefault = true};
            firstModeButton.Click += FirstMode;
            mainCanvas.Children.Add(firstModeButton);
            margin.Top += buttonHeight;
            Button secondModeButton = new Button() { Margin = margin, Height = buttonHeight, Width = buttonHeight * 3, FontSize = buttonHeight / 5, Content = "Hey, not too rough" };
            secondModeButton.Click += SecondMode;
            mainCanvas.Children.Add(secondModeButton);
            margin.Top += buttonHeight;
            Button thirdModeButton = new Button() { Margin = margin, Height = buttonHeight, Width = buttonHeight * 3, FontSize = buttonHeight / 5, Content = "Hurt me plenty" };
            thirdModeButton.Click += ThirdMode;
            mainCanvas.Children.Add(thirdModeButton);
            margin.Top += buttonHeight;
            Button backButton = new Button() { Margin = margin, Height = buttonHeight, Width = buttonHeight * 3, FontSize = buttonHeight / 5, Content = "Назад" };
            backButton.Click += Back;
            mainCanvas.Children.Add(backButton);
        }
        private void FirstMode(object sender, RoutedEventArgs e)
        {

        }
        private void SecondMode(object sender, RoutedEventArgs e)
        {

        }
        private void ThirdMode(object sender, RoutedEventArgs e)
        {

        }
        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            MainMenu mainMenu = new MainMenu { mainCanvas = mainCanvas, mainWindow = mainWindow };
            mainMenu.Build();
        }
    }
}