using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game_Treasure_Hunter
{
    public enum DirectionTerrorist
    {
        Left = 1,
        Right = 2
    }
    public class Terrorist:ICharacterAnimationSprites
    {
        int health;
        int speed;
        public ImageBrush terroristSprite = new ImageBrush();

        //продолжительность того или иного действия персонажа
        public int ShootStateDuration = 100;
        public int RunStatesDuration = 400;
        public bool Shoot = false;
        public int StateFrameCounter = 0;
        public DirectionTerrorist Direction { get; set; } = DirectionTerrorist.Right;

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

        public Terrorist()
        {
            Health = 2;
            Speed = 5;
        }

        public void RunSprites(double i)
        {
            switch(i)
            {
                case 1:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Walk_00.png"));
                    break;
                case 2:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Walk_01.png"));
                    break;
                case 3:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Walk_02.png"));
                    break;
                case 4:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Walk_03.png"));
                    break;
                case 5:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Walk_04.png"));
                    break;
                case 6:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Walk_05.png"));
                    break;
                case 7:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Walk_06.png"));
                    break;
                case 8:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Walk_07.png"));
                    break;
            }
        }

        public void AttackSprites(double x)
        {
            switch(x)
            {
                case 1:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Attack1_00.png"));
                    break;
                case 2:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Attack1_01.png"));
                    break;
                case 3:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Attack1_02.png"));
                    break;
                case 4:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Attack1_03.png"));
                    break;
                case 5:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Attack1_04.png"));
                    break;
                case 6:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Attack1_05.png"));
                    break;
            }
        }
         
        public void HurtSprites(double y)
        {
            switch(y)
            {
                case 1:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_00.png"));
                    break;
                case 2:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_01.png"));
                    break;
                case 3:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_02.png"));
                    break;
                case 4:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_03.png"));
                    break;
                case 5:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_04.png"));
                    break;
                case 6:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_05.png"));
                    break;

            }
        }

        public void DieSprites(double j)
        {
            switch(j)
            {
                case 1:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_00.png"));
                    break;
                case 2:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_01.png"));
                    break;
                case 3:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_02.png"));
                    break;
                case 4:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_03.png"));
                    break;
                case 5:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_04.png"));
                    break;
                case 6:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_05.png"));
                    break;
                case 7:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_06.png"));
                    break;
                case 8:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_07.png"));
                    break;
                case 9:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Hurt_08.png"));
                    break;
            }
        }

        public void ShootSprites(double k)
        {
            switch(k)
            {
                case 1:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Attack_00.png"));
                    break;
                case 2:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Attack_01.png"));
                    break;
                case 3:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Attack_02.png"));
                    break;
                case 4:
                    terroristSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesTerrorist/terrorist_Attack_03.png"));
                    break;
            }
        }
    }
}
