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
    class ReadMode
    {
        public Window mainWindow;
        public Canvas mainCanvas;
        private Label InfoLabel = new Label() { Content = "Введите название файла", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private Button ApplyButton = new Button() { Content = "Ok" };
        private Button BackButton = new Button() { Content = "Назад" };
        private TextBox EnterWay = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium, MaxLength = 24 };
        public void Build()
        {
            mainCanvas.Children.Clear();
            mainCanvas.Children.Add(InfoLabel);
            mainCanvas.Children.Add(ApplyButton);
            mainCanvas.Children.Add(BackButton);
            mainCanvas.Children.Add(EnterWay);
            ApplyButton.Click += Apply;
            BackButton.Click += Back;
            EnterWay.TextChanged += WayChanged;
            mainWindow.SizeChanged += WindowSizeChanged;
            WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged (object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Height = mainWindow.Height - 30;
            mainCanvas.Width = mainWindow.Width - 6;
            Thickness margin = new Thickness() { Top = mainCanvas.Height / 4, Left = 0 };
            int fontSize = (int)Math.Min(mainCanvas.Width / 35, mainCanvas.Height / 24);
            InfoLabel.Margin = margin;
            InfoLabel.Height = mainCanvas.Height / 8;
            InfoLabel.Width = mainCanvas.Width;
            InfoLabel.FontSize = fontSize;
            margin.Left = mainWindow.Width / 4;
            margin.Top += mainCanvas.Height / 8;
            EnterWay.Margin = margin;
            EnterWay.Height = mainCanvas.Height / 12;
            EnterWay.Width = mainCanvas.Width / 2;
            EnterWay.FontSize = fontSize;
            margin.Top += mainCanvas.Height / 12;
            BackButton.Margin = margin;
            BackButton.Height = mainCanvas.Height / 14;
            BackButton.Width = mainCanvas.Width / 4;
            BackButton.FontSize = fontSize;
            margin.Left += mainCanvas.Width / 4;
            ApplyButton.Margin = margin;
            ApplyButton.Height = mainCanvas.Height / 14;
            ApplyButton.Width = mainCanvas.Width / 4;
            ApplyButton.FontSize = fontSize;
        }
        private void Apply(object sender, RoutedEventArgs e)
        {
            try
            {
                string t = System.IO.File.ReadAllLines(EnterWay.Text)[0];
                string[] s = t.Split(' ');
                if(s.Length != 4)
                {
                    MessageBox.Show("Файл не существует или повреждён", "FortWar");
                    return;
                }
                switch (s[3])
                {
                    case "3":
                        mainWindow.SizeChanged -= WindowSizeChanged;
                        ThirdMode thirdMode = new ThirdMode() { mainCanvas = mainCanvas, mainWindow = mainWindow, LoadMode = 2, LoadWay = EnterWay.Text};
                        thirdMode.Build();
                        break;
                    case "1":
                        mainWindow.SizeChanged -= WindowSizeChanged;
                        SecondMode secondMode = new SecondMode() { mainCanvas = mainCanvas, mainWindow = mainWindow, isRead = true, readWay = EnterWay.Text };
                        secondMode.Build();
                        break;
                    case "2":
                        mainWindow.SizeChanged -= WindowSizeChanged;
                        SecondMode secontMode = new SecondMode() { mainCanvas = mainCanvas, mainWindow = mainWindow, isRead = true, readWay = EnterWay.Text };
                        secontMode.Build();
                        break;
                    default:
                        MessageBox.Show("Файл не существует или повреждён", "FortWar");
                        return;
                }
            }
            catch { MessageBox.Show("Файл не существует или повреждён", "FortWar"); }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            StartGameMenu startGameMenu = new StartGameMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            startGameMenu.Build();
        }
        private void WayChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox) sender;
            string ans = "", t = textBox.Text;
            int k = 0;
            for (int i = 0; i < t.Length; i++)
            {
                if ((((int)(t[i]) <= (int)'9') && ((int)t[i] >= (int)'0')) || (((int)(t[i]) <= (int)'z') && ((int)t[i] >= (int)'a')) || (((int)(t[i]) <= (int)'Z') && ((int)t[i] >= (int)'A')) || t[i] == '.')
                    ans += t[i];
                if (i == textBox.CaretIndex - 1)
                    k = ans.Length;
            }
            textBox.Text = ans;
            textBox.CaretIndex = k;
        }
    }
}
