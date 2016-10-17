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
        public delegate void HexClickHandler(int i, int j);
        public event HexClickHandler HexClick;
        public int[] playerPoints = new int[2]  {1, 1};
        public bool build = true;
        static private int[,,] possibleSteps = new int[2, 6, 2] { { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, -1 } }, { { -1, 0 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } } };
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
                        for (int i = value; i < fieldHeight; i++)
                        {
                            for (int j = 0; j < fieldWidth; j++)
                            {
                                thisCanvas.Children.Remove(field[i, j]);
                            }
                        }
                    }
                }
                fieldHeight = value;
                if (isBuilded)
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
                        for (int j = value; j < fieldWidth; j++)
                            thisCanvas.Children.Remove(field[i, j]);
                    }
                }
                fieldWidth = value;
                if (isBuilded)
                    Rebuild();
            }
        }
        private double height = 200;
        private double width = 200;
        private int fieldHeight;
        private int fieldWidth;
        //Сыылки на главное окно и координатную сетку
        public Canvas mainCanvas;
        public Window mainWindow;
        //высота и ширина поля
        public double Height
        {
            get { return height; }
            set { height = value - 10; Rebuild(); }
        }
        public double Width
        {
            get { return width; }
            set { width = value - 10; Rebuild(); }
        }
        private bool isBuilded = false;
        //отсуп относительнородительского элемента(mainCanvas)
        public Thickness Margin
        {
            get { return Mmargin; }
            set { Mmargin.Top = value.Top + 10; Mmargin.Left = value.Left + 10; thisCanvas.Margin = Mmargin; }
        }
        private Thickness Mmargin;
        public Canvas thisCanvas = new Canvas() { Background = Brushes.Black };
        public void SetHexValue(int i, int j, int value)
        {
            field[i, j].Value = value;
            field[i, j].Source = imageSources[value];
        }
        private BitmapImage[] imageSources = new BitmapImage[16];
        public void Build()
        {
            if (!build)
                for (int i = 0; i < 50; i++)
                {
                    for (int j = 0; j < 50; j++)
                    {
                        field[i, j] = new Hexagon() { Value = 0 };
                    }
                }
            else
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

                fieldWidth = FieldWidth;
                thisCanvas.MouseDown += AnyHexClick;
                for (int i = 0; i < 50; i++)
                {
                    for (int j = 0; j < 50; j++)
                    {
                        field[i, j] = new Hexagon() { Source = imageSources[0] };
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
        }
        private void Rebuild()
        {
            if (!isBuilded)
                return;
            Thickness margin = new Thickness() { Top = 0, Left = 0 };
            int imageHeight = (int)Math.Min(height / (fieldHeight + 1), width / 1.1547 / ((fieldWidth - 1) * 3 / 4 + 2));
            double dist = Math.Max(imageHeight / 30, 1);
            imageHeight = (int)(imageHeight - dist);
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    field[i, j].Height = imageHeight;
                    field[i, j].Margin = margin;
                    if (j % 2 == 0)
                        margin.Top += imageHeight / 2 + dist / 2;
                    else
                        margin.Top -= imageHeight / 2 + dist / 2;
                    margin.Left += imageHeight * 1.1547 / 4 * 3 + dist;
                }
                margin.Top = (i + 1) * (imageHeight + dist);
                margin.Left = 0;
            }
        }
        private void AnyHexClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < fieldHeight; i++)
                for (int j = 0; j < fieldWidth; j++)
                    if (field[i, j].isMouseOver(e))
                    {
                        try
                        {
                            HexClick(i, j);
                        }
                        catch { }
                        return;
                    }
            return;
        }
        public void Step(int line, int column, int playerSteep)
        {
            if (field[line, column].Value % 2 == playerSteep && field[line, column].Value > 2)
                playerPoints[1 - playerSteep]--;
            if (field[line, column].Value < 3 || field[line, column].Value % 2 == playerSteep)
                playerPoints[playerSteep]++;
            if (field[line, column].Value != 11 && field[line, column].Value != 12)
                SetHexValue(line, column, 5 + playerSteep);
            for (int i = 0; i < 6; i++)
            {
                int i1 = line + possibleSteps[column % 2, i, 0];
                int j1 = column + possibleSteps[column % 2, i, 1];
                if(i1 >= 0 && j1 >=0 && i1 < fieldHeight && j1 < fieldWidth && field[i1, j1].Value < 11)
                {
                    if(field[i1, j1].Value < 3)
                    {
                        playerPoints[playerSteep]++;
                        switch(field[i1, j1].Value)
                        {
                            case 0:
                                SetHexValue(i1, j1, 3 + playerSteep);
                                break;
                            case 1:
                                SetHexValue(i1, j1, 9 + playerSteep);
                                break;
                            case 2:
                                SetHexValue(i1, j1, 7 + playerSteep);
                                break;
                        }
                    }
                    else
                    if(field[i1, j1].Value % 2 == playerSteep)
                    {
                        playerPoints[playerSteep]++;
                        playerPoints[1 - playerSteep]--;
                        if (playerSteep == 0)
                            SetHexValue(i1, j1, field[i1, j1].Value - 1);
                        else
                            SetHexValue(i1, j1, field[i1, j1].Value + 1);
                    }
                }
            }
        }
        public bool IsStepPossible(int i, int j, int playerStep)
        {
            if (field[i, j].Value != 0 && field[i, j].Value != 3 && field[i, j].Value != 4)
                return false;
            if (field[i, j].Value == 3 + playerStep)
                return true; 
            for(int l = 0; l < 6; l++)
            {
                int i1 = i + possibleSteps[j % 2, l, 0];
                int j1 = j + possibleSteps[j % 2, l, 1];
                if (i1 >= 0 && j1 >= 0 && i1 < fieldHeight && j1 < fieldWidth && field[i1, j1].Value > 2 && field[i1, j1].Value % 2 != playerStep)
                    return true;
            }
            return false;
        }
    }
}