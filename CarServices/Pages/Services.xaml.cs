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
    /// Логика взаимодействия для Services.xaml
    /// </summary>
    public partial class Services : Page
    {
        public Services()
        {
            InitializeComponent();
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AddEditService());
        }

        private void BtnEditService_Click(object sender, RoutedEventArgs e)
        {
            Entity.Service _currentService = (sender as Button).DataContext as Entity.Service;
            NavigationService.Navigate(new Pages.AddEditService(_currentService));
        }

        private void BtnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            var _currentService = (sender as Button).DataContext as Entity.Service;
            if (System.Windows.MessageBox.Show($"Вы уверены, что хотите удалить данную услугу" + "?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                App.Context.Services.Remove(_currentService);
                App.Context.SaveChanges();
            }
        }

        private void LViewService_Loaded(object sender, RoutedEventArgs e)
        {
            LViewService.ItemsSource = App.Context.Services.ToList();
        }
    }
}
