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
    class SecondModeMenu
    {
        public Canvas mainCanvas;
        public Window mainWindow;
        private Button ContinueButton = new Button() { Content = "Продолжить" };
        private Button SaveButton = new Button() { Content = "Сохранить" };
        private Button ReturnButton = new Button() { Content = "В главное меню" };
        private Label EnterWayInfo = new Label() { Content = "Введите название сохранения", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private TextBox EnterWay = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, MaxLength = 18 };
        private Button AgreeWay = new Button() { Content = "Ok" };
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
            AgreeWay.Click += AgreeSave;
            EnterWay.TextChanged += BoxChanged;
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
            margin.Top = mainCanvas.Height / 8 + buttonHeight;
            EnterWayInfo.Height = buttonHeight / 2;
            EnterWayInfo.Margin = margin;
            EnterWayInfo.Width = buttonHeight * 3;
            EnterWayInfo.FontSize = buttonHeight / 5;
            margin.Top += buttonHeight / 2;
            EnterWay.Height = buttonHeight * 3 / 8;
            EnterWay.Width = buttonHeight * 9 / 4;
            EnterWay.Margin = margin;
            EnterWay.FontSize = buttonHeight / 5;
            margin.Left += buttonHeight * 9 / 4;
            AgreeWay.Margin = margin;
            AgreeWay.Height = buttonHeight * 3 / 8;
            AgreeWay.Width = buttonHeight * 3 / 4;
            AgreeWay.FontSize = buttonHeight / 5;
        }
        private void Continue(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            SecondMode secondMode = new SecondMode() { mainCanvas = mainCanvas, mainWindow = mainWindow, LoadMode = 1, GameMode = 1, LoadWay = "SecondModeFastSave.map" };
            secondMode.Build();
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            mainCanvas.Children.Remove(SaveButton);
            mainCanvas.Children.Add(EnterWayInfo);
            mainCanvas.Children.Add(EnterWay);
            mainCanvas.Children.Add(AgreeWay);
        }
        private void Return(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            MainMenu mainMenu = new MainMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            mainMenu.Build();
        }
        private void AgreeSave(object sender, RoutedEventArgs e)
        {
            if (EnterWay.Text.Length == 0)
                return;
            string[] s, res;
            try
            {
                s = System.IO.File.ReadAllLines("SecondModeSaves.list");
                res = new string[s.Length + 1];
                res[0] = EnterWay.Text;
                for (int i = 0; i < s.Length; i++)
                    res[i + 1] = s[i];
            }
            catch { res = new string[1] { EnterWay.Text }; }
            try
            {
                System.IO.Directory.CreateDirectory("Saves");
                System.IO.File.WriteAllLines("SecondModeSaves.list", res);
                string r = System.IO.File.ReadAllText("SecondModeFastSave.map");
                System.IO.File.WriteAllText("Saves/2" + EnterWay.Text + ".save", r);
                mainCanvas.Children.Remove(EnterWayInfo);
                mainCanvas.Children.Remove(EnterWay);
                mainCanvas.Children.Remove(AgreeWay);
                mainCanvas.Children.Add(SaveButton);
            }
            catch { }
            
        }
        private void BoxChanged(object sender, TextChangedEventArgs e)
        {
            string ans = "", t = EnterWay.Text;
            int k = 0;
            for (int i = 0; i < t.Length; i++)
            {
                if (((int)(t[i]) <= (int)'9' && (int)t[i] >= (int)'0') || ((int)(t[i]) <= (int)'Z' && (int)t[i] >= (int)'A') || ((int)(t[i]) <= (int)'z' && (int)t[i] >= (int)'a'))
                    ans += t[i];
                if (i == EnterWay.CaretIndex - 1)
                    k = ans.Length;
            }
            EnterWay.Text = ans;
            EnterWay.CaretIndex = k;
        }
    }
}
