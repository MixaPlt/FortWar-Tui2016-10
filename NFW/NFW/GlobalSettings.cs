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
//klass prinadlegit Dmitriyu
namespace NFW
{
    class GlobalSettings
    {
        public Window mainWindow;
        public Canvas mainCanvas;
        public void Build()
        {
            mainWindow.SizeChanged += WindowSizeChanged;
            WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged(object send, SizeChangedEventArgs e)
        {
            mainCanvas.Children.Clear();
            mainCanvas.Height = mainWindow.Height - 30;
            mainCanvas.Width = mainWindow.Width - 4;
            Thickness margin = new Thickness() { Top = mainCanvas.Height / 8, Left = 0 };
            Label screenSizeInfoLabel = new Label() { Height = mainCanvas.Height / 8, Width = mainWindow.Width, Margin = margin, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Разрешение экрана", FontSize = mainCanvas.Height / 25};
            mainCanvas.Children.Add(screenSizeInfoLabel);
            
        }
    }

}
