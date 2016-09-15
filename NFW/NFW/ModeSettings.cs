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
    class ModeSettings
    {
        public Window mainWindow;
        public Canvas mainCanvas;
        public void Build()
        {
            mainWindow.SizeChanged += MenuBuild;
            MenuBuild(null, null);
        } 
        public void MenuBuild(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Children.Clear();
            //mainCanvas.Width = mainWindow.ActualWidth - 10;
            mainCanvas.Height = mainWindow.ActualHeight - 30;
            Thickness margin = new Thickness() { Top = 0 };
            Label sizeinfo = new Label() { Margin = margin, Width = mainCanvas.Width, Height = mainCanvas.Height / 8, FontSize = mainCanvas.Height / 30, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Введите размер поля" };
            mainCanvas.Children.Add(sizeinfo);
            margin.Top = mainCanvas.Height / 8;
            margin.Left = 0;
            Label xinfo = new Label() { Margin = margin, Width = mainCanvas.Width / 8, Height = mainCanvas.Height / 18, FontSize = mainCanvas.Height / 40, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Высота" };
            mainCanvas.Children.Add(xinfo);
            margin.Left = mainCanvas.Width / 6;
            TextBox borderheight = new TextBox() { Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30 , TextAlignment = TextAlignment.Center};
            mainCanvas.Children.Add(borderheight);
            margin.Left += (mainCanvas.Width / 3 + mainCanvas.Width / 6);
            TextBox borderwidth = new TextBox() { Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30, TextAlignment = TextAlignment.Center};
            mainCanvas.Children.Add(borderwidth);
            margin.Left -= mainCanvas.Width / 6;
            Label yinfo = new Label() { Margin = margin, Width = mainCanvas.Width / 8, Height = mainCanvas.Height / 18, FontSize = mainCanvas.Height / 40, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Ширина" };
            mainCanvas.Children.Add(yinfo);
            margin.Top += mainCanvas.Height / 8;
            margin.Left = 0;
            Label startinfo = new Label() { Margin = margin, Width = mainCanvas.Width, Height = mainCanvas.Height / 8, FontSize = mainCanvas.Height / 30, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Введите координаты городов" };
            mainCanvas.Children.Add(startinfo);
            margin.Top += mainCanvas.Height / 8;
            margin.Left = 0;
            Label cityfinfo = new Label() { Margin = margin, Width = mainCanvas.Width / 8, Height = mainCanvas.Height / 18, FontSize = mainCanvas.Height / 40, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Первый город"};
            mainCanvas.Children.Add(cityfinfo);
            margin.Left = mainCanvas.Width / 6;
            TextBox firstcityx = new TextBox() {Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30, TextAlignment = TextAlignment.Center };
            mainCanvas.Children.Add(firstcityx);
            margin.Left += (mainCanvas.Width / 3 + mainCanvas.Width / 6);
            TextBox firstcityy = new TextBox() { Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30, TextAlignment = TextAlignment.Center };
            mainCanvas.Children.Add(firstcityy);
            margin.Top += mainCanvas.Height / 6;
            margin.Left = 0;
            Label citysinfo = new Label() { Margin = margin, Width = mainCanvas.Width / 8, Height = mainCanvas.Height / 18, FontSize = mainCanvas.Height / 40, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Второй город" };
            mainCanvas.Children.Add(citysinfo);
            margin.Left = mainCanvas.Width / 6;
            TextBox secondcityx = new TextBox() { Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30, TextAlignment = TextAlignment.Center };
            mainCanvas.Children.Add(secondcityx);
            margin.Left += (mainCanvas.Width / 3 + mainCanvas.Width / 6);
            TextBox secondcityy = new TextBox() { Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30, TextAlignment = TextAlignment.Center };
            mainCanvas.Children.Add(secondcityy);
            margin.Top += mainCanvas.Height / 6;
            margin.Left = mainCanvas.Width / 6;
            Button startgame = new Button() { Margin = margin, Width = mainCanvas.Width / 6, Height = mainCanvas.Height / 12, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 45, Content = "Начать игру"};
            mainCanvas.Children.Add(startgame);
            margin.Top += mainCanvas.Height / 8;
            Button back = new Button() { Margin = margin, Width = mainCanvas.Width / 6, Height = mainCanvas.Height / 12, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 45, Content = "Назад" };
            mainCanvas.Children.Add(back);
        }
    }
}
