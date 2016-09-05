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
    class Hexagon : Image
    {
        //v - value, значение поля
        //0 - Пустя клеточка, 1 - горы, 2 - река, 3 - клетка первого, 4 - клеткаа второго, 5 - крепость первого, 6 - крепость второго, 7 - море первого, 8 - море второго, 9 - горы первого, 10 - горы второго, 11 - замок первого, 12 - замок второго
        public int Value;
        //Находиться ли клик мыши в гексе
        public bool isMouseOver(MouseEventArgs e)
        {
            //Координаты клика относительно картинки (this) - сам объект
            Point pt = e.GetPosition(this);
            //Проверили вертикальные прямые
            if (pt.X < 0 || pt.Y < 0 || pt.X > Width || pt.Y > Height)
                return false;
            //две верхние прямые y = xtg60 - w/2sin60 и правее 
            if (pt.Y > 1.732 * pt.X + Height / 2 || pt.Y > -1.732 * pt.X + 2.165 * Width)
                return false;
            //Две нижние
            if (pt.Y < -1.732 * pt.X + Width / 2 || pt.Y < 1.732 * pt.X - 1.299 * Width)
                return false;
            return true;

        }
    }
}
