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
    /// Логика взаимодействия для News.xaml
    /// </summary>
    public partial class News : Page
    {
        public News()
        {
            InitializeComponent();
            LViewNews.ItemsSource = App.Context.News.ToList();
        }
        private void Update()
        {
            LViewNews.ItemsSource = App.Context.News.ToList();
        }

        private void BtnAddNews_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditNews());
        }


        private void LViewNews_Loaded(object sender, RoutedEventArgs e)
        {
            LViewNews.ItemsSource = App.Context.News.ToList();
        }

        private void BtnDeleteNews_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var _currentNews = (sender as Button).DataContext as Entity.News;
                if (System.Windows.MessageBox.Show($"Вы уверены, что хотите удалить данную новость" + "?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {

                    App.Context.News.Remove(_currentNews);
                    App.Context.SaveChanges();
                    Update();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сообщение ошибки: {ex.Message}", "Ошибка при удалении");
            }
        }

        private void BtnEditNews_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var _currentNews = (sender as Button).DataContext as Entity.News;
                this.NavigationService.Navigate(new AddEditNews(_currentNews));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сообщение ошибки: {ex.Message}", "Ошибка при переходе на другую страницу");
            }

        }
    }
}
