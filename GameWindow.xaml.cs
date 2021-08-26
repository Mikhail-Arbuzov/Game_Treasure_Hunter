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
        bool goLeft, goRight,goDown;
        bool gameOver;
        bool jumping;
        public bool jumpDown;//для включения и отключения прыжка вниз
        double speedBullet = 10;// скорость пули игрока
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
        double speedEnemyArrows = 10;
        int bulletTimer = 0;
        int bulletTimerLimit = 90;
        //для переключения спрайтов
        double playerSpriteIndex = 0;
        double playerSpriteIndex2 = 0;
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
        //счетчики бонусов в игре
        int score;
        public int bulletsScore = 20;// по умолчанию
        int gem;

        TimeSpan counterTime = new TimeSpan();//переменная для счетчика времени игры
       
        public Player player;
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

        //для озвучки игры
        public MediaPlayer backgroundMedia = new MediaPlayer();
        public bool turnOffsong = false;//для отключения фонового звука уровней
        public bool soundsGame = false;//запустить или отключить звуки из игры
        bool soundTrollRun = false;//для отключения звука хотьбы троля
        
        MediaPlayer backgroundMediaTwo = new MediaPlayer();
        MediaPlayer backgroundMediaThree = new MediaPlayer();
        MediaPlayer playerMedia = new MediaPlayer();
        MediaPlayer levelEnd = new MediaPlayer();
        MediaPlayer doorOpenMedia = new MediaPlayer();
        MediaPlayer openBox = new MediaPlayer();
        MediaPlayer mediaBullets = new MediaPlayer();
        MediaPlayer bonusMedia = new MediaPlayer();
        MediaPlayer bonusSounds = new MediaPlayer();
        MediaPlayer enemySounds = new MediaPlayer();
        MediaPlayer shooterSounds = new MediaPlayer();
        MediaPlayer trollSounds = new MediaPlayer();
        MediaPlayer trollAttackSounds = new MediaPlayer();
        MediaPlayer trollWalkSounds = new MediaPlayer();
        MediaPlayer trollIdleSounds = new MediaPlayer();
        MediaPlayer stonesSound = new MediaPlayer();
        MediaPlayer wolfAttackSounds = new MediaPlayer();
        MediaPlayer wolfDieSounds = new MediaPlayer();
        MediaPlayer robotRunSounds = new MediaPlayer();
        MediaPlayer robotSounds = new MediaPlayer();
        MediaPlayer robotLazerSounds = new MediaPlayer();
        MediaPlayer robotAttackSounds = new MediaPlayer();
        MediaPlayer snakeAttackSound = new MediaPlayer();
        MediaPlayer snakeSound = new MediaPlayer();
        MediaPlayer terroristSounds = new MediaPlayer();
        MediaPlayer terroristShootSounds = new MediaPlayer();
        MediaPlayer soldierSounds = new MediaPlayer();
        MediaPlayer bearSound = new MediaPlayer();
        MediaPlayer bearAttackSound = new MediaPlayer();
        MediaPlayer birdSound = new MediaPlayer();
        MediaPlayer hogSound = new MediaPlayer();
        MediaPlayer ninjaSounds = new MediaPlayer();
        MediaPlayer ninjaBossSounds = new MediaPlayer();
        MediaPlayer gameOverSound = new MediaPlayer();
        MediaPlayer raneniePlayer = new MediaPlayer();
        MediaPlayer raneniePlayer2 = new MediaPlayer();

        int counterCartridges = 0;// счетчик для счета всех патронов
        public int counterCoins = 0;//счетчик для счета всех собранных монет в игре
        public string choiceComplexity; //выбор сложности игры


        public GameWindow()
        {
            InitializeComponent();
           
            MyCanvas.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            LoadingBackground();// загрузка изображений фона уровней
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
            wolf = new Wolf("pack://application:,,,/Sprites/SpritesWolf/Wolf.png", 141, 189, 98, "wolf", 6, 0);
            bear = new Bear("pack://application:,,,/Sprites/SpritesBear/m_Bear.png", 180, 135, 160, "bear", 8, 0);
            hog = new Hog("pack://application:,,,/Sprites/SpritesHog/m_zp.png", 120, 196, 120, "hog", 3, 0);
            bird = new Bird("pack://application:,,,/Sprites/SpritesBird/Bird_Black.png", 0, 96, 96, "bird", 4, 0);
            snake = new Snake("pack://application:,,,/Sprites/SpritesSnake/snake.png", 80, 80, 80, "snake", 4, 0);
            // загрузка изображений животных в игре
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
            LoadingSounds();//метод по загрузке более продолжительных звуков в игре
        }

        //управление 

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
            if(!jumpDown)
            {
                if (e.Key == Key.Down)
                {
                    goDown = true;
                    player.ForceJump = -8;
                    player.jumpDownSpeed = 10;
                    player.JumpSprites();
                    hero.Fill = player.playerSprite;
                    if (Keyboard.IsKeyDown(Key.Left))
                    {
                        hero.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
                    }
                    if (Keyboard.IsKeyDown(Key.Right))
                    {
                        hero.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
                    }
                }
            }
            

            if (e.Key == Key.R && gameOver == true)
            {
                gameOverSound.Stop();//отключение звука проигрыша игрока
                GameWindow game = new GameWindow();
                game.Show();
                game.backgroundMedia.Play();//включить фоновый звук на первом уровне
                game.soundsGame = true;//запустить звуки из игры
                game.choiceComplexity = "Easy";// сложность игры по умолчанию
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

            if(e.Key == Key.Down)
            {
                goDown = false;
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
                    playerMedia.Open(new Uri(@"../../GameSounds/playerShoot.mp3", UriKind.Relative));//звук выстрела из ружья игрока
                    playerMedia.Position = new TimeSpan(0, 0, 0);
                    playerMedia.Play();
                    MyCanvas.Children.Add(newBullet);
                    counterCartridges++;//cчетчик потраченных патронов
                }
                if(bulletsScore <=0)
                {
                    bulletsScore = 0;
                }

                playerSpriteIndex +=0.5;
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
                }
               
            }

            if (e.Key == Key.Left)
            {
                goLeft = false;
            }

            if (e.Key == Key.Right)
            {
                goRight = false;
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
            backgroundMedia.Open(new Uri(@"../../GameSounds/gory.mp3", UriKind.Relative));// фоновый звук в первом уровне
            backgroundMediaTwo.Open(new Uri(@"../../GameSounds/beloe_solnce_pustyni.mp3", UriKind.Relative));// фоновый звук во втором уровне
            backgroundMediaThree.Open(new Uri(@"../../GameSounds/forest.mp3", UriKind.Relative));// фоновый звук в третьем уровне
            // начальное положение игрока при старте игры
            Canvas.SetLeft(hero, 31);
            Canvas.SetTop(hero, 422);
            //Canvas.SetLeft(hero, 3242);
            //Canvas.SetTop(hero, 632);
            //scroll.ScrollToHorizontalOffset(3200);

            // визуальный уровень здоровья Босса
            healthProgress.Value = trollOne.Health;
            healthProgress.Minimum = 0;
            healthProgress.Maximum = 5;
            healthProgress.Foreground = Brushes.Purple;
            healthProgress.Opacity = 0.2;// прозрачный
            // кол-во монет и сокровищ при старте игры
            score = 0;
            gem = 0;
            // прыжек игрока и проигрыш при старте игры 
            gameOver = false;
            jumping = false;
                                          // загрузка изображений всех элементов игры
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
            SnowFalls(1);// снег
            ImageBrush entry1 = new ImageBrush();
            entry1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/doorClose.png"));
            door1.Fill = entry1;
            player.IdleSprites(1);
            hero.Fill = player.playerSprite;
            //hero.Stroke = Brushes.Gray;//выделение границ прямоугольника в котором установлен спрайт игрока
            enemy.RunSprites(1);
            enemy1.Fill = enemy.enemySprite;
            shooterOne.RunSprites(1);
            shooter1.Fill = shooterOne.shooterSprite;
            trollOne.RunSprites(1);
            troll.Fill = trollOne.trollSprite;
            // положение волка в игре
            Canvas.SetTop(wolf.wolfOne, 665);
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
            robot.RunSprites(1);
            robo.Fill = robot.robotSprite;
            // положение змеи в игре
            Canvas.SetTop(snake.snakeOne,571);
            Canvas.SetLeft(snake.snakeOne, 3097);
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
            // положение медведя в игре
            Canvas.SetTop(bear.bearOne,353);
            Canvas.SetLeft(bear.bearOne, 4400);
            // положение кобана в игре
            Canvas.SetTop(hog.hogOne,593);
            Canvas.SetLeft(hog.hogOne, 4443);
            // положение птицы в игре
            Canvas.SetTop(bird.birdOne, 53);
            Canvas.SetLeft(bird.birdOne,3227);
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
            // прямоугольники деревья
            //ImageBrush treeImage = new ImageBrush();
            //treeImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tree.png"));
            //tree.Fill = treeImage;
            //ImageBrush trImage = new ImageBrush();
            //trImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/tree2.png"));
            //tree2.Fill = trImage;

            gameTimer.Start(); //запуск работы DispatcherTimer
        }

        //закрытие всех аудиофайлов при (нажатии Х) выходе из окна игры
        private void GameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gameTimer.IsEnabled)
            {
                shooterSounds.Stop();
                trollWalkSounds.Stop();
                trollIdleSounds.Stop();
                stonesSound.Stop();
                robotRunSounds.Stop();
                robotLazerSounds.Stop();
                snakeSound.Stop();
                bearSound.Stop();
                terroristShootSounds.Stop();
                birdSound.Stop();
                hogSound.Stop();
                ninjaSounds.Stop();
                ninjaBossSounds.Stop();
                gameTimer.IsEnabled = false;
                string message2 = "Хотите выйти из игры ?";
                MessageBoxResult result2 = MessageBox.Show(message2, "TREASURE HUNTER", MessageBoxButton.YesNo);
                switch (result2)
                {
                    case MessageBoxResult.Yes:
                        backgroundMedia.Close();
                        backgroundMediaTwo.Close();
                        backgroundMediaThree.Close();
                        playerMedia.Close();
                        levelEnd.Close();
                        doorOpenMedia.Close();
                        openBox.Close();
                        mediaBullets.Close();
                        bonusMedia.Close();
                        bonusSounds.Close();
                        enemySounds.Close();
                        shooterSounds.Close();
                        trollSounds.Close();
                        trollAttackSounds.Close();
                        trollWalkSounds.Close();
                        trollIdleSounds.Close();
                        stonesSound.Close();
                        wolfAttackSounds.Close();
                        wolfDieSounds.Close();
                        robotRunSounds.Close();
                        robotSounds.Close();
                        robotLazerSounds.Close();
                        robotAttackSounds.Close();
                        snakeAttackSound.Close();
                        snakeSound.Close();
                        terroristSounds.Close();
                        terroristShootSounds.Close();
                        soldierSounds.Close();
                        bearSound.Close();
                        bearAttackSound.Close();
                        birdSound.Close();
                        hogSound.Close();
                        ninjaSounds.Close();
                        ninjaBossSounds.Close();
                        gameOverSound.Close();
                        raneniePlayer.Close();
                        raneniePlayer2.Close();
                        
                        Application.Current.Shutdown();
                        break;
                    case MessageBoxResult.No:
                        gameTimer.IsEnabled = true;
                        e.Cancel = true;
                        break;
                }
            }
        }
        // загружга продолжительных аудиозвуков в игре
        private void LoadingSounds()
        {
            levelEnd.Open(new Uri(@"../../GameSounds/klubnichki.mp3", UriKind.Relative));
            doorOpenMedia.Open(new Uri(@"../../GameSounds/dooropen.mp3", UriKind.Relative));
            openBox.Open(new Uri(@"../../GameSounds/final.mp3", UriKind.Relative));
            trollAttackSounds.Open(new Uri(@"../../GameSounds/trollAttack.wma", UriKind.Relative));
            trollWalkSounds.Open(new Uri(@"../../GameSounds/trollWalk.mp3", UriKind.Relative));
            trollIdleSounds.Open(new Uri(@"../../GameSounds/trollIdle.mp3", UriKind.Relative));
            stonesSound.Open(new Uri(@"../../GameSounds/rockfall.mp3", UriKind.Relative));
            wolfAttackSounds.Open(new Uri(@"../../GameSounds/wolfAttack.mp3", UriKind.Relative));
            wolfDieSounds.Open(new Uri(@"../../GameSounds/wolfdie.mp3", UriKind.Relative));
            robotRunSounds.Open(new Uri(@"../../GameSounds/runrobot.mp3", UriKind.Relative));
            robotAttackSounds.Open(new Uri(@"../../GameSounds/robotAttack.mp3", UriKind.Relative));
            snakeAttackSound.Open(new Uri(@"../../GameSounds/atakujuschaja-zmeja.mp3", UriKind.Relative));
            snakeSound.Open(new Uri(@"../../GameSounds/zvuk-zmeya.mp3", UriKind.Relative));
            terroristShootSounds.Open(new Uri(@"../../GameSounds/terror.mp3", UriKind.Relative));
            bearSound.Open(new Uri(@"../../GameSounds/dyhanie-medvedja.mp3", UriKind.Relative));
            bearAttackSound.Open(new Uri(@"../../GameSounds/bearAttack.mp3", UriKind.Relative));
            birdSound.Open(new Uri(@"../../GameSounds/zvuk-vorony.mp3", UriKind.Relative));
            hogSound.Open(new Uri(@"../../GameSounds/hog.mp3", UriKind.Relative));
            gameOverSound.Open(new Uri(@"../../GameSounds/gameover.mp3", UriKind.Relative));

        }
        // метод паузы в игре
        private void Pause()
        {
            if(gameTimer.IsEnabled)
            {
                backgroundMedia.Stop();
                backgroundMediaTwo.Stop();
                backgroundMediaThree.Stop();
                shooterSounds.Stop();
                trollWalkSounds.Stop();
                trollIdleSounds.Stop();
                stonesSound.Stop();
                robotRunSounds.Stop();
                robotLazerSounds.Stop();
                snakeSound.Stop();
                bearSound.Stop();
                terroristShootSounds.Stop();
                birdSound.Stop();
                hogSound.Stop();
                ninjaSounds.Stop();
                ninjaBossSounds.Stop();
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

    }
}
