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
    //Класс с полезными функциями
    class MyFunctions
    {
        public int StringToInt (string s)
        {
            int ans = 0;
            if (Int32.TryParse(s, out ans) && ans > 0)
                return ans;
            MessageBox.Show("Введены некорректные данные");
            return 0; 
        }
        //Cтабильный StingToInt; Не выдаёт ошибок
        /*int StringToInt (string s)
        {
            int ans = 0;
            for(int i = 0; i < s.Length; i++)
            {
                if(( s[i]< '0')&& (s[i] > '9'))
                {
                    MessageBox.Show ("Введены некорректные данные");
                }
                ans *= 10;
                ans += (int)s[i] - '0';
            }
            return ans;
        }*/
    }
}
