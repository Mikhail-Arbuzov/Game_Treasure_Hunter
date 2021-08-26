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
            //изменяемые показатели в игре
            healthScore.Content = "Здоровье: " + player.Health;
            coinScore.Content = "Монеты: " + score;
            catridgesScore.Content = "Патроны: " + bulletsScore;
            treasuresScore.Content = "Сокровища: " + gem;
            healthBoss.Content = "Здоровье Босса: " + healthProgress.Value;
            //таймер времени в игре
            counterTime+=TimeSpan.FromSeconds(1) - TimeSpan.FromMilliseconds(955);
            timer.Content = counterTime.ToString(@"hh\:mm\:ss");

            //чтоб игрок не выходил за экран а также настройка фонового звука уровней
            if (backgroundLevel.Focusable == false && backgroundLevel3.Focusable == false && backgroundLevel2.Focusable == false)
            {
                if (Canvas.GetLeft(hero) >= 1460)
                {
                    Canvas.SetLeft(hero, Canvas.GetLeft(hero) - 10);
                    //goRight = false;
                }
                //для повтора фонового звука на первом уровне
                if (!turnOffsong)
                {
                    if (backgroundMedia.Position >= new TimeSpan(0, 1, 4))
                    {
                        backgroundMedia.Position = new TimeSpan(0, 0, 1);
                        backgroundMedia.Play();
                    }
                }
                //звук камнепада в первом уровне
                if (soundsGame && gem < 3)
                {
                    stonesSound.Play();
                    if(stonesSound.Position >= new TimeSpan(0,0,5))
                    {
                        stonesSound.Position = new TimeSpan(0, 0, 1);
                        stonesSound.Play();
                    }
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
                //для повтора фонового звука на третьем уровне
                if (!turnOffsong)
                {
                    if (backgroundMediaThree.Position >= new TimeSpan(0, 0, 59))
                    {
                        backgroundMediaThree.Position = new TimeSpan(0, 0, 1);
                        backgroundMediaThree.Play();
                    }
                }
                

            }

            //игрок в покое
            if(!player.shoot)
            {
                if ((!goLeft && !goRight && !jumping && !goDown) && (!Keyboard.IsKeyDown(Key.Space)) && player.Health > 0)
                {
                    playerSpriteIndex2 += 0.5;
                    if (playerSpriteIndex2 > 10)
                    {
                        playerSpriteIndex2 = 0;
                    }
                    player.IdleSprites(playerSpriteIndex2);
                    hero.Fill = player.playerSprite;
                }
            }
           
            if(Keyboard.IsKeyDown(Key.Space))
            {
                player.shoot = true;
                playerSpriteIndex2 = 0;
            }
            else
            {
                player.shoot = false;
            }

            //движение игрока
            if (goLeft == true && Canvas.GetLeft(hero) > 0)
            {
                Canvas.SetLeft(hero, Canvas.GetLeft(hero) - player.Speed);
            }

            if (goRight == true && Canvas.GetLeft(hero) + (hero.Width + 10) <= this.MyCanvas.ActualWidth)
            {
                Canvas.SetLeft(hero, Canvas.GetLeft(hero) + player.Speed);
            }

            //остановка работы бонуса ускорение игрока 
            player.timeActionsSpeedup++;
            if(player.timeActionsSpeedup >=750)
            {
                player.timeActionsSpeedup = 0;
            }
            if (player.speedup && player.timeActionsSpeedup > player.speedupStateDuration)
            {
                player.speedup = false;
                player.timeActionsSpeedup = 0;
                player.Speed = 6;
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
            //для работы прыжка вниз
            if(!jumpDown)
            {
                if (goDown && Canvas.GetTop(hero) + (hero.Height * 2) <= this.MyCanvas.ActualHeight)
                {
                    //Canvas.SetTop(hero, -80);
                    Canvas.SetTop(hero, Canvas.GetTop(hero) + player.jumpDownSpeed);
                }
            }
     
           

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
                if (foe.Name.ToString() == "enemy1")
                {
                    Rect enHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if(enemy.Health > 0)
                    {
                        if (enHitBox.IntersectsWith(playerHitBox))
                        {
                            enemySpriteIndex += 0.5;
                            if (enemySpriteIndex > 8)
                            {
                                enemySounds.Open(new Uri(@"../../GameSounds/kingal.mp3", UriKind.Relative));
                                enemySounds.Position = new TimeSpan(0, 0, 0);
                                enemySounds.Play();
                                enemySpriteIndex = 0;
                            }
                            enemy.AttackSprites(enemySpriteIndex);
                            enemy1.Fill = enemy.enemySprite;

                        }
                        if (playerHitBox.IntersectsWith(enHitBox))
                        {
                            player.Health -= 1;
                            playerSpriteIndex += 0.5;
                            if (playerSpriteIndex > 10)
                            {
                                playerSpriteIndex = 1;
                                playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                                playerMedia.Position = new TimeSpan(0, 0, 0);
                                playerMedia.Play();
                            }
                            player.HurtSprites(playerSpriteIndex);
                            hero.Fill = player.playerSprite;
                        }
                    }
                }

                if(foe.Name.ToString() == "enemy2")
                {
                    Rect enHitBox2 = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if(enemyTwo.Health > 0)
                    {
                        if (enHitBox2.IntersectsWith(playerHitBox))
                        {
                            enemySpriteIndex += 0.5;
                            if (enemySpriteIndex > 8)
                            {
                                enemySounds.Open(new Uri(@"../../GameSounds/kingal.mp3", UriKind.Relative));
                                enemySounds.Position = new TimeSpan(0, 0, 0);
                                enemySounds.Play();
                                enemySpriteIndex = 0;
                            }

                            enemyTwo.AttackSprites(enemySpriteIndex);
                            enemy2.Fill = enemyTwo.enemySprite;
                        }
                        if (playerHitBox.IntersectsWith(enHitBox2))
                        {
                            player.Health -= 1;
                            playerSpriteIndex += 0.5;
                            if (playerSpriteIndex > 10)
                            {
                                playerSpriteIndex = 1;
                                playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                                playerMedia.Position = new TimeSpan(0, 0, 0);
                                playerMedia.Play();
                            }
                            player.HurtSprites(playerSpriteIndex);
                            hero.Fill = player.playerSprite;
                        }
                    }
                }

                if (foe.Name.ToString() == "shooter1")
                {
                    Rect shHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if(shooterOne.Health > 0)
                    {
                        if (shHitBox.IntersectsWith(playerHitBox))
                        {
                            shooterSpriteIndex += 0.5;
                            if (shooterSpriteIndex > 10)
                            {
                                shooterSpriteIndex = 1;
                                shooterSounds.Open(new Uri(@"../../GameSounds/shooter.mp3", UriKind.Relative));
                                shooterSounds.Position = new TimeSpan(0, 0, 0);
                                shooterSounds.Play();
                            }
                            shooterOne.AttackSprites(shooterSpriteIndex);
                            shooter1.Fill = shooterOne.shooterSprite;
                        }
                        if (playerHitBox.IntersectsWith(shHitBox))
                        {
                            player.Health -= 1;
                            playerSpriteIndex += 0.5;
                            if (playerSpriteIndex > 10)
                            {
                                playerSpriteIndex = 1;
                                playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                                playerMedia.Position = new TimeSpan(0, 0, 0);
                                playerMedia.Play();
                            }
                            player.HurtSprites(playerSpriteIndex);
                            hero.Fill = player.playerSprite;
                        }
                    }  
                }

                if(foe.Name.ToString() == "shooter2")
                {
                    Rect shHitBox2 = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if(shooterTwo.Health > 0)
                    {
                        if (shHitBox2.IntersectsWith(playerHitBox))
                        {
                            shooterSpriteIndex += 0.5;
                            if (shooterSpriteIndex > 10)
                            {
                                shooterSpriteIndex = 1;
                                shooterSounds.Open(new Uri(@"../../GameSounds/shooter.mp3", UriKind.Relative));
                                shooterSounds.Position = new TimeSpan(0, 0, 0);
                                shooterSounds.Play();
                            }
                            shooterTwo.AttackSprites(shooterSpriteIndex);
                            shooter2.Fill = shooterTwo.shooterSprite;
                        }
                        if (playerHitBox.IntersectsWith(shHitBox2))
                        {
                            player.Health -= 1;
                            playerSpriteIndex += 0.5;
                            if (playerSpriteIndex > 10)
                            {
                                playerSpriteIndex = 1;
                                playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                                playerMedia.Position = new TimeSpan(0, 0, 0);
                                playerMedia.Play();
                            }
                            player.HurtSprites(playerSpriteIndex);
                            hero.Fill = player.playerSprite;
                        }
                    }
                }

                if (foe.Name.ToString() == "troll")
                {
                    Rect trHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if(trollOne.Health > 0)
                    {
                        if (trHitBox.IntersectsWith(playerHitBox))
                        {
                            soundTrollRun = true;
                            trollAttackSounds.Play();
                            trollSpriteIndex += 0.5;
                            if (trollSpriteIndex > 10)
                            {
                                trollSpriteIndex = 0;
                            }
                            trollOne.AttackSprites(trollSpriteIndex);
                            troll.Fill = trollOne.trollSprite;
                           
                        }
                        if (playerHitBox.IntersectsWith(trHitBox))
                        {
                            player.Health -= 1;
                            playerSpriteIndex += 0.5;
                            if (playerSpriteIndex > 10)
                            {
                                playerSpriteIndex = 1;
                                playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                                playerMedia.Position = new TimeSpan(0, 0, 0);
                                playerMedia.Play();
                            }
                            player.HurtSprites(playerSpriteIndex);
                            hero.Fill = player.playerSprite;
                        }
                    }
                    
                }

                if (foe.Name.ToString() == "terror")
                {
                    Rect terHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if(terrorist.Health > 0)
                    {
                        if (terHitBox.IntersectsWith(playerHitBox))
                        {
                            terroristShootSounds.Play();
                            if (terroristShootSounds.Position >= new TimeSpan(0, 0, 3))
                            {
                                terroristShootSounds.Position = new TimeSpan(0, 0, 0);
                                terroristShootSounds.Play();
                            }
                            terroristSpriteIndex += 0.5;
                            if (terroristSpriteIndex > 6)
                            {
                                terroristSpriteIndex = 0;
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
                                playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                                playerMedia.Position = new TimeSpan(0, 0, 0);
                                playerMedia.Play();
                            }
                            player.HurtSprites(playerSpriteIndex);
                            hero.Fill = player.playerSprite;
                        }
                    }
                }

                if (foe.Name.ToString() == "soldier")
                {
                    Rect soHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if(soldierOne.Health > 0)
                    {
                        if (soHitBox.IntersectsWith(playerHitBox))
                        {
                            soldierSpriteIndex += 0.5;
                            if (soldierSpriteIndex > 6)
                            {
                                soldierSounds.Open(new Uri(@"../../GameSounds/kingal2.mp3", UriKind.Relative));//удар кинжалом
                                soldierSounds.Position = new TimeSpan(0, 0, 0);
                                soldierSounds.Play();
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
                                playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                                playerMedia.Position = new TimeSpan(0, 0, 0);
                                playerMedia.Play();
                            }
                            player.HurtSprites(playerSpriteIndex);
                            hero.Fill = player.playerSprite;
                        }
                    }
                }

                if (foe.Name.ToString() == "robo")
                {
                    Rect robHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if(robot.Health > 0)
                    {
                        if (robHitBox.IntersectsWith(playerHitBox))
                        {
                            robotRunSounds.Stop();
                            robotLazerSounds.Stop();
                            //звук удара щипцем робота
                            robotAttackSounds.Play();
                            if(robotAttackSounds.Position >= new TimeSpan(0,0,1))
                            {
                                robotAttackSounds.Position = new TimeSpan(0, 0, 0);
                                robotAttackSounds.Play();
                            }
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
                                playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                                playerMedia.Position = new TimeSpan(0, 0, 0);
                                playerMedia.Play();
                            }
                            player.HurtSprites(playerSpriteIndex);
                            hero.Fill = player.playerSprite;
                        }
                    }
                }

                if (foe.Name.ToString() == "ninja1")
                {
                    Rect ninjHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if(ninja.Health > 0)
                    {
                        if (ninjHitBox.IntersectsWith(playerHitBox))
                        {
                            ninjaSpriteIndex += 0.5;
                            if (ninjaSpriteIndex > 10)
                            {
                                ninjaSounds.Open(new Uri(@"../../GameSounds/sword.mp3", UriKind.Relative));//звук взмаха мечем
                                ninjaSounds.Position = new TimeSpan(0, 0, 0);
                                ninjaSounds.Play();
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
                                playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                                playerMedia.Position = new TimeSpan(0, 0, 0);
                                playerMedia.Play();
                            }
                            player.HurtSprites(playerSpriteIndex);
                            hero.Fill = player.playerSprite;
                        }
                    }
                    
                }

                if (foe.Name.ToString() == "ninja2")
                {
                    Rect niHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if(ninjaTwo.Health > 0)
                    {
                        if (niHitBox.IntersectsWith(playerHitBox))
                        {
                            ninjaTwoSpriteIndex += 0.5;
                            if (ninjaTwoSpriteIndex > 10)
                            {
                                if (soundsGame && backgroundLevel3.Focusable == true && ninjaTwo.Health > 0)
                                {
                                    ninjaSounds.Open(new Uri(@"../../GameSounds/kunai.mp3", UriKind.Relative));//звук броска kunai
                                    ninjaSounds.Position = new TimeSpan(0, 0, 0);
                                    ninjaSounds.Play();
                                }
                                
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
                                playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                                playerMedia.Position = new TimeSpan(0, 0, 0);
                                playerMedia.Play();
                            }
                            player.HurtSprites(playerSpriteIndex);
                            hero.Fill = player.playerSprite;
                        }
                    }
                   
                }

                if (foe.Name.ToString() == "ninjaBow")
                {
                    Rect nbHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if(ninjaBoss.Health > 0)
                    {
                        if (nbHitBox.IntersectsWith(playerHitBox))
                        {
                            ninjaBossSpriteIndex += 0.5;
                            if (ninjaBossSpriteIndex > 22)
                            {
                                if (soundsGame && backgroundLevel3.Focusable == true && ninjaBoss.Health > 0)
                                {
                                    ninjaBossSounds.Open(new Uri(@"../../GameSounds/arrows.mp3", UriKind.Relative));//звук стрельбы из лука
                                    ninjaBossSounds.Position = new TimeSpan(0, 0, 0);
                                    ninjaBossSounds.Play();
                                }
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
                                playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                                playerMedia.Position = new TimeSpan(0, 0, 0);
                                playerMedia.Play();
                            }
                            player.HurtSprites(playerSpriteIndex);
                            hero.Fill = player.playerSprite;
                        }
                    }
                   
                }
            }

            //взаимодействие животных с игроком 
            foreach (var animal in MyCanvas.Children.OfType<Image>())
            {
                if (animal is Image && (string)animal.Tag == "wolf")
                {
                    Rect wolfHitBox2 = new Rect(Canvas.GetLeft(animal), Canvas.GetTop(animal), animal.Width, animal.Height);

                    if (wolfHitBox2.IntersectsWith(playerHitBox))
                    {
                        wolfAttackSounds.Play();
                        wolf.AttackRight();
                        //движение при атаке волка не корректно был сработан
                        //if (Canvas.GetLeft(animal) < Canvas.GetLeft(ground4))
                        //{
                        //    wolf.AttackLeft();
                        //}
                        //if (Canvas.GetLeft(animal) + animal.Width > Canvas.GetLeft(ground4) + ground4.Width)
                        //{
                        //    wolf.AttackRight();
                        //}
                    }
                    if (playerHitBox.IntersectsWith(wolfHitBox2))
                    {
                        player.Health -= 2;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                            playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                            playerMedia.Position = new TimeSpan(0, 0, 0);
                            playerMedia.Play();
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
                        bearSound.Stop();
                        bearAttackSound.Play();//рычание медведя при атаке
                        if(bearAttackSound.Position >= new TimeSpan(0,0,7))
                        {
                            bearAttackSound.Position = new TimeSpan(0, 0, 1);
                            bearAttackSound.Play();
                        }
                        bear.AttackLeft();
                    }

                    if (playerHitBox.IntersectsWith(bearHitBox2))
                    {
                        player.Health -= 2;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                            playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                            playerMedia.Position = new TimeSpan(0, 0, 0);
                            playerMedia.Play();
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
                        snakeSound.Stop();
                        snakeAttackSound.Play();//атака змеи
                        if (snakeAttackSound.Position >= new TimeSpan(0, 0, 2))
                        {
                            snakeAttackSound.Position = new TimeSpan(0, 0, 0);
                            snakeAttackSound.Play();
                        }
                        snake.AttackRight();
                        //движение при атаке змеи не корректно был сработан
                        //if (Canvas.GetLeft(animal) > 3097)
                        //{
                        //    snake.AttackLeft();
                        //}
                        //if (Canvas.GetLeft(animal) < 2671)
                        //{
                        //     snake.AttackRight();
                        //}
                    }
                    if (playerHitBox.IntersectsWith(snakeHitBox2))
                    {
                        player.Health -= 2;
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                            playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                            playerMedia.Position = new TimeSpan(0, 0, 0);
                            playerMedia.Play();
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
                        //звук ранения игрока 
                        raneniePlayer2.Open(new Uri(@"../../GameSounds/ranen.mp3", UriKind.Relative));
                        raneniePlayer2.Position = new TimeSpan(0, 0, 0);
                        raneniePlayer2.Play();
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
                        //звук ранения игрока 
                        raneniePlayer2.Open(new Uri(@"../../GameSounds/ranen.mp3", UriKind.Relative));
                        raneniePlayer2.Position = new TimeSpan(0, 0, 0);
                        raneniePlayer2.Play();

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
                                    wolfDieSounds.Play();// волк скулит
                                    //itemToRemover.Add(wf);
                                }
                            }
                          
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
                                bear.Health -= 1;
                                if (bear.Health <= 0)
                                {
                                    bearSound.Stop();//остановить звук дыхания медведя
                                    itemToRemover.Add(be);
                                }
                            }
                            
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
                                    snakeSound.Stop();//остановить звук шипения змеи
                                    itemToRemover.Add(r);
                                }
                            }
                           
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
                                    hogSound.Stop();
                                    itemToRemover.Add(k);
                                }
                            }
                           
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
                                    birdSound.Stop();
                                    itemToRemover.Add(p);
                                }
                            }
                            
                        }
                    }

                    foreach (var q in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (q.Name.ToString() == "enemy1")
                        {
                            Rect enemyHitBox = new Rect(Canvas.GetLeft(q), Canvas.GetTop(q), q.Width, q.Height);
                            if (bulletHitBox.IntersectsWith(enemyHitBox))
                            {
                                enemySpriteIndex += 0.5;

                                if (enemySpriteIndex > 5)
                                {
                                    enemy.Health -= 1;
                                }
                                if (enemySpriteIndex > 8)
                                {
                                    enemySpriteIndex = 0;
                                }
                                enemy.HurtSprites(enemySpriteIndex);
                                enemy1.Fill = enemy.enemySprite;
                                itemRemover.Add(y);
                            }
                            if (enemy.Health <= 0)
                            {
                                enemySounds.Open(new Uri(@"../../GameSounds/enemyDie.mp3", UriKind.Relative));
                                enemySounds.Position = new TimeSpan(0, 0, 0);
                                enemySounds.Play();
                            }
                        }

                        if (q.Name.ToString() == "enemy2")
                        {
                            Rect enemyHitBox2 = new Rect(Canvas.GetLeft(q), Canvas.GetTop(q), q.Width, q.Height);
                            if (bulletHitBox.IntersectsWith(enemyHitBox2))
                            {
                                enemySpriteIndex += 0.5;
                                if(enemySpriteIndex > 5)
                                {
                                    enemyTwo.Health -= 1;
                                }
                                if (enemySpriteIndex > 8)
                                {
                                    enemySpriteIndex = 0;
                                }
                                enemyTwo.HurtSprites(enemySpriteIndex);
                                enemy2.Fill = enemyTwo.enemySprite;
                                itemRemover.Add(y);
                            }
                            if (enemyTwo.Health <= 0)
                            {
                                enemySounds.Open(new Uri(@"../../GameSounds/enemyDie.mp3", UriKind.Relative));
                                enemySounds.Position = new TimeSpan(0, 0, 0);
                                enemySounds.Play();
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
                                shooterSpriteIndex+=0.5;

                                if (shooterSpriteIndex > 6)
                                {
                                    shooterOne.Health -= 1;
                                }
                                if (shooterSpriteIndex > 10)
                                {
                                    shooterSpriteIndex = 0;
                                }
                                shooterOne.HurtSprites(shooterSpriteIndex);
                                shooter1.Fill = shooterOne.shooterSprite;

                                itemRemover.Add(y);
                            }
                            if (shooterOne.Health <= 0)
                            {
                                shooterSounds.Open(new Uri(@"../../GameSounds/deathSoldier.mp3", UriKind.Relative));
                                shooterSounds.Position = new TimeSpan(0, 0, 0);
                                shooterSounds.Play();
                            }
                        }

                        if (g.Name.ToString() == "shooter2")
                        {
                            Rect shooterHitBox2 = new Rect(Canvas.GetLeft(g), Canvas.GetTop(g), g.Width, g.Height);
                            if (bulletHitBox.IntersectsWith(shooterHitBox2))
                            {
                                shooterSpriteIndex += 0.5;
                                if(shooterSpriteIndex > 6)
                                {
                                    shooterTwo.Health -= 1;
                                }
                                if (shooterSpriteIndex > 10)
                                {
                                    shooterSpriteIndex = 0;
                                }
                                shooterTwo.HurtSprites(shooterSpriteIndex);
                                shooter2.Fill = shooterTwo.shooterSprite;
                                itemRemover.Add(y);
                            }
                            if (shooterTwo.Health <= 0)
                            {
                                shooterSounds.Open(new Uri(@"../../GameSounds/deathSoldier.mp3", UriKind.Relative));
                                shooterSounds.Position = new TimeSpan(0, 0, 0);
                                shooterSounds.Play();
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
                                soldierSpriteIndex += 0.5;
                                if(soldierSpriteIndex > 6)
                                {
                                    soldierOne.Health -= 1;
                                }
                                if (soldierSpriteIndex > 10)
                                {
                                    soldierSpriteIndex = 0;
                                }
                                soldierOne.HurtSprites(soldierSpriteIndex);
                                soldier.Fill = soldierOne.soldierSprite;
                                itemRemover.Add(y);
                            }
                            if (soldierOne.Health <= 0)
                            {
                                soldierSounds.Open(new Uri(@"../../GameSounds/ranenie1.mp3", UriKind.Relative));
                                soldierSounds.Position = new TimeSpan(0,0,0);
                                soldierSounds.Play();
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
                                terroristSpriteIndex += 0.5;
                                if(terroristSpriteIndex > 4)
                                {
                                    terrorist.Health -= 1;
                                }
                                if (terroristSpriteIndex > 6)
                                {
                                    terroristSpriteIndex = 0;
                                }
                                terrorist.HurtSprites(terroristSpriteIndex);
                                terror.Fill = terrorist.terroristSprite;
                                itemRemover.Add(y);
                            }
                            if (terrorist.Health <= 0)
                            {
                                terroristSounds.Open(new Uri(@"../../GameSounds/terroristDie.mp3", UriKind.Relative));
                                terroristSounds.Position = new TimeSpan(0, 0, 0);
                                terroristSounds.Play();
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
                                healthProgress.Value -= 1;
                                robotSpriteIndex += 0.5;
                                if (robotSpriteIndex > 2)
                                {
                                    robotSpriteIndex = 0;
                                }
                                robot.HurtSprites(robotSpriteIndex);
                                robo.Fill = robot.robotSprite;
                            }
                            if (robot.Health <= 0)
                            {
                                robotRunSounds.Stop();
                                robotLazerSounds.Stop();
                                robotSounds.Open(new Uri(@"../../GameSounds/robotdie.mp3", UriKind.Relative));
                                robotSounds.Position = new TimeSpan(0, 0, 0);
                                robotSounds.Play();
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
                                trollSpriteIndex += 0.5;
                                if(trollSpriteIndex > 6)
                                {
                                    trollOne.Health -= 1;
                                    healthProgress.Value -= 1;
                                }
                                if (trollSpriteIndex > 10)
                                {
                                    trollSpriteIndex = 1;
                                }
                                trollOne.HurtSprites(trollSpriteIndex);
                                troll.Fill = trollOne.trollSprite;
                                itemRemover.Add(y);
                            }
                            if (trollOne.Health <= 0)
                            {
                                trollSounds.Open(new Uri(@"../../GameSounds/trollDie .wma", UriKind.Relative));
                                trollSounds.Position = new TimeSpan(0, 0, 0);
                                trollSounds.Play();
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
                                ninjaBossSpriteIndex += 0.5;
                                if(ninjaBossSpriteIndex > 6)
                                {
                                    ninjaBoss.Health -= 1;
                                    healthProgress.Value -= 1;
                                }
                                if (ninjaBossSpriteIndex > 10)
                                {
                                    ninjaBossSpriteIndex = 0;
                                }
                                ninjaBoss.HurtSprites(ninjaBossSpriteIndex);
                                ninjaBow.Fill = ninjaBoss.ninjaBossSprite;
                                itemRemover.Add(y);
                            }
                            if (ninjaBoss.Health <= 0)
                            {
                                ninjaBossSounds.Open(new Uri(@"../../GameSounds/dieNinjaboss.mp3", UriKind.Relative));
                                ninjaBossSounds.Position = new TimeSpan(0, 0, 0);
                                ninjaBossSounds.Play();
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
                                ninjaSounds.Open(new Uri(@"../../GameSounds/deathNinja.mp3", UriKind.Relative));
                                ninjaSounds.Position = new TimeSpan(0, 0, 0);
                                ninjaSounds.Play();
                            }
                        }

                        if (n.Name.ToString() == "ninja2")
                        {
                            Rect ninjaHitBox2 = new Rect(Canvas.GetLeft(n), Canvas.GetTop(n), n.Width, n.Height);
                            if (bulletHitBox.IntersectsWith(ninjaHitBox2))
                            {
                                itemRemover.Add(y);
                                ninjaTwo.Health -= 1;
                                ninjaTwoSpriteIndex += 0.5;
                                if (ninjaTwoSpriteIndex > 2)
                                {
                                    ninjaTwoSpriteIndex = 1;
                                }
                                ninjaTwo.HurtSprites(ninjaTwoSpriteIndex);
                                ninja2.Fill = ninjaTwo.ninjaSprite;
                            }
                            if (ninjaTwo.Health <= 0)
                            {
                                ninjaSounds.Open(new Uri(@"../../GameSounds/deathNinja.mp3", UriKind.Relative));
                                ninjaSounds.Position = new TimeSpan(0, 0, 0);
                                ninjaSounds.Play();
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

                if ((string)x.Tag == "ground2")
                {
                    Rect groundHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(groundHitBox))
                    {
                        player.JumpSpeed = 0;
                        jumping = false;
                        Canvas.SetTop(hero, Canvas.GetTop(x) - hero.Height);
                    }
                }

                if ((string)x.Tag == "platforma")
                {
                    Rect platformHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if(!goDown)
                    {
                        if (playerHitBox.IntersectsWith(platformHitBox))
                        {
                            player.ForceJump = 8;
                            Canvas.SetTop(hero, Canvas.GetTop(x) - hero.Height);
                        }
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
                        bonusMedia.Open(new Uri(@"../../GameSounds/bonus.mp3", UriKind.Relative));
                        bonusMedia.Position = new TimeSpan(0, 0, 0);
                        bonusMedia.Play();
                        itemRemover.Add(x);
                        score += 1;
                        counterCoins += 1; //счетчик собираемых монет
                        if (score >= 7)
                        {
                            bonusMedia.Stop();
                            mediaBullets.Open(new Uri(@"../../GameSounds/coin..mp3", UriKind.Relative));
                            mediaBullets.Position = new TimeSpan(0, 0, 0);
                            mediaBullets.Play();
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
                        bonusSounds.Open(new Uri(@"../../GameSounds/bullet.mp3", UriKind.Relative));
                        bonusSounds.Position = new TimeSpan(0, 0, 0);
                        bonusSounds.Play();
                        itemRemover.Add(x);
                        bulletsScore += 10;
                    }
                }

                if ((string)x.Tag == "healths")
                {
                    Rect healthsHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(healthsHitBox))
                    {
                        bonusSounds.Open(new Uri(@"../../GameSounds/bulletsScore.mp3", UriKind.Relative));
                        bonusSounds.Position = new TimeSpan(0, 0, 0);
                        bonusSounds.Play();
                        itemRemover.Add(x);
                        player.Health += 5;
                    }
                }

                if ((string)x.Tag == "treasure")
                {
                    Rect treasureHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(treasureHitBox))
                    {
                        bonusSounds.Open(new Uri(@"../../GameSounds/treasure.mp3", UriKind.Relative));
                        bonusSounds.Position = new TimeSpan(0, 0, 0);
                        bonusSounds.Play();
                        itemRemover.Add(x);
                        gem += 1;
                    }
                }

                if ((string)x.Tag == "speeds")
                {
                    Rect speedBonusHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(speedBonusHitBox))
                    {
                        bonusSounds.Open(new Uri(@"../../GameSounds/bonusspeed.mp3", UriKind.Relative));
                        bonusSounds.Position = new TimeSpan(0, 0, 0);
                        bonusSounds.Play();
                        player.Speed = 9;
                        player.speedup = true;
                        itemRemover.Add(x);
                    }

                }


                if (x is Rectangle && (string)x.Tag == "enemyBullet")
                {
                    //1-й способ
                    //if (shooterOne.Speed < 0)
                    //    Canvas.SetLeft(x, Canvas.GetLeft(x) - speedEnemyBullet);

                    //if (shooterOne.Speed > 0)
                    //    Canvas.SetLeft(x, Canvas.GetLeft(x) + speedEnemyBullet);
                    if (Canvas.GetLeft(x) < Canvas.GetLeft(shooter1) + shooter1.Height)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - speedEnemyBullet);
                    }

                    if (Canvas.GetLeft(x) > Canvas.GetLeft(shooter1))
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + speedEnemyBullet);
                    }

                    if (Canvas.GetLeft(x) > 1400 || Canvas.GetLeft(x) < 30)
                    {
                        itemRemover.Add(x);
                    }


                    Rect enemyBulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyBulletHitBox))
                    {
                        player.Health -= 2;
                        //звук ранения игрока от пули
                        raneniePlayer.Open(new Uri(@"../../GameSounds/ranenBullet.mp3", UriKind.Relative));
                        raneniePlayer.Position = new TimeSpan(0, 0, 0);
                        raneniePlayer.Play();

                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                        itemRemover.Add(x);
                    }
                }

                if (x is Rectangle && (string)x.Tag == "enemyBullet2")
                {
                  
                    if (Canvas.GetLeft(x) < Canvas.GetLeft(shooter2) + shooter2.Height)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - speedEnemyBullet);
                    }
                       
                    if (Canvas.GetLeft(x) > Canvas.GetLeft(shooter2))
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + speedEnemyBullet);
                    }
                        

                    if (Canvas.GetLeft(x) > 3100 || Canvas.GetLeft(x) < 1630)
                    {
                        itemRemover.Add(x);
                    }

                    Rect enemyBulletHitBox2 = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyBulletHitBox2))
                    {
                        player.Health -= 2;
                        //звук ранения игрока от пули
                        raneniePlayer.Open(new Uri(@"../../GameSounds/ranenBullet.mp3", UriKind.Relative));
                        raneniePlayer.Position = new TimeSpan(0, 0, 0);
                        raneniePlayer.Play();

                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                        itemRemover.Add(x);
                    }
                }

                if (x is Rectangle && (string)x.Tag == "enemyBullet3")
                {
                    if(Canvas.GetLeft(x) < Canvas.GetLeft(terror) + terror.Height)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - speedEnemyBullet);
                    }
                    if (Canvas.GetLeft(x) > Canvas.GetLeft(terror))
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + speedEnemyBullet);
                    }

                    if (Canvas.GetLeft(x) > 2850 || Canvas.GetLeft(x) < 1730)
                    {
                        itemRemover.Add(x);
                    }

                    Rect enemyBulletHitBox3 = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyBulletHitBox3))
                    {
                        player.Health -= 2;
                        //звук ранения игрока от пули
                        raneniePlayer.Open(new Uri(@"../../GameSounds/ranenBullet.mp3", UriKind.Relative));
                        raneniePlayer.Position = new TimeSpan(0, 0, 0);
                        raneniePlayer.Play();

                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            playerSpriteIndex = 1;
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                        itemRemover.Add(x);
                    }
                }

                if (x is Rectangle && (string)x.Tag == "lazer")
                {
                    if(Canvas.GetLeft(x) < Canvas.GetLeft(robo) + robo.Height)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - speedEnemyArrows);
                    }

                    if (Canvas.GetLeft(x) > Canvas.GetLeft(robo))
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + speedEnemyArrows);
                    }


                    if (Canvas.GetLeft(x) > 3180 || Canvas.GetLeft(x) < 1630)
                    {
                        itemRemover.Add(x);
                    }

                    Rect lazerHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(lazerHitBox))
                    {
                        player.Health -= 2;
                        //звук ранения игрока 
                        raneniePlayer2.Open(new Uri(@"../../GameSounds/ranen.mp3", UriKind.Relative));
                        raneniePlayer2.Position = new TimeSpan(0, 0, 0);
                        raneniePlayer2.Play();

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
                        //звук ранения игрока 
                        raneniePlayer2.Open(new Uri(@"../../GameSounds/ranen.mp3", UriKind.Relative));
                        raneniePlayer2.Position = new TimeSpan(0, 0, 0);
                        raneniePlayer2.Play();

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
                        //звук ранения игрока 
                        raneniePlayer2.Open(new Uri(@"../../GameSounds/ranen.mp3", UriKind.Relative));
                        raneniePlayer2.Position = new TimeSpan(0, 0, 0);
                        raneniePlayer2.Play();

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
                    if (Canvas.GetTop(x) > 278)
                    {
                        itemRemover.Add(x);
                    }
                    Rect stoneHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(stoneHitBox))
                    {
                        //звук ранения игрока 
                        raneniePlayer2.Open(new Uri(@"../../GameSounds/ranen.mp3", UriKind.Relative));
                        raneniePlayer2.Position = new TimeSpan(0, 0, 0);
                        raneniePlayer2.Play();

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
                    if(!goDown)
                    {
                        if (playerHitBox.IntersectsWith(surfaceHitBox))
                        {
                            player.ForceJump = 8;
                            //player.JumpSpeed = 0;
                            Canvas.SetTop(hero, Canvas.GetTop(x) - hero.Height);
                        }
                    }
                    
                    if (x.Name.ToString() == "surface10")
                    {
                        if (playerHitBox.IntersectsWith(surfaceHitBox))
                        {
                            Canvas.SetLeft(hero, Canvas.GetLeft(hero) + horizontalSpeedPlatform2);
                        }
                    }

                    if (x.Name.ToString() == "surface14")
                    {
                        if (playerHitBox.IntersectsWith(surfaceHitBox))
                        {
                            Canvas.SetLeft(hero, Canvas.GetLeft(hero) + horizontalSpeedPlatform);
                        }
                    }
                }

                if((string)x.Tag =="bush")
                {
                    Rect bushHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(bushHitBox))
                    {
                        playerSpriteIndex += 0.5;
                        if (playerSpriteIndex > 10)
                        {
                            player.Health -= 1;
                            playerSpriteIndex = 0;
                            playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                            playerMedia.Position = new TimeSpan(0, 0, 0);
                            playerMedia.Play();
                        }
                        player.HurtSprites(playerSpriteIndex);
                        hero.Fill = player.playerSprite;
                        //player.ForceJump = 8;
                        Canvas.SetTop(hero, Canvas.GetTop(x) - hero.Height);
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
                            playerMedia.Open(new Uri(@"../../GameSounds/playerBol.mp3", UriKind.Relative));
                            playerMedia.Position = new TimeSpan(0, 0, 0);
                            playerMedia.Play();
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
                            //переключение фонового звука
                            levelEnd.Stop();
                            if (!turnOffsong)
                            {
                                //backgroundMedia.Stop();
                                backgroundMediaTwo.Play();
                            }
                            //смена фокуса фона локации уровня
                            backgroundLevel.Focusable = false;
                            backgroundLevel2.Focusable = true;
                            //сброс счетчика бонуса сокровищ и смена его цвета
                            gem = 0;
                            treasuresScore.Foreground = Brushes.White;
                            //переключения здоровья нового босса и смена цвета уровня его здоровья
                            healthProgress.Value = robot.Health;
                            healthProgress.Foreground = Brushes.Green;
                            //перемещение игрока и указателей игровых характеристик 
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
                            if (!turnOffsong)
                            {
                                backgroundMediaThree.Play();
                            }
                            backgroundLevel3.Focusable = true;
                            backgroundLevel2.Focusable = false;
                            gem = 0;
                            treasuresScore.Foreground = Brushes.White;
                            healthBoss.Foreground = Brushes.LightGray;
                            timer.Foreground = Brushes.LightGray;
                            healthProgress.Value = ninjaBoss.Health;
                            healthProgress.Minimum = 0;
                            healthProgress.Maximum = 8;
                            healthProgress.Foreground = Brushes.Red;
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
                    if (gem == 3 && ninjaBoss.Health <= 0 )
                    {
                        if (playerHitBox.IntersectsWith(chestHitBox))
                        {
                            backgroundMediaThree.Stop();
                            bearSound.Stop();
                            birdSound.Stop();
                            hogSound.Stop();
                            healthScore.Foreground = Brushes.Green;
                            ShowGameOver("Победа!!!\nСокровища\nнайдены!\nЗакройте окно \nрейтинга");
                            //открывается окно с таблицей рейтинга
                            RatingWindow ratingWindow = new RatingWindow();
                            //передача данных из игры 
                            ratingWindow.result.health = player.Health;
                            ratingWindow.result.coins = counterCoins;
                            ratingWindow.result.cartridges = counterCartridges;
                            ratingWindow.result.complexity = choiceComplexity;
                            ratingWindow.result.time = (string)timer.Content;
                            ratingWindow.Show();
                        }
                    }
                }
            }

            if (player.Health <= 0)
            {
                player.Health = 0;
                healthScore.Foreground = Brushes.Red;
                playerSpriteIndex += 0.5;
                if (playerSpriteIndex > 12)
                {
                    ShowGameOver("Вы проиграли!!!");
                    playerMedia.Open(new Uri(@"../../GameSounds/deathplayer.mp3", UriKind.Relative));
                    playerMedia.Position = new TimeSpan(0, 0, 0);
                    playerMedia.Play();
                }
                player.DieSprites(playerSpriteIndex);
                hero.Fill = player.playerSprite;
                //звук проигрыша игрока
                gameOverSound.Play();
                //отключение лишних звуков
                backgroundMedia.Stop();
                backgroundMediaTwo.Stop();
                backgroundMediaThree.Stop();
                trollWalkSounds.Stop();
                robotRunSounds.Stop();
                snakeSound.Stop();
                bearSound.Stop();
                birdSound.Stop();
                hogSound.Stop();
                raneniePlayer.Stop();
                raneniePlayer2.Stop();

            }
            
           
            if (gem == 3 && (backgroundLevel.Focusable == false && backgroundLevel3.Focusable == false && backgroundLevel2.Focusable == false))
            {
                ImageBrush entry = new ImageBrush();
                entry.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/doorOpen.png"));
                door1.Fill = entry;
                treasuresScore.Foreground = Brushes.Yellow;


                if (trollOne.Health <= 0)
                {
                    backgroundMedia.Stop();
                    levelEnd.Play();//звук об окончании первого уровня
                    treasuresScore.Content = "Сокровища: " + gem + Environment.NewLine + "Первый уровень" + Environment.NewLine + "пройден!";
                }

                if(enemy.Health > 0)
                {
                    itemRemover.Add(enemy1);
                }
                if(shooterOne.Health > 0)
                {
                    shooterSounds.Stop();
                    itemRemover.Add(shooter1);
                }
                if (wolf.Health > 0)
                {
                    itemToRemover.Add(wolf.wolfOne);
                }
            }

            if (gem == 3 && backgroundLevel2.Focusable == true)
            {
                ImageBrush doorOpen = new ImageBrush();
                doorOpen.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/doorOpen_02.png"));
                door2.Fill = doorOpen;
                treasuresScore.Foreground = Brushes.Yellow;

               
                if (robot.Health <= 0)
                {
                    backgroundMediaTwo.Stop();
                    doorOpenMedia.Play();//звук об окончании второго уровня
                    treasuresScore.Content = "Сокровища: " + gem + Environment.NewLine + "Второй уровень" + Environment.NewLine + "пройден!";
                }
                if(shooterTwo.Health > 0)
                {
                    shooterSounds.Stop();
                    itemRemover.Add(shooter2);
                }

                if(soldierOne.Health > 0)
                {
                    itemRemover.Add(soldier);
                }

                if(terrorist.Health > 0)
                {
                    itemRemover.Add(terror);
                }
                if(snake.Health > 0)
                {
                    snakeSound.Stop();
                    itemToRemover.Add(snake.snakeOne);
                }
            }

            if (gem == 3 && backgroundLevel3.Focusable == true)
            {
                ImageBrush chestImage = new ImageBrush();
                chestImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Treasure_open.png"));
                chest.Fill = chestImage;
                treasuresScore.Foreground = Brushes.Yellow;
                openBox.Play();//звук открытия сундука
                //if(ninja.Health > 0)
                //{
                //    itemRemover.Add(ninja1);
                //}
                //if(ninjaTwo.Health > 0)
                //{
                //    itemRemover.Add(ninja2);
                //}
                //if(bird.Health > 0)
                //{
                //    itemToRemover.Add(bird.birdOne);
                //}
            }

            // удаление элементов игры
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
