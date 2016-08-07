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

namespace FortWar
{
    class SecondMode
    {
        //сетка и окно
        private Canvas MainCanvas;
        private Window MainWindow;
        private Hexagon[,] field = new Hexagon[51, 51];
        private BitmapImage[] sources = new BitmapImage[13];
        private int fieldHeight, fieldWidth;
        public void Build()
        {
            MainCanvas.Children.Clear();
            SourceInit();
            String text = "";
            bool isLoad = false;
            try
            {
                text = System.IO.File.ReadAllText("SecondModeCustomField.map");
                isLoad = true;

            }
            catch(System.IO.FileNotFoundException)
            { }
        }
        private void SourceInit()
        {
            sources[0] = new BitmapImage();
            sources[0].BeginInit();
            sources[0].UriSource = new Uri("Geks0.png", UriKind.Relative);
            sources[0].EndInit();

            sources[1] = new BitmapImage();
            sources[1].BeginInit();
            sources[1].UriSource = new Uri("Geks7.png", UriKind.Relative);
            sources[1].EndInit();

            sources[2] = new BitmapImage();
            sources[2].BeginInit();
            sources[2].UriSource = new Uri("Geks10.png", UriKind.Relative);
            sources[2].EndInit();

            sources[3] = new BitmapImage();
            sources[3].BeginInit();
            sources[3].UriSource = new Uri("Geks5.png", UriKind.Relative);
            sources[3].EndInit();

            sources[4] = new BitmapImage();
            sources[4].BeginInit();
            sources[4].UriSource = new Uri("Geks6.png", UriKind.Relative);
            sources[4].EndInit();

            sources[5] = new BitmapImage();
            sources[5].BeginInit();
            sources[5].UriSource = new Uri("Geks3.png", UriKind.Relative);
            sources[5].EndInit();

            sources[6] = new BitmapImage();
            sources[6].BeginInit();
            sources[6].UriSource = new Uri("Geks4.png", UriKind.Relative);
            sources[6].EndInit();

            sources[7] = new BitmapImage();
            sources[7].BeginInit();
            sources[7].UriSource = new Uri("Geks11.png", UriKind.Relative);
            sources[7].EndInit();

            sources[8] = new BitmapImage();
            sources[8].BeginInit();
            sources[8].UriSource = new Uri("Geks12.png", UriKind.Relative);
            sources[8].EndInit();

            sources[9] = new BitmapImage();
            sources[9].BeginInit();
            sources[9].UriSource = new Uri("Geks8.png", UriKind.Relative);
            sources[9].EndInit();

            sources[10] = new BitmapImage();
            sources[10].BeginInit();
            sources[10].UriSource = new Uri("Geks8.png", UriKind.Relative);
            sources[10].EndInit();

            sources[11] = new BitmapImage();
            sources[11].BeginInit();
            sources[11].UriSource = new Uri("Geks1.png", UriKind.Relative);
            sources[11].EndInit();

            sources[12] = new BitmapImage();
            sources[12].BeginInit();
            sources[12].UriSource = new Uri("Geks2.png", UriKind.Relative);
            sources[12].EndInit();
        }
    }
}
