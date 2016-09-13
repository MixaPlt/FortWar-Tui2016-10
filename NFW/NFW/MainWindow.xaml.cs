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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainCanvas.HorizontalAlignment = HorizontalAlignment.Center;
            mainCanvas.VerticalAlignment = VerticalAlignment.Center;
            if (File.Exists("screenresolution.txt") == true)
            {
                StreamReader file = new StreamReader("screenresolution.txt");
                mainWindow.Height = double.Parse(file.ReadLine());
                mainWindow.Width = double.Parse(file.ReadLine());
                file.Close();
            }
            else
            {
                string[] arr = { mainWindow.ActualHeight.ToString(), mainWindow.ActualWidth.ToString() };
                File.WriteAllLines("screenresolution.txt", arr);
            }
            MainMenu mainMenu = new MainMenu { mainCanvas = mainCanvas, mainWindow = mainWindow};
            mainMenu.Build();
        }
    }
}
