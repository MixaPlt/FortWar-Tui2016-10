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
       
        public void Build()
        {
            mainCanvas.Children.Clear();
            mainCanvas.Children.Add(EnterFieldPropInfo);
            mainWindow.SizeChanged += WindowSizeChanged;
            WindowSizeChanged(null, null);
        }
        private  void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Height = Math.Min(mainWindow.Height, mainWindow.Width / 2 * 3);
            mainCanvas.Width = mainCanvas.Height / 3 * 2;
            Thickness margin = new Thickness() { Top = mainCanvas.Height / 10, Left = 0 };
            EnterFieldPropInfo.Margin = margin;
            EnterFieldPropInfo.Height = mainCanvas.Height / 10;
            EnterFieldPropInfo.Width = mainCanvas.Width;
            EnterFieldPropInfo.FontSize = Math.Min(mainCanvas.Height / 15, mainCanvas.Width / 15);
            
        }
    }
}
