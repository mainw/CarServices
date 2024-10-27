using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CarServices
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entity.CarServiceEntities Context { get; } = new Entity.CarServiceEntities();
        public static Entity.User CurrentUser = null;
    }
}
