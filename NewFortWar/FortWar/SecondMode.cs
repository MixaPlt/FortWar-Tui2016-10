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
        //Поле
        private Hexagon[,] field = new Hexagon[51, 51];
        //Соурсы
        private BitmapImage[] sources = new BitmapImage[13];
        //высота и ширина поля
        private int fieldHeight = Properties.Settings.Default.gameHeight, fieldWidth = Properties.Settings.Default.gameWidth;
        //клетки, в которые можно попасть относительно данной;
        //Первое измерение - остаток по модулю 2, 2 - шесть возможных клеток, 3 - 2 элемента - y и x координата хода относительно данного
        private int[,,] possibleSteps = new int[,,] { { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, -1 } }, { { -1, 0 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } } };
        //Продолжается ли игра
        private bool isContinue, isExit = false;
        //ход
        private int playerStep;
        private double imageHeight, imageWidth;
        public void Build()
        {
            MainCanvas.MouseUp += MainCanvasClick;
            MainWindow.KeyUp += AnyKeyUp;
            MainCanvas.Children.Clear();
            SourceInit();
            //Продолжение игры
            if (isContinue)
            {
                String text = "";
                bool isLoad = false;
                int loadedFieldHeight = 0, loadedFieldWidth = 0;
                //Загрузка
                try
                {
                    text = System.IO.File.ReadAllText("SecondModeSavedField.map");
                    if (text.Length >= 7)
                    {
                        loadedFieldHeight = (int)text[0] * 10 + (int)text[1] - 528;
                        loadedFieldWidth = (int)text[2] * 10 + (int)text[3] - 528;
                        playerStep = (int)text[4] - 48;
                        if (text.Length == loadedFieldHeight * loadedFieldWidth + 5 && loadedFieldHeight > 0 && loadedFieldHeight < 51 && loadedFieldWidth > 0 && loadedFieldWidth < 51)
                        {
                            for(int i = 0; i < loadedFieldHeight; i++)
                            {
                                for(int j = 0; j < loadedFieldWidth; j++)
                                {
                                    field[i, j] = new Hexagon() { V = (int)text[i * loadedFieldWidth + j + 5] };
                                    if(field[i, j].V < 0 || field[i, j].V > 12)
                                    {
                                        MessageBox.Show("Файл сохранения повреждён");
                                        Properties.Settings.Default.isGameSaved = false;
                                        Properties.Settings.Default.Save();
                                        Exit();
                                        return;
                                    }
                                }
                            }                            
                            isLoad = true;
                        }
                    }
                    if(!isLoad)
                    {
                        MessageBox.Show("Файл сохранения повреждён");
                        Properties.Settings.Default.isGameSaved = false;
                        Properties.Settings.Default.Save();
                        Exit();
                        return;
                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                    MessageBox.Show("Файл сохранения отсутствует");
                    Properties.Settings.Default.isGameSaved = false;
                    Properties.Settings.Default.Save();
                    Exit();
                    return;
                }
                fieldHeight = loadedFieldHeight;
                fieldWidth = loadedFieldWidth;
            }
            else
            {
                int loadedFieldHeight = 0, loadedFieldWidth = 0;
                bool isLoad = false;
                String text = "";
                try
                {
                    text = System.IO.File.ReadAllText("SecondModeCustomField.map");
                    if (text.Length >= 6)
                    {
                        loadedFieldHeight = ((int)text[0] - 48) * 10 + (int)text[1] - 48;
                        loadedFieldWidth = ((int)text[2] - 48) * 10 + (int)text[3] - 48;
                        //Проверка *правильности* файла
                        if (loadedFieldHeight > 0 && loadedFieldHeight <= 50 && loadedFieldWidth > 0 && loadedFieldWidth <= 50 && text.Length == loadedFieldHeight * loadedFieldWidth + 4)
                            isLoad = true;
                    }
                }
                catch (System.IO.FileNotFoundException)
                { isLoad = false; }
                //Обнуление массива поля
                for (int i = 0; i < Properties.Settings.Default.gameHeight; i++)
                    for (int j = 0; j < Properties.Settings.Default.gameWidth; j++)
                    {
                        field[i, j] = new Hexagon() { V = 0, X = j, Y = i };
                    }
                //Повторная проверка всех элементов файла и запись сохранения в массив field
                if (isLoad)
                {
                    bool isZeroingRepeat = false;
                    for (int i = 0; i < loadedFieldHeight; i++)
                        for (int j = 0; j < loadedFieldWidth; j++)
                        {
                            field[i, j].V = (int)text[loadedFieldWidth * i + j + 4] - 48;
                            if (field[i, j].V < 0 || field[i, j].V > 2)
                                isZeroingRepeat = true;
                        }
                    if (isZeroingRepeat)
                    {
                        for (int i = 0; i < fieldHeight; i++)
                            for (int j = 0; j < fieldWidth; j++)
                                field[i, j].V = 0;
                    }
                }
            }
            //Поле загружено и полностью готово к использованию
            //Вычисление размеров шестиугольничков
            imageWidth = (MainWindow.Width) * 4 / (3 * fieldWidth + 1);
            imageHeight = (MainWindow.Height - 26) / (fieldHeight + 0.5);
            if (imageWidth > imageHeight * 1.1547)
                imageWidth = imageHeight * 1.1547;
            else
                imageHeight = imageWidth / 1.1547;
            //Заполение поля картиночек
            {
                //Умножаем отступы на 0.97 для избежания белых полос между картинками
                Thickness imageMargin = new Thickness { Top = 0, Left = 0 };
                for (int i = 0; i < fieldHeight; i++)
                {
                    for (int j = 0; j < fieldWidth; j++)
                    {
                        field[i, j] = new Hexagon() { Margin = imageMargin, Source = sources[field[i, j].V], Height = imageHeight, Width = imageWidth };
                        MainCanvas.Children.Add(field[i, j]);
                        imageMargin.Left += (3 * imageWidth / 4) * 0.97;
                        if (j % 2 == 0)
                            imageMargin.Top += (imageHeight / 2) * 0.97;
                        else
                            imageMargin.Top -= (imageHeight / 2) * 0.97;
                    }
                    imageMargin.Top = imageHeight * (i + 1) * 0.97;
                    imageMargin.Left = 0;
                }
            }
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
        private void Exit()
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Build(MainCanvas, MainWindow);
        }
        //Метод события нажатия кнопки
        private void AnyKeyUp(object sender, KeyEventArgs e)
        {

        }
        private void MainCanvasClick(object sender, MouseEventArgs e)
        {

        }
    }
}
