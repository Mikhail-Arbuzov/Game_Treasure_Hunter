using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Game_Treasure_Hunter
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            SplashScreen splashScreen = new SplashScreen("backgroundForest2.jpg"); // при загрузки приложения отображается заставка на экране
            splashScreen.Show(false);
            splashScreen.Close(TimeSpan.FromSeconds(6));// время на ее закрытие
        }
    }
}
