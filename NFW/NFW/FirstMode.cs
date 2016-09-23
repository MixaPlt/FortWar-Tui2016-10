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
    class FirstMode
    {
        public int firstcityx, firstcityy, secondcityx, secondcityy,height,width;
        public Canvas mainCanvas;
        public Window mainWindow;
        public bool isContinue = false;
        public string LoadWay = "";
        private HexField hexField;
        private Label infoLabel = new Label() { Content = "Ходит первый. Счёт 0:0", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Foreground = Brushes.LightGreen };
        private Button menuButton = new Button() { Content = "Меню", BorderBrush = Brushes.LightGray, Background = Brushes.Gray };
        private int AIStatus = 0;
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
        public void Build()
        {
            //mainWindow.KeyUp += KeyPressed;
            Thickness margin = new Thickness() { Top = 0, Right = 0 };
            mainCanvas.Children.Clear();
            mainWindow.SizeChanged += WindowSizeChanged;
            mainCanvas.Children.Add(infoLabel);
            hexField = new HexField() { mainCanvas = mainCanvas, mainWindow = mainWindow, FieldHeight = height, FieldWidth = width };
            hexField.Build();
            hexField.SetHexValue(firstcityx - 1, firstcityy - 1, 11);
            hexField.SetHexValue(secondcityx - 1, secondcityy - 1, 12);
            //hexField.HexClick += HexClick;
            menuButton.Margin = margin;
            //menuButton.Click += OpenMenu;
            mainCanvas.Children.Add(menuButton);
            /*if (!isContinue)
                LoadMap();
            else
                LoadSave(LoadWay);
                */
            WindowSizeChanged(null, null);
        }
    }
}
