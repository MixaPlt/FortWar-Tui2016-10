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
    class FirstModeSettings
    {
        public Canvas mainCanvas;
        public Window mainWindow;
        private Label EnterFieldPropInfo = new Label() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Введите размеры поля" };
        private Label EnterFieldHeightInfo = new Label() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Высота:" };
        private Label EnterFieldWidthInfo = new Label() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Ширина:" };
        private TextBox EnterFieldHeightBox = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium, MaxLength = 3 };
        private TextBox EnterFieldWidthBox = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium, MaxLength = 3 };
        private Label EnterCityCordInfo = new Label() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Введите координаты городов" };
        private Label LineInfo = new Label() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Строка" };
        private Label ColumnInfo = new Label() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Столбец" };
        private Label FirstInfo = new Label() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Первый" };
        private Label SecondInfo = new Label() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Второй" };
        private TextBox FirstLine = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium, MaxLength = 3 };
        private TextBox FirstColumn = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium, MaxLength = 3 };
        private TextBox SecondLine = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium, MaxLength = 3 };
        private TextBox SecondColumn = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium, MaxLength = 3 };
        private Label EnterTurnsInfo = new Label() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Введите количество ходов" };
        private TextBox EnterTurns = new TextBox() { HorizontalContentAlignment = HorizontalAlignment.Right, VerticalContentAlignment = VerticalAlignment.Center, FontWeight = FontWeights.Medium, MaxLength = 4};
        private Label AIInfo = new Label() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Режим ИИ" };
        private ComboBox SelectAIStatus = new ComboBox() { HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private Button BackButton = new Button() { Content = "Назад" };
        private Button StartButton = new Button() { Content = "Начать игру" };
        public void Build()
        {
            mainCanvas.Children.Clear();
            mainCanvas.Children.Add(EnterFieldPropInfo);
            mainCanvas.Children.Add(EnterFieldHeightInfo);
            mainCanvas.Children.Add(EnterFieldWidthInfo);
            mainCanvas.Children.Add(EnterFieldHeightBox);
            mainCanvas.Children.Add(EnterFieldWidthBox);
            mainCanvas.Children.Add(EnterCityCordInfo);
            mainCanvas.Children.Add(LineInfo);
            mainCanvas.Children.Add(ColumnInfo);
            mainCanvas.Children.Add(FirstInfo);
            mainCanvas.Children.Add(SecondInfo);
            mainCanvas.Children.Add(FirstLine);
            mainCanvas.Children.Add(FirstColumn);
            mainCanvas.Children.Add(SecondLine);
            mainCanvas.Children.Add(SecondColumn);
            mainCanvas.Children.Add(EnterTurnsInfo);
            mainCanvas.Children.Add(EnterTurns);
            mainCanvas.Children.Add(AIInfo);
            mainCanvas.Children.Add(BackButton);
            mainCanvas.Children.Add(StartButton);
            BackButton.Click += Back;
            StartButton.Click += StartGame;
            string[] menu = new string[3] { "ИИ выключен", "ИИ ходит первым", "ИИ ходит вторым" };
            SelectAIStatus.ItemsSource = menu;
            mainCanvas.Children.Add(SelectAIStatus);
            mainWindow.SizeChanged += WindowSizeChanged;
            WindowSizeChanged(null, null);
        }
        private  void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Height = Math.Min(mainWindow.Height, mainWindow.Width / 2 * 3);
            mainCanvas.Width = mainCanvas.Height / 3 * 2;
            Thickness margin = new Thickness() { Top = mainCanvas.Height / 8, Left = 0 };
            EnterFieldPropInfo.Margin = margin;
            EnterFieldPropInfo.Height = mainCanvas.Height / 10;
            EnterFieldPropInfo.Width = mainCanvas.Width;
            EnterFieldPropInfo.FontSize = mainCanvas.Width / 15;
            margin.Top += mainCanvas.Height / 10;
            margin.Left = mainCanvas.Width / 16;
            EnterFieldHeightInfo.Margin = margin;
            EnterFieldHeightInfo.Height = mainCanvas.Height / 16;
            EnterFieldHeightInfo.Width = mainCanvas.Width / 4;
            EnterFieldHeightInfo.FontSize = mainCanvas.Width / 16;
            margin.Left += mainCanvas.Width / 4;
            margin.Top += mainCanvas.Height / 84;
            EnterFieldHeightBox.Margin = margin;
            EnterFieldHeightBox.Height = mainCanvas.Height / 18;
            EnterFieldHeightBox.Width = mainCanvas.Width / 8;
            EnterFieldHeightBox.FontSize = mainCanvas.Width / 16;
            margin.Top -= mainCanvas.Height / 84;
            margin.Left += mainCanvas.Width / 5;
            EnterFieldWidthInfo.Margin = margin;
            EnterFieldWidthInfo.Width = mainCanvas.Width / 3.6;
            EnterFieldWidthInfo.FontSize = mainCanvas.Width / 16;
            margin.Left += mainCanvas.Width / 3.6;
            margin.Top += mainCanvas.Height / 84;
            EnterFieldWidthBox.Margin = margin;
            EnterFieldWidthBox.Height = mainCanvas.Height / 18;
            EnterFieldWidthBox.Width = mainCanvas.Width / 8;
            EnterFieldWidthBox.FontSize = mainCanvas.Width / 16;
            margin.Top += mainCanvas.Height / 16 - mainCanvas.Height / 84;
            margin.Left = 0;
            EnterCityCordInfo.Margin = margin;
            EnterCityCordInfo.Width = mainCanvas.Width;
            EnterCityCordInfo.FontSize = mainCanvas.Width / 16;
            margin.Top += mainCanvas.Width / 10;
            margin.Left = mainCanvas.Width / 3;
            LineInfo.Margin = margin;
            LineInfo.Width = mainCanvas.Width / 3;
            LineInfo.FontSize = mainCanvas.Width / 16;
            margin.Left += mainCanvas.Width / 3.4;
            ColumnInfo.Margin = margin;
            ColumnInfo.Width = mainCanvas.Width / 3;
            ColumnInfo.FontSize = mainCanvas.Width / 16;
            margin.Top += mainCanvas.Height / 16;
            margin.Left = 0;
            FirstInfo.Margin = margin;
            FirstInfo.Width = mainCanvas.Width / 3;
            FirstInfo.FontSize = mainCanvas.Width / 16;
            margin.Left = mainCanvas.Width / 2;
            margin.Top += mainCanvas.Height / 100;
            FirstLine.Margin = margin;
            FirstLine.Height = mainCanvas.Height / 18;
            FirstLine.Width = mainCanvas.Width / 8;
            FirstLine.FontSize = mainCanvas.Width / 16;
            margin.Left += mainCanvas.Width / 6;
            FirstColumn.Margin = margin;
            FirstColumn.Height = mainCanvas.Height / 18;
            FirstColumn.Width = mainCanvas.Width / 8;
            FirstColumn.FontSize = mainCanvas.Width / 16;
            margin.Top += mainCanvas.Height / 16 - mainCanvas.Height / 100;
            margin.Left = 0;
            SecondInfo.Margin = margin;
            SecondInfo.Width = mainCanvas.Width / 3;
            SecondInfo.FontSize = mainCanvas.Width / 16;
            margin.Left = mainCanvas.Width / 2;
            margin.Top += mainCanvas.Height / 100;
            SecondLine.Margin = margin;
            SecondLine.Height = mainCanvas.Height / 18;
            SecondLine.Width = mainCanvas.Width / 8;
            SecondLine.FontSize = mainCanvas.Width / 16;
            margin.Left += mainCanvas.Width / 6;
            SecondColumn.Margin = margin;
            SecondColumn.Height = mainCanvas.Height / 18;
            SecondColumn.Width = mainCanvas.Width / 8;
            SecondColumn.FontSize = mainCanvas.Width / 16;
            margin.Top += mainCanvas.Height / 16 - mainCanvas.Height / 100;
            margin.Left = 0;
            EnterTurnsInfo.Margin = margin;
            EnterTurnsInfo.Width = mainCanvas.Width / 6 * 5;
            EnterTurnsInfo.FontSize = mainCanvas.Width / 16;
            margin.Left = mainCanvas.Width * 5 / 6;
            margin.Top += mainCanvas.Height / 100;
            EnterTurns.Margin = margin;
            EnterTurns.Height = mainCanvas.Height / 18;
            EnterTurns.Width = mainCanvas.Width / 8;
            EnterTurns.FontSize = mainCanvas.Width / 16;
            margin.Top += mainCanvas.Height / 16;
            margin.Left = 0;
            AIInfo.Margin = margin;
            AIInfo.Width = mainCanvas.Width / 2;
            AIInfo.FontSize = mainCanvas.Width / 16;
            margin.Left += mainCanvas.Width / 2;
            SelectAIStatus.Margin = margin;
            SelectAIStatus.FontSize = mainCanvas.Width / 18;
            SelectAIStatus.Height = mainCanvas.Height / 16;
            SelectAIStatus.Width = mainCanvas.Width / 2;
            margin.Top += mainCanvas.Height / 15;
            margin.Left = 0;
            BackButton.Margin = margin;
            BackButton.Height = mainCanvas.Height / 14;
            BackButton.Width = mainCanvas.Width / 2;
            BackButton.FontSize = mainCanvas.Width / 16;
            margin.Left = mainCanvas.Width / 2;
            StartButton.Margin = margin;
            StartButton.Height = mainCanvas.Height / 14;
            StartButton.Width = mainCanvas.Width / 2;
            StartButton.FontSize = mainCanvas.Width / 16;
        }
        private void StartGame(Object sender, RoutedEventArgs e)
        {

        }
        private void Back(Object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= WindowSizeChanged;
            StartGameMenu startGameMenu = new StartGameMenu() { mainWindow = mainWindow, mainCanvas = mainCanvas };
            startGameMenu.Build();
        }
    }
}
