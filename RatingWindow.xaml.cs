using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public Result result;// переменная для экземпляра класса модели полей из таблицы БД
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Programs\Game_Treasure_Hunter\Database\GameDB.mdf;Integrated Security=True";
       
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
                MessageBox.Show("имя добавлено!","База данных",MessageBoxButton.OK, MessageBoxImage.Information);
                NewNameText.Text = "";
                NewNameText.BorderBrush = Brushes.Black;

            }
            else//данные не отправлены
            {
                MessageBox.Show("имя не добавлено!","База данных", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearingTextField()
        {
            //NewNameText.Text = "";
            NewNameText.ToolTip = "";
            NewNameText.BorderBrush = Brushes.Transparent;
        }
    }
}
