using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Treasure_Hunter
{
    public class Result
    {
        public string name { get; set; }
        public int health { get; set; }
        public int coins { get; set; }
        public int cartridges { get; set; }
        public string complexity { get; set; }
        public string time { get; set; }// для ввода данных
        public TimeSpan time2 { get; set; } // для вывода данных
        public override string ToString()
        {
            return $"{name} {health} {coins} {cartridges} {complexity} {time} {time2}";
        }
    }
}
