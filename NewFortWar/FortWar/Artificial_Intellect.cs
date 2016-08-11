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
    public struct pair
    {
        public int x;
        public int y;
    }
    //исскуственные интеллекты для разных режимов
    class ArtificialIntellect
    {
        //первый режим (первый бот для сражения со вторым)
        public pair FirstModeFirstBot (Hexagon[, ] field, int f)
        {
            //f - номер хода игрока
            //field - игровое поле
            //0 = пустая клетка 1 = замок первого 2 = замок второго 3 = крепость первого 4 = крепость второго
            //5 = клетка первого 6 = клетка второго
            pair ans;
            ans.x = 5;
            ans.y = 5;
            for(int i = 0; i < Properties.Settings.Default.gameHeight; i++)
            {
                for(int j = 0; j < Properties.Settings.Default.gameWidth; j++)
                {
                        ans.x = j;
                        ans.y = i;
                        return ans;
                }
            }
            return ans;
        }
        //первый режим второй бот
        public pair FirstModeSecondBot(Hexagon[,] field, int f)
        {
            //f - номер хода игрока
            //field - игровое поле
            //0 = пустая клетка 1 = замок первого 2 = замок второго 3 = крепость первого 4 = крепость второго
            //5 = клетка первого 6 = клетка второго
            pair ans;
            ans.x = 5;
            ans.y = 5;
                        ans.x = 0;
                        ans.y = 0;
                        return ans;
        }
    }
}

