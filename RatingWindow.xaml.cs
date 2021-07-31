using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Speech.Synthesis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game_Treasure_Hunter
{
    /// <summary>
    /// Логика взаимодействия для RatingWindow.xaml
    /// </summary>
    public partial class RatingWindow : Window
    {
        bool isSuccess;
        bool successfully;
        public Result result;// переменная для сбора данных из игры и отправки их в таблицу БД
        

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Programs\Game_Treasure_Hunter\Database\GameDB.mdf;Integrated Security=True";

        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();//для озвучки текста

        public RatingWindow()
        {
            InitializeComponent();
            result = new Result();
            
        }

        private void AddRatingButton_Click(object sender, RoutedEventArgs e)
        {
           
            string name = NewNameText.Text.Trim();// добавляю данные из textbox и убираю лишнии пробелы
            string sqlExpression = String.Format("INSERT INTO Results (name,health,coins,cartridges,complexity,time) VALUES (@name,'{0}','{1}','{2}','{3}','{4}')",result.health,result.coins,result.cartridges, result.complexity,result.time);
            //проверка на ввод данных в текстовое поле с учетом регулярного выражения и пустого текстового поля 
            if (Regex.IsMatch(name, @"^[а-яА-ЯёЁa-zA-Z]+$") && NewNameText.Text != String.Empty)
            {
                ClearingTextField();//метод для очистки всплывающей подсказки
                using (SqlConnection connection = new SqlConnection(connectionString))//подключение к БД
                {
                    try 
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlParameter nameParametr = new SqlParameter("@name", System.Data.SqlDbType.NVarChar, 8);//параметер для вводимых данных, отправляимых в БД. С указанием типа и размера 
                        nameParametr.Value = name;//вводимое значение
                        command.Parameters.Add(nameParametr);//добавляю параметер в Sql-запрос 
                        int rows = command.ExecuteNonQuery();//выполнение Sql-запроса

                        //проверка на отправку данных в БД
                        if (rows > 0)
                        {
                            isSuccess = true;
                        }
                        else
                        {
                            isSuccess = false;
                        }
                    }
                    catch 
                    {
                        MessageBox.Show("Имя не добавлено! Возможно такое имя игрока уже есть!", "База данных", MessageBoxButton.OK, MessageBoxImage.Warning);
                        connection.Close();

                    }
                   
                   
                }
            }
           
            //else if (NewNameText.Focusable == false)
            //{
            //    if(NewNameText.Text == String.Empty)
            //        MessageBox.Show("заполните поле!");

            //}
            else
            {
                NewNameText.BorderBrush = Brushes.Red;//границу текстового поля выделяю красным цветом
                NewNameText.ToolTip = "Это поле введено не корректно! При вводе должны использоваться только русские или английские буквы";//вывожу подсказку
            }
           
            if(isSuccess == true)//данные отправлены
            {
                MessageBox.Show("Имя добавлено!","База данных",MessageBoxButton.OK, MessageBoxImage.Information);
                NewNameText.Text = "";
                NewNameText.BorderBrush = Brushes.Black;

            }
            else//данные не отправлены
            {
                MessageBox.Show("Имя не добавлено!","База данных", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearingTextField()
        {
            //NewNameText.Text = "";
            NewNameText.ToolTip = "";
            NewNameText.BorderBrush = Brushes.Transparent;
        }

        private void UpdateRatingButton_Click(object sender, RoutedEventArgs e)
        {
            string oldName = NameText.Text.Trim();// добавляю данные из textbox и убираю лишнии пробелы
            string sqlExpression2 = String.Format("UPDATE Results SET health ='{0}', coins ='{1}', cartridges ='{2}', complexity ='{3}', time ='{4}' WHERE name =@oldName ", result.health, result.coins, result.cartridges, result.complexity, result.time);
            if (Regex.IsMatch(oldName, @"^[а-яА-ЯёЁa-zA-Z]+$") && NameText.Text != String.Empty)
            {
                using (SqlConnection connection2 = new SqlConnection(connectionString))
                {
                    connection2.Open();
                    SqlCommand command2 = new SqlCommand(sqlExpression2, connection2);
                    SqlParameter nameParam = new SqlParameter("@oldName", System.Data.SqlDbType.NVarChar, 8);//параметер для вводимых данных, отправляимых в БД. С указанием типа и размера 
                    nameParam.Value = oldName;//вводимое значение
                    command2.Parameters.Add(nameParam);//добавляю параметер в Sql-запрос 
                    int check = command2.ExecuteNonQuery();

                    if (check > 0)//делаю проверку
                    {
                        successfully = true;
                    }
                    else
                    {
                        successfully = false;
                    }
                }
            }
            else
            {
                NameText.BorderBrush = Brushes.Red;
            }

            if(successfully == true)
            {
                MessageBox.Show("Рейтинг игрока обнавлен!", "База данных", MessageBoxButton.OK, MessageBoxImage.Information);
                NameText.Text = "";
                NameText.BorderBrush = Brushes.Green;
            }
            else
            {
                MessageBox.Show("Рейтинг игрока не обнавился! Возможно такого имени игрока нет в базе данных. Или это поле введено не корректно! При вводе должны использоваться только русские или английские буквы ", "База данных", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnEasyButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt; // переменная автаномная часть структуры бд ввиде таблицы  
            PromptBuilder promptBuilder = new PromptBuilder(); // для настройки произношения текста 

            string sqlExpression3 = String.Format("SELECT name, health, coins, cartridges, complexity, time FROM Results WHERE complexity = 'Easy' ORDER BY time ASC, cartridges ASC, coins DESC ");

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                SqlCommand command3 = new SqlCommand(sqlExpression3, connect);

                using (SqlDataReader reader = command3.ExecuteReader())
                {
                    if (reader.HasRows)//проверка данных
                    {
                        while(reader.Read()) //построчно считываем данные
                        {
                            Result result2 = new Result() //для вывода данных из таблицы БД
                            {
                                name = reader.GetString(0),
                                health = reader.GetInt32(1),
                                coins = reader.GetInt32(2),
                                cartridges = reader.GetInt32(3),
                                complexity = reader.GetString(4),
                                time2 = reader.GetTimeSpan(5)
                            };
                            //string name = reader.GetString(0);
                            //int health = reader.GetInt32(1);
                            //int coins = reader.GetInt32(2);
                            //int cartridges = reader.GetInt32(3);
                            //string complexity = reader.GetString(4);
                            //TimeSpan time = reader.GetTimeSpan(5);
                        }
                    }
                    else
                    {
                        speechSynthesizer.SpeakAsyncCancelAll();// отменяет все асинхронные операции синтеза речи
                        MessageBox.Show("Данных нет! ", "База данных", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                //создаю модель виртуальной таблицы чтобы перенести ее в DataGrid для визуализации
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlExpression3, connect);
                dt = new DataTable();
                dataAdapter.Fill(dt);// заполняем таблицу
                ratingGrid.ItemsSource = dt.DefaultView; // делаю привязку к представлению таблицы
                //получение значение первой ячейки первой строки  через DataTable
                string firstCell = dt.Rows[0][0].ToString();
                //получение значение первой ячейки первой строки  с помощью DataGridCellInfo не до конца логика была развита..
                //var cellInfo = ratingGrid.SelectedCells[0];
                //var content = cellInfo.Column.GetCellContent(cellInfo.Item);

                promptBuilder.StartStyle(new PromptStyle()// настройка речи
                {
                    Emphasis = PromptEmphasis.Reduced,//низкий уровень выделения текста
                    Rate = PromptRate.Medium,// средняя скорость речи
                    Volume = PromptVolume.Loud // высокая громкость речи
                });
                promptBuilder.AppendText("Победителем");//добавляю текст
                promptBuilder.AppendText("ста");
                promptBuilder.AppendText("но", PromptEmphasis.Strong);//ударение
                promptBuilder.AppendText("вится");
                //promptBuilder.AppendTextWithPronunciation("становится", "становица");// иключение выходит не прафильная фонетика "с"
                promptBuilder.AppendBreak(TimeSpan.FromMilliseconds(200));//пауза
                promptBuilder.AppendText(String.Format("{0}", firstCell), PromptEmphasis.Strong);// называет имя из поля name
                promptBuilder.EndStyle();

                speechSynthesizer.SpeakAsync(promptBuilder);//воспроизведение речи
            }
        }

        private void OnNormalButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable;
            PromptBuilder promptBuilder2 = new PromptBuilder();
            string sqlExpression4 = String.Format("SELECT name, health, coins, cartridges, complexity, time FROM Results WHERE complexity = 'Normal' ORDER BY time ASC, cartridges ASC, coins DESC ");

            using (SqlConnection connect2 = new SqlConnection(connectionString))
            {
                connect2.Open();
                SqlCommand command4 = new SqlCommand(sqlExpression4, connect2);

                using (SqlDataReader reader2 = command4.ExecuteReader())
                {
                    if (reader2.HasRows)//проверка данных
                    {
                        while (reader2.Read()) //построчно считываем данные
                        {
                            Result result3 = new Result()
                            {
                                name = reader2.GetString(0),
                                health = reader2.GetInt32(1),
                                coins = reader2.GetInt32(2),
                                cartridges = reader2.GetInt32(3),
                                complexity = reader2.GetString(4),
                                time2 = reader2.GetTimeSpan(5)
                            };
                        }
                    }
                    else
                    {
                        speechSynthesizer.SpeakAsyncCancelAll();// отменяет все асинхронные операции синтеза речи
                        MessageBox.Show("Данных нет! ", "База данных", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(sqlExpression4, connect2);
                dataTable = new DataTable();
                dataAdapter2.Fill(dataTable);// заполняем таблицу
                ratingGrid.ItemsSource = dataTable.DefaultView;

                string firstCell2 = dataTable.Rows[0][0].ToString();

                promptBuilder2.StartStyle(new PromptStyle()// настройка речи
                {
                    Emphasis = PromptEmphasis.Reduced,//низкий уровень выделения текста
                    Rate = PromptRate.Medium,// средняя скорость речи
                    Volume = PromptVolume.Loud // высокая громкость речи
                });
                promptBuilder2.AppendText("Выигрывает");//добавляю текст
                promptBuilder2.AppendBreak(TimeSpan.FromMilliseconds(200));//пауза
                promptBuilder2.AppendText(String.Format("{0}", firstCell2), PromptEmphasis.Strong);// называет имя из поля name
                promptBuilder2.EndStyle();

                speechSynthesizer.SpeakAsync(promptBuilder2);//воспроизведение речи
            }
        }

        private void OnHardButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable2;
            PromptBuilder promptBuilder3 = new PromptBuilder();
            string sqlExpression5 = String.Format("SELECT name, health, coins, cartridges, complexity, time FROM Results WHERE complexity = 'Hard' ORDER BY time ASC, cartridges ASC, coins DESC ");

            using (SqlConnection connect3 = new SqlConnection(connectionString))
            {
                connect3.Open();
                SqlCommand command5 = new SqlCommand(sqlExpression5, connect3);

                using (SqlDataReader reader3 = command5.ExecuteReader())
                {
                    if (reader3.HasRows)//проверка данных
                    {
                        while (reader3.Read()) //построчно считываем данные
                        {
                            Result result4 = new Result()
                            {
                                name = reader3.GetString(0),
                                health = reader3.GetInt32(1),
                                coins = reader3.GetInt32(2),
                                cartridges = reader3.GetInt32(3),
                                complexity = reader3.GetString(4),
                                time2 = reader3.GetTimeSpan(5)
                            };
                        }
                    }
                    else
                    {
                        speechSynthesizer.SpeakAsyncCancelAll();// отменяет все асинхронные операции синтеза речи
                        MessageBox.Show("Данных нет! ", "База данных", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

                SqlDataAdapter dataAdapter3 = new SqlDataAdapter(sqlExpression5, connect3);
                dataTable2 = new DataTable();
                dataAdapter3.Fill(dataTable2);// заполняем таблицу
                ratingGrid.ItemsSource = dataTable2.DefaultView;

                string firstCell3 = dataTable2.Rows[0][0].ToString();

                promptBuilder3.StartStyle(new PromptStyle()// настройка речи
                {
                    Emphasis = PromptEmphasis.Reduced,//низкий уровень выделения текста
                    Rate = PromptRate.Medium,// средняя скорость речи
                    Volume = PromptVolume.Loud // высокая громкость речи
                });
                promptBuilder3.AppendText("Победил");//добавляю текст
                promptBuilder3.AppendBreak(TimeSpan.FromMilliseconds(200));//пауза
                promptBuilder3.AppendText(String.Format("{0}", firstCell3), PromptEmphasis.Strong);// называет имя из поля name
                promptBuilder3.EndStyle();

                speechSynthesizer.SpeakAsync(promptBuilder3);//воспроизведение речи
            }
        }

        private void AllOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable3;
            PromptBuilder promptBuilder4 = new PromptBuilder();
            string sqlExpression6 = String.Format("SELECT name, health, coins, cartridges, complexity, time FROM Results ORDER BY cartridges ASC, time ASC, coins DESC ");

            using (SqlConnection connect4 = new SqlConnection(connectionString))
            {
                connect4.Open();
                SqlCommand command6 = new SqlCommand(sqlExpression6, connect4);
                using (SqlDataReader reader4 = command6.ExecuteReader())
                {
                    if (reader4.HasRows)//проверка данных
                    {
                        while (reader4.Read()) //построчно считываем данные
                        {
                            Result result4 = new Result()
                            {
                                name = reader4.GetString(0),
                                health = reader4.GetInt32(1),
                                coins = reader4.GetInt32(2),
                                cartridges = reader4.GetInt32(3),
                                complexity = reader4.GetString(4),
                                time2 = reader4.GetTimeSpan(5)
                            };
                        }
                    }
                    else
                    {
                        speechSynthesizer.SpeakAsyncCancelAll();// отменяет все асинхронные операции синтеза речи
                        MessageBox.Show("Данных нет! ", "База данных", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

                SqlDataAdapter dataAdapter4 = new SqlDataAdapter(sqlExpression6, connect4);
                dataTable3 = new DataTable();
                dataAdapter4.Fill(dataTable3);// заполняем таблицу
                ratingGrid.ItemsSource = dataTable3.DefaultView;

                string firstCell4 = dataTable3.Rows[0][0].ToString();

                promptBuilder4.StartStyle(new PromptStyle()// настройка речи
                {
                    Emphasis = PromptEmphasis.Reduced,//низкий уровень выделения текста
                    Rate = PromptRate.Medium,// средняя скорость речи
                    Volume = PromptVolume.Loud // высокая громкость речи
                });
                promptBuilder4.AppendText("Чемпион");//добавляю текст
                promptBuilder4.AppendBreak(TimeSpan.FromMilliseconds(200));//пауза
                promptBuilder4.AppendText(String.Format("{0}", firstCell4), PromptEmphasis.Strong);// называет имя из поля name
                promptBuilder4.EndStyle();

                speechSynthesizer.SpeakAsync(promptBuilder4);//воспроизведение речи

            }

        }
    }
}
