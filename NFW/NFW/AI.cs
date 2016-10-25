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
        public bool b;
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
                                        d = Points(l, k, playerStep, firstPlayerPriorities, newField);
                                    else
                                        d = Points(l, k, playerStep, secondPlayerPriorities, newField);
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
        static public pair[] ThirdMode(HexField hexField, Knight[] knights, int numberOfKnights, int ps, int numberOfSteeps)
        {
            pair[] ans = new pair[numberOfKnights];
            if (numberOfSteeps > 0)
            {
                /*
                for (int i = numberOfKnights / 2 - 1; i > 1; i--)
                {
                    for (int l1 = 0; l1 < 6; l1++)
                    {
                        int i1 = knights[i * 2 + ps].i + possibleSteps[knights[i * 2 + ps].j % 2, l1, 0];
                        int j1 = knights[i * 2 + ps].j + possibleSteps[knights[i * 2 + ps].j % 2, l1, 1];
                        if (i1 >= 0 && j1 >= 0 && i1 < hexField.FieldHeight && j1 < hexField.FieldWidth)
                            if (hexField.field[i1, j1].Value != 1 && hexField.field[i1, j1].Value != 9 && hexField.field[i1, j1].Value != 10 && hexField.field[i1, j1].Knight == -1)
                                for (int l2 = 0; l2 < 6; l2++)
                                {
                                    int i2 = i1 + possibleSteps[j1 % 2, l2, 0];
                                    int j2 = j1 + possibleSteps[j1 % 2, l2, 1];
                                    if (i2 >= 0 && j2 >= 0 && i2 < hexField.FieldHeight && j2 < hexField.FieldWidth)
                                        if (hexField.field[i2, j2].Value != 1 && hexField.field[i2, j2].Value != 9 && hexField.field[i2, j2].Value != 10 && hexField.field[i2, j2].Knight == -1)
                                            for (int l3 = 0; l3 < 6; l3++)
                                            {
                                                int i3 = i2 + possibleSteps[j2 % 2, l3, 0];
                                                int j3 = j2 + possibleSteps[j2 % 2, l3, 1];
                                                if (i3 >= 0 && j3 >= 0 && i3 < hexField.FieldHeight && j3 < hexField.FieldWidth)
                                                    if (hexField.field[i3, j3].Value != 1 && hexField.field[i3, j3].Value != 9 && hexField.field[i3, j3].Value != 10 && hexField.field[i3, j3].Knight == -1)
                                                        for (int l4 = 0; l4 < 6; l4++)
                                                        {
                                                            int i4 = i3 + possibleSteps[j3 % 2, l4, 0];
                                                            int j4 = j3 + possibleSteps[j3 % 2, l4, 1];
                                                            if (i4 >= 0 && j4 >= 0 && i4 < hexField.FieldHeight && j4 < hexField.FieldWidth)
                                                            {
                                                                if (hexField.field[i4, j4].Knight == i * 2 + ps - 4)
                                                                {
                                                                    ans[i].i = i2;
                                                                    ans[i].j = j2;
                                                                    ans[i].b = false;
                                                                }
                                                            }
                                                        }
                                            }
                                }
                    }
                }
                */
                HexField newField = new HexField() { FieldHeight = hexField.FieldHeight, FieldWidth = hexField.FieldWidth, build = false };
                newField.Build();
                for(int i = 0; i < hexField.FieldHeight; i++)
                {
                    for(int j = 0; j < hexField.FieldWidth; j++)
                    {
                        newField.field[i, j].Value = hexField.field[i, j].Value;
                    }
                }
                for (int i = 0; i < numberOfKnights; i++)
                {
                    if (knights[i].Value == 0 || knights[i].Value == 1)
                    {

                        newField.Step(knights[i].i, knights[i].j, knights[i].Value);
                    }
                }
                //    
                for (int i = 0; i < numberOfKnights / 2; i++)
                {
                    int mp = -9999;
                    int i0 = knights[ps + i * 2].i;
                    int j0 = knights[ps + 2 * i].j;
                    for (int l1 = 0; l1 < 6; l1++)
                    {
                        int i1 = i0 + possibleSteps[j0 % 2, l1, 0];
                        int j1 = j0 + possibleSteps[j0 % 2, l1, 1];
                        if (i1 >= 0 && j1 >= 0 && i1 < hexField.FieldHeight && j1 < hexField.FieldWidth)
                            if ((hexField.field[i1, j1].Value == 0 || (hexField.field[i1, j1].Value >= 2 && hexField.field[i1, j1].Value <= 8)) && hexField.field[i1, j1].Knight == -1)
                                for (int l2 = 0; l2 < 6; l2++)
                                {
                                    int i2 = i1 + possibleSteps[j1 % 2, l2, 0];
                                    int j2 = j1 + possibleSteps[j1 % 2, l2, 1];
                                    if (i2 >= 0 && j2 >= 0 && i2 < hexField.FieldHeight && j2 < hexField.FieldWidth)
                                        if ((hexField.field[i2, j2].Value == 0 || (hexField.field[i2, j2].Value >= 2 && hexField.field[i2, j2].Value <= 8) ) && hexField.field[i2, j2].Knight == -1)
                                        {
                                            int pp = 0;
                                            if (ps == 0)
                                                pp = Points(i2, j2, ps, firstPlayerPriorities, newField);
                                            else
                                                pp = Points(i2, j2, ps, secondPlayerPriorities, newField);
                                            if (pp > mp)
                                            {
                                                mp = pp;
                                                ans[i].i = i2;
                                                ans[i].j = j2;
                                                hexField.field[knights[i * 2 + ps].i, knights[i * 2 + ps].j].Knight = -1;
                                                knights[i * 2 + ps].i = ans[i].i;
                                                knights[i * 2 + ps].j = ans[i].j;
                                                hexField.field[ans[i].i, ans[i].j].Knight = i * 2 + ps;
                                                knights[i * 2 + ps].Margin = hexField.field[ans[i].i, ans[i].j].Margin;
                                            }
                                        }
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
   //         MessageBox.Show(String.Format("{0}  :   {1}", x, y));
            if(hexField.field[x, y].Value < 11)
             ans += points[hexField.field[x, y].Value];
            return ans;
        }
    }

}
