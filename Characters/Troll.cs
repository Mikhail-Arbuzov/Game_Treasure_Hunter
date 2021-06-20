using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Game_Treasure_Hunter
{
    public class Troll:ICharacterAnimationSprites
    {
        int health;
        int speed;
        public ImageBrush trollSprite = new ImageBrush();

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (value > 0)
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

        public Troll()
        {
            Health = 5;
            Speed = 5;
        }

        public void RunSprites(double i)
        {
            switch(i)
            {
                case 1:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Walk_00.png"));
                    break;
                case 2:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Walk_01.png"));
                    break;
                case 3:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Walk_02.png"));
                    break;
                case 4:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Walk_03.png"));
                    break;
                case 5:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Walk_04.png"));
                    break;
                case 6:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Walk_05.png"));
                    break;
                case 7:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Walk_06.png"));
                    break;
                case 8:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Walk_07.png"));
                    break;
                case 9:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Walk_08.png"));
                    break;
                //case 10:
                //    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Walk_09.png"));
                //    break;
            }
        }

        public void AttackSprites(double x)
        {
            switch(x)
            {
                case 1:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Attack_00.png"));
                    break;
                case 2:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Attack_01.png"));
                    break;
                case 3:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Attack_02.png"));
                    break;
                case 4:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Attack_03.png"));
                    break;
                case 5:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Attack_04.png"));
                    break;
                case 6:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Attack_05.png"));
                    break;
                case 7:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Attack_06.png"));
                    break;
                case 8:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Attack_07.png"));
                    break;
                case 9:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Attack_08.png"));
                    break;
                case 10:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Attack_09.png"));
                    break;
            }
        }

        public void HurtSprites(double y)
        {
            switch(y)
            {
                case 1:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_00.png"));
                    break;
                case 2:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_01.png"));
                    break;
                case 3:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_02.png"));
                    break;
                case 4:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_03.png"));
                    break;
                case 5:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_04.png"));
                    break;
                case 6:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_05.png"));
                    break;
                case 7:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_06.png"));
                    break;
                case 8:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_07.png"));
                    break;
                case 9:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_08.png"));
                    break;
                case 10:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_09.png"));
                    break;
            }
        }

        public void DieSprites(double j)
        {
            switch(j)
            {
                case 1:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Dead_00.png"));
                    break;
                case 2:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_01.png"));
                    break;
                case 3:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_02.png"));
                    break;
                case 4:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_03.png"));
                    break;
                case 5:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_04.png"));
                    break;
                case 6:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_05.png"));
                    break;
                case 7:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_06.png"));
                    break;
                case 8:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_07.png"));
                    break;
                case 9:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_08.png"));
                    break;
                case 10:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Hurt_09.png"));
                    break;
            }
        }

        public void IdleTrSprites(double k)
        {
            switch (k)
            {
                case 1:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Idle_00.png"));
                    break;
                case 2:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Idle_01.png"));
                    break;
                case 3:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Idle_02.png"));
                    break;
                case 4:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Idle_03.png"));
                    break;
                case 5:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Idle_04.png"));
                    break;
                case 6:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Idle_05.png"));
                    break;
                case 7:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Idle_06.png"));
                    break;
                case 8:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Idle_07.png"));
                    break;
                case 9:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Idle_08.png"));
                    break;
                case 10:
                    trollSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTroll/troll_Idle_09.png"));
                    break;
            }
        }
    }
}
