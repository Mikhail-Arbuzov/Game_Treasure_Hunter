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
        double horizontalSpeedPlatform = 3;
        double horizontalSpeedPlatform2 = 3;
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

        

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                player.Direction = Direction.Left;
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
                player.Direction = Direction.Right;
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
            if (e.Key == Key.Up)
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
                //откуда стрелять в зависимости от направления игрока
                if(player.Direction == Direction.Left)
                {
                    Canvas.SetLeft(newBullet, Canvas.GetLeft(hero) - 20);
                    Canvas.SetTop(newBullet, Canvas.GetTop(hero) + hero.Height / 2 + 24);
                }

                if(player.Direction == Direction.Right)
                {
                    Canvas.SetLeft(newBullet, (Canvas.GetLeft(hero) + hero.Height) + 10);
                    Canvas.SetTop(newBullet, Canvas.GetTop(hero) + hero.Height / 2 + 24);
                }
                
                //другой способ
                // if(!Keyboard.IsKeyDown(Key.Left))
                // {
                //    Canvas.SetLeft(newBullet, Canvas.GetLeft(hero) + hero.Height);
                //    Canvas.SetTop(newBullet, Canvas.GetTop(hero) + hero.Height / 2 + 24);
                // }

                //if (Keyboard.IsKeyDown(Key.Left) && !Keyboard.IsKeyDown(Key.Right))
                //{
                //    Canvas.SetLeft(newBullet, Canvas.GetLeft(hero));
                //    Canvas.SetTop(newBullet, Canvas.GetTop(hero) + hero.Height / 2 + 24);
                //}
               
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

            if (e.Key == Key.Left)
            {
                goLeft = false;
                //playerSpriteIndex += 0.5;
                //if (playerSpriteIndex > 10)
                //    playerSpriteIndex = 1;

                //player.IdleSprites(playerSpriteIndex);
                //hero.Fill = player.playerSprite;
            }

            if (e.Key == Key.Right)
            {
                goRight = false;
                //playerSpriteIndex += 0.5;
                //if (playerSpriteIndex > 10)
                //    playerSpriteIndex = 1;

                //player.IdleSprites(playerSpriteIndex);
                //hero.Fill = player.playerSprite;
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
            //Canvas.SetLeft(hero, 3242);
            //Canvas.SetTop(hero, 632);
            //scroll.ScrollToHorizontalOffset(3200);

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
            Canvas.SetTop(wolf.wolfOne, 677);
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
            //robo.Stroke = Brushes.Black;
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
            Canvas.SetTop(hog.hogOne,593);
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

    }
}
