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
    public struct pair
    {
        public int i;
        public int j;
    }
    class AI
    {
        //0 - Пустя клеточка, 1 - горы, 2 - река, 3 - клетка первого, 4 - клеткаа второго, 5 - крепость первого, 6 - крепость второго, 7 - море первого, 8 - море второго, 9 - горы первого, 10 - горы второго, 11 - замок первого, 12 - замок второго
        static private int[] firstPlayerPriorities = new int[11] { 2, 3, 3, 0, 4, 0, 6, 0, 6, 0, 6 };
        static private int[] secondPlayerPriorities = new int[11] { 2, 3, 3, 4, 0, 6, 0, 6, 0, 6, 0 };
        static private int[,,] possibleSteps = new int[2, 6, 2] { { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, -1 } }, { { -1, 0 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } } };
        static public pair SecondMode(HexField hexField, int playerStep)
        {
            pair ans = new pair() { i = 0, j = 0 };
            int maxPoints = -99999;
            for (int i = 0; i < hexField.FieldHeight; i++)
            {
                for (int j = 0; j < hexField.FieldWidth; j++)
                {
                    if (hexField.IsStepPossible(i, j, playerStep))
                    {
                        HexField newField = new HexField() { FieldHeight = hexField.FieldHeight, FieldWidth = hexField.FieldWidth, build = false};
                        newField.Build();
                        for(int l = 0; l < newField.FieldHeight; l++)
                        {
                            for(int k = 0; k < newField.FieldWidth; k++)
                            {
                                newField.field[l, k].Value 
                                    = hexField.field[l, k].Value;
                            }
                        }
                        int p = 0;
                        if (playerStep == 0)
                            p = Points(i, j, playerStep, firstPlayerPriorities, hexField);
                        else
                            p = Points(i, j, playerStep, secondPlayerPriorities, hexField);
                        int minPoints = -9999;
                        for(int l = 0; l < newField.FieldHeight; l++)
                        {
                            for(int k = 0; k < newField.FieldWidth; k++)
                            {
                                if (newField.IsStepPossible(l, k, 1 - playerStep))
                                {
                                    int d = 0;
                                    if (playerStep == 1)
                                        d = Points(l, k, playerStep, firstPlayerPriorities, hexField);
                                    else
                                        d = Points(l, k, playerStep, secondPlayerPriorities, hexField);
                                    minPoints = Math.Max(d, minPoints);
                                }
                            }
                        }
                        if (p - minPoints > maxPoints)
                        {
                            maxPoints = p - minPoints;
                            ans.i = i;
                            ans.j = j;
                        }
                    }
                }
            }
            return ans; 
        }
        static private int Points(int x, int y, int ps, int[] points, HexField hexField)
        {
            int ans = 0;
            for (int i = 0; i < 6; i++)
                if (possibleSteps[y % 2, i, 0] + x < hexField.FieldHeight && possibleSteps[y % 2, i, 0] + x >= 0 && possibleSteps[y % 2, i, 1] + y < hexField.FieldWidth && possibleSteps[y % 2, i, 1] + y >= 0)
                {
                    if (hexField.field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].Value < 11)
                        ans += points[hexField.field[possibleSteps[y % 2, i, 0] + x, possibleSteps[y % 2, i, 1] + y].Value];
                }
            ans += points[hexField.field[x, y].Value];
            return ans;
        }
    }

}
