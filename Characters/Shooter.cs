using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game_Treasure_Hunter
{
    public enum DirectionShooter
    {
        Left = 1,
        Right = 2
    }

    public class Shooter:ICharacterAnimationSprites
    {
        int health;
        int speed;
        //public bool life = false;
        public ImageBrush shooterSprite = new ImageBrush();

        //продолжительность того или иного действия персонажа
        public int ShootStateDuration = 100;
        public int RunStatesDuration = 300;
        public bool Shoot = false;
        public int StateFrameCounter = 0;
        public DirectionShooter Direction { get; set; } = DirectionShooter.Right;

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

        public Shooter()
        {
            Health = 2;
            Speed = 5;
        }

        public void RunSprites(double i)
        {
            switch(i)
            {
                case 1:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Run_00.png"));
                    break;
                case 2:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Run_01.png"));
                    break;
                case 3:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Run_02.png"));
                    break;
                case 4:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Run_03.png"));
                    break;
                case 5:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Run_04.png"));
                    break;
                case 6:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Run_05.png"));
                    break;
                case 7:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Run_06.png"));
                    break;
                case 8:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Run_07.png"));
                    break;
                case 9:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Run_08.png"));
                    break;
                case 10:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Run_09.png"));
                    break;
            }
        }

        public void AttackSprites(double x)
        {
            switch(x)
            {
                case 1:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Attack_00.png"));
                    break;
                case 2:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Attack_01.png"));
                    break;
                case 3:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Attack_02.png"));
                    break;
                case 4:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Attack_03.png"));
                    break;
                case 5:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Attack_04.png"));
                    break;
                case 6:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Attack_05.png"));
                    break;
                case 7:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Attack_06.png"));
                    break;
                case 8:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Attack_07.png"));
                    break;
                case 9:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Attack_08.png"));
                    break;
                case 10:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Attack_09.png"));
                    break;
            }
        }

        public void HurtSprites(double y)
        {
            switch(y)
            {
                case 1:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Hurt_00.png"));
                    break;
                case 2:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Hurt_01.png"));
                    break;
                case 3:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Hurt_02.png"));
                    break;
                case 4:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Hurt_03.png"));
                    break;
                case 5:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Hurt_04.png"));
                    break;
                case 6:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Hurt_05.png"));
                    break;
                case 7:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Hurt_06.png"));
                    break;
                case 8:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Hurt_07.png"));
                    break;
                case 9:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Hurt_08.png"));
                    break;
                case 10:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Hurt_09.png"));
                    break;
            }
        }

        public void DieSprites(double j)
        {
            switch(j)
            {
                case 1:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Die_00.png"));
                    break;
                case 2:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Die_01.png"));
                    break;
                case 3:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Die_02.png"));
                    break;
                case 4:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Die_03.png"));
                    break;
                case 5:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Die_04.png"));
                    break;
                case 6:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Die_05.png"));
                    break;
                case 7:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Die_06.png"));
                    break;
                case 8:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Die_07.png"));
                    break;
                case 9:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Die_08.png"));
                    break;
                case 10:
                    shooterSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesShooter/solderSh_Die_09.png"));
                    break;
            }
        }


    }
}
