using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Game_Treasure_Hunter
{
    public class NinjaBoss:ICharacterAnimationSprites
    {
        int health;
        int speed;
        public ImageBrush ninjaBossSprite = new ImageBrush();

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

        public NinjaBoss()
        {
            Health = 8;
            Speed = 5;
        }

        public void RunSprites(double i)
        {
            switch(i)
            {
                case 1:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_00.png"));
                    break;
                case 2:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_01.png"));
                    break;
                case 3:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_02.png"));
                    break;
                case 4:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_03.png"));
                    break;
                case 5:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_04.png"));
                    break;
                case 6:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_05.png"));
                    break;
                case 7:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_06.png"));
                    break;
                case 8:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_07.png"));
                    break;
                case 9:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_08.png"));
                    break;
                case 10:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_009.png"));
                    break;
                case 11:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_10.png"));
                    break;
                case 12:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_11.png"));
                    break;
                case 13:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_12.png"));
                    break;
                case 14:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_13.png"));
                    break;
                case 15:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_14.png"));
                    break;
                case 16:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_run_15.png"));
                    break;
            }
        }

        public void AttackSprites(double x)
        {
            switch(x)
            {
                case 1:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_00.png"));
                    break;
                case 2:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_01.png"));
                    break;
                case 3:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_02.png"));
                    break;
                case 4:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_03.png"));
                    break;
                case 5:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_04.png"));
                    break;
                case 6:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_05.png"));
                    break;
                case 7:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_06.png"));
                    break;
                case 8:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_07.png"));
                    break;
                case 9:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_08.png"));
                    break;
                case 10:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_09.png"));
                    break;
                case 11:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_10.png"));
                    break;
                case 12:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_11.png"));
                    break;
                case 13:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_12.png"));
                    break;
                case 14:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_13.png"));
                    break;
                case 15:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_14.png"));
                    break;
                case 16:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_15.png"));
                    break;
                case 17:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_16.png"));
                    break;
                case 18:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_17.png"));
                    break;
                case 19:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_18.png"));
                    break;
                case 20:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_19.png"));
                    break;
                case 21:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_20.png"));
                    break;
                case 22:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_shoot_21.png"));
                    break;
            }
        }

        public void HurtSprites(double y)
        {
            switch(y)
            {
                case 1:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_hurt_00.png"));
                    break;
                case 2:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_hurt_01.png"));
                    break;
                case 3:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_hurt_02.png"));
                    break;
                case 4:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_hurt_03.png"));
                    break;
                case 5:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_hurt_04.png"));
                    break;
                case 6:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_hurt_05.png"));
                    break;
                case 7:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_hurt_06.png"));
                    break;
                case 8:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_hurt_07.png"));
                    break;
                case 9:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_hurt_08.png"));
                    break;
                case 10:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_hurt_09.png"));
                    break;
            }
        }

        public void DieSprites(double j)
        {
            switch(j)
            {
                case 1:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_00.png"));
                    break;
                case 2:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_01.png"));
                    break;
                case 3:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_02.png"));
                    break;
                case 4:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_03.png"));
                    break;
                case 5:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_04.png"));
                    break;
                case 6:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_05.png"));
                    break;
                case 7:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_06.png"));
                    break;
                case 8:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_07.png"));
                    break;
                case 9:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_08.png"));
                    break;
                case 10:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_09.png"));
                    break;
                case 11:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_10.png"));
                    break;
                case 12:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_11.png"));
                    break;
                case 13:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_12.png"));
                    break;
                case 14:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_13.png"));
                    break;
                case 15:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_death_14.png"));
                    break;
            }
        }

        public void IdleNiSprites(double k)
        {
            switch(k)
            {
                case 1:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_00.png"));
                    break;
                case 2:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_01.png"));
                    break;
                case 3:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_02.png"));
                    break;
                case 4:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_03.png"));
                    break;
                case 5:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_04.png"));
                    break;
                case 6:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_05.png"));
                    break;
                case 7:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_06.png"));
                    break;
                case 8:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_07.png"));
                    break;
                case 9:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_08.png"));
                    break;
                case 10:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_09.png"));
                    break;
                case 11:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_10.png"));
                    break;
                case 12:
                    ninjaBossSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesNinjaBos/bow_idle_11.png"));
                    break;
            }
        }


    }
}
