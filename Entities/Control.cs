using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Treasure_Hunter
{
    public class Control
    {
        public string control { get; set; }
        public string action { get; set;}
        public override string ToString()
        {
            return $"{control} {action}";
        }
    }
}
