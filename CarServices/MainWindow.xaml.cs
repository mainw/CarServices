using CarServices.Entity;
using CarServices.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

namespace CarServices
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameMain.Navigate(new Pages.Registration());
        }
        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Registration || e.Content is Avtorization)
            {
                ButtonsPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                ButtonsPanel.Visibility = Visibility.Visible;
            }
        }
        private void News_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new Pages.News());
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new MainPage());
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Закрыть приложение?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void Services_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new Pages.Services());
        }

        private void Discount_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new Discount());
        }

        private void Contacts_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new Contacts());
        }
    }
}
