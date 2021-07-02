using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game_Treasure_Hunter
{
   public class Soldier:ICharacterAnimationSprites
    {
        int health;
        int speed;
        public ImageBrush soldierSprite = new ImageBrush();

        public double countDie = 0;

        //продолжительность того или иного действия персонажа
        public int IdleStatesDuration = 100;
        public int RunStatesDuration = 300;
        public bool Idle = false;
        public int StateFrameCounter = 0;

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                //if (value > 0)
                    health = value;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        public Soldier()
        {
            Health = 2;
            Speed = 5;
        }

        public void RunSprites(double i)
        {
            switch(i)
            {
                case 1:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Run_00.png"));
                    break;
                case 2:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Run_01.png"));
                    break;
                case 3:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Run_02.png"));
                    break;
                case 4:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Run_03.png"));
                    break;
                case 5:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Run_04.png"));
                    break;
                case 6:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Run_05.png"));
                    break;
                case 7:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Run_06.png"));
                    break;
                case 8:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Run_07.png"));
                    break;
                case 9:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Run_08.png"));
                    break;
                case 10:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Run_09.png"));
                    break;
            }
        }

        public void AttackSprites(double x)
        {
            switch(x)
            {
                case 1:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Attack_00.png"));
                    break;
                case 2:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Attack_01.png"));
                    break;
                case 3:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Attack_02.png"));
                    break;
                case 4:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Attack_03.png"));
                    break;
                case 5:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Attack_04.png"));
                    break;
                case 6:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Attack_05.png"));
                    break;
                case 7:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Attack_06.png"));
                    break;
                case 8:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Attack_07.png"));
                    break;
                case 9:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Attack_08.png"));
                    break;
                case 10:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Attack_09.png"));
                    break;
            }
        }

        public void HurtSprites(double y)
        {
            switch(y)
            {
                case 1:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Hurt_00.png"));
                    break;
                case 2:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Hurt_01.png"));
                    break;
                case 3:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Hurt_02.png"));
                    break;
                case 4:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Hurt_03.png"));
                    break;
                case 5:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Hurt_04.png"));
                    break;
                case 6:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Hurt_05.png"));
                    break;
                case 7:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Hurt_06.png"));
                    break;
                case 8:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Hurt_07.png"));
                    break;
                case 9:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Hurt_08.png"));
                    break;
                case 10:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Hurt_09.png"));
                    break;
            }
        }

        public void DieSprites(double j)
        {
            switch(j)
            {
                case 1:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Die_00.png"));
                    break;
                case 2:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Die_01.png"));
                    break;
                case 3:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Die_02.png"));
                    break;
                case 4:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Die_03.png"));
                    break;
                case 5:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Die_04.png"));
                    break;
                case 6:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Die_05.png"));
                    break;
                case 7:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Die_06.png"));
                    break;
                case 8:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Die_07.png"));
                    break;
                case 9:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Die_08.png"));
                    break;
                case 10:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Die_09.png"));
                    break;
            }
        }

        public void IdleSoSprites(double k)
        {
            switch(k)
            {
                case 1:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Idle_00.png"));
                    break;
                case 2:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Idle_01.png"));
                    break;
                case 3:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Idle_02.png"));
                    break;
                case 4:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Idle_03.png"));
                    break;
                case 5:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Idle_04.png"));
                    break;
                case 6:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Idle_05.png"));
                    break;
                case 7:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Idle_06.png"));
                    break;
                case 8:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Idle_07.png"));
                    break;
                case 9:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Idle_08.png"));
                    break;
                case 10:
                    soldierSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSoldier/solder_Idle_09.png"));
                    break;
            }
        }
    }

}

