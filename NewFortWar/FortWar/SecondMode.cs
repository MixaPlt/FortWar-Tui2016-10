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
        private int[] points = new int[2] {1, 1};
        //сетка и окно
        public Canvas MainCanvas;
        public Window MainWindow;
        //0 - Пустя клеточка, 1 - горы, 2 - река, 3 - клетка первого, 4 - клеткаа второго, 5 - крепость первого, 6 - крепость второго, 7 - море первого, 8 - море второго, 9 - горы первого, 10 - горы второго, 11 - замок первого, 12 - замок второго
        private Hexagon[,] field = new Hexagon[51, 51];
        //Соурсы
        private BitmapImage[] sources = new BitmapImage[13];
        //высота и ширина поля
        private int fieldHeight = Properties.Settings.Default.gameHeight, fieldWidth = Properties.Settings.Default.gameWidth;
        //клетки, в которые можно попасть относительно данной;
        //Первое измерение - остаток по модулю 2, 2 - шесть возможных клеток, 3 - 2 элемента - y и x координата хода относительно данного
        private int[,,] possibleSteps = new int[2,6,2] { { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, -1 } }, { { -1, 0 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } } };
        //Продолжается ли игра
        public bool isContinue  = false;
        //ход
        private int playerStep = 0, steps = Properties.Settings.Default.numberSteeps;
        //Экземпляр аи
        ArtificialIntellect AI;
        private double imageHeight, imageWidth;
        //Счёт
        Label scoreLabel = new Label() {HorizontalContentAlignment = HorizontalAlignment.Center, Height = 24, Content = "kek"};
        public void Build()
        {
            Properties.Settings.Default.windowMode = 5;
            MainCanvas.MouseUp += MainCanvasClick;
            MainWindow.KeyUp += AnyKeyUp;
            MainCanvas.Children.Clear();
            SourceInit();
            Thickness margin = new Thickness() { Top = 0, Left = 0};
            scoreLabel.Width = MainWindow.Width;
            scoreLabel.Margin = margin;
            MainCanvas.Children.Add(scoreLabel);
            //Загрузка карты
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
                    for (int i = 0; i < Math.Min(loadedFieldHeight, Properties.Settings.Default.gameHeight); i++)
                        for (int j = 0; j < Math.Min(loadedFieldWidth, Properties.Settings.Default.gameWidth); j++)
                        {
                            field[i, j].V = (int)text[loadedFieldWidth * i + j + 4] - 48;
                            if (field[i, j].V < 0 || field[i, j].V > 2)
                                isZeroingRepeat = true;
                        }
                    if (isZeroingRepeat || Properties.Settings.Default.GameMode == 0)
                    {
                        for (int i = 0; i < Properties.Settings.Default.gameHeight; i++)
                            for (int j = 0; j < Properties.Settings.Default.gameWidth; j++)
                                field[i, j].V = 0;
                    }
                }
                field[Properties.Settings.Default.firstPlayerCityY - 1, Properties.Settings.Default.firstPlayerCityX - 1].V = 11;
                field[Properties.Settings.Default.secondPlayerCityY - 1, Properties.Settings.Default.secondPlayerCityX - 1].V = 12;
                field[Properties.Settings.Default.firstPlayerCityY - 1, Properties.Settings.Default.firstPlayerCityX - 1].Source = sources[11];
                field[Properties.Settings.Default.secondPlayerCityY - 1, Properties.Settings.Default.secondPlayerCityX - 1].Source = sources[12];
                Step(Properties.Settings.Default.firstPlayerCityY - 1, Properties.Settings.Default.firstPlayerCityX - 1, 0);
                Step(Properties.Settings.Default.secondPlayerCityY - 1, Properties.Settings.Default.secondPlayerCityX - 1, 1);
            }
            //Поле загружено и полностью готово к использованию
            //Вычисление размеров шестиугольничков
            imageWidth = (MainWindow.Width) * 4 / (3 * fieldWidth + 1);
            imageHeight = (MainWindow.Height - 50) / (fieldHeight + 0.5);
            if (imageWidth > imageHeight * 1.1547)
                imageWidth = imageHeight * 1.1547;
            else
                imageHeight = imageWidth / 1.1547;
            //Заполение поля картиночек
            {
                //Умножаем отступы на 0.97 для избежания белых полос между картинками
                Thickness imageMargin = new Thickness { Top = 24, Left = 0 };
                for (int i = 0; i < fieldHeight; i++)
                {
                    for (int j = 0; j < fieldWidth; j++)
                    {
                        field[i, j].Margin = imageMargin; field[i, j].Source = sources[field[i, j].V]; field[i, j].Height = imageHeight; field[i, j].Width = imageWidth;
                        MainCanvas.Children.Add(field[i, j]);
                        imageMargin.Left += (3 * imageWidth / 4) * 0.97;
                        if (j % 2 == 0)
                            imageMargin.Top += (imageHeight / 2) * 0.97;
                        else
                            imageMargin.Top -= (imageHeight / 2) * 0.97;
                    }
                    imageMargin.Top = imageHeight * (i + 1) * 0.97 + 24;
                    imageMargin.Left = 0;
                }
            }
            if (playerStep == 0)
                scoreLabel.Content = String.Format("Ходит первый.  Счёт: {0}:{1}", points[0], points[1]);
            else
                scoreLabel.Content = String.Format("Ходит второй.  Счёт: {0}:{1}", points[0], points[1]);
            AI = new ArtificialIntellect() { fieldHeight = fieldHeight, fieldWidth = fieldWidth };
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
            sources[10].UriSource = new Uri("Geks9.png", UriKind.Relative);
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
            MainCanvas.MouseUp -= MainCanvasClick;
            MainWindow.KeyUp -= AnyKeyUp;
            MainMenu mainMenu = new MainMenu();
            mainMenu.Build(MainCanvas, MainWindow);
        }
        //Метод события нажатия кнопки
        private void AnyKeyUp(object sender, KeyEventArgs e)
        {

        }
        private void MainCanvasClick(object sender, MouseEventArgs e)
        {
            for(int i = 0; i < fieldHeight; i++)
            {
                for(int j = 0; j < fieldWidth; j++)
                {
                    if(field[i, j].isMouseOver(e))
                    {
                        if (isStepPossible(i, j, playerStep))
                        {

                            Step(i, j, playerStep);
                            if (playerStep == 1)
                                steps--;
                            playerStep = 1 - playerStep;

                            if(Properties.Settings.Default.gameBot == 1)
                            {
                                pair step = AI.SecondModeFirstBot(field, playerStep);
                                Step(step.x, step.y, playerStep);
                                if (playerStep == 1)
                                    steps--;
                                playerStep = 1 - playerStep;
                            }
                            if (steps == 0)
                                EndGame();
                            if (playerStep == 0)
                                scoreLabel.Content = String.Format("Ходит первый.  Счёт: {0}:{1}", points[0], points[1]);
                            else
                                scoreLabel.Content = String.Format("Ходит второй.  Счёт: {0}:{1}", points[0], points[1]);
                        }
                        return;
                    }
                }
            }
        }
        private bool isStepPossible(int x, int y, int ps)
        {
            if (field[x, y].V != 0 && field[x, y].V != 3 && field[x, y].V != 4)
                return false;
            for (int i = 0; i < 6; i++)
                if (possibleSteps[y % 2, i, 0] + x < fieldHeight && possibleSteps[y % 2, i, 0] + x >= 0 && possibleSteps[y % 2, i, 1] + y < fieldWidth && possibleSteps[y % 2, i, 1] + y >= 0)
                {
                    if (field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V % 2 == 1 - ps && field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V > 2)
                        return true;                  
                }
                    return false;
        }
        private void Step(int x, int y, int ps)
        {
            for (int i = 0; i < 6; i++)
                if (possibleSteps[y % 2, i, 0] + x < fieldHeight && possibleSteps[y % 2, i, 0] + x >= 0 && possibleSteps[y % 2, i, 1] + y < fieldWidth && possibleSteps[y % 2, i, 1] + y >= 0)
                {
                    if (field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V < 3)
                        points[ps]++;
                    if (field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V < 11 && field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V > 2 && field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V % 2 == ps)
                    {
                        if (field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V % 2 == 0)
                            field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V--;
                        else
                            field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V++;
                        points[ps]++;
                        points[1 - ps]--;
                    }
                    else
                       switch(field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V)
                        {
                            case 0:
                                field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V = ps + 3;
                                break;
                            case 1:
                                field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V = ps + 9;
                                break;
                            case 2:
                                field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V = ps + 7;
                                break;
                        }
                    field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].Source = sources[field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V];
                }
            if(field[x, y].V < 5)
            {
                if (field[x, y].V != ps + 3)
                    points[ps]++;
                if (field[x, y].V == 1 - ps + 3)
                    points[1 - ps]--;
                field[x, y].V = ps + 5;
                field[x, y].Source = sources[field[x, y].V];
            }
        }
        private void EndGame()
        {
            if (points[0] > points[1])
                MessageBox.Show(String.Format("Победил первый со счётом {0}:{1}", points[0], points[1]));
            if (points[0] < points[1])
                MessageBox.Show(String.Format("Победил второй со счётом {0}:{1}", points[0], points[1]));
            if (points[0] == points[1])
                MessageBox.Show(String.Format("Победила дружба со счётом {0}:{1}", points[0], points[1]));
            Exit();
        }
    }
}
