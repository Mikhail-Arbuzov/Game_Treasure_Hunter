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
        Result result2;// переменная для вывода данных из таблицы БД

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
                            result2 = new Result()
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
    }
}
