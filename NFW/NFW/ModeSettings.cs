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
        TextBox borderwidth = new TextBox();
        TextBox borderheight = new TextBox();
        Label yinfo;
        Label startinfo;
        Label cityfinfo;
        TextBox firstcityx = new TextBox();
        TextBox firstcityy = new TextBox();
        Label citysinfo;
        TextBox secondcityx = new TextBox();
        TextBox secondcityy = new TextBox();
        Button startgame;
        Button back;
        Button modechange;
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
            mainCanvas.Width = mainWindow.ActualWidth - 10;
            mainCanvas.Height = mainWindow.ActualHeight - 30;
            margin = new Thickness() { Top = 0 };
            sizeinfo = new Label() { Margin = margin, Width = mainCanvas.Width, Height = mainCanvas.Height / 8, FontSize = mainCanvas.Height / 30, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Введите размер поля" };
            mainCanvas.Children.Add(sizeinfo);
            margin.Top = mainCanvas.Height / 8;
            margin.Left = 0;
            xinfo = new Label() { Margin = margin, Width = mainCanvas.Width / 8, Height = mainCanvas.Height / 18, FontSize = mainCanvas.Height / 40, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Высота" };
            mainCanvas.Children.Add(xinfo);
            margin.Left = mainCanvas.Width / 6;
            borderheight.Margin = margin;
            borderheight.Width = mainCanvas.Width / 4;
            borderheight.Height = mainCanvas.Height / 8;
            borderheight.HorizontalContentAlignment = HorizontalAlignment.Left;
            borderheight.VerticalContentAlignment = VerticalAlignment.Center;
            borderheight.FontSize = mainCanvas.Height / 30;
            borderheight.TextAlignment = TextAlignment.Center;
            mainCanvas.Children.Add(borderheight);
            margin.Left += (mainCanvas.Width / 3 + mainCanvas.Width / 6);
            borderwidth.Margin = margin;
            borderwidth.Width = mainCanvas.Width / 4;
            borderwidth.Height = mainCanvas.Height / 8;
            borderwidth.HorizontalContentAlignment = HorizontalAlignment.Left;
            borderwidth.VerticalContentAlignment = VerticalAlignment.Center;
            borderwidth.FontSize = mainCanvas.Height / 30;
            borderwidth.TextAlignment = TextAlignment.Center;
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
            firstcityx.Margin = margin;
            firstcityx.Width = mainCanvas.Width / 4;
            firstcityx.Height = mainCanvas.Height / 8;
            firstcityx.HorizontalContentAlignment = HorizontalAlignment.Left;
            firstcityx.VerticalContentAlignment = VerticalAlignment.Center;
            firstcityx.FontSize = mainCanvas.Height / 30;
            firstcityx.TextAlignment = TextAlignment.Center;
            mainCanvas.Children.Add(firstcityx);
            margin.Left += (mainCanvas.Width / 3 + mainCanvas.Width / 6);
            firstcityy.Margin = margin;
            firstcityy.Width = mainCanvas.Width / 4;
            firstcityy.Height = mainCanvas.Height / 8;
            firstcityy.HorizontalContentAlignment = HorizontalAlignment.Left;
            firstcityy.VerticalContentAlignment = VerticalAlignment.Center;
            firstcityy.FontSize = mainCanvas.Height / 30;
            firstcityy.TextAlignment = TextAlignment.Center;
            mainCanvas.Children.Add(firstcityy);
            margin.Top += mainCanvas.Height / 6;
            margin.Left = 0;
            citysinfo = new Label() { Margin = margin, Width = mainCanvas.Width / 8, Height = mainCanvas.Height / 18, FontSize = mainCanvas.Height / 40, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center, Content = "Второй город" };
            mainCanvas.Children.Add(citysinfo);
            margin.Left = mainCanvas.Width / 6;
            secondcityx.Margin = margin;
            secondcityx.Width = mainCanvas.Width / 4;
            secondcityx.Height = mainCanvas.Height / 8;
            secondcityx.HorizontalContentAlignment = HorizontalAlignment.Left;
            secondcityx.VerticalContentAlignment = VerticalAlignment.Center;
            secondcityx.FontSize = mainCanvas.Height / 30;
            secondcityx.TextAlignment = TextAlignment.Center;
            mainCanvas.Children.Add(secondcityx);
            margin.Left += (mainCanvas.Width / 3 + mainCanvas.Width / 6);
            secondcityy.Margin = margin;
            secondcityy.Width = mainCanvas.Width / 4;
            secondcityy.Height = mainCanvas.Height / 8;
            secondcityy.HorizontalContentAlignment = HorizontalAlignment.Left;
            secondcityy.VerticalContentAlignment = VerticalAlignment.Center;
            secondcityy.FontSize = mainCanvas.Height / 30;
            secondcityy.TextAlignment = TextAlignment.Center;
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
            margin.Top -= mainCanvas.Height / 8;
            margin.Left += (mainCanvas.Width / 3 + mainCanvas.Width / 6);
            modechange = new Button() { Margin = margin, Width = mainCanvas.Width / 6, Height = mainCanvas.Height / 12, HorizontalContentAlignment = HorizontalAlignment.Left, VerticalContentAlignment = VerticalAlignment.Center, FontSize = mainCanvas.Height / 45, Content = "ИИ выключен" };
            mainCanvas.Children.Add(modechange);
            firstcityx.TextChanged += BoxChanged;
            firstcityy.TextChanged += BoxChanged;
            secondcityx.TextChanged += BoxChanged;
            secondcityy.TextChanged += BoxChanged;
            borderheight.TextChanged += BoxChanged;
            borderwidth.TextChanged += BoxChanged;
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
        private void BoxChanged(object sender, TextChangedEventArgs e)
        {
            AnyBoxChanged(borderwidth);
            AnyBoxChanged(borderheight);
            AnyBoxChanged(firstcityx);
            AnyBoxChanged(firstcityy);
            AnyBoxChanged(secondcityx);
            AnyBoxChanged(secondcityy);
        }
        private void AnyBoxChanged(TextBox textBox)
        {
            string ans = "", t = textBox.Text;
            int k = 0;
            for (int i = 0; i < t.Length; i++)
            {
                if (((int)(t[i]) <= (int)'9') && ((int)t[i] >= (int)'0'))
                    ans += t[i];
                if (i == textBox.CaretIndex - 1)
                    k = ans.Length;
            }
            textBox.Text = ans;
            textBox.CaretIndex = k;
        }
    }
}
