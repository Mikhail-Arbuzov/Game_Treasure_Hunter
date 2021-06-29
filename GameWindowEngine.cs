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
        private void GameEngine(object sender, EventArgs e)
        {
            healthScore.Content = "Здоровье: " + player.Health;
            coinScore.Content = "Монеты: " + score;
            catridgesScore.Content = "Патроны: " + bulletsScore;
            treasuresScore.Content = "Сокровища: " + gem;

            //чтоб игрок не выходил за экран
            if (backgroundLevel.Focusable == false && backgroundLevel3.Focusable == false && backgroundLevel2.Focusable == false)
            {
                if (Canvas.GetLeft(hero) >= 1460)
                {
                    Canvas.SetLeft(hero, Canvas.GetLeft(hero) - 10);
                    //goRight = false;
                }
            }

            if (backgroundLevel2.Focusable == true)
            {
                if (Canvas.GetLeft(hero) <= 1610)
                {
                    Canvas.SetLeft(hero, Canvas.GetLeft(hero) + 10);
                    //goLeft = false;
                }
            }
            if (backgroundLevel3.Focusable == false)
            {
                if (Canvas.GetLeft(hero) >= 3060)
                {
                    Canvas.SetLeft(hero, Canvas.GetLeft(hero) - 10);
                }
            }


            if (backgroundLevel3.Focusable == true)
            {
                if (Canvas.GetLeft(hero) <= 3210)
                {
                    Canvas.SetLeft(hero, Canvas.GetLeft(hero) + 10);
                }
            }



            if (goLeft == true && Canvas.GetLeft(hero) > 0)
            {
                Canvas.SetLeft(hero, Canvas.GetLeft(hero) - player.Speed);
            }

            if (goRight == true && Canvas.GetLeft(hero) + (hero.Width + 10) <= this.MyCanvas.ActualWidth)
            {
                Canvas.SetLeft(hero, Canvas.GetLeft(hero) + player.Speed);
            }



            //движение прыжка
            Canvas.SetTop(hero, Canvas.GetTop(hero) + player.JumpSpeed);

            if (player.ForceJump < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                player.JumpSpeed = -8;
                player.ForceJump -= 1;
            }
            else
            {
                player.JumpSpeed = 10;
            }

            //if (Canvas.GetTop(hero) + (hero.Height * 2) > this.MyCanvas.ActualHeight)
            //{
            //    Canvas.SetTop(hero, -80);
            //}
            // движение объектов игры
            MotionLevelOne();
            MotionLevelTwo();
            MotionLevelThree();

            // действие пулей врагов
            bulletTimer -= 3;
            if (bulletTimer < 0)
            {
                EnemyBulletMaker();
                GunFireMaker();
                bulletTimer = bulletTimerLimit;
            }
            //взаимодействие врагов с игроком 
            playerHitBox = new Rect(Canvas.GetLeft(hero), Canvas.GetTop(hero), hero.Width, hero.Height);

            foreach (var foe in MyCanvas.Children.OfType<Rectangle>())
            {
                if (foe.Name.ToString() == "enemy1" || foe.Name.ToString() == "enemy2")
                {
                    Rect enHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if (enHitBox.IntersectsWith(playerHitBox))
                    {
                        enemySpriteIndex += 0.5;
                        if (enemySpriteIndex > 8)
                        {
                            enemySpriteIndex = 1;
                        }
                        enemy.AttackSprites(enemySpriteIndex);
                        enemy1.Fill = enemy.enemySprite;
                        enemyTwo.AttackSprites(enemySpriteIndex);
                        enemy2.Fill = enemyTwo.enemySprite;
                    }
                    if (playerHitBox.IntersectsWith(enHitBox))
                    {
                        player.Health -= 1;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }

                }

                if (foe.Name.ToString() == "shooter1" || foe.Name.ToString() == "shooter2")
                {
                    Rect shHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if (shHitBox.IntersectsWith(playerHitBox))
                    {
                        shooterSpriteIndex += 0.5;
                        if (shooterSpriteIndex > 10)
                        {
                            shooterSpriteIndex = 1;
                        }
                        shooterOne.AttackSprites(shooterSpriteIndex);
                        shooter1.Fill = shooterOne.shooterSprite;
                        shooterTwo.AttackSprites(shooterSpriteIndex);
                        shooter2.Fill = shooterTwo.shooterSprite;
                    }
                    if (playerHitBox.IntersectsWith(shHitBox))
                    {
                        player.Health -= 1;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (foe.Name.ToString() == "troll")
                {
                    Rect trHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if (trHitBox.IntersectsWith(playerHitBox))
                    {
                        trollSpriteIndex += 0.2;
                        if (trollSpriteIndex > 10)
                        {
                            trollSpriteIndex = 1;
                        }
                        trollOne.AttackSprites(trollSpriteIndex);
                        troll.Fill = trollOne.trollSprite;
                    }
                    if (playerHitBox.IntersectsWith(trHitBox))
                    {
                        player.Health -= 3;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (foe.Name.ToString() == "terror")
                {
                    Rect terHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if (terHitBox.IntersectsWith(playerHitBox))
                    {
                        terroristSpriteIndex += 0.5;
                        if (terroristSpriteIndex > 6)
                        {
                            terroristSpriteIndex = 1;
                        }
                        terrorist.AttackSprites(terroristSpriteIndex);
                        terror.Fill = terrorist.terroristSprite;
                    }
                    if (playerHitBox.IntersectsWith(terHitBox))
                    {
                        player.Health -= 2;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (foe.Name.ToString() == "soldier")
                {
                    Rect soHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if (soHitBox.IntersectsWith(playerHitBox))
                    {
                        soldierSpriteIndex += 0.5;
                        if (soldierSpriteIndex > 6)
                        {
                            soldierSpriteIndex = 1;
                        }
                        soldierOne.AttackSprites(soldierSpriteIndex);
                        soldier.Fill = soldierOne.soldierSprite;
                    }
                    if (playerHitBox.IntersectsWith(soHitBox))
                    {
                        player.Health -= 1;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (foe.Name.ToString() == "robo")
                {
                    Rect robHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if (robHitBox.IntersectsWith(playerHitBox))
                    {
                        robotSpriteIndex += 0.2;
                        if (robotSpriteIndex > 18)
                        {
                            robotSpriteIndex = 1;
                        }
                        robot.AttackSprites(robotSpriteIndex);
                        robo.Fill = robot.robotSprite;
                    }
                    if (playerHitBox.IntersectsWith(robHitBox))
                    {
                        player.Health -= 3;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (foe.Name.ToString() == "ninja1")
                {
                    Rect ninjHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if (ninjHitBox.IntersectsWith(playerHitBox))
                    {
                        ninjaSpriteIndex += 0.5;
                        if (ninjaSpriteIndex > 10)
                        {
                            ninjaSpriteIndex = 1;
                        }
                        ninja.AttackSprites(ninjaSpriteIndex);
                        ninja1.Fill = ninja.ninjaSprite;
                    }
                    if (playerHitBox.IntersectsWith(ninjHitBox))
                    {
                        player.Health -= 1;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (foe.Name.ToString() == "ninja2")
                {
                    Rect niHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if (niHitBox.IntersectsWith(playerHitBox))
                    {
                        ninjaTwoSpriteIndex += 0.5;
                        if (ninjaTwoSpriteIndex > 10)
                        {
                            ninjaTwoSpriteIndex = 1;
                        }
                        ninjaTwo.ThrowSprites(ninjaTwoSpriteIndex);
                        ninja2.Fill = ninjaTwo.ninjaSprite;
                    }
                    if (playerHitBox.IntersectsWith(niHitBox))
                    {
                        player.Health -= 2;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (foe.Name.ToString() == "ninjaBow")
                {
                    Rect nbHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if (nbHitBox.IntersectsWith(playerHitBox))
                    {
                        ninjaBossSpriteIndex += 0.5;
                        if (ninjaBossSpriteIndex > 22)
                        {
                            ninjaBossSpriteIndex = 1;
                        }
                        ninjaBoss.AttackSprites(ninjaBossSpriteIndex);
                        ninjaBow.Fill = ninjaBoss.ninjaBossSprite;
                    }
                    if (playerHitBox.IntersectsWith(nbHitBox))
                    {
                        player.Health -= 3;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }
            }

            foreach (var animal in MyCanvas.Children.OfType<Image>())
            {
                if (animal is Image && (string)animal.Tag == "wolf")
                {
                    Rect wolfHitBox2 = new Rect(Canvas.GetLeft(animal), Canvas.GetTop(animal), animal.Width, animal.Height);

                    if (wolfHitBox2.IntersectsWith(playerHitBox))
                    {
                        if (Canvas.GetLeft(animal) < Canvas.GetLeft(ground4))
                        {
                            wolf.AttackLeft();
                        }
                        if (Canvas.GetLeft(animal) + animal.Width > Canvas.GetLeft(ground4) + ground4.Width)
                        {
                            wolf.AttackRight();
                        }
                    }
                    if (playerHitBox.IntersectsWith(wolfHitBox2))
                    {
                        player.Health -= 2;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (animal is Image && (string)animal.Tag == "bear")
                {
                    Rect bearHitBox2 = new Rect(Canvas.GetLeft(animal), Canvas.GetTop(animal), animal.Width, animal.Height);

                    if (bearHitBox2.IntersectsWith(playerHitBox))
                    {

                        bear.AttackLeft();
                        //if (Canvas.GetLeft(animal) < Canvas.GetLeft(ground13))
                        //{
                        //    bear.AttackRight();
                        //}
                        //if (Canvas.GetLeft(animal) + animal.Width > Canvas.GetLeft(ground13) + ground13.Width)
                        //{
                           
                        //}
                    }
                    if (playerHitBox.IntersectsWith(bearHitBox2))
                    {
                        player.Health -= 2;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (animal is Image && (string)animal.Tag == "snake")
                {
                    Rect snakeHitBox2 = new Rect(Canvas.GetLeft(animal), Canvas.GetTop(animal), animal.Width, animal.Height);

                    if (snakeHitBox2.IntersectsWith(playerHitBox))
                    {
                        if (Canvas.GetLeft(animal) > 3097)
                        {
                            snake.AttackLeft();
                        }
                        if (Canvas.GetLeft(animal) < 2671)
                        {
                            snake.AttackRight();
                        }
                    }
                    if (playerHitBox.IntersectsWith(snakeHitBox2))
                    {
                        player.Health -= 2;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (animal is Image && (string)animal.Tag == "hog")
                {
                    Rect hogHitBox2 = new Rect(Canvas.GetLeft(animal), Canvas.GetTop(animal), animal.Width, animal.Height);

                    if (playerHitBox.IntersectsWith(hogHitBox2))
                    {
                        player.Health -= 2;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }


                if (animal is Image && (string)animal.Tag == "bird")
                {
                    Rect birdHitBox2 = new Rect(Canvas.GetLeft(animal), Canvas.GetTop(animal), animal.Width, animal.Height);

                    if (playerHitBox.IntersectsWith(birdHitBox2))
                    {
                        player.Health -= 1;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }
            }
            //стрельба по врагам и объектам
            foreach (var y in MyCanvas.Children.OfType<Rectangle>())
            {
                if (y is Rectangle && (string)y.Tag == "bullet")
                {
                    //Взависимости от направления игрока меняется и направление полета  пули 
                    if (Canvas.GetLeft(y) < Canvas.GetLeft(hero) + hero.Height)
                    {
                        Canvas.SetLeft(y, Canvas.GetLeft(y) - speedBullet);
                    }

                    if (Canvas.GetLeft(y) > Canvas.GetLeft(hero)-20)
                    {
                        Canvas.SetLeft(y, Canvas.GetLeft(y) + speedBullet);
                    }

                    // для исчезновения пули
                    Rect bulletHitBox = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                    if (backgroundLevel.Focusable == false && backgroundLevel3.Focusable == false && backgroundLevel2.Focusable == false)
                    {
                        if (Canvas.GetLeft(y) > 1430 || Canvas.GetLeft(y) < 30)
                        {
                            itemRemover.Add(y);
                        }
                    }


                    if (backgroundLevel2.Focusable == true)
                    {
                        if (Canvas.GetLeft(y) > 3000 || Canvas.GetLeft(y) < 1615)
                        {
                            itemRemover.Add(y);
                        }
                    }


                    if (backgroundLevel3.Focusable == true)
                    {
                        if (Canvas.GetLeft(y) > 4660 || Canvas.GetLeft(y) < 3215)
                        {
                            itemRemover.Add(y);
                        }
                    }

                    //if (playerHitBox.IntersectsWith(bulletHitBox))
                    //{
                    //    itemRemover.Add(y);
                    //}

                        foreach (var z in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (z is Rectangle && (string)z.Tag == "stone")
                        {
                            Rect stoneHitBox = new Rect(Canvas.GetLeft(z), Canvas.GetTop(z), z.Width, z.Height);
                            if (bulletHitBox.IntersectsWith(stoneHitBox))
                            {
                                itemRemover.Add(y);
                                itemRemover.Add(z);
                            }
                        }
                    }

                    foreach (var wf in MyCanvas.Children.OfType<Image>())
                    {
                        if (wf is Image && (string)wf.Tag == "wolf")
                        {
                            Rect wolfHitBox = new Rect(Canvas.GetLeft(wf), Canvas.GetTop(wf), wf.Width, wf.Height);
                            if (bulletHitBox.IntersectsWith(wolfHitBox))
                            {
                                itemRemover.Add(y);

                                wolf.Health -= 1;
                                if (wolf.Health <= 0)
                                {
                                    itemToRemover.Add(wf);
                                }
                            }
                            //if (wolfHitBox.IntersectsWith(playerHitBox))
                            //{
                            //    if (Canvas.GetLeft(wf) < Canvas.GetLeft(ground4))
                            //    {
                            //        wolf.AttackLeft();
                            //    }
                            //    if (Canvas.GetLeft(wf) + wf.Width > Canvas.GetLeft(ground4) + ground4.Width)
                            //    {
                            //        wolf.AttackRight();
                            //    }
                            //}
                            //if (playerHitBox.IntersectsWith(wolfHitBox))
                            //{
                            //    player.Health -= 2;
                            //    playerSpriteIndex += 0.5;
                            //    if (playerSpriteIndex > 10)
                            //    {
                            //        playerSpriteIndex = 1;
                            //    }
                            //    player.HurtSprites(playerSpriteIndex);
                            //    hero.Fill = player.playerSprite;
                            //}
                        }
                    }

                    foreach (var be in MyCanvas.Children.OfType<Image>())
                    {
                        if (be is Image && (string)be.Tag == "bear")
                        {
                            Rect bearHitBox = new Rect(Canvas.GetLeft(be), Canvas.GetTop(be), be.Width, be.Height);
                            if (bulletHitBox.IntersectsWith(bearHitBox))
                            {
                                itemRemover.Add(y);

                                bear.Health -= 3;
                                if (bear.Health <= 0)
                                {
                                    itemToRemover.Add(be);
                                }
                            }
                            //if (bearHitBox.IntersectsWith(playerHitBox))
                            //{
                            //    if (Canvas.GetLeft(be) < Canvas.GetLeft(ground13))
                            //    {
                            //        bear.AttackLeft();
                            //    }
                            //    if (Canvas.GetLeft(be) + be.Width > Canvas.GetLeft(ground13) + ground13.Width)
                            //    {
                            //        bear.AttackRight();
                            //    }
                            //}
                            //if (playerHitBox.IntersectsWith(bearHitBox))
                            //{
                            //    player.Health -= 2;
                            //    playerSpriteIndex += 0.5;
                            //    if (playerSpriteIndex > 10)
                            //    {
                            //        playerSpriteIndex = 1;
                            //    }
                            //    player.HurtSprites(playerSpriteIndex);
                            //    hero.Fill = player.playerSprite;
                            //}
                        }
                    }

                    foreach (var r in MyCanvas.Children.OfType<Image>())
                    {
                        if (r is Image && (string)r.Tag == "snake")
                        {
                            Rect snakeHitBox = new Rect(Canvas.GetLeft(r), Canvas.GetTop(r), r.Width, r.Height);
                            if (bulletHitBox.IntersectsWith(snakeHitBox))
                            {
                                itemRemover.Add(y);
                                snake.Health -= 1;
                                if (snake.Health <= 0)
                                {
                                    itemToRemover.Add(r);
                                }
                            }
                            //if (snakeHitBox.IntersectsWith(playerHitBox))
                            //{
                            //    if (Canvas.GetLeft(r) < 3097)
                            //    {
                            //        snake.AttackLeft();
                            //    }
                            //    if (Canvas.GetLeft(r) > 2671)
                            //    {
                            //        snake.AttackRight();
                            //    }
                            //}
                            //if (playerHitBox.IntersectsWith(snakeHitBox))
                            //{
                            //    player.Health -= 2;
                            //    playerSpriteIndex += 0.5;
                            //    if (playerSpriteIndex > 10)
                            //    {
                            //        playerSpriteIndex = 1;
                            //    }
                            //    player.HurtSprites(playerSpriteIndex);
                            //    hero.Fill = player.playerSprite;
                            //}
                        }
                    }

                    foreach (var k in MyCanvas.Children.OfType<Image>())
                    {
                        if (k is Image && (string)k.Tag == "hog")
                        {
                            Rect hogHitBox = new Rect(Canvas.GetLeft(k), Canvas.GetTop(k), k.Width, k.Height);
                            if (bulletHitBox.IntersectsWith(hogHitBox))
                            {
                                itemRemover.Add(y);
                                hog.Health -= 1;
                                if (hog.Health <= 0)
                                {
                                    itemToRemover.Add(k);
                                }
                            }
                            //if (playerHitBox.IntersectsWith(hogHitBox))
                            //{
                            //    player.Health -= 2;
                            //    playerSpriteIndex += 0.5;
                            //    if (playerSpriteIndex > 10)
                            //    {
                            //        playerSpriteIndex = 1;
                            //    }
                            //    player.HurtSprites(playerSpriteIndex);
                            //    hero.Fill = player.playerSprite;
                            //}
                        }
                    }

                    foreach (var p in MyCanvas.Children.OfType<Image>())
                    {
                        if (p is Image && (string)p.Tag == "bird")
                        {
                            Rect birdHitBox = new Rect(Canvas.GetLeft(p), Canvas.GetTop(p), p.Width, p.Height);
                            if (bulletHitBox.IntersectsWith(birdHitBox))
                            {
                                itemRemover.Add(y);
                                bird.Health -= 1;
                                if (bird.Health <= 0)
                                {
                                    itemToRemover.Add(p);
                                }
                            }
                            //if (playerHitBox.IntersectsWith(birdHitBox))
                            //{
                            //    player.Health -= 1;
                            //    playerSpriteIndex += 0.5;
                            //    if (playerSpriteIndex > 10)
                            //    {
                            //        playerSpriteIndex = 1;
                            //    }
                            //    player.HurtSprites(playerSpriteIndex);
                            //    hero.Fill = player.playerSprite;
                            //}
                        }
                    }

                    foreach (var q in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (q.Name.ToString() == "enemy1")
                        {
                            Rect enemyHitBox = new Rect(Canvas.GetLeft(q), Canvas.GetTop(q), q.Width, q.Height);
                            if (bulletHitBox.IntersectsWith(enemyHitBox))
                            {
                                itemRemover.Add(y);
                                enemy.Health -= 1;
                                enemySpriteIndex += 0.5;
                                if (enemySpriteIndex > 8)
                                {
                                    enemySpriteIndex = 1;
                                }
                                enemy.HurtSprites(enemySpriteIndex);
                                enemy1.Fill = enemy.enemySprite;
                            }
                            if (enemy.Health <= 0)
                            {
                                enemySpriteIndex += 0.5;
                                if (enemySpriteIndex > 8)
                                {
                                    itemRemover.Add(q);
                                }
                                enemy.DieSprites(enemySpriteIndex);
                                enemy1.Fill = enemy.enemySprite;
                            }
                        }

                        if (q.Name.ToString() == "enemy2")
                        {
                            Rect enemyHitBox2 = new Rect(Canvas.GetLeft(q), Canvas.GetTop(q), q.Width, q.Height);
                            if (bulletHitBox.IntersectsWith(enemyHitBox2))
                            {
                                itemRemover.Add(y);
                                enemyTwo.Health -= 1;
                                enemySpriteIndex += 0.5;
                                if (enemySpriteIndex > 8)
                                {
                                    enemySpriteIndex = 1;
                                }
                                enemyTwo.HurtSprites(enemySpriteIndex);
                                enemy2.Fill = enemyTwo.enemySprite;
                            }
                            if (enemyTwo.Health <= 0)
                            {
                                enemySpriteIndex += 0.5;
                                if (enemySpriteIndex > 8)
                                {
                                    itemRemover.Add(q);
                                }
                                enemyTwo.DieSprites(enemySpriteIndex);
                                enemy2.Fill = enemyTwo.enemySprite;
                            }
                        }
                    }

                    foreach (var g in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (g.Name.ToString() == "shooter1")
                        {
                            Rect shooterHitBox = new Rect(Canvas.GetLeft(g), Canvas.GetTop(g), g.Width, g.Height);
                            if (bulletHitBox.IntersectsWith(shooterHitBox))
                            {
                                itemRemover.Add(y);
                                shooterOne.Health -= 1;
                                shooterSpriteIndex += 0.5;
                                if (shooterSpriteIndex > 10)
                                {
                                    shooterSpriteIndex = 1;
                                }
                                shooterOne.HurtSprites(shooterSpriteIndex);
                                shooter1.Fill = shooterOne.shooterSprite;
                            }
                            if (shooterOne.Health <= 0)
                            {
                                shooterSpriteIndex += 0.5;
                                if (shooterSpriteIndex > 10)
                                {
                                    itemRemover.Add(g);
                                }
                                shooterOne.DieSprites(shooterSpriteIndex);
                                shooter1.Fill = shooterOne.shooterSprite;
                            }
                        }

                        if (g.Name.ToString() == "shooter2")
                        {
                            Rect shooterHitBox2 = new Rect(Canvas.GetLeft(g), Canvas.GetTop(g), g.Width, g.Height);
                            if (bulletHitBox.IntersectsWith(shooterHitBox2))
                            {
                                itemRemover.Add(y);
                                shooterTwo.Health -= 1;
                                shooterSpriteIndex += 0.5;
                                if (shooterSpriteIndex > 10)
                                {
                                    shooterSpriteIndex = 1;
                                }
                                shooterTwo.HurtSprites(shooterSpriteIndex);
                                shooter2.Fill = shooterTwo.shooterSprite;
                            }
                            if (shooterTwo.Health <= 0)
                            {
                                shooterSpriteIndex += 0.5;
                                if (shooterSpriteIndex > 10)
                                {
                                    itemRemover.Add(g);
                                }
                                shooterTwo.DieSprites(shooterSpriteIndex);
                                shooter2.Fill = shooterOne.shooterSprite;
                            }
                        }
                    }

                    foreach (var h in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (h.Name.ToString() == "soldier")
                        {
                            Rect soldierHitBox = new Rect(Canvas.GetLeft(h), Canvas.GetTop(h), h.Width, h.Height);
                            if (bulletHitBox.IntersectsWith(soldierHitBox))
                            {
                                itemRemover.Add(y);
                                soldierOne.Health -= 1;
                                soldierSpriteIndex += 0.5;
                                if (soldierSpriteIndex > 10)
                                {
                                    soldierSpriteIndex = 1;
                                }
                                soldierOne.HurtSprites(soldierSpriteIndex);
                                soldier.Fill = soldierOne.soldierSprite;
                            }
                            if (soldierOne.Health <= 0)
                            {
                                soldierSpriteIndex += 0.5;
                                if (soldierSpriteIndex > 10)
                                {
                                    itemRemover.Add(h);
                                }
                                soldierOne.DieSprites(soldierSpriteIndex);
                                soldier.Fill = soldierOne.soldierSprite;
                            }
                        }
                    }

                    foreach (var t in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (t.Name.ToString() == "terror")
                        {
                            Rect terroristHitBox = new Rect(Canvas.GetLeft(t), Canvas.GetTop(t), t.Width, t.Height);
                            if (bulletHitBox.IntersectsWith(terroristHitBox))
                            {
                                itemRemover.Add(y);
                                terrorist.Health -= 1;
                                terroristSpriteIndex += 0.2;
                                if (terroristSpriteIndex > 6)
                                {
                                    terroristSpriteIndex = 1;
                                }
                                terrorist.HurtSprites(terroristSpriteIndex);
                                terror.Fill = terrorist.terroristSprite;
                            }
                            if (terrorist.Health <= 0)
                            {
                                terroristSpriteIndex ++;
                                if (terroristSpriteIndex > 9)
                                {
                                    itemRemover.Add(t);
                                }
                                terrorist.DieSprites(terroristSpriteIndex);
                                terror.Fill = terrorist.terroristSprite;
                            }
                        }
                    }

                    foreach (var i in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (i.Name.ToString() == "robo")
                        {
                            Rect roboHitBox = new Rect(Canvas.GetLeft(i), Canvas.GetTop(i), i.Width, i.Height);
                            if (bulletHitBox.IntersectsWith(roboHitBox))
                            {
                                itemRemover.Add(y);
                                robot.Health -= 1;
                                robotSpriteIndex += 0.5;
                                if (robotSpriteIndex > 2)
                                {
                                    robotSpriteIndex = 1;
                                }
                                robot.HurtSprites(robotSpriteIndex);
                                robo.Fill = robot.robotSprite;
                            }
                            if (robot.Health <= 0)
                            {
                                robotSpriteIndex += 2;
                                if (robotSpriteIndex > 15)
                                {
                                    itemRemover.Add(i);
                                }
                                robot.DieSprites(robotSpriteIndex);
                                robo.Fill = robot.robotSprite;
                            }
                        }
                    }

                    foreach (var j in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (j.Name.ToString() == "troll")
                        {
                            Rect trollHitBox = new Rect(Canvas.GetLeft(j), Canvas.GetTop(j), j.Width, j.Height);
                            if (bulletHitBox.IntersectsWith(trollHitBox))
                            {
                                itemRemover.Add(y);
                                trollOne.Health -= 1;
                                trollSpriteIndex += 0.5;
                                if (trollSpriteIndex > 10)
                                {
                                    trollSpriteIndex = 1;
                                }
                                trollOne.HurtSprites(trollSpriteIndex);
                                troll.Fill = trollOne.trollSprite;
                            }
                            if (trollOne.Health <= 0)
                            {
                                trollSpriteIndex += 0.5;
                                if (trollSpriteIndex > 10)
                                {
                                    itemRemover.Add(j);
                                }
                                trollOne.DieSprites(trollSpriteIndex);
                                troll.Fill = trollOne.trollSprite;
                            }
                        }
                    }

                    foreach (var m in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (m.Name.ToString() == "ninjaBow")
                        {
                            Rect ninjaBossHitBox = new Rect(Canvas.GetLeft(m), Canvas.GetTop(m), m.Width, m.Height);
                            if (bulletHitBox.IntersectsWith(ninjaBossHitBox))
                            {
                                itemRemover.Add(y);
                                ninjaBoss.Health -= 1;
                                ninjaBossSpriteIndex += 0.5;
                                if (ninjaBossSpriteIndex > 10)
                                {
                                    ninjaBossSpriteIndex = 1;
                                }
                                ninjaBoss.HurtSprites(ninjaBossSpriteIndex);
                                ninjaBow.Fill = ninjaBoss.ninjaBossSprite;
                            }
                            if (ninjaBoss.Health <= 0)
                            {
                                ninjaBossSpriteIndex += 0.5;
                                if (ninjaBossSpriteIndex > 15)
                                {
                                    itemRemover.Add(m);
                                }
                                ninjaBoss.DieSprites(ninjaBossSpriteIndex);
                                ninjaBow.Fill = ninjaBoss.ninjaBossSprite;
                            }
                        }
                    }

                    foreach (var n in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (n.Name.ToString() == "ninja1")
                        {
                            Rect ninjaHitBox = new Rect(Canvas.GetLeft(n), Canvas.GetTop(n), n.Width, n.Height);
                            if (bulletHitBox.IntersectsWith(ninjaHitBox))
                            {
                                itemRemover.Add(y);
                                ninja.Health -= 1;
                                ninjaSpriteIndex += 0.5;
                                if (ninjaSpriteIndex > 2)
                                {
                                    ninjaSpriteIndex = 1;
                                }
                                ninja.HurtSprites(ninjaSpriteIndex);
                                ninja1.Fill = ninja.ninjaSprite;

                            }
                            if (ninja.Health <= 0)
                            {
                                ninjaSpriteIndex += 0.5;
                                if (ninjaSpriteIndex > 10)
                                {
                                    itemRemover.Add(n);
                                }
                                ninja.DieSprites(ninjaSpriteIndex);
                                ninja1.Fill = ninja.ninjaSprite;
                            }
                        }

                        if (n.Name.ToString() == "ninja2")
                        {
                            Rect ninjaHitBox2 = new Rect(Canvas.GetLeft(n), Canvas.GetTop(n), n.Width, n.Height);
                            if (bulletHitBox.IntersectsWith(ninjaHitBox2))
                            {
                                itemRemover.Add(y);
                                ninjaTwo.Health -= 1;
                                ninjaSpriteIndex += 0.5;
                                if (ninjaSpriteIndex > 2)
                                {
                                    ninjaSpriteIndex = 1;
                                }
                                ninjaTwo.HurtSprites(ninjaSpriteIndex);
                                ninja2.Fill = ninjaTwo.ninjaSprite;
                            }
                            if (ninjaTwo.Health <= 0)
                            {
                                ninjaSpriteIndex += 0.5;
                                if (ninjaSpriteIndex > 10)
                                {
                                    itemRemover.Add(n);
                                }
                                ninjaTwo.DieSprites(ninjaSpriteIndex);
                                ninja2.Fill = ninjaTwo.ninjaSprite;
                            }
                        }
                    }
                }
            }
            //Взаимодействие игрока с элементами игры
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "ground")
                {
                    Rect groundHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(groundHitBox))
                    {
                        //player.ForceJump = 8;
                        player.JumpSpeed = 0;
                        jumping = false;
                        Canvas.SetTop(hero, Canvas.GetTop(x) - hero.Height);
                    }

                    //if(x.Name.ToString() == "ground10")
                    //{
                    //    if (goRight == true && playerHitBox.IntersectsWith(groundHitBox))
                    //    {
                    //        Canvas.SetLeft(hero, Canvas.GetLeft(hero) - 10);
                    //        goRight = false;
                    //    }
                    //}
                }

                if ((string)x.Tag == "platforma")
                {
                    Rect platformHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(platformHitBox))
                    {
                        player.ForceJump = 8;
                        //player.JumpSpeed = 0;
                        Canvas.SetTop(hero, Canvas.GetTop(x) - hero.Height);
                    }

                    if (x.Name.ToString() == "platform1")
                    {
                        if (playerHitBox.IntersectsWith(platformHitBox))
                        {
                            Canvas.SetLeft(hero, Canvas.GetLeft(hero) + horizontalSpeedPlatform);
                        }
                    }





                    //if (x.Name.ToString() == "platform7")
                    //{
                    //    if (goRight == true && playerHitBox.IntersectsWith(platformHitBox))
                    //    {
                    //        Canvas.SetLeft(hero, Canvas.GetLeft(hero) - 10);
                    //        goRight = false;
                    //    }
                    //}
                }

                if ((string)x.Tag == "spike")
                {
                    Rect spikeHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(spikeHitBox))
                    {
                        player.Health = -10;
                    }
                }

                if ((string)x.Tag == "coin")
                {
                    Rect coinHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(coinHitBox))
                    {
                        itemRemover.Add(x);
                        score += 1;
                        if (score >= 7)
                        {
                            bulletsScore += 8;
                            score = 0;
                        }
                    }
                }

                if ((string)x.Tag == "bullets")
                {
                    Rect bulletsHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(bulletsHitBox))
                    {
                        itemRemover.Add(x);
                        bulletsScore += 10;
                    }
                }

                if ((string)x.Tag == "healths")
                {
                    Rect healthsHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(healthsHitBox))
                    {
                        itemRemover.Add(x);
                        player.Health += 5;
                    }
                }

                if ((string)x.Tag == "treasure")
                {
                    Rect treasureHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(treasureHitBox))
                    {
                        itemRemover.Add(x);
                        gem += 1;
                    }
                }

                if ((string)x.Tag == "speeds")
                {
                    Rect speedBonusHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(speedBonusHitBox))
                    {
                        itemRemover.Add(x);
                        player.Speed += 3;
                        player.speedup = true;
                    }

                    //остановка работы бонуса ускорение игрока 
                    player.timeActionsSpeedup++;
                    if (player.speedup && player.timeActionsSpeedup > player.speedupStateDuration)
                    {
                        player.speedup = false;
                        player.timeActionsSpeedup = 0;
                        player.Speed = 5;
                    }

                }


                if (x is Rectangle && (string)x.Tag == "enemyBullet")
                {

                    if (shooterOne.Speed < 0)
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - speedEnemyBullet);

                    if (shooterOne.Speed > 0)
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + speedEnemyBullet);

                    if (Canvas.GetLeft(x) > 600 || Canvas.GetLeft(x) < 30)
                    {
                        itemRemover.Add(x);
                    }


                    Rect enemyBulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyBulletHitBox))
                    {
                        player.Health -= 2;
                        itemRemover.Add(x);
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (x is Rectangle && (string)x.Tag == "enemyBullet2")
                {
                    if (shooterTwo.Speed < 0)
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - speedEnemyBullet);

                    if (shooterTwo.Speed > 0)
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + speedEnemyBullet);


                    if (Canvas.GetLeft(x) > 2450 || Canvas.GetLeft(x) < 1630)
                    {
                        itemRemover.Add(x);
                    }

                    Rect enemyBulletHitBox2 = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyBulletHitBox2))
                    {
                        player.Health -= 2;
                        itemRemover.Add(x);
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (x is Rectangle && (string)x.Tag == "enemyBullet3")
                {
                    //Canvas.SetLeft(x,Canvas.GetLeft(x) + speedEnemyBullet);  

                    if (Canvas.GetLeft(x) > 2450 || Canvas.GetLeft(x) < 1630)
                    {
                        itemRemover.Add(x);
                    }

                    Rect enemyBulletHitBox3 = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyBulletHitBox3))
                    {
                        player.Health -= 2;
                        itemRemover.Add(x);
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (x is Rectangle && (string)x.Tag == "lazer")
                {
                    //Canvas.SetLeft(x,Canvas.GetLeft(x) + speedEnemyBullet);  

                    if (Canvas.GetLeft(x) > 3180 || Canvas.GetLeft(x) < 1630)
                    {
                        itemRemover.Add(x);
                    }

                    Rect lazerHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(lazerHitBox))
                    {
                        player.Health -= 2;
                        itemRemover.Add(x);
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (x is Rectangle && (string)x.Tag == "kunai")
                {
                    if(Canvas.GetLeft(x) < Canvas.GetLeft(ninja2) + ninja2.Height)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - speedEnemyArrows);
                    }
                    if(Canvas.GetLeft(x) > Canvas.GetLeft(ninja2))
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + speedEnemyArrows);
                    }

                   
                    
                    if (Canvas.GetLeft(x) > 4750 || Canvas.GetLeft(x) < 3260)
                    {
                        itemRemover.Add(x);
                    }
                    

                    Rect kunaiHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(kunaiHitBox))
                    {
                        player.Health -= 2;
                        itemRemover.Add(x);
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (x is Rectangle && (string)x.Tag == "arrow")
                {
                    if (Canvas.GetLeft(x) < Canvas.GetLeft(ninjaBow) + ninjaBow.Height)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - speedEnemyArrows);
                    }
                    if (Canvas.GetLeft(x) > Canvas.GetLeft(ninjaBow))
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + speedEnemyArrows);
                    }

                    if (Canvas.GetLeft(x) > 4650 || Canvas.GetLeft(x) < 3260)
                    {
                        itemRemover.Add(x);
                    }

                    Rect arrowHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(arrowHitBox))
                    {
                        player.Health -= 2;
                        itemRemover.Add(x);
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (x is Rectangle && (string)x.Tag == "stone")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + stoneSpeed);
                    if (Canvas.GetTop(x) > 342)
                    {
                        itemRemover.Add(x);
                    }
                    Rect stoneHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(stoneHitBox))
                    {
                        itemRemover.Add(x);
                        player.Health -= 4;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if ((string)x.Tag == "surface")
                {
                    Rect surfaceHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(surfaceHitBox))
                    {
                        player.ForceJump = 8;
                        //player.JumpSpeed = 0;
                        Canvas.SetTop(hero, Canvas.GetTop(x) - hero.Height);
                    }

                    if (x.Name.ToString() == "surface10")
                    {
                        if (playerHitBox.IntersectsWith(surfaceHitBox))
                        {
                            Canvas.SetLeft(hero, Canvas.GetLeft(hero) + horizontalSpeedPlatform2);
                        }
                    }
                }

                //if (x.Name.ToString() =="grass")
                //{
                //    Rect grassHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                //    if (playerHitBox.IntersectsWith(grassHitBox))
                //    {
                //        player.ForceJump = 8;
                //        Canvas.SetTop(hero, Canvas.GetTop(x) - hero.Height);
                //    }
                //}

                if (x.Name.ToString() == "cactus")
                {
                    Rect cactusHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(cactusHitBox))
                    {
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                    }
                }

                if (x.Name.ToString() == "door1")
                {
                    Rect doorHitBox1 = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(doorHitBox1))
                    {
                        if (gem == 3 && trollOne.Health <= 0)
                        {
                            backgroundLevel2.Focusable = true;
                            gem = 0;
                            treasuresScore.Foreground = Brushes.White;
                            Canvas.SetLeft(hero, 1624);
                            Canvas.SetTop(hero, 580);
                            Canvas.SetLeft(myGrid, 1630);
                            Canvas.SetTop(myGrid, 10);
                        }

                    }
                }

                if (x.Name.ToString() == "door2")
                {
                    Rect doorHitBox2 = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (gem == 3 && robot.Health <= 0)
                    {
                        if (playerHitBox.IntersectsWith(doorHitBox2))
                        {
                            backgroundLevel3.Focusable = true;
                            backgroundLevel2.Focusable = false;
                            gem = 0;
                            treasuresScore.Foreground = Brushes.White;
                            Canvas.SetLeft(hero, 3242);
                            Canvas.SetTop(hero, 632);
                            Canvas.SetLeft(myGrid, 3230);
                            Canvas.SetTop(myGrid, 10);
                        }
                    }
                }

                if (x.Name.ToString() == "chest")
                {
                    Rect chestHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (gem == 3 && ninjaBoss.Health <= 0 && bear.Health <= 0)
                    {
                        if (playerHitBox.IntersectsWith(chestHitBox))
                        {
                            healthScore.Foreground = Brushes.Green;
                            ShowGameOver("Победа!!!\nСокровища\nнайдены!");
                        }
                    }
                }
            }

            if (player.Health <= 0)
            {
                player.Health = 0;
                healthScore.Foreground = Brushes.Red;
                playerSpriteIndex += 0.2;
                if (playerSpriteIndex > 10)
                {
                    ShowGameOver("Вы проиграли!!!");
                }
                player.DieSprites(playerSpriteIndex);
                hero.Fill = player.playerSprite;
            }

            if (gem == 3 && (backgroundLevel.Focusable == false && backgroundLevel3.Focusable == false && backgroundLevel2.Focusable == false))
            {
                ImageBrush entry = new ImageBrush();
                entry.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/doorOpen.png"));
                door1.Fill = entry;
                treasuresScore.Foreground = Brushes.Yellow;
                if (trollOne.Health <= 0)
                {
                    treasuresScore.Content = "Сокровища: " + gem + Environment.NewLine + "Первый уровень" + Environment.NewLine + "пройден!";
                }

                //MessageBox.Show("Первый уровень пройден!", "TREASURE HUNTER");
            }

            if (gem == 3 && backgroundLevel2.Focusable == true)
            {
                ImageBrush doorOpen = new ImageBrush();
                doorOpen.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/doorOpen_02.png"));
                door2.Fill = doorOpen;
                //treasuresScore.Foreground = Brushes.Yellow;
                if (robot.Health <= 0)
                {
                    treasuresScore.Content = "Сокровища: " + gem + Environment.NewLine + "Второй уровень" + Environment.NewLine + "пройден!";
                }
                //MessageBox.Show("Второй уровень пройден!", "TREASURE HUNTER");
            }

            if (gem == 3 && backgroundLevel3.Focusable == true)
            {
                ImageBrush chestImage = new ImageBrush();
                chestImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Treasure_open.png"));
                chest.Fill = chestImage;
                treasuresScore.Foreground = Brushes.Yellow;
            }

            foreach (Rectangle item in itemRemover)
            {
                MyCanvas.Children.Remove(item);
            }

            foreach (Image itemTwo in itemToRemover)
            {
                MyCanvas.Children.Remove(itemTwo);
            }
        }
    }
}
