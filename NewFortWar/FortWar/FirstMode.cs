﻿using System;
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
    class FirstMode
    {
        ArtificialIntellect aI = new ArtificialIntellect();
        MainMenu mainMenu = new MainMenu();
        bool IsLoad;
        //описание
        BitmapImage source0, source1, source2, source3, source4, source5, source6;
        Hexagon[, ] field = new Hexagon[55, 55];
        int playerSteep = new int();
        int fieldWidth = 0, fieldHeight = 0;
        int[] steeps = new int[2];
        //Поле 0 = нейтразльное 1 = замок первого игрока 2 = замок второго игрока 3 = крепость первого 4 = крепосто второго
        //5 = поле первого 6 = поле второго
        Window MainWindow;
        Canvas MainCanvas;
        public void Build  (Canvas mainCanvas, Window mainWindow, bool isLoad)
        {
            IsLoad = isLoad;
            if(!isLoad)
            {
                fieldHeight = Properties.Settings.Default.gameHeight;
                fieldWidth = Properties.Settings.Default.gameWidth;
            }
            //Ходы
            steeps[0] = Properties.Settings.Default.numberSteeps;
            steeps[1] = Properties.Settings.Default.numberSteeps;
            Properties.Settings.Default.windowMode = 2;
            MainCanvas = mainCanvas;
            MainWindow = mainWindow;
            //Ивент нажатия какой-либо кнопки 
            MainWindow.KeyUp += AnyKeyUp;
            //Ивент клика по полю
            MainCanvas.MouseUp += geksimage_Click;
            //Чистим без вилки
            MainCanvas.Children.Clear();
            //Теперь поле
            if (!isLoad)
            {
                for (int i = 0; i <= fieldHeight; i++)
                {
                    for (int j = 0; j <= fieldWidth; j++)
                    {
                        field[i, j] = new Hexagon() { X = j, Y = i, V = 0 };
                    }
                }
                //Первые замки
                field[Properties.Settings.Default.firstPlayerCityY - 1, Properties.Settings.Default.firstPlayerCityX - 1].V = 1;
                field[Properties.Settings.Default.secondPlayerCityY - 1, Properties.Settings.Default.secondPlayerCityX - 1].V = 2;
            }
            else
            {
                try
                {
                    String text = System.IO.File.ReadAllText("FirstModeSave.map");
                    fieldHeight = ((int)text[0] - 48) * 10 + (int)text[1] - 48;
                    fieldWidth = ((int)text[2] - 48) * 10 + (int)text[3] - 48;
                    playerSteep = (int)text[4] - 48;
                    for (int i = 0; i < fieldHeight; i++)
                    {
                        for (int j = 0; j < fieldWidth; j++)
                        {
                            field[i, j].V = (int)text[i * fieldWidth + j + 5] - 48;
                        }
                    }
                }
                catch(System.IO.FileNotFoundException)
                {
                    Properties.Settings.Default.isGameSaved = false;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Файл сохранения повреждён.\nПерезапуск исправит проблему");
                    Environment.Exit(0);
                }
            }

            MakeField();
        }
        //Красим  соседние с кликом клетки
        private void steep (int x, int y, int f)
        {
            //Я гуманитарий, поэтому x - строчка, y - столбец 
            if (field[x, y].V == 0 || field[x, y].V > 4)
                if (f == 0)
                {
                    field[x, y].Source = source3;
                    field[x, y].V = 3;
                }
                else
                {
                    field[x, y].Source = source4;
                    field[x, y].V = 4;
                }
            if(y % 2 == 0)
            {
                if (x > 0)
                {
                    if (field[x - 1, y].V == 0 || field[x - 1, y].V == 6 - f)
                    {
                        field[x - 1, y].V = 5 + f;
                        if (f == 0)
                            field[x - 1, y].Source = source5;
                        else
                            field[x - 1, y].Source = source6;

                    }
                    if (field[x - 1, y].V == 4 - f)
                    {
                        field[x - 1, y].V = 3 + f;
                        if (f == 0)
                            field[x - 1, y].Source = source3;
                        else
                            field[x - 1, y].Source = source4;

                    }
                    if ((field[x - 1, y + 1].V == 0 || field[x - 1, y + 1].V == 6 - f)&&(y + 1 < fieldWidth))
                    {
                        field[x - 1, y + 1].V = 5 + f;
                        if (f == 0)
                            field[x - 1, y + 1].Source = source5;
                        else
                            field[x - 1, y + 1].Source = source6;
                    }
                    if ((field[x - 1, y + 1].V == 4 - f) && (y + 1 < fieldWidth))
                    {
                        field[x - 1, y + 1].V = 3 + f;
                        if (f == 0)
                            field[x - 1, y + 1].Source = source3;
                        else
                            field[x - 1, y + 1].Source = source4;
                    }
                }
                if(y > 0)
                {
                    if (field[x, y - 1].V == 0 || field[x, y - 1].V == 6 - f)
                    {
                        field[x, y - 1].V = 5 + f;
                        if (f == 0)
                            field[x, y - 1].Source = source5;
                        else
                            field[x, y - 1].Source = source6;
                    }
                    if (field[x, y - 1].V == 4 - f)
                    {
                        field[x, y - 1].V = 3 + f;
                        if (f == 0)
                            field[x, y - 1].Source = source3;
                        else
                            field[x, y - 1].Source = source4;
                    }
                    if (x > 0)
                        if (field[x - 1, y - 1].V == 0 || field[x - 1, y - 1].V == 6 - f)
                        {
                            field[x - 1, y - 1].V = 5 + f;
                            if (f == 0)
                                field[x - 1, y - 1].Source = source5;
                            else
                                field[x - 1, y - 1].Source = source6;
                        }
                    if(x > 0)
                    if (field[x - 1, y - 1].V == 4 - f)
                    {
                        field[x - 1, y - 1].V = 3 + f;
                        if (f == 0)
                            field[x - 1, y - 1].Source = source3;
                        else
                            field[x - 1, y - 1].Source = source4;
                    }
                }
                if ((field[x + 1, y].V == 0 || field[x + 1, y].V == 6 - f) && (x + 1 < fieldHeight))
                {
                    field[x + 1, y].V = 5 + f;
                    if (f == 0)
                        field[x + 1, y].Source = source5;
                    else
                        field[x + 1, y].Source = source6;
                }
                if ((field[x + 1, y].V == 4 - f) && (x + 1 < fieldHeight))
                {
                    field[x + 1, y].V = 3 + f;
                    if (f == 0)
                        field[x + 1, y].Source = source3;
                    else
                        field[x + 1, y].Source = source4;
                }

                if ((field[x, y + 1].V == 0 || field[x, y + 1].V == 6 - f) && (y + 1 < fieldWidth))
                {
                    field[x, y + 1].V = 5 + f;
                    if (f == 0)
                        field[x, y + 1].Source = source5;
                    else
                        field[x, y + 1].Source = source6;
                }
                if ((field[x, y + 1].V == 4 - f))
                {
                    field[x, y + 1].V = 3 + f;
                    if (f == 0)
                        field[x, y + 1].Source = source3;
                    else
                        field[x, y + 1].Source = source4;
                }
            }
            else
            {
                if (x > 0)
                {
                    if (field[x - 1, y].V == 0 || field[x - 1, y].V == 6 - f)
                    {
                        field[x - 1, y].V = 5 + f;
                        if (f == 0)
                            field[x - 1, y].Source = source5;
                        else
                            field[x - 1, y].Source = source6;
                    }
                    if (field[x - 1, y].V == 4 - f)
                    {
                        field[x - 1, y].V = 3 + f;
                        if (f == 0)
                            field[x - 1, y].Source = source3;
                        else
                            field[x - 1, y].Source = source4;
                    }
                }
                if (y > 0)
                {
                    if (field[x, y - 1].V == 0 || field[x, y - 1].V == 6 - f)
                    {
                        field[x, y - 1].V = 5 + f;
                        if (f == 0)
                            field[x, y - 1].Source = source5;
                        else
                            field[x, y - 1].Source = source6;
                    }
                    if (field[x, y - 1].V == 4 - f)
                    {
                        field[x, y - 1].V = 3 + f;
                        if (f == 0)
                            field[x, y - 1].Source = source3;
                        else
                            field[x, y - 1].Source = source4;
                    }
                    if ((field[x + 1, y - 1].V == 0 || field[x + 1, y - 1].V == 6 - f) && (x + 1 < fieldHeight))
                    {
                        field[x + 1, y - 1].V = 5 + f;
                        if (f == 0)
                            field[x + 1, y - 1].Source = source5;
                        else
                            field[x + 1, y - 1].Source = source6;
                    }
                    if ((field[x + 1, y - 1].V == 4 - f) && (x + 1 < fieldHeight))
                    {
                        field[x + 1, y - 1].V = 3 + f;
                        if (f == 0)
                            field[x + 1, y - 1].Source = source3;
                        else
                            field[x + 1, y - 1].Source = source4;
                    }
                    if ((field[x + 1, y + 1].V == 0 || field[x + 1, y + 1].V == 6 - f) && (y + 1 < fieldWidth) && (x + 1 < fieldHeight))
                    {
                        field[x + 1, y + 1].V = 5 + f;
                        if (f == 0)
                            field[x + 1, y + 1].Source = source5;
                        else
                            field[x + 1, y + 1].Source = source6;
                    }
                    if ((field[x + 1, y + 1].V == 4 - f) && (y + 1 < fieldWidth) && (x + 1 < fieldHeight))
                    {
                        field[x + 1, y + 1].V = 3 + f;
                        if (f == 0)
                            field[x + 1, y + 1].Source = source3;
                        else
                            field[x + 1, y + 1].Source = source4;
                    }
                }
                if ((field[x + 1, y].V == 0 || field[x + 1, y].V == 6 - f) && (x + 1 < fieldHeight))
                {
                    field[x + 1, y].V = 5 + f; if (f == 0)
                        field[x + 1, y].Source = source5;
                    else
                        field[x + 1, y].Source = source6;

                }
                if ((field[x + 1, y].V == 4 - f) && (x + 1 < fieldHeight))
                {
                    field[x + 1, y].V = 3 + f;
                    if (f == 0)
                        field[x + 1, y].Source = source3;
                    else
                        field[x + 1, y].Source = source4;

                }
                if ((field[x, y + 1].V == 0 || field[x, y + 1].V == 6 - f) && (y + 1 < fieldWidth))
                {
                    field[x, y + 1].V = 5 + f;
                    if (f == 0)
                        field[x, y + 1].Source = source5;
                    else
                        field[x, y + 1].Source = source6;
                }
                if ((field[x, y + 1].V == 4 - f) && (y + 1 < fieldWidth))
                {
                    field[x, y + 1].V = 3 + f;
                    if (f == 0)
                        field[x, y + 1].Source = source3;
                    else
                        field[x, y + 1].Source = source4;
                }
            }
        }
        //Рисуем поле
        public void MakeField()
        {
            //ссылочки
            BitmapImage standartGeksSource = new BitmapImage() { };
            standartGeksSource.BeginInit();
            standartGeksSource.UriSource = new Uri("Geks0.png", UriKind.Relative);
            standartGeksSource.EndInit();
            source0 = standartGeksSource;
            BitmapImage firstCastleSource = new BitmapImage() { };
            firstCastleSource.BeginInit();
            firstCastleSource.UriSource = new Uri("Geks1.png", UriKind.Relative);
            firstCastleSource.EndInit();
            source1 = firstCastleSource;
            BitmapImage secondCastleSource = new BitmapImage() { };
            secondCastleSource.BeginInit();
            secondCastleSource.UriSource = new Uri("Geks2.png", UriKind.Relative);
            secondCastleSource.EndInit();
            source2 = secondCastleSource;
            BitmapImage secondFortSource = new BitmapImage() { };
            secondFortSource.BeginInit();
            secondFortSource.UriSource = new Uri("Geks4.png", UriKind.Relative);
            secondFortSource.EndInit();
            source4 = secondFortSource;
            BitmapImage firstFortSource = new BitmapImage() { };
            firstFortSource.BeginInit();
            firstFortSource.UriSource = new Uri("Geks3.png", UriKind.Relative);
            firstFortSource.EndInit();
            source3 = firstFortSource;
            BitmapImage firstFieldSource = new BitmapImage() { };
            firstFieldSource.BeginInit();
            firstFieldSource.UriSource = new Uri("Geks5.png", UriKind.Relative);
            firstFieldSource.EndInit();
            source5 = firstFieldSource;
            BitmapImage secondFieldSource = new BitmapImage() { };
            secondFieldSource.BeginInit();
            secondFieldSource.UriSource = new Uri("Geks6.png", UriKind.Relative);
            secondFieldSource.EndInit();
            source6 = secondFieldSource;
            //Рисуем
            //Определяем размеры шестиугольничков по формулкам
            double imageWidth, imageHeight;
            //1.1547 - соотношение сторон шестиугольника
            imageWidth = MainWindow.Width * 4 / (3 * fieldWidth + 1);
            imageHeight = (MainWindow.Height - 26) / (fieldHeight + 0.5);
            if (imageWidth > imageHeight * 1.1547)
                imageWidth = imageHeight * 1.1547;
            else
                imageHeight = imageWidth / 1.1547;
            Thickness imageMargin = new Thickness() { Top = 0, Left = 0 };
            for (int i = 0; i < fieldHeight; i++)
            {
                for(int j = 0; j < fieldWidth; j++)
                {
                    field[i, j].Margin = imageMargin; field[i, j].Height = imageHeight; field[i, j].Width = imageWidth;
                    switch(field [i, j].V)
                    {
                        case 0:
                            {
                                field[i, j].Source = standartGeksSource;
                                MainCanvas.Children.Add(field[i, j]);
                            }
                            break;
                        case 1:
                            {
                                field[i, j].Source = firstCastleSource;
                                MainCanvas.Children.Add(field[i, j]);
                            }
                            break;
                        case 2:
                            {
                                field[i, j].Source = secondCastleSource;
                                MainCanvas.Children.Add(field[i, j]);
                            }
                            break;
                        case 5:
                            {
                                field[i, j].Source = firstFieldSource;
                                MainCanvas.Children.Add(field[i, j]);
                            }
                            break;
                        case 6:
                            {
                                field[i, j].Source = secondFieldSource;
                                MainCanvas.Children.Add(field[i, j]);
                            }
                            break;
                        case 3:
                            {
                                field[i, j].Source = firstFortSource;
                                MainCanvas.Children.Add(field[i, j]);
                            }
                            break;
                        case 4:
                            {
                                field[i, j].Source = secondFortSource;
                                MainCanvas.Children.Add(field[i, j]);
                            }
                            break;
                    }
                    //Умножаем на 0.97 для избежания белых полос между гексами (доработанный костыль)
                    if (j % 2 == 0)
                        imageMargin.Top += (imageHeight / 2)*0.97;
                    else
                        imageMargin.Top -= (imageHeight / 2)*0.97;
                        imageMargin.Left += (imageWidth / 4 * 3)*0.97;
                }
                imageMargin.Top += imageHeight * 0.97;
                if (fieldWidth % 2 == 1)
                    imageMargin.Top -= (imageHeight / 2) * 0.97;
                imageMargin.Left = 0;
            }
            if (!IsLoad)
            {
                steep(Properties.Settings.Default.firstPlayerCityY - 1, Properties.Settings.Default.firstPlayerCityX - 1, 0);
                steep(Properties.Settings.Default.secondPlayerCityY - 1, Properties.Settings.Default.secondPlayerCityX - 1, 1);
            }
            if(Properties.Settings.Default.gameBot == 2)
            {
                for(int i = 0; i < Properties.Settings.Default.numberSteeps; i++)
                {
                    if(isAnySteepAviable(0))
                    {
                        pair botSteep = aI.FirstModeFirstBot(field, 0);
                        steep(botSteep.y, botSteep.x, 0);
                    }
                    if(isAnySteepAviable(1))
                    {
                        pair botSteep = aI.FirstModeSecondBot(field, 1);
                        steep(botSteep.y, botSteep.x, 1);
                    }
                }
                EndGame(2);
            }
            else
                if(Properties.Settings.Default.botSteep == 0 && Properties.Settings.Default.gameBot == 1 && playerSteep == 0)
                {
                    pair botSteep = aI.FirstModeFirstBot(field, 0);
                    steep(botSteep.y, botSteep.x, 0);
                    steeps[0]--;
                    playerSteep = 1;
                }
        }
        //проверяет возможность хода
        public bool isSteepAviable(int i, int j, int f)
        {
            //i - строчка j - столбец f - номер игрока
            int maxi = fieldHeight, maxj = fieldWidth;
            if (field[i, j].V != 0 && field[i, j].V < 5)
                return false;
            if (field[i, j].V == 5 + f)
                return true;
            if (j > 0 && field[i, j - 1].V != 0 && (field[i, j - 1].V % 2 == (f + 1) % 2))
                return true;
            if (j + 1 < maxj  && field[i, j + 1].V != 0 && (field[i, j + 1].V % 2 == (f + 1) % 2))
                return true;
            if (i > 0 && field[i - 1, j].V != 0 && (field[i - 1, j].V % 2 == (f + 1) % 2))
                return true;
            if (i  + 1 < maxi && field[i + 1, j].V != 0 && (field[i + 1, j].V % 2 == (f + 1) % 2))
                return true;
            if(j % 2 == 0)
            {
                if (i > 0 && j + 1 < maxj && field[i - 1, j + 1].V != 0 && (field[i - 1, j + 1].V % 2 == (f + 1) % 2))
                    return true;
                if (i > 0 && j > 0 && field[i - 1, j - 1].V != 0 && (field[i - 1, j - 1].V % 2 == (f + 1) % 2))
                    return true;
            }
            else
            {
                if (i + 1 < maxi && j + 1 < maxj && field[i + 1, j + 1].V != 0 && (field[i + 1, j + 1].V % 2 == (f + 1) % 2))
                    return true;
                if (i + 1 < maxi && j > 0 && field[i + 1, j - 1].V != 0 && (field[i + 1, j - 1].V % 2 == (f + 1) % 2))
                    return true;
            }

            return false;
        }
        private bool isAnySteepAviable(int f)
        {
            for(int i = 0; i < fieldHeight; i++)
            {
                for(int j = 0; j < fieldWidth; j++)
                {
                    if (isSteepAviable(i, j, f))
                        return true;
                }
            }
            return false;
        }
        private void geksimage_Click(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    if (field[i, j].isMouseOver(e))
                    {
                        if(isSteepAviable(i, j, playerSteep))
                        {

                            if (isAnySteepAviable(playerSteep) && steeps[playerSteep] > 0)
                            {
                                steep(i, j, playerSteep);
                                steeps[playerSteep]--;
                            }
                            if (isAnySteepAviable(1 - playerSteep))
                                playerSteep = 1 - playerSteep;
                            if (steeps[playerSteep] == 0)
                            {
                                EndGame(2);
                                return;
                            }
                            if (!isAnySteepAviable(1 - playerSteep) && !isAnySteepAviable(playerSteep))
                            {
                                EndGame(2);
                                return;
                            }
                            if(playerSteep == Properties.Settings.Default.botSteep && Properties.Settings.Default.gameBot == 1)
                            {
                                pair botSteep = aI.FirstModeFirstBot(field, playerSteep);                                
                                steep(botSteep.y, botSteep.x, playerSteep);
                                steeps[playerSteep]--;
                                if(!isAnySteepAviable(1 - playerSteep))
                                {
                                    while((isAnySteepAviable(playerSteep)) && steeps[playerSteep] > 0)
                                    {
                                        botSteep = aI.FirstModeFirstBot(field, playerSteep);
                                        steep(botSteep.y, botSteep.x, playerSteep);
                                        steeps[playerSteep]--;
                                    }
                                }
                                playerSteep = 1 - playerSteep;
                            }
                            if (steeps[playerSteep] == 0 && steeps[1 - playerSteep] == 0)
                                EndGame(2);

                                                                  
                        }
                        return;
                    }
                }
            }
            return;
        }
        void EndGame (int f)
        {
            int firstPlayerPoints = 0, secondPlayerPoints = 0;
            for (int i = 0; i < fieldHeight; i++)
            {
                for (int j = 0; j < fieldWidth; j++)
                {
                    if (field[i, j].V != 0)
                    {
                        if ((field[i, j].V - 1) % 2 == 0)
                            firstPlayerPoints++;
                        else
                            secondPlayerPoints++;
                    }
                    else
                    {
                        if (f == 0)
                            firstPlayerPoints++;
                        if (f == 1)
                            secondPlayerPoints++;
                    }
                }
            }
            MainWindow.KeyUp -= AnyKeyUp;
            MainCanvas.MouseUp -= geksimage_Click;
            Properties.Settings.Default.isGameSaved = false;
            Properties.Settings.Default.Save();
            if (firstPlayerPoints > secondPlayerPoints)
                MessageBox.Show(String.Format("Победил первый со счётом {0}:{1}", firstPlayerPoints, secondPlayerPoints));
            else
                if(firstPlayerPoints < secondPlayerPoints)
                    MessageBox.Show(String.Format("Победил втрой со счётом {0}:{1}", firstPlayerPoints, secondPlayerPoints));
                else

                MessageBox.Show(String.Format("Победила дружба! {0}:{1}", firstPlayerPoints, secondPlayerPoints));
            mainMenu.Build(MainCanvas, MainWindow);
        }
        private void AnyKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                if (MessageBox.Show("Действительно ли вы хотите выйти?\nТекущая игра будет сохранена", "Подтверждение выхода", buttons) == MessageBoxResult.Yes)
                {
                    String text = "";
                    if (fieldHeight < 10)
                        text += "0" + fieldHeight.ToString();
                    else
                        text += fieldHeight.ToString();
                    if (fieldWidth < 10)
                        text += "0" + fieldWidth.ToString();
                    else
                        text += fieldWidth.ToString();
                    text += playerSteep.ToString();
                    for(int i = 0; i < fieldHeight; i++)
                    {
                        for(int j = 0; j < fieldWidth; j++)
                        {
                            text += (field[i, j].V).ToString();
                        }
                    }
                    System.IO.File.WriteAllText("FirstModeSave.map",text);
                    Properties.Settings.Default.savedMode = 0;
                    Properties.Settings.Default.isGameSaved = true;
                    Properties.Settings.Default.Save();
                    mainMenu.Build(MainCanvas, MainWindow);
                    MainWindow.KeyUp -= AnyKeyUp;
                    MainCanvas.MouseUp -= geksimage_Click;
                }
            }
        }
    }
}