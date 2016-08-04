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
    public partial class MainWindow : Window
    {
        public static int windowMode;
        public MainWindow()
        {
            InitializeComponent();
            //Извлечение сохранённых настроек из Properties.Settings
            Height = Properties.Settings.Default.mainWindowHeight;
            Width = Properties.Settings.Default.mainWindowWidth;
            //Запуск метода создания главного меню из класса mainMenu в mainMenu.cs
            MainMenu mainMenu = new MainMenu();
            mainMenu.Build(mainCanvas, mainWindow);
        }
        //Properties.Settings.Default.windowMode - режим окна
        private void isWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            switch(Properties.Settings.Default.windowMode)
            {
                case 0:
                    {
                        MainMenu mainMenu = new MainMenu();
                        mainMenu.Build(mainCanvas, mainWindow);
                    }
                    break;
            }
        }
    }
}
