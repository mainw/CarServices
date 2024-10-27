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
    /// Логика взаимодействия для AddEditNews.xaml
    /// </summary>
    public partial class AddEditNews : Page
    {
        public Entity.News _currentNews = null;
        public AddEditNews()
        {
            InitializeComponent();
        }
        public AddEditNews(Entity.News news)
        {
            InitializeComponent();
            NamePageBox.Text = "Редактировать новость";
            _currentNews = news;
            NameTBox.Text = _currentNews.NameNews;
            DescTBox.Text = _currentNews.News1;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = CheckErrors();

            try
            {
                if (errorMessage.Length > 0)
                {
                    System.Windows.MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (_currentNews == null)
                    {
                        var news = new Entity.News
                        {
                            NameNews = NameTBox.Text,
                            News1 = DescTBox.Text
                        };
                        App.Context.News.Add(news);
                        App.Context.SaveChanges();
                    }
                    else
                    {
                        _currentNews.NameNews = NameTBox.Text;
                        _currentNews.News1 = DescTBox.Text;
                        App.Context.SaveChanges();
                    }
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сообщение ошибки: {ex.Message}", "Ошибка при попытке сохранения");
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки: ");
            }
            return errorBuilder.ToString();
        }
    }
}
