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
        //Первое измерение - остаток по модулю 2, 2 - шесть возможных клеток, 3 - 2 элемента - y и x координата хода относительно данного
        static private int[,,] possibleSteps = new int[2, 6, 2] { { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, -1 } }, { { -1, 0 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } } };
        public int firstCityLine = 0, firstCityColumn = 0, secondCityLine = 9, secondCityColumn = 9;
        public Canvas mainCanvas;
        public Window mainWindow;
        public bool isContinue = false;
        public string LoadWay = "";
        private HexField hexField;
        private Label infoLabel = new Label() { Content = "Ходит первый. Счёт 0:0", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Foreground = Brushes.LightGreen };
        private Button menuButton = new Button() { Content = "Меню", BorderBrush = Brushes.LightGray, Background = Brushes.Gray };
        private int numberOfKnights = 0, AIStatus = 0, maxNumberOfSteeps;
        private int playerSteep = 1, numberOfSteeps = 0, usedKnights = 0, ausedKnights = 0, useKnight = -1;
        private int[] numberKnights = new int[2]  {0, 0};
        private Knight[] knights = new Knight[2500];
        private bool generalMode = true;
        private BitmapImage firstKnightSource, secondKnightSource;
        private Button changeGeneralModeButton = new Button { Content = "Cтроить крепости" };
        private Button endStepButton = new Button() { Content = "Завершить ход" };
        public void Build()
        {
            firstKnightSource = new BitmapImage();
            firstKnightSource.BeginInit();
            firstKnightSource.UriSource = new Uri("Images/Knight1.png", UriKind.Relative);
            firstKnightSource.EndInit();
            secondKnightSource = new BitmapImage();
            secondKnightSource.BeginInit();
            secondKnightSource.UriSource = new Uri("Images/Knight2.png", UriKind.Relative);
            secondKnightSource.EndInit();
            mainWindow.KeyUp += KeyPressed;
            Thickness margin = new Thickness() { Top = 0, Right = 0 };
            mainCanvas.Children.Clear();
            mainWindow.SizeChanged += WindowSizeChanged;
            mainCanvas.Children.Add(infoLabel);
            mainCanvas.Children.Add(changeGeneralModeButton);
            changeGeneralModeButton.Click += ChangeGeneralMode;
            hexField = new HexField() { mainCanvas = mainCanvas, mainWindow = mainWindow, FieldHeight = 10, FieldWidth = 10 };
            hexField.Build();
            hexField.HexClick += HexClick;
            menuButton.Margin = margin;
            menuButton.Click += OpenMenu;
            mainCanvas.Children.Add(menuButton);
            endStepButton.Click += EndOfSteep;
            if (!isContinue)
                LoadMap();
            else
                LoadSave(LoadWay);
            WindowSizeChanged(null, null);
            mainCanvas.Children.Add(endStepButton);
            EndOfSteep(null, null);
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
            infoLabel.Width = mainCanvas.Width * 2 / 5;
            margin.Top = mainCanvas.Height / 15;
            margin.Left = 0;
            hexField.Margin = margin;
            hexField.Height = mainCanvas.Height * 14 / 15;
            hexField.Width = mainCanvas.Width;
            margin.Top = 0;
            margin.Left = mainCanvas.Width * 3 / 5;
            changeGeneralModeButton.Margin = margin;
            changeGeneralModeButton.Height = mainCanvas.Height / 15;
            changeGeneralModeButton.Width = mainCanvas.Width / 5;
            changeGeneralModeButton.FontSize = (int)Math.Min(mainCanvas.Width / 50, mainCanvas.Height / 27);
            margin.Left = mainCanvas.Width * 4 / 5;
            endStepButton.Margin = margin;
            endStepButton.Height = mainCanvas.Height / 15;
            endStepButton.Width = mainCanvas.Width / 5;
            endStepButton.FontSize = (int)Math.Min(mainCanvas.Width / 50, mainCanvas.Height / 27);
            for (int i = 0; i < numberOfKnights; i++)
            {
                knights[i].Height = hexField.field[0, 0].Height;
                knights[i].Margin = hexField.field[knights[i].i, knights[i].j].Margin;
            }
        }
        private void OpenMenu (object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            Save();
            ThirdModeMenu thirdModeMenu = new ThirdModeMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            thirdModeMenu.Build();
        }
        private void ChangeGeneralMode(object sender, RoutedEventArgs e)
        {
            if(generalMode)
            {
                generalMode = false;
                changeGeneralModeButton.Content = "Ходить генералами";
            }
            else
            {
                generalMode = true;
                changeGeneralModeButton.Content = "Cтроить крепости";
            }
            MessageBox.Show(String.Format("{0}:{1}", usedKnights, numberKnights[playerSteep]));
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
                maxNumberOfSteeps = (int)t[1] * 1000 + (int)t[2] * 100 + (int)t[3] * 10 + (int)t[4] - 53328;
                hexField.FieldHeight = (int)t[5] * 10 + t[6] - 528;
                hexField.FieldWidth = (int)t[7] * 10 + (int)t[8] - 528;
                for (int i = 0; i < 50; i++)
                    for (int j = 0; j < 50; j++)
                    {
                        hexField.SetHexValue(i, j, (int)t[i * 100 + j * 2 + 17] * 10 + (int)t[i * 100 + j * 2 + 18] - 528);
                        if (hexField.field[i, j].Value == 11) { firstCityLine = i; firstCityColumn = j; }
                        if (hexField.field[i, j].Value == 12) { secondCityLine = i; secondCityColumn = j; }
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
                        if(hexField.field[i, j].Value == 11)
                        {
                            firstCityLine = i;
                            firstCityColumn = j;
                        }
                        if(hexField.field[i, j].Value == 12)
                        {
                            secondCityLine = i;
                            secondCityColumn = j;
                        }
                    }
                numberOfKnights = (int)t[5005] * 10 + (int)t[5006] - 528;
                for (int i = 0; i < numberOfKnights; i++)
                    knights[i] = new Knight() { Value = (int)t[i * 5 + 5007] - 48, i = (int)t[i * 5 + 5008] * 10 + (int)t[i * 5 + 5009] - 528, j = (int)t[i * 5 + 5010] * 10 + (int)t[i * 5 + 5011] - 528 };
                playerSteep = (int)t[5006 + 5 * numberOfKnights] - 48;
            }
            catch { MessageBox.Show("Файл сохранения повреждён"); }
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
                t += playerSteep.ToString();
            }
            File.WriteAllText("ThirdModeFastSave.map", t);
        }
        private void HexClick(int i, int j)
        {
            if (hexField.field[i, j].Value == 1 || hexField.field[i, j].Value == 9 || hexField.field[i, j].Value == 10)
                return;
            if (hexField.field[i, j].Knight >= 0 && knights[hexField.field[i, j].Knight].Value == playerSteep)
            {
                if(generalMode)
                    useKnight = hexField.field[i, j].Knight;
                else
                {
                    if (knights[hexField.field[i, j].Knight].isTurned < 2 && hexField.field[i, j].Value < 5 && hexField.field[i, j].Value != 2)
                    {
                        hexField.Step(i, j, playerSteep);
                        knights[hexField.field[i, j].Knight].Value = -1;
                        numberKnights[playerSteep]--;
                        if (knights[hexField.field[i, j].Knight].isTurned == 1)
                            usedKnights--;
                        hexField.thisCanvas.Children.Remove(knights[hexField.field[i, j].Knight]);
                        hexField.field[i, j].Knight = -1;
                        if (playerSteep == 0)
                            infoLabel.Content = String.Format("Ходит первый. Счёт: {0}:{1}", hexField.playerPoints[0], hexField.playerPoints[1]);
                        else
                            infoLabel.Content = String.Format("Ходит второй. Счёт: {0}:{1}", hexField.playerPoints[0], hexField.playerPoints[1]);
                    }
                }
                return;
            }
            if (hexField.field[i, j].Value >= 11)
                return;
            if (!generalMode)
                return;
            if( useKnight >=0 && knights[useKnight].isTurned == 0)
            {
                for(int l = 0; l < 6; l++)
                {
                    if(knights[useKnight].i + possibleSteps[knights[useKnight].j % 2, l, 0] == i && knights[useKnight].j + possibleSteps[knights[useKnight].j % 2, l, 1] == j)
                    {
                        hexField.field[knights[useKnight].i, knights[useKnight].j].Knight = -1;
                        knights[useKnight].i = i;
                        knights[useKnight].j = j;
                        hexField.field[i, j].Knight = useKnight;
                        knights[useKnight].Margin = hexField.field[i, j].Margin;
                        knights[useKnight].isTurned = 1;
                        usedKnights++;

                        if (playerSteep == 0)
                        {
                            if (hexField.field[firstCityLine, firstCityColumn].Knight == -1 && !endStepButton.IsVisible)
                                mainCanvas.Children.Add(endStepButton);
                        }
                        else
                        {
                            if (hexField.field[secondCityLine, secondCityColumn].Knight == -1 && !endStepButton.IsVisible)
                                mainCanvas.Children.Add(endStepButton);
                        }
                        /*  if (usedKnights >= numberKnights[playerSteep] && endStepButton.IsVisible == false)
                              mainCanvas.Children.Add(endStepButton);*/
                        return;
                    }
                }
                for (int l = 0; l < 6; l++)
                {
                    int i1 = knights[useKnight].i + possibleSteps[knights[useKnight].j % 2, l, 0];
                    int j1 = knights[useKnight].j + possibleSteps[knights[useKnight].j % 2, l, 1];
                    if(i1 >=0 && j1 >=0 && i1 < hexField.FieldHeight && j1 < hexField.FieldWidth)
                    if(hexField.field[i1, j1].Value != 1 && hexField.field[i1, j1].Value != 9 && hexField.field[i1, j1].Value != 10 && hexField.field[i1, j1].Knight == -1)
                        for (int k = 0; k < 6; k++)
                        {
                            if (i1 + possibleSteps[j1 % 2, k, 0] == i && j1 + possibleSteps[j1 % 2, k, 1] == j)
                                {
                                    hexField.field[knights[useKnight].i, knights[useKnight].j].Knight = -1;
                                    knights[useKnight].i = i;
                                    knights[useKnight].j = j;
                                    hexField.field[i, j].Knight = useKnight;
                                    knights[useKnight].Margin = hexField.field[i, j].Margin;
                                    knights[useKnight].isTurned = 2;
                                    useKnight = -1;
                                    ausedKnights++;
                                    usedKnights++;

                                    if (playerSteep == 0)
                                    {
                                        if (hexField.field[firstCityLine, firstCityColumn].Knight == -1 && !endStepButton.IsVisible)
                                            mainCanvas.Children.Add(endStepButton);
                                    }
                                    else
                                    {
                                        if (hexField.field[secondCityLine, secondCityColumn].Knight == -1 && !endStepButton.IsVisible)
                                            mainCanvas.Children.Add(endStepButton);
                                    }
                                    /*             if (usedKnights >= numberKnights[playerSteep] && endStepButton.IsVisible == false)
                                                     mainCanvas.Children.Add(endStepButton);*/
                                    return;
                                }
                        }
                }
            }          
        }
        private void EndOfSteep(object sender, RoutedEventArgs e)
        {
            mainCanvas.Children.Remove(endStepButton);
            if(playerSteep == 1 && maxNumberOfSteeps <= numberOfSteeps)
            {
                MessageBox.Show("Игра окончена");
                mainWindow.SizeChanged -= WindowSizeChanged;
                MainMenu mainMenu = new MainMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
                mainMenu.Build();
            }
            if (playerSteep == 1)
            {
                knights[numberOfKnights] = new Knight() { Height = hexField.field[0, 0].Height, i = firstCityLine, j = firstCityColumn, Margin = hexField.field[firstCityLine, firstCityColumn].Margin, Source = firstKnightSource, Value = 0 };
                hexField.thisCanvas.Children.Add(knights[numberOfKnights]);
                knights[numberOfKnights + 1] = new Knight() { Height = hexField.field[0, 0].Height, i = secondCityLine, j = secondCityColumn, Margin = hexField.field[secondCityLine, secondCityColumn].Margin, Source = secondKnightSource, Value = 1 };
                hexField.thisCanvas.Children.Add(knights[numberOfKnights + 1]);
                hexField.field[firstCityLine, firstCityColumn].Knight = numberOfKnights;
                hexField.field[secondCityLine, secondCityColumn].Knight = numberOfKnights + 1;
                numberKnights[0]++;
                numberKnights[1]++;
                numberOfKnights += 2;
                numberOfSteeps++;
                for (int i = 0; i < numberOfKnights; i++)
                    knights[i].isTurned = 0;
            }
            playerSteep = 1 - playerSteep;
            usedKnights = 0;
            ausedKnights = 0;
            if (playerSteep == 0)
                infoLabel.Content = String.Format("Ходит первый. Счёт: {0}:{1}", hexField.playerPoints[0], hexField.playerPoints[1]);
            else
                infoLabel.Content = String.Format("Ходит второй. Счёт: {0}:{1}", hexField.playerPoints[0], hexField.playerPoints[1]);
        }
    }
}
