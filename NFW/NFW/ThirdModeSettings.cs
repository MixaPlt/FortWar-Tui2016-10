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
        private Label enterFieldPropInfo = new Label() { Content = "Задайте размеры поля", HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
        private Label enterFieldHeightInfo = new Label() { Content = "Высота:", HorizontalContentAlignment = HorizontalAlignment.Right, VerticalContentAlignment = VerticalAlignment.Center };
        private TextBox enterFieldHeightBox = new TextBox() { VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center, FontWeight = FontWeights.Medium };
        private Label enterFieldWidthInfo = new Label() { Content = "Ширина:", HorizontalContentAlignment = HorizontalAlignment.Right, VerticalContentAlignment = VerticalAlignment.Center};
        private TextBox enterFieldWidthBox = new TextBox() { VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center, FontWeight = FontWeights.Medium };
        public void Build()
        {
            mainWindow.SizeChanged += WindowSizeChanged;
            mainCanvas.Children.Add(enterFieldPropInfo);
            mainCanvas.Children.Add(enterFieldHeightInfo);
            mainCanvas.Children.Add(enterFieldHeightBox);
            mainCanvas.Children.Add(enterFieldWidthInfo);
            mainCanvas.Children.Add(enterFieldWidthBox);
            WindowSizeChanged(null, null);
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            int fontSize = (int)Math.Min(mainCanvas.Width / 45, mainCanvas.Height / 27);
            mainCanvas.Height = mainWindow.ActualHeight - 30;
            mainCanvas.Width = mainWindow.ActualWidth - 4;
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
            enterFieldHeightBox.Width = mainCanvas.Width / 16;
            enterFieldHeightBox.Height = mainCanvas.Height / 25;
            enterFieldHeightBox.FontSize = fontSize;
            margin.Left = 0;
            margin.Top = mainCanvas.Height * 7 / 30;
            enterFieldWidthInfo.Margin = margin;
            enterFieldWidthInfo.Width = mainCanvas.Width / 8;
            enterFieldWidthInfo.Height = mainCanvas.Height / 15;
            enterFieldWidthInfo.FontSize = fontSize;
            margin.Left = mainCanvas.Width / 8;
            margin.Top += mainCanvas.Height / 60;
            enterFieldWidthBox.Margin = margin;
            enterFieldWidthBox.Width = mainCanvas.Width / 16;
            enterFieldWidthBox.Height = mainCanvas.Height / 25;
            enterFieldWidthBox.FontSize = fontSize;
        }
    }
}