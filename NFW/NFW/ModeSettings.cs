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
        Thickness margin;
        Label sizeinfo;
        Label xinfo;
        TextBox borderwidth;
        TextBox borderheight;
        Label yinfo;
        Label startinfo;
        Label cityfinfo;
        TextBox firstcityx;
        TextBox firstcityy;
        Label citysinfo;
        TextBox secondcityx;
        TextBox secondcityy;
        Button startgame;
        Button back;
        public Window mainWindow;
        public Canvas mainCanvas;
        public void Build()
        {
            mainWindow.SizeChanged += MenuSettings;
            MenuBuild(null, null);
        } 
        public void MenuSettings(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Width = mainWindow.ActualWidth - 10;
            mainCanvas.Height = mainWindow.ActualHeight - 30;
        }
        public void MenuBuild(object sender, SizeChangedEventArgs e)
        {
            mainCanvas.Children.Clear();
            
            margin = new Thickness() { Top = 0 };
            sizeinfo = new Label() { Margin = margin, Width = mainCanvas.Width, Height = mainCanvas.Height / 8, FontSize = mainCanvas.Height / 30, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Введите размер поля" };
            mainCanvas.Children.Add(sizeinfo);
            margin.Top = mainCanvas.Height / 8;
            margin.Left = 0;
            xinfo = new Label() { Margin = margin, Width = mainCanvas.Width / 8, Height = mainCanvas.Height / 18, FontSize = mainCanvas.Height / 40, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Высота" };
            mainCanvas.Children.Add(xinfo);
            margin.Left = mainCanvas.Width / 6;
            borderheight = new TextBox() { Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30 , TextAlignment = TextAlignment.Center};
            mainCanvas.Children.Add(borderheight);
            margin.Left += (mainCanvas.Width / 3 + mainCanvas.Width / 6);
            borderwidth = new TextBox() { Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30, TextAlignment = TextAlignment.Center};
            mainCanvas.Children.Add(borderwidth);
            margin.Left -= mainCanvas.Width / 6;
            yinfo = new Label() { Margin = margin, Width = mainCanvas.Width / 8, Height = mainCanvas.Height / 18, FontSize = mainCanvas.Height / 40, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Ширина" };
            mainCanvas.Children.Add(yinfo);
            margin.Top += mainCanvas.Height / 8;
            margin.Left = 0;
            startinfo = new Label() { Margin = margin, Width = mainCanvas.Width, Height = mainCanvas.Height / 8, FontSize = mainCanvas.Height / 30, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Введите координаты городов" };
            mainCanvas.Children.Add(startinfo);
            margin.Top += mainCanvas.Height / 8;
            margin.Left = 0;
            cityfinfo = new Label() { Margin = margin, Width = mainCanvas.Width / 8, Height = mainCanvas.Height / 18, FontSize = mainCanvas.Height / 40, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Первый город"};
            mainCanvas.Children.Add(cityfinfo);
            margin.Left = mainCanvas.Width / 6;
            firstcityx = new TextBox() {Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30, TextAlignment = TextAlignment.Center };
            mainCanvas.Children.Add(firstcityx);
            margin.Left += (mainCanvas.Width / 3 + mainCanvas.Width / 6);
            firstcityy = new TextBox() { Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30, TextAlignment = TextAlignment.Center };
            mainCanvas.Children.Add(firstcityy);
            margin.Top += mainCanvas.Height / 6;
            margin.Left = 0;
            citysinfo = new Label() { Margin = margin, Width = mainCanvas.Width / 8, Height = mainCanvas.Height / 18, FontSize = mainCanvas.Height / 40, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Второй город" };
            mainCanvas.Children.Add(citysinfo);
            margin.Left = mainCanvas.Width / 6;
            secondcityx = new TextBox() { Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30, TextAlignment = TextAlignment.Center };
            mainCanvas.Children.Add(secondcityx);
            margin.Left += (mainCanvas.Width / 3 + mainCanvas.Width / 6);
            secondcityy = new TextBox() { Margin = margin, Width = mainCanvas.Width / 4, Height = mainCanvas.Height / 8, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 30, TextAlignment = TextAlignment.Center };
            mainCanvas.Children.Add(secondcityy);
            margin.Top += mainCanvas.Height / 6;
            margin.Left = mainCanvas.Width / 6;
            startgame = new Button() { Margin = margin, Width = mainCanvas.Width / 6, Height = mainCanvas.Height / 12, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 45, Content = "Начать игру"};
            startgame.Click += startgame_click;
            mainCanvas.Children.Add(startgame);
            margin.Top += mainCanvas.Height / 8;
            back = new Button() { Margin = margin, Width = mainCanvas.Width / 6, Height = mainCanvas.Height / 12, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 45, Content = "Назад"};
            back.Click += back_click;
            mainCanvas.Children.Add(back);
        }
         private void back_click(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= MenuBuild;
            StartGameMenu startGameMenu = new StartGameMenu() { mainCanvas = mainCanvas, mainWindow = mainWindow };
            startGameMenu.Build();
        }
        private void startgame_click(object sender, RoutedEventArgs e)
        {
            mainWindow.SizeChanged -= MenuBuild;
            FirstMode firstmode = new FirstMode() { mainCanvas = mainCanvas, mainWindow = mainWindow,firstcityy = Int32.Parse(firstcityy.Text),firstcityx = Int32.Parse(firstcityx.Text),secondcityx = Int32.Parse(secondcityx.Text),secondcityy = Int32.Parse(secondcityy.Text),height = Int32.Parse(borderheight.Text),width = Int32.Parse(borderwidth.Text)};
            firstmode.Build();
        }
    }
}
