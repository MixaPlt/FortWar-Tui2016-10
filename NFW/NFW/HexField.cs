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
        //Краткое описание класса
        //Перед использованием обязательно выполнить Build(), назначить все публичные переменные
        //Если Вы изменили какой-либо параметр (кроме fieldHeight и fieldWidth, которые менять нельзя), влияющий на расположение поля, запустите Rebuild()
        //0 - Пустя клеточка, 1 - горы, 2 - река, 3 - клетка первого, 4 - клеткаа второго, 5 - крепость первого, 6 - крепость второго, 7 - море первого, 8 - море второго, 9 - горы первого, 10 - горы второго, 11 - замок первого, 12 - замок второго
        //Само поле. Дополнительная информация в классе Hexagon. !!!Менять значение только через метод SetHexValue!!!
        public Hexagon[,] field = new Hexagon[51, 51];
        //Количество строчек и столбцов поля
        public int FieldHeight
        {
            get { return fieldHeight; }
            set
            {
                if (isBuilded)
                {
                    if (value > fieldHeight)
                    {
                        for (int i = fieldHeight; i < value; i++)
                        {
                            for (int j = 0; j < fieldWidth; j++)
                            {
                                thisCanvas.Children.Add(field[i, j]);
                            }
                        }
                    }
                    else
                    {
                        for (int i = fieldHeight; i < value; i++)
                        {
                            for (int j = 0; j < fieldWidth; j++)
                            {
                                thisCanvas.Children.Remove(field[i, j]);
                            }
                        }
                    }
                }
                fieldHeight = value;
                if(isBuilded)
                 Rebuild();
            }
        }
        public int FieldWidth
        {
            get { return fieldWidth; }
            set
            {
                if (isBuilded)
                {
                    for (int i = 0; i < fieldHeight; i++)
                    {
                        for (int j = fieldWidth; j < value; j++)
                            thisCanvas.Children.Add(field[i, j]);
                        for (int j = value; j < fieldHeight; j++)
                            thisCanvas.Children.Remove(field[i, j]);
                    }
                }
                fieldWidth = value;
                if(isBuilded)
                    Rebuild();
            }
        }
        private double height;
        private double width;
        private int fieldHeight;
        private int fieldWidth;
        //Сыылки на главное окно и координатную сетку
        public Canvas mainCanvas;
        public Window mainWindow;
        //высота и ширина поля
        public double Height
        {
            get { return height; }
            set { height = value - 5; Rebuild(); }
        }
        public double Width
        {
            get { return width; }
            set { width = value - 5; Rebuild(); }
        }
        private bool isBuilded = false;
        //отсуп относительнородительского элемента(mainCanvas)
        public Thickness Margin
        {
            get { return Mmargin; }
            set { Mmargin.Top = value.Top + 10; Mmargin.Left = value.Left + 10; thisCanvas.Margin = Mmargin; }
        }
        private Thickness Mmargin;
        private Canvas thisCanvas = new Canvas() { Background = Brushes.Black};
        public void SetHexValue(int i, int j, int value)
        {
            field[i, j].Value = value;
            field[i, j].Source = imageSources[value];
        }
        private BitmapImage[] imageSources = new BitmapImage[16];
        public void Build()
        {
            {
                isBuilded = true;
                thisCanvas.Margin = Mmargin;
                mainCanvas.Children.Add(thisCanvas);
                imageSources[0] = new BitmapImage();
                imageSources[0].BeginInit();
                imageSources[0].UriSource = new Uri("Images/Geks0.png", UriKind.Relative);
                imageSources[0].EndInit();

                imageSources[1] = new BitmapImage();
                imageSources[1].BeginInit();
                imageSources[1].UriSource = new Uri("Images/Geks7.png", UriKind.Relative);
                imageSources[1].EndInit();

                imageSources[2] = new BitmapImage();
                imageSources[2].BeginInit();
                imageSources[2].UriSource = new Uri("Images/Geks10.png", UriKind.Relative);
                imageSources[2].EndInit();

                imageSources[3] = new BitmapImage();
                imageSources[3].BeginInit();
                imageSources[3].UriSource = new Uri("Images/Geks5.png", UriKind.Relative);
                imageSources[3].EndInit();

                imageSources[4] = new BitmapImage();
                imageSources[4].BeginInit();
                imageSources[4].UriSource = new Uri("Images/Geks6.png", UriKind.Relative);
                imageSources[4].EndInit();

                imageSources[5] = new BitmapImage();
                imageSources[5].BeginInit();
                imageSources[5].UriSource = new Uri("Images/Geks3.png", UriKind.Relative);
                imageSources[5].EndInit();

                imageSources[6] = new BitmapImage();
                imageSources[6].BeginInit();
                imageSources[6].UriSource = new Uri("Images/Geks4.png", UriKind.Relative);
                imageSources[6].EndInit();

                imageSources[7] = new BitmapImage();
                imageSources[7].BeginInit();
                imageSources[7].UriSource = new Uri("Images/Geks11.png", UriKind.Relative);
                imageSources[7].EndInit();

                imageSources[8] = new BitmapImage();
                imageSources[8].BeginInit();
                imageSources[8].UriSource = new Uri("Images/Geks12.png", UriKind.Relative);
                imageSources[8].EndInit();

                imageSources[9] = new BitmapImage();
                imageSources[9].BeginInit();
                imageSources[9].UriSource = new Uri("Images/Geks8.png", UriKind.Relative);
                imageSources[9].EndInit();

                imageSources[10] = new BitmapImage();
                imageSources[10].BeginInit();
                imageSources[10].UriSource = new Uri("Images/Geks9.png", UriKind.Relative);
                imageSources[10].EndInit();

                imageSources[11] = new BitmapImage();
                imageSources[11].BeginInit();
                imageSources[11].UriSource = new Uri("Images/Geks1.png", UriKind.Relative);
                imageSources[11].EndInit();

                imageSources[12] = new BitmapImage();
                imageSources[12].BeginInit();
                imageSources[12].UriSource = new Uri("Images/Geks2.png", UriKind.Relative);
                imageSources[12].EndInit();
            }
            fieldWidth = FieldWidth;
            for (int i = 0; i < 50; i++)
            {
                for(int j = 0; j < 50; j++)
                {
                    field[i, j] = new Hexagon() {Source = imageSources[0] };
                }
            }
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    thisCanvas.Children.Add(field[i, j]);
                }
            }
            Rebuild();
        }
        private void Rebuild()
        {
            if (!isBuilded)
                return;
            Thickness margin = new Thickness() { Top = 0, Left = 0};
            int imageHeight = (int)Math.Min(height / (fieldHeight + 1), width / 1.1547 / ((fieldWidth - 1) * 3/ 4 + 1));
            int dist = Math.Max(imageHeight / 30, 1);
            imageHeight -= dist;
            for(int i = 0; i < fieldHeight; i++)
            {
                for(int j = 0; j < fieldWidth; j++)
                {
                    field[i, j].Height = imageHeight;
                    field[i, j].Margin = margin;
                    if (j % 2 == 0)
                        margin.Top += imageHeight / 2;
                    else
                        margin.Top -= imageHeight / 2;
                    margin.Left += imageHeight * 1.1547 / 4 * 3 + dist;
                }
                margin.Top = (i + 1) * imageHeight;
                margin.Left = 0;
            }
        }
    }
}
