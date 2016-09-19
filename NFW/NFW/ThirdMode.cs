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
    class ThirdMode
    {
        public Canvas mainCanvas;
        public Window mainWindow;
        public bool isContinue = false;
        public string LoadWay = "";
        private HexField hexField;
        private Label infoLabel = new Label() { Content = "Ходит первый. Счёт 0:0", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Foreground = Brushes.LightGreen};
        private Button menuButton = new Button() { Content = "Меню", BorderBrush = Brushes.LightGray, Background = Brushes.Gray};
        private int numberOfKnights = 0, AIStatus = 0;
        private Knight[] knights = new Knight[2500];
        public void Build()
        {
            mainWindow.KeyUp += KeyPressed;
            Thickness margin = new Thickness() { Top = 0, Right = 0 };
            mainCanvas.Children.Clear();
            mainWindow.SizeChanged += WindowSizeChanged;
            mainCanvas.Children.Add(infoLabel);
            hexField = new HexField() { mainCanvas = mainCanvas, mainWindow = mainWindow, FieldHeight = 10, FieldWidth = 10 };
            hexField.Build();
            hexField.HexClick += HexClick;
            menuButton.Margin = margin;
            menuButton.Click += OpenMenu;
            mainCanvas.Children.Add(menuButton);
            if (!isContinue)
                LoadMap();
            else
                LoadSave(LoadWay);
            WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Height = mainWindow.ActualHeight - 30;
            mainCanvas.Width = mainWindow.ActualWidth - 4;
            Thickness margin = new Thickness() { Top = 0, Left = 0 };
            menuButton.Margin = margin;
            menuButton.Height = mainCanvas.Height / 15;
            menuButton.Width = mainCanvas.Width / 5;
            menuButton.FontSize = (int)Math.Min(mainCanvas.Width / 45, mainCanvas.Height / 27);
            margin.Left = mainCanvas.Width / 5;
            infoLabel.Margin = margin;
            infoLabel.Height = mainCanvas.Height / 15;
            infoLabel.FontSize = (int)Math.Min(mainCanvas.Width / 45, mainCanvas.Height / 27);
            infoLabel.Width = mainCanvas.Width * 4 / 5;
            margin.Top = mainCanvas.Height / 15;
            margin.Left = 0;
            hexField.Margin = margin;
            hexField.Height = mainCanvas.Height * 14 / 15;
            hexField.Width = mainCanvas.Width;
        }
        private void OpenMenu (object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            Save();
            ThirdModeMenu thirdModeMenu = new ThirdModeMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            thirdModeMenu.Build();
        }
        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                OpenMenu(null, null);
        }
        private void LoadMap()
        {
            try
            {
                string t = System.IO.File.ReadAllText("ThirdMode.map");
                AIStatus = (int)t[0] - 48;
                hexField.FieldHeight = (int)t[1] * 10 + t[2] - 528;
                hexField.FieldWidth = (int)t[3] * 10 + (int)t[4] - 528;
                for (int i = 0; i < 50; i++)
                    for (int j = 0; j < 50; j++)
                    {
                        hexField.SetHexValue(i, j, (int)t[i * 100 + j * 2 + 13] * 10 + (int)t[i * 100 + j * 2 + 14] - 528);
                    }
            }
            catch { MessageBox.Show("Файл с настройками повреждён"); }
        }
        private void LoadSave(string way)
        {
            try
            {
                string t = System.IO.File.ReadAllText(way);
                AIStatus = (int)t[0] - 48;
                hexField.FieldHeight = (int)t[1] * 10 + t[2] - 528;
                hexField.FieldWidth = (int)t[3] * 10 + (int)t[4] - 528;
                for (int i = 0; i < 50; i++)
                    for (int j = 0; j < 50; j++)
                    {
                        hexField.SetHexValue(i, j, (int)t[i * 100 + j * 2 + 5] * 10 + (int)t[i * 100 + j * 2 + 6] - 528);
                    }
                numberOfKnights = (int)t[5005] * 10 + (int)t[5006] - 528;
                for (int i = 0; i < numberOfKnights; i++)
                    knights[i] = new Knight() { Value = (int)t[i * 5 + 5007] - 48, i = (int)t[i * 5 + 5008] * 10 + (int)t[i * 5 + 5009] - 528, j = (int)t[i * 5 + 5010] * 10 + (int)t[i * 5 + 5011] - 528 };
            }
            catch { MessageBox.Show("Файл с повреждён"); }
        }
        private void HexClick(int i, int j)
        {
            
        }
        private void Save()
        {
            string t = AIStatus.ToString();
            if (hexField.FieldHeight <= 9)
                t += "0";
            t += hexField.FieldHeight.ToString();
            if (hexField.FieldWidth <= 9)
                t += "0";
            t += hexField.FieldWidth.ToString();
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (hexField.field[i, j].Value <= 9)
                        t += "0";
                    t += hexField.field[i, j].Value.ToString();
                }
            }
            if (numberOfKnights <= 9)
                t += "0";
            t += numberOfKnights.ToString();
            for(int i = 0; i < numberOfKnights; i++)
            {
                t += knights[i].Value.ToString();
                if (knights[i].i <= 9)
                    t += "0";
                t += knights[i].i.ToString();
                if (knights[i].j <= 9)
                    t += "0";
                t += knights[i].j.ToString();
            }
            File.WriteAllText("ThirdModeFastSave.map", t);
        }
    }
}
