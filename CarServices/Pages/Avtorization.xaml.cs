using CarServices.Entity;
using CarServices.Pages;
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
using System.Xml.Linq;

namespace CarServices.Pages
{
    /// <summary>
    /// Логика взаимодействия для Avtorization.xaml
    /// </summary>
    public partial class Avtorization : Page
    {
        public Avtorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentUser = App.Context.Users.FirstOrDefault(x => x.Email == txtEmail.Text && x.Password == txtPassword.Password);
                if (currentUser != null)
                {
                    App.CurrentUser = currentUser;
                    this.NavigationService.Navigate(new MainPage());
                }
                else
                {
                    MessageBox.Show("Логин или пароль не совпадают", "Системная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сообщение ошибки: {ex.Message}", "Ошибка при авторизации");
            }



            
        }
    }
}
