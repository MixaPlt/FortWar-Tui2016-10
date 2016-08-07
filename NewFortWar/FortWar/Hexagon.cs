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
    class Hexagon : Image
    {
        //Просто 2 параметра для личного использования (в моём случае - координаты относительно поля)
        public int X, Y;
        //Находиться ли клик мыши в гексе
        public bool isMouseOver(object sender, MouseEventArgs e)
        {
            //Координаты клика относительно картинки (this) - сам объект
            Point pt = e.GetPosition(this);
            //Проверили вертикальные прямые
            if(pt.X < 0 || pt.Y < 0 || pt.X > Width || pt.Y > Height)
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
