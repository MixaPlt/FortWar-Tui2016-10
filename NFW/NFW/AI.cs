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
    static class AI
    {
        static private int[,,] possibleSteps = new int[2, 6, 2] { { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, -1 } }, { { -1, 0 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 } } };
        static public pair SecondMode(HexField hexField, int playerStep)
        {
            hexField.field[0, 0].Value = hexField.field[0, 0].Value;
            //     hexField.IsStepPossible(
            pair ans = new pair() { i = 0, j = 0 };
            return ans; 
        }
    }
}
