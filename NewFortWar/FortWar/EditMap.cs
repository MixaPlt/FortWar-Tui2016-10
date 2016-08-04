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
    class EditMap : Window
    {
        //Ссылочки на онно и сетку
        Window MainWindow;
        Canvas MainCanvas;
        //Само поле
        //0 - Пустя клеточка, 1 - 
        int[, ] field = new int[55, 55];
        public void Build(Canvas mainCanvas, Window mainWindow)
        {
            //Чистим-чистим хорошо,  чтобы было чисто
            MainCanvas.Children.Clear();
            //Тут всё ясно
            MainWindow = mainWindow;
            MainCanvas = mainCanvas;
            //Кнопочка
            //Вычисление размеров шестиугольников
        }
    }
}
