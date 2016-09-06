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
    class HexField
    {
        //0 - Пустя клеточка, 1 - горы, 2 - река, 3 - клетка первого, 4 - клеткаа второго, 5 - крепость первого, 6 - крепость второго, 7 - море первого, 8 - море второго, 9 - горы первого, 10 - горы второго, 11 - замок первого, 12 - замок второго
        public Hexagon[,] field = new Hexagon[51, 51];
        public int fieldHeight;
        public int fieldWidth;
        public Canvas mainCanvas;
        public Window mainWindow;
        public int Height;
        public int Width;
        public Thickness Margin;
        private BitmapImage[] imageSources = new BitmapImage[16];
        public void Build()
        {
            mainCanvas.Children.Clear();
            mainCanvas.Height = mainWindow.Height - 30;
            mainCanvas.Width = mainWindow.Width - 4;
            mainWindow.SizeChanged += WindowSizeChanged;
            {
                imageSources[0] = new BitmapImage();
                imageSources[0].BeginInit();
                imageSources[0].UriSource = new Uri("Geks0.png", UriKind.Relative);
                imageSources[0].EndInit();

                imageSources[1] = new BitmapImage();
                imageSources[1].BeginInit();
                imageSources[1].UriSource = new Uri("Geks7.png", UriKind.Relative);
                imageSources[1].EndInit();

                imageSources[2] = new BitmapImage();
                imageSources[2].BeginInit();
                imageSources[2].UriSource = new Uri("Geks10.png", UriKind.Relative);
                imageSources[2].EndInit();

                imageSources[3] = new BitmapImage();
                imageSources[3].BeginInit();
                imageSources[3].UriSource = new Uri("Geks5.png", UriKind.Relative);
                imageSources[3].EndInit();

                imageSources[4] = new BitmapImage();
                imageSources[4].BeginInit();
                imageSources[4].UriSource = new Uri("Geks6.png", UriKind.Relative);
                imageSources[4].EndInit();

                imageSources[5] = new BitmapImage();
                imageSources[5].BeginInit();
                imageSources[5].UriSource = new Uri("Geks3.png", UriKind.Relative);
                imageSources[5].EndInit();

                imageSources[6] = new BitmapImage();
                imageSources[6].BeginInit();
                imageSources[6].UriSource = new Uri("Geks4.png", UriKind.Relative);
                imageSources[6].EndInit();

                imageSources[7] = new BitmapImage();
                imageSources[7].BeginInit();
                imageSources[7].UriSource = new Uri("Geks11.png", UriKind.Relative);
                imageSources[7].EndInit();

                imageSources[8] = new BitmapImage();
                imageSources[8].BeginInit();
                imageSources[8].UriSource = new Uri("Geks12.png", UriKind.Relative);
                imageSources[8].EndInit();

                imageSources[9] = new BitmapImage();
                imageSources[9].BeginInit();
                imageSources[9].UriSource = new Uri("Geks8.png", UriKind.Relative);
                imageSources[9].EndInit();

                imageSources[10] = new BitmapImage();
                imageSources[10].BeginInit();
                imageSources[10].UriSource = new Uri("Geks9.png", UriKind.Relative);
                imageSources[10].EndInit();

                imageSources[11] = new BitmapImage();
                imageSources[11].BeginInit();
                imageSources[11].UriSource = new Uri("Geks1.png", UriKind.Relative);
                imageSources[11].EndInit();

                imageSources[12] = new BitmapImage();
                imageSources[12].BeginInit();
                imageSources[12].UriSource = new Uri("Geks2.png", UriKind.Relative);
                imageSources[12].EndInit();
            }
            for (int i = 0; i < fieldHeight; i++)
            {
                for(int j = 0; j < fieldWidth; j++)
                {
                    field[i, j] = new Hexagon() {Source = imageSources[0] };
   //                 mainCanvas.Children.Add(field[i, j]);
                }
            }
            WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Children.Clear();
            int imageHeight, imageWidth;
            imageHeight = Math.Min((int)(Height / (fieldHeight + 0.5)), (int)((Width / ((fieldWidth - 1) * 3 / 4 + 1)) / 1.1547)) - 2;
            imageWidth = (int)(imageHeight * 1.1547);
            Thickness margin = new Thickness() { Top = Margin.Top, Left = Margin.Left };
            for(int i = 0; i < fieldHeight; i++)
            {
                for(int j = 0; j < fieldWidth; j++)
                {
                    field[i, j].Margin = margin;
                    field[i, j].Height = imageHeight;
                    field[i, j].Width = imageWidth;
                    margin.Left += imageWidth * 3 / 4 + 2;
                    if(j % 2 == 0)
                        margin.Top += imageHeight / 2;
                    else
                        margin.Top -= imageHeight / 2;
                    mainCanvas.Children.Add(field[i, j]);
                }
                Margin.Left = 0;
                Margin.Top = (imageWidth + 2) * i;
            }
        }
    }
}
