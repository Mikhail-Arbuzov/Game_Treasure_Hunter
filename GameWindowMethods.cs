using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game_Treasure_Hunter
{
    public partial class GameWindow : Window
    {
        private void ShowGameOver(string msg)
        {
            gameOver = true;
            gameTimer.Stop();
            healthScore.Content = "Здоровье: " + player.Health + Environment.NewLine + msg + Environment.NewLine + "Нажмите R" + Environment.NewLine + "чтобы начать игру" + Environment.NewLine + "заново";
        }

        private void SnowFalls(double a)
        {
            ImageBrush snowSprite = new ImageBrush();
            switch (a)
            {
                case 1:
                    snowSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSnow/snow_01.png"));
                    break;
                case 2:
                    snowSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSnow/snow_02.png"));
                    break;
                case 3:
                    snowSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesSnow/snow_03.png"));
                    break;
            }
            snow.Fill = snowSprite;
        }

        private void MakeStones()
        {
            ImageBrush stoneSprite = new ImageBrush();
            stoneSpriteCounter = rand.Next(0, 7);
            switch (stoneSpriteCounter)
            {
                case 0:
                    stoneSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesStones/stone_0.png"));
                    break;
                case 1:
                    stoneSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesStones/stone_1.png"));
                    break;
                case 2:
                    stoneSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesStones/stone_2.png"));
                    break;
                case 3:
                    stoneSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesStones/stone_3.png"));
                    break;
                case 4:
                    stoneSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesStones/stone_4.png"));
                    break;
                case 5:
                    stoneSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesStones/stone_5.png"));
                    break;
                case 6:
                    stoneSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesStones/stone_6.png"));
                    break;
                case 7:
                    stoneSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Sprites/SpritesStones/stone_7.png"));
                    break;
            }

            Rectangle newStone = new Rectangle
            {
                Tag = "stone",
                Height = 64,
                Width = 64,
                Fill = stoneSprite
            };
            Canvas.SetTop(newStone, -50);
            Canvas.SetLeft(newStone, rand.Next(1228, 1280));
            if(gem < 3)
            {
                MyCanvas.Children.Add(newStone);
            }
        }

        private void EnemyBulletMaker()
        {
            Rectangle enemyBullet = new Rectangle
            {
                Tag = "enemyBullet",
                Height = 10,
                Width = 20,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Rectangle enemyBullet2 = new Rectangle
            {
                Tag = "enemyBullet2",
                Height = 10,
                Width = 20,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            Rectangle enemyBullet3 = new Rectangle
            {
                Tag = "enemyBullet3",
                Height = 5,
                Width = 10,
                Fill = Brushes.Yellow,
                Stroke = Brushes.White,
                StrokeThickness = 2
            };
            // положение откуда стреляют
            if (shooterOne.Direction == DirectionShooter.Right)
            {
                Canvas.SetLeft(enemyBullet, Canvas.GetLeft(shooter1) + shooter1.Height);
                Canvas.SetTop(enemyBullet, Canvas.GetTop(shooter1) + shooter1.Height / 2 + 24);
            }

            if (shooterOne.Direction == DirectionShooter.Left)
            {
                Canvas.SetLeft(enemyBullet, Canvas.GetLeft(shooter1) - 10);
                Canvas.SetTop(enemyBullet, Canvas.GetTop(shooter1) + shooter1.Height / 2 + 24);
            }

            if (shooterTwo.Direction == DirectionShooter.Right)
            {
                Canvas.SetLeft(enemyBullet2, Canvas.GetLeft(shooter2) + shooter2.Height);
                Canvas.SetTop(enemyBullet2, Canvas.GetTop(shooter2) + shooter2.Height / 2 + 24);
            }

            if(shooterTwo.Direction == DirectionShooter.Left)
            {
                Canvas.SetLeft(enemyBullet2, Canvas.GetLeft(shooter2) - 10);
                Canvas.SetTop(enemyBullet2, Canvas.GetTop(shooter2) + shooter2.Height / 2 + 24);
            }
           
            if(terrorist.Direction == DirectionTerrorist.Right)
            {
                Canvas.SetLeft(enemyBullet3, Canvas.GetLeft(terror) + terror.Height);
                Canvas.SetTop(enemyBullet3, Canvas.GetTop(terror) + terror.Height / 2 - 10);
            }

            if (terrorist.Direction == DirectionTerrorist.Left)
            {
                Canvas.SetLeft(enemyBullet3, Canvas.GetLeft(terror));
                Canvas.SetTop(enemyBullet3, Canvas.GetTop(terror) + terror.Height / 2 - 10);
            }

            if(shooterOne.Shoot && shooterOne.Health > 0 && gem < 3)
            {
                MyCanvas.Children.Add(enemyBullet);
            }
            //1-й способ
            //if (Canvas.GetTop(hero) < 260 && Canvas.GetTop(hero) > 145 && shooterOne.Health > 0)
            //{
            //    MyCanvas.Children.Add(enemyBullet);
            //}
            if (shooterTwo.Shoot && shooterTwo.Health > 0 && gem < 3)
            {
                MyCanvas.Children.Add(enemyBullet2);
            }

            if(terrorist.Shoot && terrorist.Health > 0 && gem < 3)
            {
                MyCanvas.Children.Add(enemyBullet3);
            }

            //направление полета пули
            //if (Canvas.GetLeft(shooter1) < 407)
            //{
            //    speedEnemyBullet++;
            //    Canvas.SetLeft(enemyBullet, Canvas.GetLeft(enemyBullet) - speedEnemyBullet); 
            //}
            //if (Canvas.GetLeft(shooter1) > 151)
            //{
            //    //MyCanvas.Children.Add(enemyBullet);
            //    speedEnemyBullet++;
            //    Canvas.SetLeft(enemyBullet, Canvas.GetLeft(enemyBullet) + speedEnemyBullet);
            //}
            //if (Canvas.GetLeft(shooter2) < 1878 || Canvas.GetLeft(shooter2) == 1858)
            //{
            //    speedEnemyBullet++;
            //    Canvas.SetLeft(enemyBullet2, Canvas.GetLeft(enemyBullet2) - speedEnemyBullet);
            //}
            //if (Canvas.GetLeft(shooter2) > 1836 || Canvas.GetLeft(shooter2) == 1856)
            //{
            //    //MyCanvas.Children.Add(enemyBullet2);
            //    speedEnemyBullet++;
            //    Canvas.SetLeft(enemyBullet2, Canvas.GetLeft(enemyBullet2) + speedEnemyBullet);
            //}

            //if (Canvas.GetLeft(terror) < 2214 || Canvas.GetLeft(terror) == 2194)
            //{

            //    speedEnemyBullet++;
            //    Canvas.SetLeft(enemyBullet3, Canvas.GetLeft(enemyBullet3) - speedEnemyBullet);
            //}
            //if (Canvas.GetLeft(terror) > 2174 || Canvas.GetLeft(terror) == 2194)
            //{
            //    //MyCanvas.Children.Add(enemyBullet3);
            //    speedEnemyBullet++;
            //    Canvas.SetLeft(enemyBullet3, Canvas.GetLeft(enemyBullet3) + speedEnemyBullet);
            //}
        }

        private void GunFireMaker()
        {
            ImageBrush lazerImage = new ImageBrush();
            lazerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/lazer.png"));
            ImageBrush lazerImage2 = new ImageBrush();
            lazerImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/m_lazerlef.png"));

            ImageBrush kunaiImage = new ImageBrush();
            kunaiImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/right_Kunai.png"));
            ImageBrush kunaiImage2 = new ImageBrush();
            kunaiImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/left_Kunai.png"));

            ImageBrush arrowImage = new ImageBrush();
            arrowImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/arrow.png"));
            ImageBrush arrowImage2 = new ImageBrush();
            arrowImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/arrowleft.png"));

            Rectangle newLazer = new Rectangle
            {
                Tag = "lazer",
                Height = 20,
                Width = 90,
                Fill = lazerImage
            };
            Rectangle newKunai = new Rectangle
            {
                Tag = "kunai",
                Height = 20,
                Width = 48,
                Fill = kunaiImage
            };
            Rectangle newArrow = new Rectangle
            {
                Tag = "arrow",
                Height = 10,
                Width = 57,
                Fill = arrowImage
            };

            if(robot.Direction == DirectionRobot.Right)
            {
                Canvas.SetLeft(newLazer, Canvas.GetLeft(robo) + robo.Height);
                Canvas.SetTop(newLazer, Canvas.GetTop(robo) + robo.Height / 2 - 30);
            }

            if (robot.Direction == DirectionRobot.Left)
            {
                newLazer.Fill = lazerImage2;
                Canvas.SetLeft(newLazer, Canvas.GetLeft(robo));
                Canvas.SetTop(newLazer, Canvas.GetTop(robo) + robo.Height / 2 - 30);
            }

            if (ninjaTwo.Direction == DirectionNinja.Right)
            {
                Canvas.SetLeft(newKunai, (Canvas.GetLeft(ninja2) + ninja2.Height)+10);
                Canvas.SetTop(newKunai, Canvas.GetTop(ninja2) + ninja2.Height / 2 + 6);
            }

            if (ninjaTwo.Direction == DirectionNinja.Left)
            {
                newKunai.Fill = kunaiImage2;
                Canvas.SetLeft(newKunai, Canvas.GetLeft(ninja2));
                Canvas.SetTop(newKunai, Canvas.GetTop(ninja2) + ninja2.Height / 2 + 6);
            }

            if(ninjaBoss.Direction == DirectionNinjaBoss.Right)
            {
                Canvas.SetLeft(newArrow, Canvas.GetLeft(ninjaBow) + ninjaBow.Height);
                Canvas.SetTop(newArrow, Canvas.GetTop(ninjaBow) + ninjaBow.Height/2 + 2);
            }

            if (ninjaBoss.Direction == DirectionNinjaBoss.Left)
            {
                newArrow.Fill = arrowImage2;
                Canvas.SetLeft(newArrow, Canvas.GetLeft(ninjaBow));
                Canvas.SetTop(newArrow, Canvas.GetTop(ninjaBow) + ninjaBow.Height/2 + 2);
            }


            if (robot.Shoot && robot.Health > 0)
            {
                MyCanvas.Children.Add(newLazer);
            }

            if (ninjaTwo.Throw && ninjaTwo.Health > 0)
            {
                MyCanvas.Children.Add(newKunai);
            }

            if(ninjaBoss.Shoot && ninjaBoss.Health > 0)
            {
                MyCanvas.Children.Add(newArrow);
            }

            //if (ninjaBossSpriteIndex == 17)
            //{
            //    MyCanvas.Children.Add(newArrow);
            //}

            //if (Canvas.GetLeft(robo) < 2758 || Canvas.GetLeft(robo) == 2738)
            //{

            //    newLazer.Fill = lazerImage2;
            //    speedEnemyBullet++;
            //    Canvas.SetLeft(newLazer, Canvas.GetLeft(newLazer) - speedEnemyBullet);
            //}
            //if (Canvas.GetLeft(robo) > 2718 || Canvas.GetLeft(robo) == 2738)
            //{
            //    speedEnemyBullet++;
            //    Canvas.SetLeft(newLazer, Canvas.GetLeft(newLazer) + speedEnemyBullet);
            //}
            //if (Canvas.GetLeft(ninja2) < 4583 || Canvas.GetLeft(ninja2) == 4563)
            //{
            //    newKunai.Fill = kunaiImage2;
            //    speedEnemyBullet++;
            //    Canvas.SetLeft(newKunai, Canvas.GetLeft(newKunai) - speedEnemyBullet);
            //}
            //if (Canvas.GetLeft(ninja2) > 4543 || Canvas.GetLeft(ninja2) == 4563)
            //{
            //    speedEnemyBullet++;
            //    Canvas.SetLeft(newKunai, Canvas.GetLeft(newKunai) + speedEnemyBullet);
            //}

            //if (Canvas.GetLeft(ninjaBow) < 3839 || Canvas.GetLeft(ninjaBow) == 3819)
            //{
            //    newArrow.Fill = arrowImage2;
            //    speedEnemyBullet++;
            //    Canvas.SetLeft(newArrow, Canvas.GetLeft(newArrow) - speedEnemyBullet);
            //}
            //if (Canvas.GetLeft(ninjaBow) > 3799 || Canvas.GetLeft(ninjaBow) == 3819)
            //{
            //    speedEnemyBullet++;
            //    Canvas.SetLeft(newArrow, Canvas.GetLeft(newArrow) + speedEnemyBullet);
            //}
        }
    }
}
