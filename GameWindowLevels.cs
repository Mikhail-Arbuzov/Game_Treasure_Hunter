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
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private void MotionLevelOne()
        {
            Canvas.SetLeft(platform1, Canvas.GetLeft(platform1) + horizontalSpeedPlatform); //настроил
            Canvas.SetLeft(surface14, Canvas.GetLeft(surface14) + horizontalSpeedPlatform);
            if (Canvas.GetLeft(platform1) > 604 && Canvas.GetLeft(surface14) > 604)
            {
                horizontalSpeedPlatform--;
            }

            if (Canvas.GetLeft(platform1) < 410 && Canvas.GetLeft(surface14) < 410)
            {
                horizontalSpeedPlatform++;
            }

            Canvas.SetTop(platform2, Canvas.GetTop(platform2) + verticalSpeedPlatform);
            Canvas.SetTop(surface15, Canvas.GetTop(surface15) + verticalSpeedPlatform);
            if (Canvas.GetTop(platform2) > 181 && Canvas.GetTop(surface15) > 181)
            {
                verticalSpeedPlatform = -verticalSpeedPlatform;
            }
            if (Canvas.GetTop(platform2) > 350 && Canvas.GetTop(surface15) > 350)
            {
                verticalSpeedPlatform = -verticalSpeedPlatform;
            }

            //действия первого стрелка
            if (shooterOne.Health > 0)
            {
                if (shooterOne.Shoot)
                {
                    shooterSpriteIndex += 0.5;
                    if (shooterSpriteIndex > 10)
                    {
                        if (soundsGame && backgroundLevel2.Focusable == false && backgroundLevel3.Focusable == false)
                        {
                            shooterSounds.Open(new Uri(@"../../GameSounds/shooter.mp3", UriKind.Relative));
                            shooterSounds.Position = new TimeSpan(0, 0, 0);
                            shooterSounds.Play();
                        }    
                        shooterSpriteIndex = 1;
                    }
                    shooterOne.AttackSprites(shooterSpriteIndex);
                    shooter1.Fill = shooterTwo.shooterSprite;
                }
                else
                {
                    Canvas.SetLeft(shooter1, Canvas.GetLeft(shooter1) + shooterOne.Speed);

                    shooterSpriteIndex += 0.5;
                    if (shooterSpriteIndex > 10)
                    {
                        shooterSpriteIndex = 1;
                    }
                    shooterOne.RunSprites(shooterSpriteIndex);
                    shooter1.Fill = shooterTwo.shooterSprite;

                    if (Canvas.GetLeft(shooter1) > 407)
                    {
                        shooterOne.Direction = DirectionShooter.Left;
                        shooterOne.Speed--;
                        shooter1.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }

                    if (Canvas.GetLeft(shooter1) < 151)
                    {
                        shooterOne.Direction = DirectionShooter.Right;
                        shooterOne.Speed++;
                        shooter1.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }

                shooterOne.StateFrameCounter++;
                if (shooterOne.Shoot && shooterOne.StateFrameCounter >= shooterOne.ShootStateDuration)
                {
                    shooterOne.Shoot = false;
                    shooterOne.StateFrameCounter = 0;
                    shooterSpriteIndex = 0;
                }
                else if (!shooterOne.Shoot && shooterOne.StateFrameCounter >= shooterOne.RunStatesDuration)
                {
                    shooterOne.Shoot = true;
                    shooterOne.StateFrameCounter = 0;
                    //shooterSpriteIndex = 0;
                }
            }
            else
            {
                shooterSpriteIndex +=0.5;
                //if(shooterSpriteIndex >=10)
                //{
                //    shooterSpriteIndex = 9;
                //}
                shooterOne.DieSprites(shooterSpriteIndex);
                shooter1.Fill = shooterOne.shooterSprite;
                shooterOne.countDie += 0.2;
                if (shooterOne.countDie > 14)
                {
                    itemRemover.Add(shooter1);
                }
            }
           

            //1-й способ
            //if (Canvas.GetTop(hero) < 260 && Canvas.GetTop(hero) > 145)
            //{
            //    //shooterOne.Speed = 0;
            //    shooterSpriteIndex += 0.5;
            //    if (shooterSpriteIndex > 10)
            //    {
            //        shooterSpriteIndex = 1;
            //    }
            //    shooterOne.AttackSprites(shooterSpriteIndex);
            //    shooter1.Fill = shooterOne.shooterSprite;
            //}

            //действия первого врага
            if(enemy.Health > 0)
            {
                if (enemy.IsIdle)
                {
                    enemySpriteIndex += 0.5;
                    if (enemySpriteIndex > 12)
                    {
                        enemySpriteIndex = 1;

                    }
                    enemy.IdleEnSprites(enemySpriteIndex);
                    enemy1.Fill = enemyTwo.enemySprite;
                }
                else
                {
                    Canvas.SetLeft(enemy1, Canvas.GetLeft(enemy1) + enemy.Speed);

                    enemySpriteIndex += 0.5;
                    if (enemySpriteIndex > 8)
                    {
                        enemySpriteIndex = 1;
                    }
                    enemy.RunSprites(enemySpriteIndex);
                    enemy1.Fill = enemy.enemySprite;


                    if (Canvas.GetLeft(enemy1) > 1055)
                    {
                        enemy.Speed--;
                        enemy1.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }

                    if (Canvas.GetLeft(enemy1) < 799)
                    {
                        enemy.Speed++;
                        enemy1.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }
                enemy.StateFrameCounter++;
                if (enemy.IsIdle && enemy.StateFrameCounter >= enemy.IdleStateDuration)
                {
                    enemy.IsIdle = false;
                    enemy.StateFrameCounter = 0;
                    enemySpriteIndex = 0;
                }
                else if (!enemy.IsIdle && enemy.StateFrameCounter >= enemy.OtherStatesDuration)
                {
                    enemy.IsIdle = true;
                    enemy.StateFrameCounter = 0;
                }
            }
            else
            {
                enemySpriteIndex +=0.5;
                if (enemySpriteIndex > 9)
                {
                    itemRemover.Add(enemy1);
                   
                }
                enemy.DieSprites(enemySpriteIndex);
                enemy1.Fill = enemy.enemySprite;
            }

          
            //движение волка
            foreach (var w in MyCanvas.Children.OfType<Image>())
            {
                if (wolf.Health > 0)
                {
                    if ((string)w.Tag == "wolf")
                    {
                        Canvas.SetLeft(w, Canvas.GetLeft(w) + wolf.Speed);

                        wolf.MoveRight();
                        //if (Canvas.GetLeft(w) < Canvas.GetLeft(ground4) || Canvas.GetLeft(w) + w.Width > Canvas.GetLeft(ground4) + ground4.Width)
                        //{
                        //    wolf.Speed = -wolf.Speed;
                        //}

                        if (Canvas.GetLeft(w) > 1403)
                        {
                            wolf.Speed--;
                            wolf.wolfOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                        }
                        if (Canvas.GetLeft(w) < 1012)
                        {
                            wolf.Speed++;
                            wolf.wolfOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                        }

                    }
                }
                else
                {
                    wolf.countActions++;
                    wolf.DieWolf();// гибель волка
                    if(wolf.countActions >= 100)
                    {
                        itemToRemover.Add(wolf.wolfOne);
                    }
                    if (wolf.countActions >= 600)
                    { 
                        wolf.countActions = 0; 
                    }
                }
                

            }

            //действия Троля
            if(trollOne.Health > 0)
            {
                if (trollOne.Idle)
                {
                    trollWalkSounds.Stop();
                    trollSpriteIndex += 0.5;
                    if (trollSpriteIndex > 10)
                    {
                        trollSpriteIndex = 1;
                    }
                    trollOne.IdleTrSprites(trollSpriteIndex);
                    troll.Fill = trollOne.trollSprite;
                    if (soundsGame)//звук рыка троля
                    {
                        trollIdleSounds.Play();
                    }
                }
                else
                {
                    trollIdleSounds.Stop();
                    Canvas.SetLeft(troll, Canvas.GetLeft(troll) + trollOne.Speed);
                    trollSpriteIndex +=0.5;
                    if (trollSpriteIndex > 10)
                    {
                        trollSpriteIndex = 0;
                    }
                    trollOne.RunSprites(trollSpriteIndex);
                    troll.Fill = trollOne.trollSprite;
                    if (soundsGame && !soundTrollRun)//звук хотьбы троля
                    {
                        trollWalkSounds.Play();
                        if(trollWalkSounds.Position >= new TimeSpan(0,0,8))
                        {
                            trollWalkSounds.Position = new TimeSpan(0, 0, 1);
                            trollWalkSounds.Play();
                        }
                    }

                    if (Canvas.GetLeft(troll) > 1339)
                    {
                        trollOne.Speed--;
                        troll.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }

                    if (Canvas.GetLeft(troll) < 784)
                    {
                        trollOne.Speed++;
                        troll.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }

                trollOne.StateFrameCounter++;
                if (trollOne.Idle && trollOne.StateFrameCounter >= trollOne.IdleStateDuration)
                {
                    trollOne.Idle = false;
                    trollOne.StateFrameCounter = 0;
                    trollSpriteIndex = 0;
                }
                else if (!trollOne.Idle && trollOne.StateFrameCounter >= trollOne.WalkStatesDuration)
                {
                    trollOne.Idle = true;
                    trollOne.StateFrameCounter = 0;
                }
            }
            else
            {
                trollSpriteIndex+= 0.5;
                //if (trollSpriteIndex >= 10)
                //{
                //    trollSpriteIndex = 9;
                //    //itemRemover.Add(troll);
                //}
                trollOne.DieSprites(trollSpriteIndex);
                troll.Fill = trollOne.trollSprite;
                trollOne.countDie += 0.2;
                if (trollOne.countDie > 14)
                {
                    itemRemover.Add(troll);
                }
            }
           

            //снег
            snowSpriteIndex += 0.5;
            if (snowSpriteIndex > 3)
            {
                snowSpriteIndex = 0;
            }
            SnowFalls(snowSpriteIndex);

            //камнепад
            stoneCounter--;
            if (stoneCounter < 0)
            {
                MakeStones();
                stoneCounter = limit;
            }

        }

        private void MotionLevelTwo()
        {
            Canvas.SetTop(spike5, Canvas.GetTop(spike5) - verticalSpeedSpike);

            if (Canvas.GetTop(spike5) < 656)
            {
                verticalSpeedSpike--;
            }


            if (Canvas.GetTop(spike5) > 731)
            {
                verticalSpeedSpike++;
            }

            Canvas.SetTop(spike6, Canvas.GetTop(spike6) + verticalSpeedSpike);

            if (Canvas.GetTop(spike6) > 731)
            {
                verticalSpeedSpike--;
            }

            if (Canvas.GetTop(spike6) < 656)
            {
                verticalSpeedSpike++;
            }

            //действия солдата
            if(soldierOne.Health > 0)
            {
                if (soldierOne.Idle)
                {
                    soldierSpriteIndex += 0.5;
                    if (soldierSpriteIndex > 10)
                    {
                        soldierSpriteIndex = 1;
                    }
                    soldierOne.IdleSoSprites(soldierSpriteIndex);
                    soldier.Fill = soldierOne.soldierSprite;
                }
                else
                {
                    Canvas.SetLeft(soldier, Canvas.GetLeft(soldier) + soldierOne.Speed);

                    soldierSpriteIndex += 0.5;
                    if (soldierSpriteIndex > 10)
                    {
                        soldierSpriteIndex = 1;
                    }
                    soldierOne.RunSprites(soldierSpriteIndex);
                    soldier.Fill = soldierOne.soldierSprite;

                    if (Canvas.GetLeft(soldier) > 2175)
                    {
                        soldierOne.Speed--;
                        soldier.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }

                    if (Canvas.GetLeft(soldier) < 1983)
                    {
                        soldierOne.Speed++;
                        soldier.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }

                soldierOne.StateFrameCounter++;
                if (soldierOne.Idle && soldierOne.StateFrameCounter >= soldierOne.IdleStatesDuration)
                {
                    soldierOne.Idle = false;
                    soldierOne.StateFrameCounter = 0;
                    soldierSpriteIndex = 0;
                }
                else if (!soldierOne.Idle && soldierOne.StateFrameCounter >= soldierOne.RunStatesDuration)
                {
                    soldierOne.Idle = true;
                    soldierOne.StateFrameCounter = 0;
                }
            }
            else
            {
                soldierSpriteIndex += 0.5;
                soldierOne.DieSprites(soldierSpriteIndex);
                soldier.Fill = soldierOne.soldierSprite;
                soldierOne.countDie += 0.2;
                if(soldierOne.countDie > 14)
                {
                    itemRemover.Add(soldier);
                }
                
            }


            //действия второго стрелка
            if (shooterTwo.Health > 0)
            {
                if (shooterTwo.Shoot)
                {
                    shooterSpriteIndex += 0.5;
                    if (shooterSpriteIndex > 10)
                    {
                        if(backgroundLevel2.Focusable == true && shooterTwo.Health > 0)
                        {
                            shooterSounds.Open(new Uri(@"../../GameSounds/shooter.mp3", UriKind.Relative));
                            shooterSounds.Position = new TimeSpan(0, 0, 0);
                            shooterSounds.Play();
                        }
                        
                        shooterSpriteIndex = 1;
                    }
                    shooterTwo.AttackSprites(shooterSpriteIndex);
                    shooter2.Fill = shooterTwo.shooterSprite;
                }
                else
                {
                    Canvas.SetLeft(shooter2, Canvas.GetLeft(shooter2) + shooterTwo.Speed);

                    shooterSpriteIndex += 0.5;
                    if (shooterSpriteIndex > 10)
                    {
                        shooterSpriteIndex = 1;
                    }
                    shooterTwo.RunSprites(shooterSpriteIndex);
                    shooter2.Fill = shooterTwo.shooterSprite;

                    if (Canvas.GetLeft(shooter2) > 2136)
                    {
                        shooterTwo.Direction = DirectionShooter.Left;
                        shooterTwo.Speed--;
                        shooter2.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }

                    if (Canvas.GetLeft(shooter2) < 1624)
                    {
                        shooterTwo.Direction = DirectionShooter.Right;
                        shooterTwo.Speed++;
                        shooter2.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }

                shooterTwo.StateFrameCounter++;
                if (shooterTwo.Shoot && shooterTwo.StateFrameCounter >= shooterTwo.ShootStateDuration)
                {
                    shooterTwo.Shoot = false;
                    shooterTwo.StateFrameCounter = 0;
                    shooterSpriteIndex = 0;
                }
                else if (!shooterTwo.Shoot && shooterTwo.StateFrameCounter >= shooterTwo.RunStatesDuration)
                {
                    shooterTwo.Shoot = true;
                    shooterTwo.StateFrameCounter = 0;
                    //shooterSpriteIndex = 0;
                }
            }
            else
            {
                shooterSpriteIndex += 0.5;
                shooterTwo.DieSprites(shooterSpriteIndex);
                shooter2.Fill = shooterTwo.shooterSprite;
                shooterTwo.countDie += 0.2;
                if (shooterTwo.countDie > 14)
                {
                    itemRemover.Add(shooter2);
                }
            }

            //if (Canvas.GetTop(hero) < 400 && Canvas.GetTop(hero) > 120)
            //{
            //    //shooterTwo.Speed = 0;
            //    shooterSpriteIndex += 0.5;
            //    if (shooterSpriteIndex > 10)
            //    {
            //        shooterSpriteIndex = 1;
            //    }
            //    shooterTwo.AttackSprites(shooterSpriteIndex);
            //    shooter2.Fill = shooterTwo.shooterSprite;
            //}

            // действия террориста
            if (terrorist.Health > 0)
            {
                if (terrorist.Shoot)
                {
                    if (soundsGame && backgroundLevel2.Focusable == true && terrorist.Health > 0)
                    {
                        terroristShootSounds.Play();//стрельба из автомата 
                        if (terroristShootSounds.Position >= new TimeSpan(0, 0, 3))
                        {
                            terroristShootSounds.Position = new TimeSpan(0, 0, 0);
                            terroristShootSounds.Play();
                        }
                         
                    }
                    terroristSpriteIndex += 0.5;
                    if (terroristSpriteIndex > 4)
                    {
                        terroristSpriteIndex = 0;
                    }
                    terrorist.ShootSprites(terroristSpriteIndex);
                    terror.Fill = terrorist.terroristSprite;
                }
                else
                {
                    Canvas.SetLeft(terror, Canvas.GetLeft(terror) + terrorist.Speed);

                    terroristSpriteIndex += 0.5;
                    if (terroristSpriteIndex > 8)
                    {
                        terroristSpriteIndex = 1;
                    }
                    terrorist.RunSprites(terroristSpriteIndex);
                    terror.Fill = terrorist.terroristSprite;


                    if (Canvas.GetLeft(terror) > 2438)
                    {
                        terrorist.Direction = DirectionTerrorist.Left;
                        terrorist.Speed--;
                        terror.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }

                    if (Canvas.GetLeft(terror) < 1782)
                    {
                        terrorist.Direction = DirectionTerrorist.Right;
                        terrorist.Speed++;
                        terror.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }

                terrorist.StateFrameCounter++;
                if (terrorist.Shoot && terrorist.StateFrameCounter >= terrorist.ShootStateDuration)
                {
                    terrorist.Shoot = false;
                    terrorist.StateFrameCounter = 0;
                    terroristSpriteIndex = 0;
                }
                else if (!terrorist.Shoot && terrorist.StateFrameCounter >= terrorist.RunStatesDuration)
                {
                    terrorist.Shoot = true;
                    terrorist.StateFrameCounter = 0;
                }
            }
            else
            {
                terroristSpriteIndex += 0.5;
                if (terroristSpriteIndex > 10)
                {
                    itemRemover.Add(terror);
                }
                terrorist.DieSprites(terroristSpriteIndex);
                terror.Fill = terrorist.terroristSprite;
                //terrorist.countDie += 0.2;
                //if(terrorist.countDie > 13)
                //{
                //    itemRemover.Add(terror);
                //}  
            }

            //действия робота
            if(robot.Health > 0)
            {
                if (robot.Shoot)
                {
                    robotRunSounds.Stop();
                    robotSpriteIndex += 0.5;
                    if (robotSpriteIndex > 16)
                    {
                        if(backgroundLevel2.Focusable == true)//звук лазера
                        {
                            robotLazerSounds.Open(new Uri(@"../../GameSounds/blaster.mp3", UriKind.Relative));
                            robotLazerSounds.Position = new TimeSpan(0, 0, 0);
                            robotLazerSounds.Play();
                        }
                        robotSpriteIndex = 1;
                    }
                    robot.LazerSprites(robotSpriteIndex);
                    robo.Fill = robot.robotSprite;
                }
                else
                {
                    Canvas.SetLeft(robo, Canvas.GetLeft(robo) + robot.Speed);
                    robotSpriteIndex += 0.5;
                    if (robotSpriteIndex > 12)
                    {
                        robotSpriteIndex = 1;
                    }
                    robot.RunSprites(robotSpriteIndex);
                    robo.Fill = robot.robotSprite;
                    
                    if (soundsGame && backgroundLevel2.Focusable == true && robot.Health > 0)//звук бега робота
                    {
                        robotRunSounds.Play();
                        if(robotRunSounds.Position >= new TimeSpan(0,0,14))
                        {
                            robotRunSounds.Position = new TimeSpan(0, 0, 1);
                            robotRunSounds.Play();
                        }
                    }    
                    if (Canvas.GetLeft(robo) > 2921)
                    {
                        robot.Direction = DirectionRobot.Left;
                        robot.Speed--;
                        robo.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }

                    if (Canvas.GetLeft(robo) < 2537)
                    {
                        robot.Direction = DirectionRobot.Right;
                        robot.Speed++;
                        robo.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }
                robot.StateFrameCounter++;
                if (robot.Shoot && robot.StateFrameCounter >= robot.ShootStateDuration)
                {
                    robot.Shoot = false;
                    robot.StateFrameCounter = 0;
                    robotSpriteIndex = 0;
                }
                else if (!robot.Shoot && robot.StateFrameCounter >= robot.RunStatesDuration)
                {
                    robot.Shoot = true;
                    robot.StateFrameCounter = 0;
                }
            }
            else
            {
                robotSpriteIndex +=0.5;
                if (robotSpriteIndex > 19)
                {
                    itemRemover.Add(robo);
                }
                robot.DieSprites(robotSpriteIndex);
                robo.Fill = robot.robotSprite;
            }

            foreach (var s in MyCanvas.Children.OfType<Image>())
            {
                if ((string)s.Tag == "snake")
                {
                    Canvas.SetLeft(s, Canvas.GetLeft(s) + snake.Speed);
                    snake.MoveRight();
                    if (soundsGame && backgroundLevel2.Focusable == true && snake.Health > 0)//шипения змеи когда ползет
                    {
                        snakeSound.Play();
                        if (snakeSound.Position >= new TimeSpan(0, 0, 9))
                        {
                            snakeSound.Position = new TimeSpan(0, 0, 0);
                            snakeSound.Play();
                        }
                    }
                    //if(Canvas.GetLeft(s) < 3097 || Canvas.GetLeft(s) > 2671)
                    //{
                    //    snake.Speed = -snake.Speed;
                    //}

                        if (Canvas.GetLeft(s) > 3097)
                    {
                        snake.Speed--;
                        snake.snakeOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }
                    if (Canvas.GetLeft(s) < 2671)
                    {
                        snake.Speed++;
                        snake.snakeOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }
            }
        }

        private void MotionLevelThree()
        {
            Canvas.SetTop(platform9, Canvas.GetTop(platform9) - verticalSpeedPlatform);
            Canvas.SetTop(surface17, Canvas.GetTop(surface17) - verticalSpeedPlatform);
            if (Canvas.GetTop(platform9) < 381 || Canvas.GetTop(platform9) > 181 && Canvas.GetTop(surface17) < 381 || Canvas.GetTop(surface17) > 181)
            {
                verticalSpeedPlatform = -verticalSpeedPlatform;
            }
            Canvas.SetLeft(platform10, Canvas.GetLeft(platform10) + horizontalSpeedPlatform2);
            Canvas.SetLeft(surface10, Canvas.GetLeft(surface10) + horizontalSpeedPlatform2);
           
            if (Canvas.GetLeft(platform10) > 4067 && Canvas.GetLeft(surface10) > 4067)
            {
                horizontalSpeedPlatform2--;
            }

            if (Canvas.GetLeft(platform10) < 3547 && Canvas.GetLeft(surface10) < 3547)
            {
                horizontalSpeedPlatform2++;
            }

            foreach (var b in MyCanvas.Children.OfType<Image>())
            {
                if ((string)b.Tag == "bird")
                {
                    Canvas.SetLeft(b, Canvas.GetLeft(b) + bird.Speed);
                    bird.MoveRight();
                    if (soundsGame && backgroundLevel3.Focusable == true && bird.Health > 0)
                    {
                        birdSound.Play();//карканье птицы
                        if(birdSound.Position >= new TimeSpan(0,0,6))
                        {
                            birdSound.Position = new TimeSpan(0, 0, 0);
                            birdSound.Play();
                        }
                    }
                    if (Canvas.GetLeft(b) > 4685)
                    {
                        Canvas.SetLeft(b, 3227);
                        //bird.Speed +=2;

                    }
                }

                if ((string)b.Tag == "bear")
                {
                    Canvas.SetLeft(b, Canvas.GetLeft(b) - bear.Speed);
                    bear.MoveLeft();
                    if (soundsGame && backgroundLevel3.Focusable == true && bear.Health > 0)
                    {
                        bearSound.Play();//дыхание медведя при хотьбе
                        if (bearSound.Position >= new TimeSpan(0,0,5))
                        {
                            bearSound.Position = new TimeSpan(0, 0, 0);
                            bearSound.Play();
                        }
                    }

                    if (Canvas.GetLeft(b) > 4645)
                    {
                        bear.Speed++;
                        bear.bearOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };

                    }

                    if (Canvas.GetLeft(b) < 4400)
                    {
                        bear.Speed--;
                        bear.bearOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                        //bear.MoveRight();
                    }
                    //if (Canvas.GetLeft(b) < Canvas.GetLeft(ground13) || Canvas.GetLeft(b) + b.Width > Canvas.GetLeft(ground13) + ground13.Width)
                    //{
                    //    bear.Speed = -bear.Speed;
                    //}

                    ////if (Canvas.GetLeft(b) < Canvas.GetLeft(ground13))
                    ////{
                    ////    bear.BearCurrentFrame--;
                    ////    bear.MoveRight();
                    ////}

                    //if (Canvas.GetLeft(b) + b.Width > Canvas.GetLeft(ground13) + ground13.Width)
                    //{
                    //    //bear.BearCurrentFrame++;
                    //    bear.MoveLeft();
                    //}
                }

                if ((string)b.Tag == "hog")
                {
                    Canvas.SetLeft(b, Canvas.GetLeft(b) + hog.Speed);
                    hog.MoveRight();
                    if (soundsGame && backgroundLevel3.Focusable == true && hog.Health > 0)
                    {
                        hogSound.Play();// звук кабана
                        if(hogSound.Position >= new TimeSpan(0,0,15))
                        {
                            hogSound.Position = new TimeSpan(0, 0, 1);
                            hogSound.Play();
                        }
                    }
                    if(Canvas.GetLeft(b) > 4443)
                    {
                        hog.Speed--;
                        hog.hogOne.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }

                    if (Canvas.GetLeft(b) < 4280)
                    {
                        hog.Speed++;
                        hog.hogOne.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }

                    //    if (Canvas.GetLeft(b) < Canvas.GetLeft(ground10) || Canvas.GetLeft(b) + b.Width > Canvas.GetLeft(ground10) + ground10.Width)
                    //    {
                    //        hog.Speed = -hog.Speed;
                    //    }

                    //    if (Canvas.GetLeft(b) < Canvas.GetLeft(ground10))
                    //    {
                    //        hog.MoveRight();
                    //    }

                    //    if (Canvas.GetLeft(b) + b.Width > Canvas.GetLeft(ground10) + ground10.Width)
                    //    {
                    //        hog.MoveLeft();
                    //    }
                }
            }
           
            //смена спрайтов персонажей взависимости от пересечения персонажей определенной позиции


            //foreach (var element in MyCanvas.Children.OfType<Rectangle>())
            //{
            //    if (element.Name.ToString() == "enemy2")
            //    {

            //        Rect enHitBox2 = new Rect(Canvas.GetLeft(element), Canvas.GetTop(element), element.Width, element.Height);
            //        Rect positionHitBox4 = new Rect(Canvas.GetLeft(position4), Canvas.GetTop(position4), position4.Width, position4.Height);
            //        if (enHitBox2.IntersectsWith(positionHitBox4))
            //        {
            //            //enemyTwo.Speed = 0;
            //            enemySpriteIndex += 0.5;
            //            if (enemySpriteIndex > 12)
            //            {
            //                enemySpriteIndex = 1;
            //                //enemyTwo.Speed = 1;
            //            }
            //            enemyTwo.IdleEnSprites(enemySpriteIndex);
            //            enemy2.Fill = enemyTwo.enemySprite;
            //        }

            //    }
            //}

            // смена действий второго врага
            if(enemyTwo.Health > 0)
            {
                if (enemyTwo.IsIdle)
                {
                    if (enemySpriteIndex > 12)
                    {
                        enemySpriteIndex = 1;

                    }
                    enemyTwo.IdleEnSprites(enemySpriteIndex);
                    enemy2.Fill = enemyTwo.enemySprite;
                }
                else
                {
                    Canvas.SetLeft(enemy2, Canvas.GetLeft(enemy2) + enemyTwo.Speed);

                    enemySpriteIndex += 0.5;
                    if (enemySpriteIndex > 8)
                    {
                        enemySpriteIndex = 1;
                    }
                    enemyTwo.RunSprites(enemySpriteIndex);
                    enemy2.Fill = enemyTwo.enemySprite;

                    if (Canvas.GetLeft(enemy2) > 4139)
                    {
                        enemyTwo.Speed--;
                        enemy2.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }

                    if (Canvas.GetLeft(enemy2) < 3459)
                    {
                        enemyTwo.Speed++;
                        enemy2.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }
                enemyTwo.StateFrameCounter++;
                if (enemyTwo.IsIdle && enemyTwo.StateFrameCounter >= enemyTwo.IdleStateDuration)
                {
                    enemyTwo.IsIdle = false;
                    enemyTwo.StateFrameCounter = 0;
                    enemySpriteIndex = 0;
                }
                else if (!enemyTwo.IsIdle && enemyTwo.StateFrameCounter >= enemyTwo.OtherStatesDuration)
                {
                    enemyTwo.IsIdle = true;
                    enemyTwo.StateFrameCounter = 0;
                    enemySpriteIndex += 0.5;
                }
            }
            else
            {
                enemySpriteIndex += 0.5;
                if (enemySpriteIndex > 11)
                {
                    itemRemover.Add(enemy2);
                }
                enemyTwo.DieSprites(enemySpriteIndex);
                enemy2.Fill = enemyTwo.enemySprite;
            }

            //смена действий второго ниндзи
            if (ninjaTwo.Health > 0)
            {
                if (ninjaTwo.Throw)
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
                else
                {
                    Canvas.SetLeft(ninja2, Canvas.GetLeft(ninja2) + ninjaTwo.Speed);

                    ninjaTwoSpriteIndex += 0.5;
                    if (ninjaTwoSpriteIndex > 10)
                    {
                        ninjaTwoSpriteIndex = 1;
                    }
                    ninjaTwo.RunSprites(ninjaTwoSpriteIndex);
                    ninja2.Fill = ninjaTwo.ninjaSprite;

                    if (Canvas.GetLeft(ninja2) > 4669)
                    {
                        ninjaTwo.Direction = DirectionNinja.Left;
                        ninjaTwo.Speed--;
                        ninja2.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }

                    if (Canvas.GetLeft(ninja2) < 4416)
                    {
                        ninjaTwo.Direction = DirectionNinja.Right;
                        ninjaTwo.Speed++;
                        ninja2.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }

                ninjaTwo.StateFrameCounter++;
                if (ninjaTwo.Throw && ninjaTwo.StateFrameCounter >= ninjaTwo.ThrowStateDuration)
                {
                    ninjaTwo.Throw = false;
                    ninjaTwo.StateFrameCounter = 0;
                    ninjaTwoSpriteIndex = 0;
                }
                else if (!ninjaTwo.Throw && ninjaTwo.StateFrameCounter >= ninjaTwo.RunStatesDuration)
                {
                    ninjaTwo.Throw = true;
                    ninjaTwo.StateFrameCounter = 0;
                    //ninjaTwoSpriteIndex += 0.5;
                }
            }
            else
            {
                ninjaTwoSpriteIndex += 0.5;
                if(ninjaTwoSpriteIndex > 14)
                {
                   itemRemover.Add(ninja2);
                }
                ninjaTwo.DieSprites(ninjaTwoSpriteIndex);
                ninja2.Fill = ninjaTwo.ninjaSprite;
                //ninjaTwo.countDie += 0.2;
                //if(ninjaTwo.countDie > 14)
                //{
                //    itemRemover.Add(ninja2);
                //}
                
            }
           

            //смена действий  ниндзи босса
           
            if(ninjaBoss.Health > 0)
            {
                if (ninjaBoss.Shoot)
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

                else if (ninjaBoss.Idles)
                {
                    ninjaBossSpriteIndex += 0.5;
                    if (ninjaBossSpriteIndex > 12)
                    {
                        ninjaBossSpriteIndex = 1;
                    }
                    ninjaBoss.IdleNiSprites(ninjaBossSpriteIndex);
                    ninjaBow.Fill = ninjaBoss.ninjaBossSprite;
                }

                else
                {
                    Canvas.SetLeft(ninjaBow, Canvas.GetLeft(ninjaBow) + ninjaBoss.Speed);

                    ninjaBossSpriteIndex += 0.5;
                    if (ninjaBossSpriteIndex > 16)
                    {
                        ninjaBossSpriteIndex = 1;
                    }
                    ninjaBoss.RunSprites(ninjaBossSpriteIndex);
                    ninjaBow.Fill = ninjaBoss.ninjaBossSprite;

                    if (Canvas.GetLeft(ninjaBow) > 3990)
                    {
                        ninjaBoss.Direction = DirectionNinjaBoss.Left;
                        ninjaBoss.Speed--;
                        ninjaBow.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }
                    if (Canvas.GetLeft(ninjaBow) < 3290)
                    {
                        ninjaBoss.Direction = DirectionNinjaBoss.Right;
                        ninjaBoss.Speed++;
                        ninjaBow.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }


                ninjaBoss.StateFrameCounter++;
                ninjaBoss.StateFrameCounter2 += 0.5;


                if (ninjaBoss.Duration && ninjaBoss.StateFrameCounter2 >= 100)
                {
                    ninjaBoss.Duration = false;
                    ninjaBoss.StateFrameCounter2 = 0;
                    if (ninjaBoss.Shoot && ninjaBoss.StateFrameCounter >= ninjaBoss.ShootStateDuration)
                    {
                        ninjaBoss.Shoot = false;
                        ninjaBoss.StateFrameCounter = 0;
                        ninjaBossSpriteIndex = 0;
                    }
                    else if (!ninjaBoss.Shoot && ninjaBoss.StateFrameCounter >= ninjaBoss.RunStateDuration)
                    {
                        ninjaBoss.Shoot = true;
                        ninjaBoss.StateFrameCounter = 0;
                    }
                }

                if (!ninjaBoss.Duration && ninjaBoss.StateFrameCounter2 >= 100)
                {
                    ninjaBoss.Duration = true;
                    ninjaBoss.StateFrameCounter2 = 0;
                    if (ninjaBoss.Idles && ninjaBoss.StateFrameCounter >= ninjaBoss.IdleStateDuration)
                    {
                        ninjaBoss.Idles = false;
                        ninjaBoss.StateFrameCounter = 0;
                        ninjaBossSpriteIndex = 0;
                    }
                    else if (!ninjaBoss.Idles && ninjaBoss.StateFrameCounter >= ninjaBoss.RunStateDuration)
                    {
                        ninjaBoss.Idles = true;
                        ninjaBoss.StateFrameCounter = 0;
                    }
                }
            }
            else
            {
                ninjaBossSpriteIndex += 0.5;
                if (ninjaBossSpriteIndex > 19)
                {
                    itemRemover.Add(ninjaBow);
                }
                ninjaBoss.DieSprites(ninjaBossSpriteIndex);
                ninjaBow.Fill = ninjaBoss.ninjaBossSprite;
            }
           
           

            //int ninjaBossState = rand.Next(1, 2);
            //switch(ninjaBossState)
            //{
            //    case 1:
            //        if (ninjaBoss.Idles && ninjaBoss.StateFrameCounter >= ninjaBoss.IdleStateDuration)
            //        {
            //            //ninjaBoss.Shoot = true;
            //            ninjaBoss.Idles = false;
            //            //ninjaBoss.State = State.Running;
            //            ninjaTwo.StateFrameCounter = 0;
            //            ninjaBossSpriteIndex = 0;
            //        }
            //        else if (!ninjaBoss.Idles && ninjaBoss.StateFrameCounter >= ninjaBoss.RunStateDuration)
            //        {
            //            //ninjaBoss.Shoot = false;
            //            ninjaBoss.Idles = true;
            //            //ninjaBoss.State = State.Inactive;
            //            ninjaTwo.StateFrameCounter = 0;
            //            //ninjaBossSpriteIndex = 0;
            //        }
            //        break;
            //    case 2:
            //        if (ninjaBoss.Shoot && ninjaBoss.StateFrameCounter >= ninjaBoss.ShootStateDuration)
            //        {
            //            ninjaBoss.Shoot = false;
            //            //ninjaBoss.Idles = false;
            //            //ninjaBoss.State = State.Shooting;
            //            ninjaTwo.StateFrameCounter = 0;
            //            ninjaBossSpriteIndex = 0;
            //        }
            //        else if (!ninjaBoss.Shoot && ninjaBoss.StateFrameCounter >= ninjaBoss.RunStateDuration)
            //        {
            //            ninjaBoss.Shoot = true;
            //            ninjaTwo.StateFrameCounter = 0;
            //        }
            //        break;
            //}



            //бег первого ниндзи
            if(ninja.Health > 0)
            {
                Canvas.SetLeft(ninja1, Canvas.GetLeft(ninja1) + ninja.Speed);
                ninjaSpriteIndex += 0.5;
                if (ninjaSpriteIndex > 10)
                {
                    ninjaSpriteIndex = 1;
                }
                ninja.RunSprites(ninjaSpriteIndex);
                ninja1.Fill = ninja.ninjaSprite;

                if (Canvas.GetLeft(ninja1) > 3645)
                {
                    ninja.Speed--;
                    ninja1.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                }

                if (Canvas.GetLeft(ninja1) < 3259)
                {
                    ninja.Speed++;
                    ninja1.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                }
            }
            else
            {
                ninjaSpriteIndex += 0.5;
                if (ninjaSpriteIndex > 14)
                {
                    itemRemover.Add(ninja1);
                }
                ninja.DieSprites(ninjaSpriteIndex);
                ninja1.Fill = ninja.ninjaSprite;
                //ninja.countDie += 0.2;
                //if (ninja.countDie > 14)
                //{
                //    itemRemover.Add(ninja1);
                //}
            }

        }
    }
}
