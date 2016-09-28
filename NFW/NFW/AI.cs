using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFW
{
    public struct pair
    {
        public int i;
        public int j;
    }
    static class AI
    {
        static public pair SecondMode(HexField hexField, int playerStep)
        {
            hexField.field[0, 0].Value = hexField.field[0, 0].Value;
            //     hexField.IsStepPossible(
            pair ans = new pair() { i = 0, j = 0 };
            return ans; 
        }
    }
}
