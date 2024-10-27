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

namespace CarServices.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }


        private void toAvrorizationformHyperLink_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Avtorization());
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            txtPasswordVisible.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Collapsed;
            txtPasswordVisible.Text = txtPassword.Password;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPassword.Visibility = Visibility.Visible;
            txtPasswordVisible.Visibility = Visibility.Collapsed;
            txtPassword.Password = txtPasswordVisible.Text;
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            // Попытка заполнить все поля
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) ||
                    string.IsNullOrEmpty(txtSurname.Text) ||
                    string.IsNullOrEmpty(txtEmail.Text) ||
                    string.IsNullOrEmpty(txtPassword.Password) ||
                    string.IsNullOrEmpty(txtRepeatPassword.Password))
                    throw new Exception("Поля заполнены не полностью");

                // Проверка, совпадают ли пароли
                if (txtPassword.Password != txtRepeatPassword.Password)
                    throw new Exception("Пароли не совпадают");

                // Регистрация пользователя...
                if (App.Context.Users.Any(user => user.Email == txtEmail.Text))
                    throw new Exception("Пользователь с такой почтой ранее был зарегистрирован!");

                var newUser = new Entity.User
                {
                    UserSurname = txtSurname.Text,
                    UserName = txtName.Text,
                    Email = txtEmail.Text,
                    Password = txtRepeatPassword.Password,
                    RoleID = 0
                };
                App.Context.Users.Add(newUser);
                App.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
