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
    //Настройки в главном меню
    class GlobalSettings
    {
        //Кнопочки, юзающиеся в других методах
        Button botButton, steepBotButton;
        TextBox gameHeightBox, gameWidthBox, firstCityX, firstCityY, secondCityX, secondCityY, numberSteepsBox;
        //Ссылки на окно и координатную сетку
        Window MainWindow;
        Canvas MainCanvas;
        //Создание меню
        public void Build (Canvas mainCanvas, Window mainWindow)
        {
            Properties.Settings.Default.windowMode = 1;
            MainCanvas = mainCanvas;
            MainWindow = mainWindow;
            //Чистка
            mainCanvas.Children.Clear();
            //Все отступы в одном экземпляре (для удобства)
            Thickness margin = new Thickness() { Top = 0, Left = 48 };
            //Панель с надписью "Выберите игровой режим"
            Label chooseGameModeInfo = new Label() {Margin = margin, Content = "Выберите игровой режим", Height = 32, Width = 208};
            mainCanvas.Children.Add(chooseGameModeInfo);
            //Изменение вида настроек в зависимости от выбранного режима
            switch (Properties.Settings.Default.GameMode)
            {
                case 0: { FirstMode(); } break;
                case 1: { SecondMode(); } break;
                case 2: { ThirdMode(); } break;
            }
        }
        //Настройки для режима "I'm too young to die"
        public void FirstMode()
        {
            Thickness margin = new Thickness() { Top = 32 };
            //Кнока смены режима
            Button chooseModeButton = new Button() {Margin = margin, Content = "I'm too young to die", Height = 64, Width = 256 };
            chooseModeButton.Click += changeGameMode;
            MainCanvas.Children.Add(chooseModeButton);
            margin.Top = 96;
            margin.Left = 0;
            //Информция о TextBoxe для введения размеров поля 
            Label GameHeightWidthInfo = new Label() { Height = 32, Margin = margin, Content = "Введите размеры поля" };
            MainCanvas.Children.Add(GameHeightWidthInfo);
            //Текстбоксы с размерами поля
            margin.Top = 128;
            margin.Left = 0;
            //Поле с надписью "Высота:"
            Label gameHeightBoxInfo = new Label() { Height = 24, Width = 64, Margin = margin, Content = "Высота:" };
            MainCanvas.Children.Add(gameHeightBoxInfo);
            margin.Left = 64;
            //поле для введения высоты
            gameHeightBox = new TextBox() { Height = 24, Width = 64, Margin = margin, Text = Properties.Settings.Default.gameHeight.ToString() };
            margin.Left = 128;
            //Текст "Ширина:"
            Label gameWidthInfo = new Label() { Height = 32, Width = 64, Margin = margin, Content = "Ширина:" };
            MainCanvas.Children.Add(gameWidthInfo);
            margin.Left = 192;
            //Поле для введения ширины
            gameWidthBox = new TextBox() { Height = 24, Width = 64, Margin = margin, Text = Properties.Settings.Default.gameWidth.ToString() };
            MainCanvas.Children.Add(gameHeightBox);
            MainCanvas.Children.Add(gameWidthBox);
            margin.Left = 0;
            margin.Top = 152;
            //Текст "Введите координаты городов"
            Label cityCordInfo = new Label() { Margin = margin, Content = "Введите координаты городов" };
            MainCanvas.Children.Add(cityCordInfo);
            margin.Top = 176;
            margin.Left = 0;
            //Надписи "Первый игрок" и "Второй игрок"
            Label firstCityCordInfo = new Label() { Margin = margin, Content = "Первый игрок:" };
            margin.Left = 128;
            Label secondCityCordInfo = new Label() { Margin = margin, Content = "Второй игрок:" };
            MainCanvas.Children.Add(firstCityCordInfo);
            MainCanvas.Children.Add(secondCityCordInfo);
            margin.Top = 200;
            margin.Left = 0;
            //X:
            Label firstCityXInfo = new Label() { Margin = margin, Height = 24, Width = 24, Content = "X:"};
            MainCanvas.Children.Add(firstCityXInfo);
            margin.Left = 24;
            firstCityX = new TextBox() { Margin = margin, Height = 24, Width = 40, Text = Properties.Settings.Default.firstPlayerCityX.ToString() };
            MainCanvas.Children.Add(firstCityX);
            margin.Left = 64;
            //Y
            Label firstCityYInfo = new Label() {Margin = margin, Width = 24, Content = "Y:" };
            MainCanvas.Children.Add(firstCityYInfo);
            margin.Left = 88;
            firstCityY = new TextBox() { Margin = margin, Height = 24, Width = 40, Text = Properties.Settings.Default.firstPlayerCityY.ToString() };
            MainCanvas.Children.Add(firstCityY);
            //Второй игрок
            //X
            margin.Left = 128;
            Label secondCityXInfo = new Label() { Margin = margin, Width = 24, Content = "X:" };
            MainCanvas.Children.Add(secondCityXInfo);
            margin.Left = 152;
            secondCityX = new TextBox() { Margin = margin, Width = 40, Height = 24, Text = Properties.Settings.Default.secondPlayerCityX.ToString() };
            MainCanvas.Children.Add(secondCityX);
            //Y
            margin.Left = 192;
            Label secondCityYInfo = new Label() { Margin = margin, Width = 24, Content = "Y:" };
            MainCanvas.Children.Add(secondCityYInfo);
            margin.Left = 216;
            secondCityY = new TextBox() { Margin = margin, Width = 40, Height = 24, Text = Properties.Settings.Default.secondPlayerCityY.ToString() };
            MainCanvas.Children.Add(secondCityY);
            margin.Left = 0;
            //Текст "введите кол-во ходов"
            margin.Top = 228;
            Label enterSteepsNumber = new Label() { Margin = margin, Width = 128, Content = "Количество ходов:"};
            MainCanvas.Children.Add (enterSteepsNumber);
            //Ячейка для ввода кол-ва ходов
            margin.Left = 128;
            numberSteepsBox = new TextBox() { Height = 24, Width = 32, Margin = margin, Text = Properties.Settings.Default.numberSteeps.ToString() };
            MainCanvas.Children.Add (numberSteepsBox);
            margin.Top = 256;
            margin.Left = 0;
            //Кнопка включения/выключения бота
            botButton = new Button() {Margin = margin, Height = 32, Width = 160};
            if (Properties.Settings.Default.gameBot == 0)
                botButton.Content = "Бот выключен";
            if (Properties.Settings.Default.gameBot == 1)
                botButton.Content = "Бот включен";
            if (Properties.Settings.Default.gameBot == 2)
                botButton.Content = "Бот с ботом";
            botButton.Click += changeBotMode;
            MainCanvas.Children.Add(botButton);
            //Кнопка номера бота
            margin.Left = 160;
            steepBotButton = new Button() { Margin = margin, Height = 32, Width = 96};
            steepBotButton.Click += changeBotSteep;
            if (Properties.Settings.Default.botSteep == 0)
                steepBotButton.Content = "Ходит первым";
            else
                steepBotButton.Content = "Ходит вторым";
            if(Properties.Settings.Default.gameBot == 1)
            MainCanvas.Children.Add(steepBotButton);
            margin.Top = 288;
            //Кнопка сохранения настроек
            margin.Left = 0;
            Button agreeConfigButton = new Button() { Margin = margin, Height = 48, Width = 128, Content = "Сохранить", IsDefault = true};
            agreeConfigButton.Click += FirstModeAgreeSettings;
            MainCanvas.Children.Add(agreeConfigButton);
            margin.Left = 128;
            //Кнопка выходa без сохранения
            Button disagreeConfigButton = new Button() { Margin = margin, Height = 48, Width = 128, Content = "Не сохранять" };
            disagreeConfigButton.Click += DisagreeSettings;
            MainCanvas.Children.Add(disagreeConfigButton);
        }
        //Настройки для режима "Hey, not too rough!"
        private void SecondMode()
        {
            Thickness margin = new Thickness() { Top = 32 };
            //Кнока смены режима
            Button chooseModeButton = new Button() { Margin = margin, Content = "Hey, not too rough!", Height = 64, Width = 256 };
            chooseModeButton.Click += changeGameMode;
            MainCanvas.Children.Add(chooseModeButton);
            margin.Top = 96;
            margin.Left = 0;
            //Информция о TextBoxe для введения размеров поля 
            Label GameHeightWidthInfo = new Label() { Height = 32, Margin = margin, Content = "Введите размеры поля" };
            MainCanvas.Children.Add(GameHeightWidthInfo);
            //Текстбоксы с размерами поля
            margin.Top = 128;
            margin.Left = 0;
            //Поле с надписью "Высота:"
            Label gameHeightBoxInfo = new Label() { Height = 24, Width = 64, Margin = margin, Content = "Высота:" };
            MainCanvas.Children.Add(gameHeightBoxInfo);
            margin.Left = 64;
            //поле для введения высоты
            gameHeightBox = new TextBox() { Height = 24, Width = 64, Margin = margin, Text = Properties.Settings.Default.gameHeight.ToString() };
            margin.Left = 128;
            //Текст "Ширина:"
            Label gameWidthInfo = new Label() { Height = 32, Width = 64, Margin = margin, Content = "Ширина:" };
            MainCanvas.Children.Add(gameWidthInfo);
            margin.Left = 192;
            //Поле для введения ширины
            gameWidthBox = new TextBox() { Height = 24, Width = 64, Margin = margin, Text = Properties.Settings.Default.gameWidth.ToString() };
            MainCanvas.Children.Add(gameHeightBox);
            MainCanvas.Children.Add(gameWidthBox);
            margin.Left = 0;
            margin.Top = 152;
            //Текст "Введите координаты городов"
            Label cityCordInfo = new Label() { Margin = margin, Content = "Введите координаты городов" };
            MainCanvas.Children.Add(cityCordInfo);
            margin.Top = 176;
            margin.Left = 0;
            //Надписи "Первый игрок" и "Второй игрок"
            Label firstCityCordInfo = new Label() { Margin = margin, Content = "Первый игрок:" };
            margin.Left = 128;
            Label secondCityCordInfo = new Label() { Margin = margin, Content = "Второй игрок:" };
            MainCanvas.Children.Add(firstCityCordInfo);
            MainCanvas.Children.Add(secondCityCordInfo);
            margin.Top = 200;
            margin.Left = 0;
            //X:
            Label firstCityXInfo = new Label() { Margin = margin, Height = 24, Width = 24, Content = "X:" };
            MainCanvas.Children.Add(firstCityXInfo);
            margin.Left = 24;
            firstCityX = new TextBox() { Margin = margin, Height = 24, Width = 40, Text = Properties.Settings.Default.firstPlayerCityX.ToString() };
            MainCanvas.Children.Add(firstCityX);
            margin.Left = 64;
            //Y
            Label firstCityYInfo = new Label() { Margin = margin, Width = 24, Content = "Y:" };
            MainCanvas.Children.Add(firstCityYInfo);
            margin.Left = 88;
            firstCityY = new TextBox() { Margin = margin, Height = 24, Width = 40, Text = Properties.Settings.Default.firstPlayerCityY.ToString() };
            MainCanvas.Children.Add(firstCityY);
            //Второй игрок
            //X
            margin.Left = 128;
            Label secondCityXInfo = new Label() { Margin = margin, Width = 24, Content = "X:" };
            MainCanvas.Children.Add(secondCityXInfo);
            margin.Left = 152;
            secondCityX = new TextBox() { Margin = margin, Width = 40, Height = 24, Text = Properties.Settings.Default.secondPlayerCityX.ToString() };
            MainCanvas.Children.Add(secondCityX);
            //Y
            margin.Left = 192;
            Label secondCityYInfo = new Label() { Margin = margin, Width = 24, Content = "Y:" };
            MainCanvas.Children.Add(secondCityYInfo);
            margin.Left = 216;
            secondCityY = new TextBox() { Margin = margin, Width = 40, Height = 24, Text = Properties.Settings.Default.secondPlayerCityY.ToString() };
            MainCanvas.Children.Add(secondCityY);
            margin.Left = 0;
            //Текст "введите кол-во ходов"
            margin.Top = 228;
            Label enterSteepsNumber = new Label() { Margin = margin, Width = 128, Content = "Количество ходов:" };
            MainCanvas.Children.Add(enterSteepsNumber);
            //Ячейка для ввода кол-ва ходов
            margin.Left = 128;
            numberSteepsBox = new TextBox() { Height = 24, Width = 32, Margin = margin, Text = Properties.Settings.Default.numberSteeps.ToString() };
            MainCanvas.Children.Add(numberSteepsBox);
            margin.Top = 256;
            margin.Left = 0;
            //Кнопка включения/выключения бота
            botButton = new Button() { Margin = margin, Height = 32, Width = 160 };
            if (Properties.Settings.Default.gameBot == 0)
                botButton.Content = "Бот выключен";
            if (Properties.Settings.Default.gameBot == 1)
                botButton.Content = "Бот включен";
            if (Properties.Settings.Default.gameBot == 2)
                botButton.Content = "Бот с ботом";
            botButton.Click += changeBotMode;
            MainCanvas.Children.Add(botButton);
            //Кнопка номера бота
            margin.Left = 160;
            steepBotButton = new Button() { Margin = margin, Height = 32, Width = 96 };
            steepBotButton.Click += changeBotSteep;
            if (Properties.Settings.Default.botSteep == 0)
                steepBotButton.Content = "Ходит первым";
            else
                steepBotButton.Content = "Ходит вторым";
            if (Properties.Settings.Default.gameBot == 1)
                MainCanvas.Children.Add(steepBotButton);
            //Кнопка смены карты
            margin.Top = 288;
            margin.Left = 0;
            Button editMapButton = new Button() { Margin = margin, Height = 32, Width = 256, Content = "Редактировать карту" };
            editMapButton.Click += SecondModeChangeMap;
            MainCanvas.Children.Add(editMapButton);
            margin.Top = 320;
            //Кнопка сохранения настроек
            margin.Left = 0;
            Button agreeConfigButton = new Button() { Margin = margin, Height = 48, Width = 128, Content = "Сохранить", IsDefault = true };
            agreeConfigButton.Click += FirstModeAgreeSettings;
            MainCanvas.Children.Add(agreeConfigButton);
            margin.Left = 128;
            //Кнопка выходa без сохранения
            Button disagreeConfigButton = new Button() { Margin = margin, Height = 48, Width = 128, Content = "Не сохранять" };
            disagreeConfigButton.Click += DisagreeSettings;
            MainCanvas.Children.Add(disagreeConfigButton);
        }
        //Настройки для режима "Hurt me plenty"
        private void ThirdMode()
        {
            Thickness margin = new Thickness() { Top = 32 };
            //Кнока смены режима
            Button chooseModeButton = new Button() { Margin = margin, Content = "Hurt me plenty", Height = 64, Width = 256 };
            chooseModeButton.Click += changeGameMode;
            MainCanvas.Children.Add(chooseModeButton);
        }
        //Смена режима
        private void changeGameMode (object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.GameMode = (Properties.Settings.Default.GameMode + 1 ) % 3;
            Build(MainCanvas, MainWindow);
        }
        private void changeBotMode(object sender, RoutedEventArgs e)
        {          
            Properties.Settings.Default.gameBot = (Properties.Settings.Default.gameBot + 1) % 3;
            switch(Properties.Settings.Default.gameBot)
            {
                case 0:
                    botButton.Content = "Бот выключен";
                    MainCanvas.Children.Remove(steepBotButton);
                    break;
                case 1:
                    botButton.Content = "Бот включен";
                    MainCanvas.Children.Add(steepBotButton);
                    break;
                case 2:
                    botButton.Content = "Бот с ботом";
                    MainCanvas.Children.Remove(steepBotButton);
                    break;

            }
        }
        private void FirstModeAgreeSettings(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Build(MainCanvas, MainWindow);
        }

        //Сохранение настроек для первого режима
        private void SaveSettings ()
        {
            MyFunctions myFunctions = new MyFunctions();
            //Проверка корректности данных
            if (myFunctions.StringToInt(gameHeightBox.Text) == 0)
                return;
            if (myFunctions.StringToInt(gameWidthBox.Text) == 0)
                return;
            if (myFunctions.StringToInt(firstCityX.Text) == 0)
                return;
            if (myFunctions.StringToInt(firstCityY.Text) == 0)
                return;
            if (myFunctions.StringToInt(secondCityX.Text) == 0)
                return;
            if (myFunctions.StringToInt(secondCityY.Text) == 0)
                return;
            if (myFunctions.StringToInt(numberSteepsBox.Text) == 0)
                return;
            if(myFunctions.StringToInt(gameHeightBox.Text) < myFunctions.StringToInt(firstCityY.Text))
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }
            if (myFunctions.StringToInt(gameWidthBox.Text) < myFunctions.StringToInt(firstCityX.Text))
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }
            if (myFunctions.StringToInt(gameHeightBox.Text) < myFunctions.StringToInt(secondCityY.Text))
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }
            if (myFunctions.StringToInt(gameWidthBox.Text) < myFunctions.StringToInt(secondCityX.Text))
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }
            if (myFunctions.StringToInt(gameWidthBox.Text) > 50 || myFunctions.StringToInt(gameHeightBox.Text) > 50)
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }
            if (myFunctions.StringToInt(firstCityY.Text) == myFunctions.StringToInt(secondCityY.Text) && myFunctions.StringToInt(firstCityX.Text) == myFunctions.StringToInt(secondCityX.Text))
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }
            //Сохранение
            Properties.Settings.Default.gameHeight = myFunctions.StringToInt(gameHeightBox.Text);
            Properties.Settings.Default.gameWidth = myFunctions.StringToInt(gameWidthBox.Text);
            Properties.Settings.Default.firstPlayerCityX = myFunctions.StringToInt(firstCityX.Text);
            Properties.Settings.Default.firstPlayerCityY = myFunctions.StringToInt(firstCityY.Text);
            Properties.Settings.Default.secondPlayerCityX = myFunctions.StringToInt(secondCityX.Text);
            Properties.Settings.Default.secondPlayerCityY = myFunctions.StringToInt(secondCityY.Text);
            Properties.Settings.Default.numberSteeps = myFunctions.StringToInt(numberSteepsBox.Text);
            Properties.Settings.Default.mainWindowHeight = (int)MainWindow.Height;
            Properties.Settings.Default.mainWindowWidth = (int)MainWindow.Width;
            Properties.Settings.Default.Save();
        }
        private void SecondModeAgreeSettings(object sender, RoutedEventArgs e)
        {
            
        }
        //Выход без сохранения
        private void DisagreeSettings (object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Build(MainCanvas, MainWindow);
        }
        //Изменяет номер хода бота
        private void changeBotSteep(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.botSteep = 1 - Properties.Settings.Default.botSteep;
            switch(Properties.Settings.Default.botSteep)
            {
                case 0:
                    steepBotButton.Content = "Ходит первым";
                    break;
                case 1:
                    steepBotButton.Content = "Ходит вторым";
                    break;
            }
        }
        //Кнопка редактирования карты для втрго режима
        private void SecondModeChangeMap(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            EditMap editMap = new FortWar.EditMap();
            editMap.Build(MainCanvas, MainWindow);
        }
    }
}
