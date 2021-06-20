using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game_Treasure_Hunter
{
    public class Enemy:ICharacterAnimationSprites
    {
        int health;
        int speed;
        public ImageBrush enemySprite = new ImageBrush();

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

        public Enemy()
        {
            Health = 2;
            Speed = 5;
        }

        public void RunSprites(double i)
        {
            switch(i)
            {
                case 1:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Walk_00.png"));
                    break;
                case 2:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Walk_01.png"));
                    break;
                case 3:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Walk_02.png"));
                    break;
                case 4:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Walk_03.png"));
                    break;
                case 5:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Walk_04.png"));
                    break;
                case 6:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Walk_05.png"));
                    break;
                case 7:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Walk_06.png"));
                    break;
                case 8:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Walk_07.png"));
                    break;
            }
        }

        public void AttackSprites(double x)
        {
            switch(x)
            {
                case 1:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Attack_00.png"));
                    break;
                case 2:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Attack_01.png"));
                    break;
                case 3:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Attack_02.png"));
                    break;
                case 4:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Attack_03.png"));
                    break;
                case 5:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Attack_04.png"));
                    break;
                case 6:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Attack_05.png"));
                    break;
                case 7:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Attack_06.png"));
                    break;
                case 8:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Attack_07.png"));
                    break;
            }
        }

        public void HurtSprites(double y)
        {
            switch(y)
            {
                case 1:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Hurt_00.png"));
                    break;
                case 2:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Hurt_01.png"));
                    break;
                case 3:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Hurt_02.png"));
                    break;
                case 4:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Hurt_03.png"));
                    break;
                case 5:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Hurt_04.png"));
                    break;
                case 6:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Hurt_05.png"));
                    break;
                case 7:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Hurt_06.png"));
                    break;
                case 8:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Hurt_07.png"));
                    break;
            }
        }

        public void DieSprites(double j)
        {
            switch(j)
            {
                case 1:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Die_00.png"));
                    break;
                case 2:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Die_01.png"));
                    break;
                case 3:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Die_02.png"));
                    break;
                case 4:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Die_03.png"));
                    break;
                case 5:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Die_04.png"));
                    break;
                case 6:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Die_05.png"));
                    break;
                case 7:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Die_06.png"));
                    break;
                case 8:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Die_07.png"));
                    break;
            }
        }

        public void IdleEnSprites(double k)
        {
            switch(k)
            {
                case 1:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_00.png"));
                    break;
                case 2:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_01.png"));
                    break;
                case 3:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_02.png"));
                    break;
                case 4:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_03.png"));
                    break;
                case 5:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_04.png"));
                    break;
                case 6:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_05.png"));
                    break;
                case 7:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_06.png"));
                    break;
                case 8:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_07.png"));
                    break;
                case 9:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_08.png"));
                    break;
                case 10:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_09.png"));
                    break;
                case 11:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_10.png"));
                    break;
                case 12:
                    enemySprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesEnemy/enemy_Idle_11.png"));
                    break;
            }
        }
    }
}
