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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Game_Treasure_Hunter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameWindow gameWindow = new GameWindow();
        MediaPlayer songMenu;
       

        public MainWindow()
        {
            InitializeComponent();
            //загрузка и включения музыки в меню
            songMenu = new MediaPlayer();
            songMenu.Open(new Uri(@"../../GameSounds/RockPrivet.wma", UriKind.Relative));
            songMenu.Play();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            songMenu.Stop();
            if (turnOn.IsChecked == true)
            {
                gameWindow.backgroundMedia.Play();//запуск звука фона первого раунда
            }
            gameWindow.Show();
            gameWindow.soundsGame = true;//запустить звуки из игры
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            songMenu.Close();//закрытие аудио файла из меню
            Environment.Exit(0);
        }

        //переключение сложности игры
        private void normal_Checked(object sender, RoutedEventArgs e)
        {
            gameWindow.bulletsScore = 17;
            gameWindow.jumpDown = false;
            gameWindow.player.Health = 20;
            ImageBrush bushImage = new ImageBrush();
            bushImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Bush.png"));
            Rectangle bushRect = new Rectangle
            {
                Tag = "bush",
                Height = 74,
                Width = 131,
                Fill = bushImage
            };

            Canvas.SetLeft(bushRect, 2955);
            Canvas.SetTop(bushRect, 500);
            gameWindow.MyCanvas.Children.Add(bushRect);
        }

        private void hard_Checked(object sender, RoutedEventArgs e)
        {
            gameWindow.bulletsScore = 12;
            gameWindow.jumpDown = true;
            gameWindow.player.Health = 15;
            ImageBrush bulletImage2 = new ImageBrush();
            bulletImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/bullets.png"));
            Rectangle bulletsRect4 = new Rectangle
            {
                Tag = "bullets",
                Height = 32,
                Width = 32,
                Fill = bulletImage2
            };

            Canvas.SetLeft(bulletsRect4, 4558);
            Canvas.SetTop(bulletsRect4, 151);
            gameWindow.MyCanvas.Children.Add(bulletsRect4);
        }

        //включение и отключение  фонового звука в игре 
        private void turnOnChecked(object sender, RoutedEventArgs e)
        {
            songMenu.Position = new TimeSpan(0, 0, 1);
            songMenu.Play(); 
        }

        private void turnOffChecked(object sender, RoutedEventArgs e)
        {
            songMenu.Stop();
            gameWindow.turnOffsong = true;
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            songMenu.Close();
        }
    }
}
