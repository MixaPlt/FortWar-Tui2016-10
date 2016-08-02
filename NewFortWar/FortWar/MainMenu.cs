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
    public class MainMenu
    {
        //Ссылки на окно и координатную сетку
        Window MainWindow;
        Canvas MainCanvas;
        //Метод, создающий на координатном поле mainCanvas главное меню
        public void Build(Canvas mainCanvas, Window mainWindow)
        {
            MainWindow = mainWindow;
            MainCanvas = mainCanvas;
            //Очистка поля
            mainCanvas.Children.Clear();
            //Создание кнопок главного меню
            //Начать игру
            Thickness buttonMargin = new Thickness() { Top = mainWindow.Height / 2 - 160, Left = mainWindow.Width / 2 - 128 };
            Button StartGameButton = new Button() { Height = 64, Width = 256, Content = "Начать игру", Margin = buttonMargin};
            mainCanvas.Children.Add(StartGameButton);
            StartGameButton.Click += StartGame;
            //Продолжить сохранённую игру, если она существует
            if(Properties.Settings.Default.isGameSaved)
            {
                buttonMargin.Top += 64;
                Button ContinueGameButton = new Button() { Height = 64, Width = 256, Content = "Продолжить", Margin = buttonMargin};
                ContinueGameButton.Click += ContinueGame;
                mainCanvas.Children.Add(ContinueGameButton);
            }
            //Глобальные настройки
            buttonMargin.Top += 64;
            Button GlobalSettingsButton = new Button() { Height = 64, Width = 256, Content = "Параметры", Margin = buttonMargin};
            GlobalSettingsButton.Click += Settings;
            mainCanvas.Children.Add(GlobalSettingsButton);
            //Выход из игры 
            buttonMargin.Top += 64;
            Button ExitButton = new Button() { Height = 64, Width = 256, Content = "Выйти", Margin = buttonMargin };
            ExitButton.Click += ExitGame;
            mainCanvas.Children.Add(ExitButton);

        }
        //Нажатие старта игры
        private void StartGame(object sender, RoutedEventArgs e)
        {
             switch(Properties.Settings.Default.GameMode)
            {
                case 0:
                    {
                        FirstMode firstMode = new FirstMode();
                        firstMode.Build(MainCanvas, MainWindow, false);
                    }
                    break;
            }
        }
        //Продолжение игры
        private void ContinueGame(object sender, RoutedEventArgs e)
        {
            switch (Properties.Settings.Default.savedMode)
            {
                case 0:
                    {
                        FirstMode firstMode = new FirstMode();
                        firstMode.Build(MainCanvas, MainWindow, true);
                    }
                    break;
            }
        }
        //Вызов настроек
        private void Settings(object sender, RoutedEventArgs e)
        {
            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.Build(MainCanvas, MainWindow);
        }
        //Выход из игры
        private void ExitGame(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
