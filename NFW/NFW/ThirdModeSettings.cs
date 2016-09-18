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
    class ThirdModeSettings
    {
        public Canvas mainCanvas;
        public Window mainWindow;
        private Thickness margin = new Thickness();
        private Label helpInfo = new Label() { Content = "Укажите типы участков поля посредством нажатия на них", VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center, Foreground = Brushes.Red };
        private Label enterFieldPropInfo = new Label() { Content = "Задайте размеры поля", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private Label enterFieldHeightInfo = new Label() { Content = "Высота:", HorizontalContentAlignment = HorizontalAlignment.Right, VerticalContentAlignment = VerticalAlignment.Center };
        private TextBox enterFieldHeightBox = new TextBox() { VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center, FontWeight = FontWeights.Medium };
        private Label enterFieldWidthInfo = new Label() { Content = "Ширина:", HorizontalContentAlignment = HorizontalAlignment.Right, VerticalContentAlignment = VerticalAlignment.Center };
        private TextBox enterFieldWidthBox = new TextBox() { VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center, FontWeight = FontWeights.Medium };
        private Label enterCordInfo = new Label() { Content = "Задайте координаты\nгородов", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private Label lineInfo = new Label() { Content = "Строка", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private Label columnInfo = new Label() { Content = "Столбец", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private Label firstInfo = new Label() { Content = "Первый", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private Label secondInfo = new Label() { Content = "Второй", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private TextBox firstLineBox = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium };
        private TextBox firstColumnBox = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium };
        private TextBox secondLineBox = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium };
        private TextBox secondColumnBox = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium };
        private Button applySettings = new Button() { Content = "Применить настройки" };
        private Button startButton = new Button() { Content = "Начать игру" };
        private Button backButton = new Button() { Content = "Назад" };
        private Button resetButton = new Button() { Content = "Очистить поле" };
        private ComboBox selectAIStatus = new ComboBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private HexField hexField;
        private int firstCityLine = 0, firstCityColumn = 0, secondCityLine = 0, secondCityColumn = 1, AIStatus = 0;
        public void Build() 
        {
            mainCanvas.Children.Clear();

            mainCanvas.Height = mainWindow.ActualHeight - 30;
            mainCanvas.Width = mainWindow.ActualWidth - 4;
            mainCanvas.Children.Add(helpInfo);
            mainWindow.SizeChanged += WindowSizeChanged;
            mainCanvas.Children.Add(enterFieldPropInfo);
            mainCanvas.Children.Add(enterFieldHeightInfo);
            mainCanvas.Children.Add(enterFieldHeightBox);
            mainCanvas.Children.Add(enterFieldWidthInfo);
            mainCanvas.Children.Add(enterFieldWidthBox);
            mainCanvas.Children.Add(enterCordInfo);
            mainCanvas.Children.Add(lineInfo);
            mainCanvas.Children.Add(columnInfo);
            mainCanvas.Children.Add(firstInfo);
            mainCanvas.Children.Add(secondInfo);
            mainCanvas.Children.Add(firstLineBox);
            mainCanvas.Children.Add(firstColumnBox);
            mainCanvas.Children.Add(secondLineBox);
            mainCanvas.Children.Add(secondColumnBox);
            mainCanvas.Children.Add(applySettings);
            mainCanvas.Children.Add(startButton);
            mainCanvas.Children.Add(backButton);
            mainCanvas.Children.Add(selectAIStatus);
            mainCanvas.Children.Add(resetButton);
            startButton.Click += StartGame;
            resetButton.Click += Reset;
            applySettings.Click += ApplySettings;
            backButton.Click += Back;
            enterFieldHeightBox.TextChanged += BoxChanged;
            enterFieldWidthBox.TextChanged += BoxChanged;
            firstLineBox.TextChanged += BoxChanged;
            firstColumnBox.TextChanged += BoxChanged;
            secondColumnBox.TextChanged += BoxChanged;
            secondLineBox.TextChanged += BoxChanged;

            {
                string[] menu = new string[3]  { "ИИ выключен", "ИИ ходит первым", "ИИ ходит вторым"};                
                selectAIStatus.ItemsSource = menu;
            }

            hexField = new HexField() { FieldHeight = 10, FieldWidth = 10, mainCanvas = mainCanvas, mainWindow = mainWindow, Height = mainCanvas.Height * 14 / 15, Width = mainCanvas.Width * 3 / 4 };
            hexField.Build();
            hexField.HexClick += HexClick;
            Load();
            WindowSizeChanged(null, null);
            
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Много однородного кода. Желательно не читать 
            mainCanvas.Height = mainWindow.ActualHeight - 30;
            mainCanvas.Width = mainWindow.ActualWidth - 4;
            int fontSize = (int)Math.Min(mainCanvas.Width / 45, mainCanvas.Height / 27);
            margin.Left = mainCanvas.Width / 4;
            margin.Top = 0;
            helpInfo.Margin = margin;
            helpInfo.Height = mainCanvas.Height / 15;
            helpInfo.Width = mainCanvas.Width * 3 / 4;
            helpInfo.FontSize = fontSize;
            margin.Top = mainCanvas.Height / 10;
            margin.Left = 0;
            enterFieldPropInfo.Margin = margin;
            enterFieldPropInfo.Width = mainCanvas.Width / 4;
            enterFieldPropInfo.Height = mainCanvas.Height / 15;
            enterFieldPropInfo.FontSize = fontSize;
            margin.Top += mainCanvas.Height / 15;
            enterFieldHeightInfo.Margin = margin;
            enterFieldHeightInfo.Width = mainCanvas.Width / 8;
            enterFieldHeightInfo.Height = mainCanvas.Height / 15;
            enterFieldHeightInfo.FontSize = fontSize;
            margin.Left = mainCanvas.Width / 8;
            margin.Top += mainCanvas.Height / 60;
            enterFieldHeightBox.Margin = margin;
            enterFieldHeightBox.Width = mainCanvas.Width / 20;
            enterFieldHeightBox.Height = mainCanvas.Height / 25;
            enterFieldHeightBox.FontSize = (int)Math.Min(mainCanvas.Width / 45, mainCanvas.Height / 32);
            margin.Left = 0;
            margin.Top = mainCanvas.Height * 7 / 30;
            enterFieldWidthInfo.Margin = margin;
            enterFieldWidthInfo.Width = mainCanvas.Width / 8;
            enterFieldWidthInfo.Height = mainCanvas.Height / 15;
            enterFieldWidthInfo.FontSize = fontSize;
            margin.Left = mainCanvas.Width / 8;
            margin.Top += mainCanvas.Height / 60;
            enterFieldWidthBox.Margin = margin;
            enterFieldWidthBox.Width = mainCanvas.Width / 20;
            enterFieldWidthBox.Height = mainCanvas.Height / 25;
            enterFieldWidthBox.FontSize = (int)Math.Min(mainCanvas.Width / 45, mainCanvas.Height / 32);
            margin.Left = 0;
            margin.Top = mainCanvas.Height * 0.3;
            enterCordInfo.Margin = margin;
            enterCordInfo.Height = mainCanvas.Height / 9;
            enterCordInfo.Width = mainCanvas.Width / 4;
            enterCordInfo.FontSize = fontSize;
            margin.Top = mainCanvas.Height * 2 / 5;
            margin.Left = mainCanvas.Width / 14;
            lineInfo.Margin = margin;
            lineInfo.Height = mainCanvas.Height / 15;
            lineInfo.Width = mainCanvas.Width / 12;
            lineInfo.FontSize = fontSize;
            margin.Left = mainCanvas.Width * 13 / 84;
            columnInfo.Margin = margin;
            columnInfo.Height = mainCanvas.Height / 15;
            columnInfo.Width = mainCanvas.Width * 2 / 21;
            columnInfo.FontSize = fontSize;
            margin.Top = mainCanvas.Height * 7 / 15;
            margin.Left = 0;
            firstInfo.Margin = margin;
            firstInfo.Height = mainCanvas.Height / 15;
            firstInfo.Width = mainCanvas.Width / 8;
            firstInfo.FontSize = fontSize;
            margin.Left = mainCanvas.Width / 8;
            margin.Top += mainCanvas.Height / 60;
            firstLineBox.Margin = margin;
            firstLineBox.Height = mainCanvas.Height / 25;
            firstLineBox.Width = mainCanvas.Width / 20;
            firstLineBox.FontSize = (int)Math.Min(mainCanvas.Width / 45, mainCanvas.Height / 32);
            margin.Left = mainCanvas.Width * 3 / 16;
            firstColumnBox.Margin = margin;
            firstColumnBox.Height = mainCanvas.Height / 25;
            firstColumnBox.Width = mainCanvas.Width / 20;
            firstColumnBox.FontSize = (int)Math.Min(mainCanvas.Width / 45, mainCanvas.Height / 32);
            margin.Top = mainCanvas.Height * 8 / 15;
            margin.Left = 0;
            secondInfo.Margin = margin;
            secondInfo.Height = mainCanvas.Height / 15;
            secondInfo.Width = mainCanvas.Width / 8;
            secondInfo.FontSize = fontSize;
            margin.Top += mainCanvas.Height / 60;
            margin.Left = mainCanvas.Width / 8;
            secondLineBox.Margin = margin;
            secondLineBox.Height = mainCanvas.Height / 25;
            secondLineBox.Width = mainCanvas.Width / 20;
            secondLineBox.FontSize = (int)Math.Min(mainCanvas.Width / 45, mainCanvas.Height / 32);
            margin.Left = mainCanvas.Width * 3 / 16;
            secondColumnBox.Height = mainCanvas.Height / 25;
            secondColumnBox.Width = mainCanvas.Width / 20;
            secondColumnBox.Margin = margin;
            secondColumnBox.FontSize = (int)Math.Min(mainCanvas.Width / 45, mainCanvas.Height / 32);
            margin.Top = mainCanvas.Height * 2 / 3;
            margin.Left = mainCanvas.Width / 50;
            applySettings.Margin = margin;
            applySettings.Height = mainCanvas.Height / 15;
            applySettings.Width = mainCanvas.Width * 0.23;
            applySettings.FontSize = fontSize;
            margin.Top += mainCanvas.Height / 15;
            startButton.Margin = margin;
            startButton.Height = mainCanvas.Height / 15;
            startButton.Width = mainCanvas.Width * 0.23;
            startButton.FontSize = fontSize;
            margin.Top = mainCanvas.Height / 15;
            margin.Left = mainCanvas.Width * 33 / 128;
            hexField.Margin = margin;
            hexField.Height = mainCanvas.Height * 14 / 15;
            hexField.Width = mainCanvas.Width * 95 / 128;
            margin.Top = mainCanvas.Height * 4 / 5;
            margin.Left = mainCanvas.Width / 50;
            resetButton.Margin = margin;
            resetButton.Height = mainCanvas.Height / 15;
            resetButton.Width = mainCanvas.Width * 0.23;
            resetButton.FontSize = fontSize;
            margin.Top = mainCanvas.Height * 13 / 15;
            margin.Left = mainCanvas.Width / 50;
            backButton.Margin = margin;
            backButton.Height = mainCanvas.Height / 15;
            backButton.Width = mainCanvas.Width * 0.23;
            backButton.FontSize = fontSize;
            margin.Top = mainCanvas.Height * 3 / 5;
            margin.Left = mainCanvas.Width / 50;
            selectAIStatus.Height = mainCanvas.Height / 15;
            selectAIStatus.Width = mainCanvas.Width * 0.23;
            selectAIStatus.Margin = margin;
            selectAIStatus.FontSize = fontSize;
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            StartGameMenu startGameMenu = new StartGameMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            startGameMenu.Build();  
        }
        private void ApplySettings ( object sender, RoutedEventArgs e)
        {
            bool isError = false;
            string errors = "Обнаружены следующие ошибки:\n";
            int h = 0, w = 0, x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            try
            {
                h = Int32.Parse(enterFieldHeightBox.Text);
                if(h > 50 || h <= 0)
                { errors += "Высота задана неправильно\n"; isError = true; }
            }
            catch(System.FormatException){ errors += "Высота не задана\n"; isError = true; };
            try
            {
                w = Int32.Parse(enterFieldWidthBox.Text);
                if (w > 50 || w <= 0)
                { errors += "Ширина задана неправильно\n"; isError = true; }
            }
            catch (System.FormatException) { errors += "Ширина не задана\n"; isError = true; }
            try
            {
                x1 = Int32.Parse(firstLineBox.Text);  y1 = Int32.Parse(firstColumnBox.Text); x2 = Int32.Parse(secondLineBox.Text); y2 = Int32.Parse(secondColumnBox.Text);
                if (x1 == x2 && y1 == y2)
                { errors += "Координаты городов совпадают\n"; isError = true; }
            }
            catch (System.FormatException) { isError = true; errors += "Не заданы координаты городов\n"; };
            if(!isError)
            {
                if(x1 > h || x2 > h || y1 > w || y2 > w || x1 <=0 || x2 <=0 || y1 <=0 || y2 <=0)
                { errors += "Координаты городов превосходят размеры поля\n"; isError = true; }
            }
            if(!isError)
            {
                hexField.SetHexValue(firstCityLine, firstCityColumn, 0);
                hexField.SetHexValue(secondCityLine, secondCityColumn, 0);
                hexField.FieldHeight = h;
                hexField.FieldWidth = w;
                AIStatus = selectAIStatus.SelectedIndex;
                firstCityLine = x1 - 1;
                firstCityColumn = y1 - 1;
                secondCityColumn = y2 - 1;
                secondCityLine = x2 - 1;
                hexField.SetHexValue(firstCityLine, firstCityColumn, 11);
                hexField.SetHexValue(secondCityLine, secondCityColumn, 12);           
                Save();
            }
            else
                MessageBox.Show(errors, "FortWar");
        }
        private void BoxChanged(object sender, TextChangedEventArgs e)
        {
            AnyBoxChanged(enterFieldHeightBox);
            AnyBoxChanged(enterFieldWidthBox);
            AnyBoxChanged(firstLineBox);
            AnyBoxChanged(firstColumnBox);
            AnyBoxChanged(secondLineBox);
            AnyBoxChanged(secondColumnBox);
        }
        private void AnyBoxChanged(TextBox textBox)
        {
            string ans = "", t = textBox.Text;
            int k = 0;
            for (int i = 0; i < t.Length; i++)
            {
                if (i == textBox.CaretIndex - 1)
                    k = ans.Length + 1;
                if (((int)(t[i]) <= (int)'9') && ((int)t[i] >= (int)'0'))
                    ans += t[i];
            }
            textBox.Text = ans;
            textBox.CaretIndex = k;
        }

        private void HexClick(int i, int j)
        {
            if(hexField.field[i, j].Value < 3)
                hexField.SetHexValue(i, j, (hexField.field[i, j].Value + 1) % 3);
        }
        private void Load()
        {
            string t;
            try
            {
                t = System.IO.File.ReadAllText("ThirdMode.map");
                AIStatus = (int)t[0] - 48;
                hexField.FieldHeight = (int)t[1] * 10 + (int)t[2] - 528; ;
                hexField.FieldWidth = (int)t[3] * 10 + (int)t[4] - 528;
                firstCityLine = (int)t[5] * 10 + (int)t[6] - 528;
                firstCityColumn = (int)t[7] * 10 + (int)t[8] - 528;
                secondCityLine = (int)t[9] * 10 + (int)t[10] - 528;
                secondCityColumn = (int)t[11] * 10 + (int)t[12] - 528;
                selectAIStatus.SelectedIndex = AIStatus;
                for(int i = 0; i < 50; i++)
                    for(int j = 0; j < 50; j++)
                    {
                        hexField.SetHexValue(i, j,  (int)t[i * 100 + j * 2 + 13] * 10 + (int)t[i * 100 + j * 2 + 14] - 528);
                    }
            }
            catch { };
            enterFieldHeightBox.Text = hexField.FieldHeight.ToString();
            enterFieldWidthBox.Text = hexField.FieldWidth.ToString();
            firstLineBox.Text = (firstCityLine + 1).ToString();
            firstColumnBox.Text = (firstCityColumn + 1).ToString();
            secondLineBox.Text = (secondCityLine + 1).ToString();
            secondColumnBox.Text = (secondCityColumn + 1).ToString();
            ApplySettings(null, null);
        }
        private void Save()
        {
            AIStatus = selectAIStatus.SelectedIndex;
            string t = AIStatus.ToString();
            if (hexField.FieldHeight <= 9)
                t += "0";
            t += hexField.FieldHeight.ToString();
            if (hexField.FieldWidth <= 9)
                t += "0";
            t += hexField.FieldWidth.ToString();
            if (firstCityLine <= 9)
                t += "0";
            t += firstCityLine.ToString();
            if (firstCityColumn <= 9)
                t += "0";
            t += firstCityColumn.ToString();
            if (secondCityLine <= 9)
                t += "0";
            t += secondCityLine.ToString();
            if (secondCityColumn <= 9)
                t += "0";
            t += secondCityColumn.ToString();
            for(int i = 0; i < 50; i++)
            {
                for(int j = 0; j < 50; j++)
                {
                    if (hexField.field[i, j].Value <= 9)
                        t += "0";
                    t += hexField.field[i, j].Value.ToString();
                }
            }
            System.IO.File.WriteAllText("ThirdMode.map", t);
        }
        private void Reset(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 50; i++)
                for (int j = 0; j < 50; j++)
                    if (hexField.field[i, j].Value < 3)
                        hexField.SetHexValue(i, j, 0);
        }
        private void StartGame(object sender, RoutedEventArgs e)
        {
            Save();
            hexField.HexClick -= HexClick;
            mainWindow.SizeChanged -= WindowSizeChanged;
            ThirdMode thirdMode = new ThirdMode() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            thirdMode.Build();
        }


    }
}