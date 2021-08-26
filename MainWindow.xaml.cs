using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        string connectionStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Programs\Game_Treasure_Hunter\Database\GameDB.mdf;Integrated Security=True";


        public MainWindow()
        {
            InitializeComponent();
            //загрузка и включения музыки в меню
            songMenu = new MediaPlayer();
            songMenu.Open(new Uri(@"../../GameSounds/RockPrivet.wma", UriKind.Relative));
            songMenu.Play();
        }

        // кнопка старта игры
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            songMenu.Stop();
            if (turnOn.IsChecked == true)
            {
                gameWindow.backgroundMedia.Play();//запуск звука фона первого раунда
            }
            if(normal.IsChecked == false && hard.IsChecked == false)
            {
                gameWindow.choiceComplexity = "Easy";// если выбор сложности не сделан, то значение будет равным easy 
            }
            gameWindow.Show();
            gameWindow.soundsGame = true;//запустить звуки из игры
            this.Close();
        }

        // кнопка выхода из игры
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
            gameWindow.choiceComplexity = "Normal";
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
            gameWindow.choiceComplexity = "Hard";
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

        //выгрузка данных из базы данных для таблицы управления
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable data;
            string sqlRequest = String.Format("SELECT control, action FROM Keys");

            using (SqlConnection connection2 = new SqlConnection(connectionStr))
            {
                await connection2.OpenAsync();
                SqlCommand cmd = new SqlCommand(sqlRequest, connection2);
                using (SqlDataReader reader5 = await cmd.ExecuteReaderAsync())
                {
                    if (reader5.HasRows)//проверка данных
                    {
                        while (await reader5.ReadAsync()) //построчно считываем данные
                        {
                            Control controlKeys = new Control()
                            {
                                control = reader5.GetString(0),
                                action = reader5.GetString(1)
                            };
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данных нет! ", "База данных", MessageBoxButton.OK, MessageBoxImage.Warning);
                        
                    }
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlRequest, connection2);
                data = new DataTable();
                sqlDataAdapter.Fill(data);
                controlGrid.ItemsSource = data.DefaultView;
            }

          
        }

        //выход из игры при нажатии на красный крестик....Не корректно взаимодействует с другими окнами приложения
        //private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    string message3 = "Вы уверены,что хотите закрыть приложение?";
        //    MessageBoxResult result3 = MessageBox.Show(message3, "TREASURE HUNTER", MessageBoxButton.YesNo);

        //    switch (result3)
        //    {
        //        case MessageBoxResult.Yes:
        //            songMenu.Close();//закрытие аудио файла из меню
        //            Application.Current.Shutdown();
        //            break;
        //        case MessageBoxResult.No:
        //            e.Cancel = true;
        //            break;
        //    }
        //}
    }
}
