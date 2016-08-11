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
    //Внимание!!! Снизу описаны очень полезные функциии
    class ArtificialIntellect
    {
        //Высота и ширина игрового поля
        public int fieldHeight, fieldWidth;
        //Массив окружающих данную клетку
        private int[,,] possibleSteps = new int[2, 6, 2] { { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, -1 } }, { { -1, 0 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } } };
        //Первый бот для первого и второго режимов
        private int[] firstPlayerPriorities = new int[11] {1, 1, 1, 0, 2, 0, 3, 0, 2, 0, 2};
        private int[] secondPlayerPriorities = new int[11] {1, 1, 1, 2, 0, 3, 0, 2, 0, 2, 0};
        public pair SecondModeFirstBot (Hexagon[, ] field, int ps)
        {
            //f - номер хода игрока
            //field.V - игровое поле
            //0 - Пустя клеточка, 1 - горы, 2 - река, 3 - клетка первого, 4 - клеткаа второго, 5 - крепость первого,
            //6 - крепость второго, 7 - море первого, 8 - море второго, 9 - горы первого, 10 - горы второго, 11 - замок первого, 12 - замок второго
            pair ans;
            ans.x = 5;
            ans.y = 5;
            int maxPoints = 0;
            for(int i = 0; i < fieldHeight; i++)
            {
                for(int j = 0; j < fieldWidth; j++)
                {
                    if (isStepPossible(i, j, ps, field))
                    {
                        int p = 0;
                        if(ps == 0)
                            p = Points(i, j, ps, firstPlayerPriorities, field);
                        else
                            p = Points(i, j, ps, secondPlayerPriorities, field);
                        if (p > maxPoints)
                        {
                            maxPoints = p;
                            ans.x = i;
                            ans.y = j;
                        }
                    }
                }
            }
            return ans;
        }
        //Проверяет клетку на возможность хода в неё x - строка, у - столбец, ps - номер игрока, который ходит (0 или 1) field - поле
        private bool isStepPossible(int x, int y, int ps, Hexagon[, ] field)
        {
            if (field[x, y].V != 0 && field[x, y].V != 3 && field[x, y].V != 4)
                return false;
            for (int i = 0; i < 6; i++)
                if (possibleSteps[y % 2, i, 0] + x < fieldHeight && possibleSteps[y % 2, i, 0] + x >= 0 && possibleSteps[y % 2, i, 1] + y < fieldWidth && possibleSteps[y % 2, i, 1] + y >= 0)
                {
                    if (field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V % 2 == 1 - ps && field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V > 2)
                        return true;
                }
            return false;
        }
        //Считает количество очков, получаемых при ходе в данную клетку (для жадинки) x - столбец хода, у - строка, ps - номер хода игрока, points - массив из 10 элементов, содержащий приоритеты
        private int Points(int x, int y, int ps, int[] points, Hexagon[, ] field)
        {
            int ans = 0;
            for (int i = 0; i < 6; i++)
                if (possibleSteps[y % 2, i, 0] + x < fieldHeight && possibleSteps[y % 2, i, 0] + x >= 0 && possibleSteps[y % 2, i, 1] + y < fieldWidth && possibleSteps[y % 2, i, 1] + y >= 0)
                {
                    if(field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V < 11)
                        ans += points[field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].V];
                }
            ans += points[field[x, y].V];
            return ans;
        }
    }
}

