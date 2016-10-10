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
        static private int[] firstPlayerPriorities = new int[11] { 1, 1, 1, 0, 2, 0, 3, 0, 2, 0, 2 };
        static private int[] secondPlayerPriorities = new int[11] { 1, 1, 1, 2, 0, 3, 0, 2, 0, 2, 0 };
        static private int[,,] possibleSteps = new int[2, 6, 2] { { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, -1 } }, { { -1, 0 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } } };
        static public pair SecondMode(HexField hexField, int playerStep)
        {
            pair ans = new pair() { i = 5, j = 5 };
            int maxPoints = -999999;
            for (int i = 0; i < hexField.FieldHeight; i++)
            {
                for (int j = 0; j < hexField.FieldWidth; j++)
                {
                    if (hexField.IsStepPossible(i, j, playerStep))
                    {
                        int p = 0;
                        if (playerStep == 0)
                            p = Points(i, j, playerStep, firstPlayerPriorities, hexField);
                        else
                            p = Points(i, j, playerStep, secondPlayerPriorities, hexField);
                        if (p > maxPoints || maxPoints == 0)
                        {
                            maxPoints = p;
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
