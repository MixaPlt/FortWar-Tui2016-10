using System.Windows;
using System.Windows.Controls;
using System;
using System.IO;

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
            Label chooseModeInfoLabel = new Label() { Margin = margin, Width = mainCanvas.Width, Height = mainCanvas.Height / 8, FontSize = mainCanvas.Height / 30, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Выберите режим игры" };
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
            mainWindow.SizeChanged -= WindowSizeChanged;
            ModeSettings firstmodeoptions = new ModeSettings() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            firstmodeoptions.Build();
        }
        private void SecondMode(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
        }
        private void ThirdMode(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            mainCanvas.Children.Clear();
            Thickness margin = new Thickness() { Top = 4, Left = 3};
            HexField hexField = new HexField() { fieldHeight = 50, fieldWidth = 50, mainCanvas = mainCanvas, Height = (int)mainCanvas.Height - 4, Width = (int)mainCanvas.Width - 4, mainWindow = mainWindow, Margin = margin};
            hexField.Build();
            hexField.SetHexValue(0, 0, 6);
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            //mainWindow.SizeChanged -= WindowSizeChanged;
            MainMenu mainMenu = new MainMenu { mainCanvas = mainCanvas, mainWindow = mainWindow };
            mainMenu.Build();
        }
    }
}