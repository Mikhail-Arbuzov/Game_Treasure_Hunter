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
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight;
        bool gameOver;
        bool jumping;
        double speedBullet = 10;
        Rect playerHitBox;// для проверки пересечения с другими игровыми объектами
        //для стирания элементов игры
        List<Rectangle> itemRemover = new List<Rectangle>();
        List<Image> itemToRemover = new List<Image>();
        //камнепад 
        Random rand = new Random();
        int stoneSpriteCounter = 0;
        int stoneCounter = 50;
        int limit = 25;
        int stoneSpeed = 10;
        //для движения платформ
        int horizontalSpeedPlatform = 3;
        int verticalSpeedPlatform = 3;
        double verticalSpeedSpike = 0.5;
        //для создания пулей врагов
        double speedEnemyBullet = 10;
        int bulletTimer = 0;
        int bulletTimerLimit = 90;
        //для переключения спрайтов
        double playerSpriteIndex = 0;
        double enemySpriteIndex = 0;
        double soldierSpriteIndex = 0;
        double shooterSpriteIndex = 0;
        double terroristSpriteIndex = 0;
        double ninjaSpriteIndex = 0;
        double ninjaTwoSpriteIndex = 0;
        double ninjaBossSpriteIndex = 0;
        double robotSpriteIndex = 0;
        double trollSpriteIndex = 0;
        double snowSpriteIndex = 0;
        //счетчики бонусов 
        int score;
        int bulletsScore;
        int gem;

        Player player;
        Wolf wolf;
        Bear bear;
        Hog hog;
        Bird bird;
        Snake snake;
        Ninja ninja;
        Ninja ninjaTwo;
        Enemy enemy;
        Enemy enemyTwo;
        Shooter shooterOne;
        Shooter shooterTwo;
        Soldier soldierOne;
        Terrorist terrorist;
        NinjaBoss ninjaBoss;
        Robot robot;
        Troll trollOne;


        public GameWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();
            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            LoadingBackground();
            player = new Player();
            ninja = new Ninja();
            ninjaTwo = new Ninja();
            enemy = new Enemy();
            enemyTwo = new Enemy();
            shooterOne = new Shooter();
            shooterTwo = new Shooter();
            soldierOne = new Soldier();
            terrorist = new Terrorist();
            ninjaBoss = new NinjaBoss();
            robot = new Robot();
            trollOne = new Troll();
            wolf = new Wolf("pack://application:,,,/Sprites/SpritesWolf/Wolf.png", 141, 191, 98, "wolf", 3, 0);
            bear = new Bear("pack://application:,,,/Sprites/SpritesBear/m_Bear.png", 180, 135, 160, "bear", 8, 0);
            hog = new Hog("pack://application:,,,/Sprites/SpritesHog/m_zp.png", 120, 192, 120, "hog", 3, 0);
            bird = new Bird("pack://application:,,,/Sprites/SpritesBird/Bird_Black.png", 0, 96, 96, "bird", 4, 0);
            snake = new Snake("pack://application:,,,/Sprites/SpritesSnake/snake.png", 80, 80, 80, "snake", 4, 0);
            wolf.UploadingImage();
            wolf.InitPictures();
            MyCanvas.Children.Add(wolf.wolfOne);
            snake.UploadingImage();
            snake.InitPictures();
            MyCanvas.Children.Add(snake.snakeOne);
            bear.UploadingImage();
            bear.InitPictures();
            MyCanvas.Children.Add(bear.bearOne);
            hog.UploadingImage();
            hog.InitPictures();
            MyCanvas.Children.Add(hog.hogOne);
            bird.UploadingImage();
            bird.InitPictures();
            MyCanvas.Children.Add(bird.birdOne);

            StartGame();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            healthScore.Content = "Здоровье: " + player.Health;
            coinScore.Content = "Монеты: " + score;
            catridgesScore.Content = "Патроны: " + bulletsScore;
            treasuresScore.Content = "Сокровища: " + gem;

            // чтоб игрок не выходил за экран
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
                    //goLeft = false;
                }
            }

            //if (backgroundLevel.Focusable == false)
            //{
            //    if (Canvas.GetLeft(hero) >= 4660)
            //    {
            //        Canvas.SetLeft(hero, Canvas.GetLeft(hero) - 10);
            //        //goRight = false;
            //    }
            //}
            //if (Canvas.GetLeft(backgroundLevel2) > 3200)
            //{
            //    Canvas.SetLeft(hero, Canvas.GetLeft(hero) - 10);
            //    goRight = false;
            //}
            //if (Canvas.GetLeft(backgroundLevel2) < 1600)
            //{
            //    Canvas.SetLeft(hero, Canvas.GetLeft(hero) + 10);
            //    goLeft = false;
            //}
            //if (Canvas.GetLeft(backgroundLevel3) > 4800)
            //{
            //    Canvas.SetLeft(hero, Canvas.GetLeft(hero) - 10);
            //    goRight = false;
            //}
            //if (Canvas.GetLeft(backgroundLevel3) < 3200)
            //{
            //    Canvas.SetLeft(hero, Canvas.GetLeft(hero) + 10);
            //    goLeft = false;
            //}

            if (goLeft == true && Canvas.GetLeft(hero) > 0)
            {
                Canvas.SetLeft(hero, Canvas.GetLeft(hero) - player.Speed);
            }

            if(goRight == true && Canvas.GetLeft(hero) + (hero.Width + 10) <= this.MyCanvas.ActualWidth)
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
                player.ForceJump-= 1;
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
            if(bulletTimer < 0)
            {
                EnemyBulletMaker();
                GunFireMaker();
                bulletTimer = bulletTimerLimit;
            }
            //взаимодействие врагов с игроком
            playerHitBox = new Rect(Canvas.GetLeft(hero), Canvas.GetTop(hero), hero.Width, hero.Height);

            foreach(var foe in MyCanvas.Children.OfType<Rectangle>())
            {
                if(foe.Name.ToString() == "enemy1" || foe.Name.ToString() == "enemy2")
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

                if(foe.Name.ToString() == "shooter1" || foe.Name.ToString() == "shooter2")
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

                if(foe.Name.ToString() == "troll")
                {
                    Rect trHitBox = new Rect(Canvas.GetLeft(foe), Canvas.GetTop(foe), foe.Width, foe.Height);
                    if (trHitBox.IntersectsWith(playerHitBox))
                    {
                        trollSpriteIndex += 0.5;
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

                if(foe.Name.ToString() == "terror")
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

                if(foe.Name.ToString() == "soldier")
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

                if(foe.Name.ToString() == "robo")
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

                if(foe.Name.ToString() == "ninja1")
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

                if(foe.Name.ToString() == "ninja2")
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

                if(foe.Name.ToString() == "ninjaBow")
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
                        if (Canvas.GetLeft(animal) < Canvas.GetLeft(ground13))
                        {
                            bear.AttackLeft();
                        }
                        if (Canvas.GetLeft(animal) + animal.Width > Canvas.GetLeft(ground13) + ground13.Width)
                        {
                            bear.AttackRight();
                        }
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
                    if (Canvas.GetLeft(y) < Canvas.GetLeft(hero) + hero.Width && !Keyboard.IsKeyDown(Key.Right))
                    {
                        //speedBullet++;
                        Canvas.SetLeft(y, Canvas.GetLeft(y) - speedBullet);
                    }

                    if (Canvas.GetLeft(y) > Canvas.GetLeft(hero))
                    {
                        Canvas.SetLeft(y, Canvas.GetLeft(y) + speedBullet);
                    }

                    Rect bulletHitBox = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                    if (backgroundLevel2.Focusable == false)
                    {
                        if (Canvas.GetLeft(y) > 1430 || Canvas.GetLeft(y) < 30)
                        {
                            itemRemover.Add(y);
                        }
                    }

                   
                    if(backgroundLevel2.Focusable == true)
                    {
                        if (Canvas.GetLeft(y) > 3000 || Canvas.GetLeft(y) < 1615)
                        {
                            itemRemover.Add(y);
                        }
                    }
                    foreach(var z in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if(z is Rectangle && (string)z.Tag == "stone")
                        {
                            Rect stoneHitBox = new Rect(Canvas.GetLeft(z), Canvas.GetTop(z), z.Width, z.Height);
                            if (bulletHitBox.IntersectsWith(stoneHitBox))
                            {
                                itemRemover.Add(y);
                                itemRemover.Add(z);
                            }
                        }
                    }

                    foreach(var wf in MyCanvas.Children.OfType<Image>())
                    {
                        if(wf is Image && (string)wf.Tag == "wolf")
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

                    foreach(var be in MyCanvas.Children.OfType<Image>())
                    {
                        if(be is Image && (string)be.Tag == "bear")
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

                    foreach(var r in MyCanvas.Children.OfType<Image>())
                    {
                        if(r is Image && (string)r.Tag == "snake")
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

                    foreach(var k in MyCanvas.Children.OfType<Image>())
                    {
                        if(k is Image && (string)k.Tag == "hog")
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

                    foreach(var p in MyCanvas.Children.OfType<Image>())
                    {
                        if(p is Image && (string)p.Tag == "bird")
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

                    foreach(var q in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if(q.Name.ToString() == "enemy1")
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

                    foreach(var g in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if(g.Name.ToString() == "shooter1")
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

                    foreach(var h in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if(h.Name.ToString() == "soldier")
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

                    foreach(var t in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if(t.Name.ToString() == "terror")
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
                                terroristSpriteIndex += 0.5;
                                if (terroristSpriteIndex > 9)
                                {
                                    itemRemover.Add(t);
                                }
                                terrorist.DieSprites(terroristSpriteIndex);
                                terror.Fill = terrorist.terroristSprite;
                            }
                        }
                    }

                    foreach(var i in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if(i.Name.ToString() == "robo")
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
                                robotSpriteIndex +=2;
                                if (robotSpriteIndex > 15)
                                {
                                    itemRemover.Add(i);
                                }
                                robot.DieSprites(robotSpriteIndex);
                                robo.Fill = robot.robotSprite;
                            }
                        }
                    }

                    foreach(var j in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if(j.Name.ToString() == "troll")
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

                    foreach(var m in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if(m.Name.ToString() == "ninjaBow")
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

                    foreach(var n in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if(n.Name.ToString() == "ninja1" || n.Name.ToString() == "ninja2")
                        {
                            Rect ninjaHitBox = new Rect(Canvas.GetLeft(n), Canvas.GetTop(n), n.Width, n.Height);
                            if (bulletHitBox.IntersectsWith(ninjaHitBox))
                            {
                                itemRemover.Add(y);
                                ninja.Health -= 1;
                                ninjaTwo.Health -= 1;
                                ninjaSpriteIndex += 0.5;
                                if (ninjaSpriteIndex > 2)
                                {
                                    ninjaSpriteIndex = 1;
                                }
                                ninja.HurtSprites(ninjaSpriteIndex);
                                ninja1.Fill = ninja.ninjaSprite;
                                ninjaTwo.HurtSprites(ninjaSpriteIndex);
                                ninja2.Fill = ninjaTwo.ninjaSprite;
                            }
                            if (ninja.Health <= 0 || ninjaTwo.Health <= 0)
                            {
                                ninjaSpriteIndex += 0.5;
                                if (ninjaSpriteIndex > 10)
                                {
                                    itemRemover.Add(n);
                                }
                                ninja.DieSprites(ninjaSpriteIndex);
                                ninja1.Fill = ninja.ninjaSprite;
                                ninjaTwo.DieSprites(ninjaSpriteIndex);
                                ninja2.Fill = ninjaTwo.ninjaSprite;
                            }
                        }
                    }
                }
            }
            //Взаимодействие игрока с элементами игры
            foreach(var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if((string)x.Tag == "ground")
                {
                    Rect groundHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(groundHitBox))
                    {
                        //player.ForceJump = 8;
                        player.JumpSpeed = 0;
                        jumping = false;
                        Canvas.SetTop(hero, Canvas.GetTop(x) - hero.Height);
                    }
                    
                    if(x.Name.ToString() == "ground10")
                    {
                        if (goRight == true && playerHitBox.IntersectsWith(groundHitBox))
                        {
                            Canvas.SetLeft(hero, Canvas.GetLeft(hero) - 10);
                            goRight = false;
                        }
                    }
                }

                if((string)x.Tag == "platforma")
                {
                    Rect platformHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(platformHitBox))
                    {
                        player.ForceJump = 8;
                        //player.JumpSpeed = 0;
                        Canvas.SetTop(hero, Canvas.GetTop(x) - hero.Height);
                    }

                    if(x.Name.ToString() == "platform1")
                    {
                        if(playerHitBox.IntersectsWith(platformHitBox))
                        {
                            Canvas.SetLeft(hero, Canvas.GetLeft(hero) + horizontalSpeedPlatform);
                        }
                    }

                    //if (x.Name.ToString() == "platform2")
                    //{
                    //    if (playerHitBox.IntersectsWith(platformHitBox))
                    //    {
                    //        Canvas.SetTop(hero, Canvas.GetTop(hero) + verticalSpeedPlatform);
                    //    }
                    //}

                

                    if (x.Name.ToString() == "platform7")
                    {
                        if (goRight == true && playerHitBox.IntersectsWith(platformHitBox))
                        {
                            Canvas.SetLeft(hero, Canvas.GetLeft(hero) - 10);
                            goRight = false;
                        }
                    }
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

                if((string)x.Tag == "bullets")
                {
                    Rect bulletsHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(bulletsHitBox))
                    {
                        itemRemover.Add(x);
                        bulletsScore += 10;
                    }
                }

                if((string)x.Tag == "healths")
                {
                    Rect healthsHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(healthsHitBox))
                    {
                        itemRemover.Add(x);
                        player.Health += 5;
                    }
                }

                if((string)x.Tag == "treasure")
                {
                    Rect treasureHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(treasureHitBox))
                    {
                        itemRemover.Add(x);
                        gem += 1;
                    }
                }

                if((string)x.Tag == "speeds")
                {
                    Rect speedBonusHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(speedBonusHitBox))
                    {
                        itemRemover.Add(x);
                        player.Speed += 15;
                        for (int counterSpeedBonus = 30; counterSpeedBonus > 0; counterSpeedBonus--)
                        {
                            if(counterSpeedBonus <= 15)
                                  player.Speed -= 1;
                        }
                    }
                }

                if(x is Rectangle && (string)x.Tag == "enemyBullet")
                {
                    
                    if (shooterOne.Speed < 0)
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - speedEnemyBullet);

                    if (shooterOne.Speed > 0 )
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

                if(x is Rectangle && (string)x.Tag == "enemyBullet2")
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

                if(x is Rectangle && (string)x.Tag == "enemyBullet3")
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

                if(x is Rectangle && (string)x.Tag == "lazer")
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

                if(x is Rectangle && (string)x.Tag == "kunai")
                {
                    //Canvas.SetLeft(x,Canvas.GetLeft(x) + speedEnemyBullet);  

                    if (Canvas.GetLeft(x) > 4650 || Canvas.GetLeft(x) < 3260)
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
                
                if(x is Rectangle && (string)x.Tag == "arrow")
                {
                    //Canvas.SetLeft(x,Canvas.GetLeft(x) + speedEnemyBullet);  

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

                if(x is Rectangle && (string)x.Tag == "stone")
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

                if(x.Name.ToString() == "cactus")
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

                if(x.Name.ToString() == "door1")
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

                if(x.Name.ToString() == "door2")
                {
                    Rect doorHitBox2 = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if(gem == 3 && robot.Health <= 0)
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

                if(x.Name.ToString() == "chest")
                {
                    Rect chestHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if(gem == 3 && ninjaBoss.Health == 0 && bear.Health == 0)
                       {
                        if (playerHitBox.IntersectsWith(chestHitBox))
                        {
                            healthScore.Foreground = Brushes.Green;
                            ShowGameOver("Победа!!!Сокровища найдены!");
                        }
                    }
                }
            }

            if (player.Health <= 0)
            {
                player.Health = 0;
                healthScore.Foreground = Brushes.Red;
                playerSpriteIndex +=0.2;
                if (playerSpriteIndex > 10)
                {
                    ShowGameOver("Вы проиграли!!!");
                }
                player.DieSprites(playerSpriteIndex);
                hero.Fill = player.playerSprite;
            }

            if(gem == 3)
            {
                ImageBrush entry = new ImageBrush();
                entry.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/doorOpen.png"));
                door1.Fill = entry;
                treasuresScore.Foreground = Brushes.Yellow;
                if(trollOne.Health <= 0)
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

            if (gem == 3 && ninjaBoss.Health == 0 && bear.Health == 0)
            {
                ImageBrush chestImage = new ImageBrush();
                chestImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Treasure_open.png"));
                chest.Fill = chestImage;
                treasuresScore.Foreground = Brushes.Yellow;
            }

            foreach(Rectangle item in itemRemover)
            {
                MyCanvas.Children.Remove(item);
            }

            foreach(Image itemTwo in itemToRemover)
            {
                MyCanvas.Children.Remove(itemTwo);
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                //scroll.LineLeft();
                goLeft = true;
                playerSpriteIndex ++;
                if(playerSpriteIndex > 10)
                {
                    playerSpriteIndex = 1;
                }
                player.RunSprites(playerSpriteIndex);
                hero.Fill = player.playerSprite;
                hero.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
            }

            if(e.Key == Key.Right)
            {
                //scroll.LineRight();
                goRight = true;
                playerSpriteIndex ++;
                if (playerSpriteIndex > 10)
                {
                    playerSpriteIndex = 1;
                }
                player.RunSprites(playerSpriteIndex);
                hero.Fill = player.playerSprite;
                hero.LayoutTransform = new ScaleTransform() { ScaleX = 1 };

            }

            if(e.Key == Key.Up && jumping == false)
            {
                //goUp = true;
                jumping = true;
                player.ForceJump = 15;
                player.JumpSpeed = -5;
                player.JumpSprites();
                hero.Fill = player.playerSprite;
                if(Keyboard.IsKeyDown(Key.Left))
                {
                    hero.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                }
                if(Keyboard.IsKeyDown(Key.Right))
                {
                    hero.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                }
            }

            if (e.Key == Key.R && gameOver == true)
            {
                //StartGame();
                GameWindow game = new GameWindow();
                    game.Show();
                this.Close();
            }

            if (e.Key == Key.Escape)
            {
                Pause();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                //goUp = false;
                jumping = false;
            }


            if (e.Key == Key.Space)
            {
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 10,
                    Width = 15,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };
                
                 if(!Keyboard.IsKeyDown(Key.Left))
                 {
                    Canvas.SetLeft(newBullet, Canvas.GetLeft(hero) + hero.Height);
                    Canvas.SetTop(newBullet, Canvas.GetTop(hero) + hero.Height / 2 + 24);
                 }

                if (Keyboard.IsKeyDown(Key.Left) && !Keyboard.IsKeyDown(Key.Right))
                {
                    Canvas.SetLeft(newBullet, Canvas.GetLeft(hero));
                    Canvas.SetTop(newBullet, Canvas.GetTop(hero) + hero.Height / 2 + 24);
                }
               
                bulletsScore--;
                if(bulletsScore > 0)
                {
                    MyCanvas.Children.Add(newBullet);
                }

                playerSpriteIndex ++;
                if(playerSpriteIndex > 10)
                {
                    playerSpriteIndex = 1;
                }
                player.AttackSprites(playerSpriteIndex);
                hero.Fill = player.playerSprite;
                if(Keyboard.IsKeyDown(Key.Left))
                {
                    hero.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                }
                if(Keyboard.IsKeyDown(Key.Right))
                {
                    hero.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    //speedBullet++;
                    //Canvas.SetLeft(newBullet, Canvas.GetLeft(newBullet) + speedBullet);
                }
            }

            if(e.Key == Key.Left)
            {
                goLeft = false;
                playerSpriteIndex += 0.5;
                if (playerSpriteIndex > 10)
                    playerSpriteIndex = 1;

                player.IdleSprites(playerSpriteIndex);
                hero.Fill = player.playerSprite;
            }

            if(e.Key == Key.Right)
            {
                goRight = false;
                playerSpriteIndex += 0.5;
                if (playerSpriteIndex > 10)
                    playerSpriteIndex = 1;

                player.IdleSprites(playerSpriteIndex);
                hero.Fill = player.playerSprite;

            }
        }

        private void LoadingBackground()
        {
            ImageBrush background = new ImageBrush();
            ImageBrush backgroundTwo = new ImageBrush();
            ImageBrush backgroundThree = new ImageBrush();

            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/backgroundLevel_01.jpg"));
            backgroundLevel.Fill = background;

            backgroundTwo.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/backgroundLevel_02.jpg"));
            backgroundLevel2.Fill = backgroundTwo;

            backgroundThree.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/backgroundLevel_03.jpg"));
            backgroundLevel3.Fill = backgroundThree;
        }

        private void StartGame()
        {
            
            //Canvas.SetLeft(backgroundLevel, 0);
            //Canvas.SetLeft(backgroundLevel2, 1600);
            //Canvas.SetLeft(backgroundLevel3, 3200);
            Canvas.SetLeft(hero, 31);
            Canvas.SetTop(hero, 422);
            score = 0;
            bulletsScore = 20;
            gem = 0;
            gameOver = false;
            jumping = false;
            //первый уровень
            ImageBrush gr1 = new ImageBrush();
            gr1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/ground1.png"));
            ground1.Fill = gr1;
            ImageBrush gr2 = new ImageBrush();
            gr2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/ground2.png"));
            ground2.Fill = gr2;
            ImageBrush gr3 = new ImageBrush();
            gr3.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/ground4.png"));
            ground3.Fill = gr3;
            ImageBrush gr4 = new ImageBrush();
            gr4.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/ground5.png"));
            ground4.Fill = gr4;
            ImageBrush pl1 = new ImageBrush();
            pl1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platf_1.png"));
            platform1.Fill = pl1;
            platform2.Fill = pl1;
            ImageBrush pl2 = new ImageBrush();
            pl2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platf_3.png"));
            platform3.Fill = pl2;
            platform4.Fill = pl2;
            ImageBrush pl3 = new ImageBrush();
            pl3.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/platf_2.png"));
            platform5.Fill = pl3;
            platform6.Fill = pl3;
            ImageBrush sp = new ImageBrush();
            sp.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Spikes.png"));
            spike1.Fill = sp;
            spike2.Fill = sp;
            spike3.Fill = sp;
            spike4.Fill = sp;
            SnowFalls(1);
            ImageBrush entry1 = new ImageBrush();
            entry1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/doorClose.png"));
            door1.Fill = entry1;
            player.IdleSprites(1);
            hero.Fill = player.playerSprite;
            hero.Stroke = Brushes.Black;
            enemy.RunSprites(1);
            enemy1.Fill = enemy.enemySprite;
            shooterOne.RunSprites(1);
            shooter1.Fill = shooterOne.shooterSprite;
            trollOne.RunSprites(1);
            troll.Fill = trollOne.trollSprite;
            //wolf.UploadingImage();
            //wolf.InitPictures();
            Canvas.SetTop(wolf.wolfOne, 644);
            Canvas.SetLeft(wolf.wolfOne, 1012);
            
            ImageBrush coin = new ImageBrush();
            coin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/coin.gif"));
            coin1.Fill = coin;
            coin2.Fill = coin;
            coin3.Fill = coin;
            coin4.Fill = coin;
            coin5.Fill = coin;
            coin6.Fill = coin;
            coin7.Fill = coin;
            coin8.Fill = coin;
            coin9.Fill = coin;
            coin10.Fill = coin;
            ImageBrush healthImage = new ImageBrush();
            healthImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/heartbeat.png"));
            healthRect.Fill = healthImage;
            ImageBrush speedImage = new ImageBrush();
            speedImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/marathon.png"));
            speedRect.Fill = speedImage;
            ImageBrush amethystImage = new ImageBrush();
            amethystImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/amethyst_01.png"));
            amethyst.Fill = amethystImage;
            ImageBrush emeraldImage = new ImageBrush();
            emeraldImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/emerald_.png"));
            emerald.Fill = emeraldImage;
            ImageBrush rubyImage = new ImageBrush();
            rubyImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/_ruby.png"));
            ruby.Fill = rubyImage;
            ImageBrush bulletImage = new ImageBrush();
            bulletImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/bullets.png"));
            bulletsRect.Fill = bulletImage;
            //Второй уровень
            ImageBrush gr5 = new ImageBrush();
            gr5.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/groundLeft.png"));
            ground5.Fill = gr5;
            ImageBrush gro = new ImageBrush();
            gro.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/groundLeft.png"));
            ground5.Fill = gro;
            ImageBrush gr6 = new ImageBrush();
            gr6.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/groundMedium.png"));
            ground6.Fill = gr6;
            spike5.Fill = sp;
            spike6.Fill = sp;
            ImageBrush gr7 = new ImageBrush();
            gr7.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/groundRight.png"));
            ground7.Fill = gr7;
            ImageBrush grassImage = new ImageBrush();
            grassImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/grass.png"));
            grass.Fill = grassImage;
            ImageBrush boxImage1 = new ImageBrush();
            boxImage1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Crate200.png"));
            box1.Fill = boxImage1;
            ImageBrush boxImage2 = new ImageBrush();
            boxImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/boxs2.png"));
            box2.Fill = boxImage2;
            ImageBrush boxImage3 = new ImageBrush();
            boxImage3.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Crate160.png"));
            box3.Fill = boxImage3;
            ImageBrush boxImage4 = new ImageBrush();
            boxImage4.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/boxs4.png"));
            box4.Fill = boxImage4;
            ImageBrush boxImage5 = new ImageBrush();
            boxImage5.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/boxs.png"));
            box5.Fill = boxImage5;
            ImageBrush cactusImage = new ImageBrush();
            cactusImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Cactus.png"));
            cactus.Fill = cactusImage;
            ImageBrush pointerImage = new ImageBrush();
            pointerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/SignArrow.png"));
            signArrow.Fill = pointerImage;
            ImageBrush entry2 = new ImageBrush();
            entry2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/doorClose_02.png"));
            door2.Fill = entry2;
            soldierOne.RunSprites(1);
            soldier.Fill = soldierOne.soldierSprite;
            shooterTwo.RunSprites(1);
            shooter2.Fill = shooterTwo.shooterSprite;
            terrorist.RunSprites(1);
            terror.Fill = terrorist.terroristSprite;
            terror.Stroke = Brushes.Black;
            robot.RunSprites(1);
            robo.Fill = robot.robotSprite;
            robo.Stroke = Brushes.Black;
            Canvas.SetTop(snake.snakeOne,571);
            Canvas.SetLeft(snake.snakeOne, 3097);
            //MyCanvas.Children.Add(snake.snakeOne);
            coin11.Fill = coin;
            coin12.Fill = coin;
            coin13.Fill = coin;
            coin14.Fill = coin;
            coin15.Fill = coin;
            coin16.Fill = coin;
            coin17.Fill = coin;
            coin18.Fill = coin;
            healthRect1.Fill = healthImage;
            speedRect1.Fill = speedImage;
            bulletsRect1.Fill = bulletImage;
            ImageBrush sapphireImage = new ImageBrush();
            sapphireImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/sapphire.png"));
            sapphire.Fill = sapphireImage;
            ImageBrush diamondImage = new ImageBrush();
            diamondImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/diamond_.png"));
            diamond.Fill = diamondImage;
            ImageBrush bsImage = new ImageBrush();
            bsImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/bloodstone_01.png"));
            bloodStone.Fill = bsImage;
            //Третий уровень
            ImageBrush gr8 = new ImageBrush();
            gr8.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/plleft.png"));
            ground8.Fill = gr8;
            ImageBrush gr9 = new ImageBrush();
            gr9.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/plright.png"));
            ground9.Fill = gr9;
            ImageBrush gr10 = new ImageBrush();
            gr10.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/ground_3.png"));
            ground10.Fill = gr10;
            ImageBrush gr11 = new ImageBrush();
            gr11.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/topplleft.png"));
            ground11.Fill = gr11;
            ImageBrush gr12 = new ImageBrush();
            gr12.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/topplright.png"));
            ground12.Fill = gr12;
            ImageBrush gr13 = new ImageBrush();
            gr13.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/toppl.png"));
            ground13.Fill = gr13;
            ImageBrush pl4 = new ImageBrush();
            pl4.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/m1_mPad.png"));
            platform7.Fill = pl4;
            platform9.Fill = pl4;
            platform10.Fill = pl4;
            ImageBrush pl5 = new ImageBrush();
            pl5.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/m_mPad.png"));
            platform8.Fill = pl5;
            ImageBrush pl6 = new ImageBrush();
            pl6.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/mPad.png"));
            platform11.Fill = pl6;
            ImageBrush chestImage = new ImageBrush();
            chestImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Treasure_closed .png"));
            chest.Fill = chestImage;
            enemyTwo.RunSprites(1);
            enemy2.Fill = enemyTwo.enemySprite;
            ninjaTwo.RunSprites(1);
            ninja2.Fill = ninjaTwo.ninjaSprite;
            ninjaBoss.RunSprites(1);
            ninjaBow.Fill = ninjaBoss.ninjaBossSprite;
            ninja.RunSprites(1);
            ninja1.Fill = ninja.ninjaSprite;
            //bear.UploadingImage();
            //bear.InitPictures();
            Canvas.SetTop(bear.bearOne,353);
            Canvas.SetLeft(bear.bearOne, 4400);
            //MyCanvas.Children.Add(bear.bearOne);
            //hog.UploadingImage();
            //hog.InitPictures();
            Canvas.SetTop(hog.hogOne,393);
            Canvas.SetLeft(hog.hogOne, 4443);
            //MyCanvas.Children.Add(hog.hogOne);
            //bird.UploadingImage();
            //bird.InitPictures();
            Canvas.SetTop(bird.birdOne, 53);
            Canvas.SetLeft(bird.birdOne,3227);
            //MyCanvas.Children.Add(bird.birdOne);
            coin19.Fill = coin;
            coin20.Fill = coin;
            coin21.Fill = coin;
            coin22.Fill = coin;
            coin23.Fill = coin;
            coin24.Fill = coin;
            coin25.Fill = coin;
            coin26.Fill = coin;
            coin27.Fill = coin;
            coin28.Fill = coin;
            healthRect3.Fill = healthImage;
            speedRect3.Fill = speedImage;
            bulletsRect3.Fill = bulletImage;
            ImageBrush sapImage = new ImageBrush();
            sapImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/sapphire_01.png"));
            sapphire1.Fill = sapImage;
            ImageBrush garnetImage = new ImageBrush();
            garnetImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/garnet.png"));
            garnet.Fill = garnetImage;
            ImageBrush dmImage = new ImageBrush();
            dmImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/m_diamond.png"));
            diamond1.Fill = dmImage;
            ImageBrush treeImage = new ImageBrush();
            treeImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tree.png"));
            tree.Fill = treeImage;
            ImageBrush trImage = new ImageBrush();
            trImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tree2.png"));
            tree2.Fill = trImage;

            gameTimer.Start();
        }

        private void ShowGameOver(string msg)
        {
            gameOver = true;
            gameTimer.Stop();
            healthScore.Content = "Здоровье: "+ player.Health + Environment.NewLine + msg + Environment.NewLine +"Нажмите R" + Environment.NewLine +"чтобы начать игру заново"+ Environment.NewLine + "заново";
        }

        private void Pause()
        {
            if(gameTimer.IsEnabled)
            {
                gameTimer.IsEnabled = false;
                string message = "Если хотите вернуться в игровое меню, то нажмите Ok";
                MessageBoxResult result = MessageBox.Show(message, "TREASURE HUNTER", MessageBoxButton.OKCancel);
                switch(result)
                {
                    case MessageBoxResult.OK:
                        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                        break;
                    case MessageBoxResult.Cancel:
                        gameTimer.IsEnabled = true;
                        break;
                }
            }
        }

        private void SnowFalls(double a)
        {
            ImageBrush snowSprite = new ImageBrush();
            switch(a)
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
            stoneSpriteCounter = rand.Next(0,7);
            switch(stoneSpriteCounter)
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
            Canvas.SetLeft(newStone, rand.Next(1228,1280));
            MyCanvas.Children.Add(newStone);

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
                StrokeThickness = 2
            };
            Rectangle enemyBullet3 = new Rectangle
            {
                Tag = "enemyBullet3",
                Height = 10,
                Width = 20,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            // положение от куда стреляют
            Canvas.SetLeft(enemyBullet, Canvas.GetLeft(shooter1) + shooter1.Height/2);
            Canvas.SetTop(enemyBullet, Canvas.GetTop(shooter1) + shooter1.Height/2 +24);

            Canvas.SetLeft(enemyBullet2, Canvas.GetLeft(shooter2) + shooter2.Height/2);
            Canvas.SetTop(enemyBullet2, Canvas.GetTop(shooter2) + shooter2.Height / 2 + 24);

            Canvas.SetLeft(enemyBullet3, Canvas.GetTop(terror) + (terror.Height / 2) - 10);
            Canvas.SetTop(enemyBullet3, Canvas.GetLeft(terror) - enemyBullet3.Width);

            if(Canvas.GetTop(hero) < 260 && Canvas.GetTop(hero) > 145 && shooterOne.Health > 0)
            {
                MyCanvas.Children.Add(enemyBullet);
            }
           if(Canvas.GetTop(hero) < 400 && Canvas.GetTop(hero) > 120 && shooterTwo.Health > 0)
           {
                MyCanvas.Children.Add(enemyBullet2);
           }
            
            MyCanvas.Children.Add(enemyBullet3);


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

            Canvas.SetLeft(newLazer, Canvas.GetTop(robo) + (robo.Height / 2) - 30);
            Canvas.SetTop(newLazer, Canvas.GetLeft(robo) - newLazer.Width);
            Canvas.SetLeft(newKunai, Canvas.GetTop(ninja2) + (ninja2.Height / 2) + 6);
            Canvas.SetTop(newKunai, Canvas.GetLeft(ninja2) - newKunai.Width);
            Canvas.SetLeft(newArrow, Canvas.GetTop(ninjaBow) + (ninjaBow.Height / 2) - 10);
            Canvas.SetTop(newArrow, Canvas.GetLeft(ninjaBow) - newArrow.Width);
            if (robotSpriteIndex == 14)
            {
                MyCanvas.Children.Add(newLazer);
            }

            if (ninjaTwoSpriteIndex == 4)
            {
                MyCanvas.Children.Add(newKunai);
            }

            if (ninjaBossSpriteIndex == 4)
            {
                MyCanvas.Children.Add(newArrow);
            }

            if (Canvas.GetLeft(robo) < 2758 || Canvas.GetLeft(robo) == 2738)
            {

                newLazer.Fill = lazerImage2;
                speedEnemyBullet++;
                Canvas.SetLeft(newLazer, Canvas.GetLeft(newLazer) - speedEnemyBullet);
            }
            if (Canvas.GetLeft(robo) > 2718 || Canvas.GetLeft(robo) == 2738)
            {
                speedEnemyBullet++;
                Canvas.SetLeft(newLazer, Canvas.GetLeft(newLazer) + speedEnemyBullet);
            }
            if (Canvas.GetLeft(ninja2) < 4583 || Canvas.GetLeft(ninja2) == 4563)
            {
                newKunai.Fill = kunaiImage2;
                speedEnemyBullet++;
                Canvas.SetLeft(newKunai, Canvas.GetLeft(newKunai) - speedEnemyBullet);
            }
            if (Canvas.GetLeft(ninja2) > 4543 || Canvas.GetLeft(ninja2) == 4563)
            {
                speedEnemyBullet++;
                Canvas.SetLeft(newKunai, Canvas.GetLeft(newKunai) + speedEnemyBullet);
            }

            if (Canvas.GetLeft(ninjaBow) < 3839 || Canvas.GetLeft(ninjaBow) == 3819)
            {
                newArrow.Fill = arrowImage2;  
                speedEnemyBullet++;
                Canvas.SetLeft(newArrow, Canvas.GetLeft(newArrow) - speedEnemyBullet);
            }
            if (Canvas.GetLeft(ninjaBow) > 3799 || Canvas.GetLeft(ninjaBow) == 3819)
            {
                speedEnemyBullet++;
                Canvas.SetLeft(newArrow, Canvas.GetLeft(newArrow) + speedEnemyBullet);
            }
        }

        private void MotionLevelOne()
        {
            Canvas.SetLeft(platform1, Canvas.GetLeft(platform1) + horizontalSpeedPlatform); //настроил
            if (Canvas.GetLeft(platform1) < 604)
            {
                horizontalSpeedPlatform = - horizontalSpeedPlatform;
            }

            if(Canvas.GetLeft(platform1) < 410)
            {
                horizontalSpeedPlatform = -horizontalSpeedPlatform;
            }

            Canvas.SetTop(platform2, Canvas.GetTop(platform2) + verticalSpeedPlatform);
            if (Canvas.GetTop(platform2) > 181) 
            {
                verticalSpeedPlatform = -verticalSpeedPlatform;
            }
            if(Canvas.GetTop(platform2) > 350)
            {
                verticalSpeedPlatform = -verticalSpeedPlatform;
            }
           

            Canvas.SetLeft(shooter1, Canvas.GetLeft(shooter1) + shooterOne.Speed); //настроил

            shooterSpriteIndex += 0.5;
            if (shooterSpriteIndex > 10)
            {
                shooterSpriteIndex = 1;
            }
            shooterOne.RunSprites(shooterSpriteIndex);
            shooter1.Fill = shooterOne.shooterSprite;

            if (Canvas.GetLeft(shooter1) > 407)
            {
                shooterOne.Speed--;
                shooter1.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
            }

            if (Canvas.GetTop(hero) < 260 && Canvas.GetTop(hero) > 145)
            {
                //shooterOne.Speed = 0;
                shooterSpriteIndex += 0.5;
                if (shooterSpriteIndex > 10)
                {
                    shooterSpriteIndex = 1;   
                }
                shooterOne.AttackSprites(shooterSpriteIndex);
                shooter1.Fill = shooterOne.shooterSprite;
            }


            if (Canvas.GetLeft(shooter1) < 151)
            {
                shooterOne.Speed++;
                shooter1.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            }
            

            Canvas.SetLeft(enemy1, Canvas.GetLeft(enemy1) + enemy.Speed);//настроил

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

            //if (Canvas.GetTop(hero) > 400)
            //{
            //    enemy.Speed = 0;
            //    enemySpriteIndex += 0.1;
            //    if (enemySpriteIndex > 12)
            //    {
            //        enemySpriteIndex = 1;
            //    }
            //    enemy.IdleEnSprites(enemySpriteIndex);
            //    enemy1.Fill = enemy.enemySprite;
            //}

            if (Canvas.GetLeft(enemy1) < 799)
            {
                enemy.Speed++;
                enemy1.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            }

            foreach(var w in MyCanvas.Children.OfType<Image>())
            {
                if((string)w.Tag == "wolf")
                {
                    Canvas.SetLeft(w, Canvas.GetLeft(w) + wolf.Speed);


                    if (Canvas.GetLeft(w) < Canvas.GetLeft(ground4) || Canvas.GetLeft(w) + w.Width > Canvas.GetLeft(ground4) + ground4.Width)
                    {
                        wolf.Speed = -wolf.Speed;
                    }

                    if (Canvas.GetLeft(w) < Canvas.GetLeft(ground4))
                    {
                       
                        wolf.WolfCurrentFrame++;
                        wolf.MoveRight();
                    }
                    if (Canvas.GetLeft(w) + w.Width > Canvas.GetLeft(ground4) + ground4.Width)
                    {
                       
                        wolf.MoveLeft();
                    }

                }
            }


            Canvas.SetLeft(troll, Canvas.GetLeft(troll) + trollOne.Speed);
            trollSpriteIndex += 0.5;
            if (trollSpriteIndex > 9)
            {
                trollSpriteIndex = 1;
            }
            trollOne.RunSprites(trollSpriteIndex);
            troll.Fill = trollOne.trollSprite;

            //if (Canvas.GetLeft(hero) < 60)
            //{
            //    trollOne.Speed = 0;
            //    trollSpriteIndex += 0.2;
            //    if (trollSpriteIndex > 10)
            //    {
            //        trollSpriteIndex = 1; 
            //    }
            //    trollOne.IdleTrSprites(trollSpriteIndex);
            //    troll.Fill = trollOne.trollSprite;
            //}


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


            snowSpriteIndex += 0.5;
            if (snowSpriteIndex > 3)
            {
                snowSpriteIndex = 0;
            }
            SnowFalls(snowSpriteIndex);

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
            //if (Canvas.GetLeft(soldier) < 2056 || Canvas.GetLeft(soldier) == 2076)
            //{
            //    soldierOne.Speed = 0;
            //    soldierSpriteIndex += 0.5;
            //    if (soldierSpriteIndex > 10)
            //    {
            //        soldierOne.Speed = -5;
            //        soldierSpriteIndex = 1;
            //    }
            //    soldierOne.IdleSoSprites(soldierSpriteIndex);
            //    soldier.Fill = soldierOne.soldierSprite;
            //}

            if (Canvas.GetLeft(soldier) < 1983)
            {
                soldierOne.Speed++;
                soldier.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            }

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
                shooterTwo.Speed--;
                shooter2.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
            }

            if (Canvas.GetTop(hero) < 400 && Canvas.GetTop(hero) > 120)
            {
                //shooterTwo.Speed = 0;
                shooterSpriteIndex += 0.5;
                if (shooterSpriteIndex > 10)
                {
                    shooterSpriteIndex = 1;
                }
                shooterTwo.AttackSprites(shooterSpriteIndex);
                shooter2.Fill = shooterTwo.shooterSprite;
            }

            if (Canvas.GetLeft(shooter2) < 1624)
            {
                shooterTwo.Speed++;
                shooter2.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            }

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
                terrorist.Speed--;
                terror.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
            }

            //if (Canvas.GetLeft(terror) < 2214 || Canvas.GetLeft(terror) == 2194)
            //{
            //    terrorist.Speed = 0;
            //    terroristSpriteIndex += 0.5;
            //    if (terroristSpriteIndex > 4)
            //    {
            //        terrorist.Speed = -5;
            //        terroristSpriteIndex = 1;
            //    }
            //    terrorist.ShootSprites(terroristSpriteIndex);
            //    terror.Fill = terrorist.terroristSprite;
            //}

            if (Canvas.GetLeft(terror) < 1782)
            {
                terrorist.Speed++;
                terror.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            }

            Canvas.SetLeft(robo, Canvas.GetLeft(robo) + robot.Speed);
            robotSpriteIndex += 0.5;
            if (robotSpriteIndex > 12)
            {
                robotSpriteIndex = 1;
            }
            robot.RunSprites(robotSpriteIndex);
            robo.Fill = robot.robotSprite;

            if (Canvas.GetLeft(robo) > 2921)
            {
                robot.Speed--;
                robo.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
            }


            //if (Canvas.GetLeft(robo) < 2758 || Canvas.GetLeft(robo) == 2738)
            //{
            //    robot.Speed = 0;
            //    robotSpriteIndex += 0.5;
            //    if (robotSpriteIndex > 16)
            //    {
            //        robot.Speed = -5;
            //        robotSpriteIndex = 1;
            //    }
            //    robot.LazerSprites(robotSpriteIndex);
            //    robo.Fill = robot.robotSprite;
            //}

            if (Canvas.GetLeft(robo) < 2537)
            {
                robot.Speed++;
                robo.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            }

            foreach(var s in MyCanvas.Children.OfType<Image>())
            {
                if((string)s.Tag == "snake")
                {
                    Canvas.SetLeft(s, Canvas.GetLeft(s) + snake.Speed);
                    //if(Canvas.GetLeft(s) < 3097 || Canvas.GetLeft(s) > 2671)
                    //{
                    //    snake.Speed = -snake.Speed;
                    //}

                    if(Canvas.GetLeft(s) > 3097)
                    {
                        snake.Speed--;
                        snake.MoveLeft();
                    }
                    if(Canvas.GetLeft(s) < 2671)
                    {
                        snake.Speed++;
                        snake.MoveRight();
                    }
                }
            }
        }

        //private void Window_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if(Keyboard.IsKeyDown(Key.A))
        //    {
        //        scroll.LineLeft();
        //    }

        //    if(Keyboard.IsKeyDown(Key.D))
        //    {
        //        scroll.LineRight();
        //    }
        //}

        private void MotionLevelThree()
        {
            Canvas.SetTop(platform9, Canvas.GetTop(platform9) - verticalSpeedPlatform);
            if (Canvas.GetTop(platform9) < 381 || Canvas.GetTop(platform9) > 181)
            {
                verticalSpeedPlatform = -verticalSpeedPlatform;
            }
            Canvas.SetLeft(platform10, Canvas.GetLeft(platform10) + horizontalSpeedPlatform);
            if (Canvas.GetLeft(platform10) < 4067 || Canvas.GetLeft(platform10) > 3547)
            {
                horizontalSpeedPlatform = -horizontalSpeedPlatform;
            }

            foreach(var b in MyCanvas.Children.OfType<Image>())
            {
                //if((string)b.Tag == "bird")
                //{
                //    Canvas.SetLeft(b, Canvas.GetLeft(b) + bird.Speed);
                //    bird.MoveRight();
                //    if (Canvas.GetLeft(b) > 4685)
                //    {
                //        bird.Speed = -bird.Speed;
                //        Canvas.SetLeft(b, 3227);
                //    }
                //}

                if((string)b.Tag == "bear")
                {
                    Canvas.SetLeft(b, Canvas.GetLeft(b) + bear.Speed);
                    if(Canvas.GetLeft(b) < Canvas.GetLeft(ground13) || Canvas.GetLeft(b) + b.Width > Canvas.GetLeft(ground13) + ground13.Width)
                    {
                        bear.Speed = -bear.Speed;
                    }

                    if(Canvas.GetLeft(b) < Canvas.GetLeft(ground13))
                    {
                        bear.MoveLeft();
                    }

                    if(Canvas.GetLeft(b) + b.Width > Canvas.GetLeft(ground13) + ground13.Width)
                    {
                        bear.MoveRight();
                    }
                }

                if ((string)b.Tag == "hog")
                {
                    Canvas.SetLeft(b, Canvas.GetLeft(b) + hog.Speed);
                    if (Canvas.GetLeft(b) < Canvas.GetLeft(ground10) || Canvas.GetLeft(b) + b.Width > Canvas.GetLeft(ground10) + ground10.Width)
                    {
                        hog.Speed = -hog.Speed;
                    }

                    if (Canvas.GetLeft(b) < Canvas.GetLeft(ground10))
                    {
                        hog.MoveLeft();
                    }

                    if (Canvas.GetLeft(b) + b.Width > Canvas.GetLeft(ground10) + ground10.Width)
                    {
                        hog.MoveRight();
                    }
                }
            }

            Canvas.SetLeft(enemy2, Canvas.GetLeft(enemy2) + enemyTwo.Speed);
            if (Canvas.GetLeft(enemy2) < 4139 || Canvas.GetLeft(enemy2) > 3459)
            {
                enemyTwo.Speed = -enemyTwo.Speed;
            }
            if (Canvas.GetLeft(enemy2) < 4139)
            {
                enemySpriteIndex += 0.5;
                if (enemySpriteIndex > 8)
                {
                    enemySpriteIndex = 1;
                }
                enemyTwo.RunSprites(enemySpriteIndex);
                enemy2.Fill = enemyTwo.enemySprite;
                enemy2.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
             }

            if (Canvas.GetLeft(enemy2) < 3914 || Canvas.GetLeft(enemy2) == 3894)
            {
                enemyTwo.Speed = 0;
                enemySpriteIndex += 0.5;
                if (enemySpriteIndex > 12)
                {
                    enemyTwo.Speed = -5;
                    enemySpriteIndex = 1;
                }
                enemyTwo.IdleEnSprites(enemySpriteIndex);
                enemy2.Fill = enemyTwo.enemySprite;
            }

            if (Canvas.GetLeft(enemy2) > 3459)
            {
                enemySpriteIndex += 0.5;
                if (enemySpriteIndex > 8)
                {
                    enemySpriteIndex = 1;
                }
                enemyTwo.RunSprites(enemySpriteIndex);
                enemy2.Fill = enemyTwo.enemySprite;
                enemy2.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            }

            Canvas.SetLeft(ninja2, Canvas.GetLeft(ninja2) - ninjaTwo.Speed);
            if (Canvas.GetLeft(ninja2) < 4669 || Canvas.GetLeft(ninja2) > 4416)
            {
                ninjaTwo.Speed = -ninjaTwo.Speed;
            }
            if (Canvas.GetLeft(ninja2) < 4669)
            {
                ninjaTwoSpriteIndex += 0.5;
                if (ninjaTwoSpriteIndex > 10)
                {
                    ninjaTwoSpriteIndex = 1;
                }
                ninjaTwo.RunSprites(ninjaTwoSpriteIndex);
                ninja2.Fill = ninjaTwo.ninjaSprite;

                if (Canvas.GetLeft(ninja2) < 4583 || Canvas.GetLeft(ninja2) == 4563)
                {
                    ninjaTwo.Speed = 0;
                    ninjaTwoSpriteIndex += 0.5;
                    if (ninjaTwoSpriteIndex > 10)
                    {
                        ninjaTwo.Speed = -5;
                        ninjaTwoSpriteIndex = 1;
                    }
                    ninjaTwo.ThrowSprites(ninjaTwoSpriteIndex);
                    ninja2.Fill = ninjaTwo.ninjaSprite;
                }

                ninja2.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
            }
            if (Canvas.GetLeft(ninja2) > 4416)
            {
                ninjaTwoSpriteIndex += 0.5;
                if (ninjaTwoSpriteIndex > 10)
                {
                    ninjaTwoSpriteIndex = 1;
                }
                ninjaTwo.RunSprites(ninjaTwoSpriteIndex);
                ninja2.Fill = ninjaTwo.ninjaSprite;

                if (Canvas.GetLeft(ninja2) > 4543 || Canvas.GetLeft(ninja2) == 4563)
                {
                    ninjaTwo.Speed = 0;
                    ninjaTwoSpriteIndex += 0.5;
                    if (ninjaTwoSpriteIndex > 10)
                    {
                        ninjaTwo.Speed = 5;
                        ninjaTwoSpriteIndex = 1;
                    }
                    ninjaTwo.ThrowSprites(ninjaTwoSpriteIndex);
                    ninja2.Fill = ninjaTwo.ninjaSprite;
                }
                ninja2.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            }

            Canvas.SetLeft(ninjaBow, Canvas.GetLeft(ninjaBow) - ninjaBoss.Speed);
            if (Canvas.GetLeft(ninjaBow) < 3990 || Canvas.GetLeft(ninjaBow) > 3520)
            {
                ninjaBoss.Speed = -ninjaBoss.Speed;
            }
            if (Canvas.GetLeft(ninjaBow) < 3990)
            {
                ninjaBossSpriteIndex += 0.5;
                if (ninjaBossSpriteIndex > 16)
                {
                    ninjaBossSpriteIndex = 1;
                }
                ninjaBoss.RunSprites(ninjaBossSpriteIndex);
                ninjaBow.Fill = ninjaBoss.ninjaBossSprite;

                if (Canvas.GetLeft(ninjaBow) < 3839 || Canvas.GetLeft(ninjaBow) == 3819)
                {
                    ninjaBoss.Speed = 0;
                    ninjaBossSpriteIndex += 0.5;
                    if (ninjaBossSpriteIndex > 22)
                    {
                        ninjaBoss.Speed = -5;
                        ninjaBossSpriteIndex = 1;
                    }
                    ninjaBoss.AttackSprites(ninjaBossSpriteIndex);
                    ninjaBow.Fill = ninjaBoss.ninjaBossSprite;
                }

                ninjaBow.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
            }
            if (Canvas.GetLeft(ninjaBow) > 3520)
            {
                ninjaBossSpriteIndex += 0.5;
                if (ninjaBossSpriteIndex > 16)
                {
                    ninjaBossSpriteIndex = 1;
                }
                ninjaBoss.RunSprites(ninjaBossSpriteIndex);
                ninjaBow.Fill = ninjaBoss.ninjaBossSprite;

                if (Canvas.GetLeft(ninjaBow) > 3799 || Canvas.GetLeft(ninjaBow) == 3819)
                {
                    ninjaBoss.Speed = 0;
                    ninjaBossSpriteIndex += 0.5;
                    if (ninjaBossSpriteIndex > 22)
                    {
                        ninjaBoss.Speed = 5;
                        ninjaBossSpriteIndex = 1;
                    }
                    ninjaBoss.AttackSprites(ninjaBossSpriteIndex);
                    ninjaBow.Fill = ninjaBoss.ninjaBossSprite;
                }
                ninjaBow.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            }

            if (Canvas.GetLeft(ninjaBow) < 3722 || Canvas.GetLeft(ninjaBow) == 3712)
            {
                ninjaBoss.Speed = 0;
                ninjaBossSpriteIndex += 0.5;
                if (ninjaBossSpriteIndex > 12)
                {
                    ninjaBoss.Speed = -5;
                    ninjaBossSpriteIndex = 1;
                }
                ninjaBoss.IdleNiSprites(ninjaBossSpriteIndex);
                ninjaBow.Fill = ninjaBoss.ninjaBossSprite;
            }

            Canvas.SetLeft(ninja1, Canvas.GetLeft(ninja1) + ninja.Speed);
            if (Canvas.GetLeft(ninja1) < 3645 || Canvas.GetLeft(ninja1) > 3259)
            {
                ninja.Speed = -ninja.Speed;
            }
            if (Canvas.GetLeft(ninja1) < 3645)
            {
                ninjaSpriteIndex += 0.5;
                if (ninjaSpriteIndex > 10)
                {
                    ninjaSpriteIndex = 1;
                }
                ninja.RunSprites(ninjaSpriteIndex);
                ninja1.Fill = ninja.ninjaSprite;
                ninja1.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
            }

            if (Canvas.GetLeft(ninja1) > 3259)
            {
                ninjaSpriteIndex += 0.5;
                if (ninjaSpriteIndex > 10)
                {
                    ninjaSpriteIndex = 1;
                }
                ninja.RunSprites(ninjaSpriteIndex);
                ninja1.Fill = ninja.ninjaSprite;
                ninja1.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            }
        }
    }
}
