using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Treasure_Hunter
{
    interface ICharacterAnimationSprites
    {
        void RunSprites(double i);
        void AttackSprites(double x);
        void HurtSprites(double y);
        void DieSprites(double j);
    }
}
