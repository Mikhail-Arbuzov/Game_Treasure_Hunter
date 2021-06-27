using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game_Treasure_Hunter
{

    public enum Direction
    {
        Left = 1,
        Right = 2
    }

    public class Player:ICharacterAnimationSprites
    {
        int health;
        int speed;
        int jumpSpeed;
        int forceJump;
        public ImageBrush playerSprite = new ImageBrush();
        //для работы  бонуса ускорения игрока 
        public int speedupStateDuration = 2000;
        public int timeActionsSpeedup = 0;
        public bool speedup = false;

        public Direction Direction { get; set; } = Direction.Right;

        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                //if (value >= 0)
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

        public int JumpSpeed
        {
            get
            {
                return jumpSpeed;
            }

            set
            {
                jumpSpeed = value;
            }
        }

        public int ForceJump
        {
            get
            {
                return forceJump;
            }
            set
            {
                forceJump = value;
            }
        }

        public Player()
        {
            Health = 30;
            Speed = 5;
            JumpSpeed = 10;
            //ForceJump = 8;
        }

        public void RunSprites(double i)
        {
            switch(i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Run_00.png"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Run_01.png"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Run_02.png"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Run_03.png"));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Run_04.png"));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Run_05.png"));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Run_06.png"));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Run_07.png"));
                    break;
                case 9:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Run_08.png"));
                    break;
                case 10:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Run_09.png"));
                    break;
            }

        }

        public void AttackSprites(double x)
        {
            switch(x)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Attack_00.png"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Attack_01.png"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Attack_02.png"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Attack_03.png"));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Attack_04.png"));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Attack_05.png"));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Attack_06.png"));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Attack_07.png"));
                    break;
                case 9:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Attack_08.png"));
                    break;
                case 10:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Attack_09.png"));
                    break;
            }
        }

        public void HurtSprites(double y)
        {
            switch(y)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Hurt_00.png"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Hurt_01.png"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Hurt_02.png"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Hurt_03.png"));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Hurt_04.png"));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Hurt_05.png"));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Hurt_06.png"));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Hurt_07.png"));
                    break;
                case 9:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Hurt_08.png"));
                    break;
                case 10:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Hurt_09.png"));
                    break;
            }
        }

        public void DieSprites(double j)
        {
            switch(j)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Die_00.png"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Die_01.png"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Die_02.png"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Die_03.png"));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Die_04.png"));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Die_05.png"));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Die_06.png"));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Die_07.png"));
                    break;
                case 9:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Die_08.png"));
                    break;
                case 10:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Die_09.png"));
                    break;
            }
        }

        public void IdleSprites(double k)
        {
            switch(k)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Idle_00.png"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Idle_01.png"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Idle_02.png"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Idle_03.png"));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Idle_04.png"));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Idle_05.png"));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Idle_06.png"));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Idle_07.png"));
                    break;
                case 9:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Idle_08.png"));
                    break;
                case 10:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Idle_09.png"));
                    break;
            }
        }

        public void JumpSprites()
        {
            playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesPlayer/player_Jump.png"));
        }
    }
}
