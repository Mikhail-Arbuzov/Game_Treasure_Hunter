using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game_Treasure_Hunter
{
    public class Ninja:ICharacterAnimationSprites
    {
        int health;
        int speed;
        public ImageBrush ninjaSprite = new ImageBrush();

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

        public Ninja()
        {
            Health = 2;
            Speed = 5;
        }

        public void RunSprites(double i)
        {
            switch(i)
            {
                case 1:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Run_00.png"));
                    break;
                case 2:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Run_01.png"));
                    break;
                case 3:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Run_02.png"));
                    break;
                case 4:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Run_03.png"));
                    break;
                case 5:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Run_04.png"));
                    break;
                case 6:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Run_05.png"));
                    break;
                case 7:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Run_06.png"));
                    break;
                case 8:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Run_07.png"));
                    break;
                case 9:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Run_08.png"));
                    break;
                case 10:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Run_09.png"));
                    break;
            }
        }

        public void AttackSprites(double x)
        {
            switch(x)
            {
                case 1:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Attack_00.png"));
                    break;
                case 2:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Attack_01.png"));
                    break;
                case 3:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Attack_02.png"));
                    break;
                case 4:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Attack_03.png"));
                    break;
                case 5:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Attack_04.png"));
                    break;
                case 6:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Attack_05.png"));
                    break;
                case 7:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Attack_06.png"));
                    break;
                case 8:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Attack_07.png"));
                    break;
                case 9:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Attack_08.png"));
                    break;
                case 10:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Attack_09.png"));
                    break;
            }
        }

        public void HurtSprites(double y)
        {
            switch(y)
            {
                case 1:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_00.png"));
                    break;
                case 2:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_01.png"));
                    break;
            }
        }

        public void DieSprites(double j)
        {
            switch(j)
            {
                case 1:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_00.png"));
                    break;
                case 2:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_01.png"));
                    break;
                case 3:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_02.png"));
                    break;
                case 4:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_03.png"));
                    break;
                case 5:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_04.png"));
                    break;
                case 6:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_05.png"));
                    break;
                case 7:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_06.png"));
                    break;
                case 8:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_07.png"));
                    break;
                case 9:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_08.png"));
                    break;
                case 10:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Dead_09.png"));
                    break;
            }
        }

        public void ThrowSprites(double k)
        {
            switch(k)
            {
                case 1:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Throw_00.png"));
                    break;
                case 2:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Throw_01.png"));
                    break;
                case 3:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Throw_02.png"));
                    break;
                case 4:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Throw_03.png"));
                    break;
                case 5:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Throw_04.png"));
                    break;
                case 6:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Throw_05.png"));
                    break;
                case 7:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Throw_06.png"));
                    break;
                case 8:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Throw_07.png"));
                    break;
                case 9:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Throw_08.png"));
                    break;
                case 10:
                    ninjaSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinja/ninja_Throw_09.png"));
                    break;
            }
        }
    }
}
