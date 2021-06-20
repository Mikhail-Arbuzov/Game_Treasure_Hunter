using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game_Treasure_Hunter
{
    public class Robot:ICharacterAnimationSprites
    {
        int health;
        int speed;
        public ImageBrush robotSprite = new ImageBrush();

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
        
        public Robot()
        {
            Health = 8;
            Speed = 5;
        }

        public void RunSprites(double i)
        {
            switch(i)
            {
                case 1:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_00.png"));
                    break;
                case 2:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_01.png"));
                    break;
                case 3:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_02.png"));
                    break;
                case 4:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_03.png"));
                    break;
                case 5:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_04.png"));
                    break;
                case 6:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_05.png"));
                    break;
                case 7:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_06.png"));
                    break;
                case 8:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_07.png"));
                    break;
                case 9:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_08.png"));
                    break;
                case 10:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_09.png"));
                    break;
                case 11:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_10.png"));
                    break;
                case 12:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Run_11.png"));
                    break;
            }
                
        }

        public void AttackSprites(double x)
        {
            switch(x)
            {
                case 1:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_00.png"));
                    break;
                case 2:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_01.png"));
                    break;
                case 3:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_02.png"));
                    break;
                case 4:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_03.png"));
                    break;
                case 5:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_04.png"));
                    break;
                case 6:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_05.png"));
                    break;
                case 7:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_06.png"));
                    break;
                case 8:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_07.png"));
                    break;
                case 9:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_08.png"));
                    break;
                case 10:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_09.png"));
                    break;
                case 11:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_10.png"));
                    break;
                case 12:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_11.png"));
                    break;
                case 13:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_12.png"));
                    break;
                case 14:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_13.png"));
                    break;
                case 15:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_14.png"));
                    break;
                case 16:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_15.png"));
                    break;
                case 17:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_16.png"));
                    break;
                case 18:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Attack_17.png"));
                    break;
            }
        }

        public void HurtSprites(double y)
        {
            switch(y)
            {
                case 1:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_00.png"));
                    break;
                case 2:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_01.png"));
                    break;
            }
        }

        public void DieSprites(double j)
        {
            switch(j)
            {
                case 1:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_00.png"));
                    break;
                case 2:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_01.png"));
                    break;
                case 3:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_02.png"));
                    break;
                case 4:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_03.png"));
                    break;
                case 5:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_04.png"));
                    break;
                case 6:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_05.png"));
                    break;
                case 7:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_06.png"));
                    break;
                case 8:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_07.png"));
                    break;
                case 9:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_08.png"));
                    break;
                case 10:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_09.png"));
                    break;
                case 11:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_10.png"));
                    break;
                case 12:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_11.png"));
                    break;
                case 13:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_12.png"));
                    break;
                case 14:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_13.png"));
                    break;
                case 15:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Death_14.png"));
                    break;
            }
        }

        public void LazerSprites(double k)
        {
            switch(k)
            {
                case 1:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_00.png"));
                    break;
                case 2:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_01.png"));
                    break;
                case 3:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_02.png"));
                    break;
                case 4:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_03.png"));
                    break;
                case 5:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_04.png"));
                    break;
                case 6:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_05.png"));
                    break;
                case 7:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_06.png"));
                    break;
                case 8:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_07.png"));
                    break;
                case 9:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_08.png"));
                    break;
                case 10:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_09.png"));
                    break;
                case 11:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_10.png"));
                    break;
                case 12:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_11.png"));
                    break;
                case 13:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_12.png"));
                    break;
                case 14:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_13.png"));
                    break;
                case 15:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_14.png"));
                    break;
                case 16:
                    robotSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesRobot/robot_Shot_15.png"));
                    break;
            }
        }
    }
}
