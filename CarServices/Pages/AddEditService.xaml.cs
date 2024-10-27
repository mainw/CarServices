using CarServices.Entity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddEditService.xaml
    /// </summary>
    public partial class AddEditService : Page
    {
        Entity.Service _currentService = null;
        private byte[] _mainImageData = null;
        public AddEditService()
        {
            InitializeComponent();
        }

        public AddEditService(Entity.Service service)
        {
            InitializeComponent();
            _currentService = service;
            NamePageBox.Text = "Редактировать услугу:";
            NameTBox.Text = service.ServiceName;
            DescTBox.Text = service.ServiceDescription;
            PriceTBox.Text = service.ServicePrice.ToString();
            if (service.ServiceIcon != null)
                ImageService.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(service.ServiceIcon);
        }

        private void LoadImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображение | *.png; *.jpg; *.jpeg; *.web";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _mainImageData = File.ReadAllBytes(openFileDialog.FileName);
                    ImageService.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImageData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Сообщение ошибки: {ex.Message}", "Ошибка при загрузке");

                }
            }
        }

        decimal costService;

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = CheckErrors();

            if (errorMessage.Length > 0)
            {
                System.Windows.MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    if (_currentService == null)
                    {
                        var service = new Entity.Service
                        {
                            ServiceName = NameTBox.Text,
                            ServiceDescription = DescTBox.Text,
                            ServicePrice = Math.Round(costService,2),
                            ServiceIcon = _mainImageData
                        };
                        App.Context.Services.Add(service);
                        App.Context.SaveChanges();
                    }
                    else
                    {
                        _currentService.ServiceName = NameTBox.Text;
                        _currentService.ServiceDescription = DescTBox.Text;
                        _currentService.ServicePrice = Math.Round(costService, 2);
                        if (_mainImageData != null)
                            _currentService.ServiceIcon = _mainImageData;
                        App.Context.SaveChanges();
                    }
                    NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Сообщение ошибки: {ex.Message}", "Ошибка при попытке сохранения");
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(NameTBox.Text))
                errorBuilder.AppendLine("Поле с названием услуги не может быть пустым;");
            else if (NameTBox.Text.Length > 50)
                errorBuilder.AppendLine("Поле с названием услуги не может содержать более 50 символов;");

            if (string.IsNullOrWhiteSpace(DescTBox.Text))
                errorBuilder.AppendLine("Поле с описанием услуги не может быть пустым;");
            else if (DescTBox.Text.Length > 400)
                errorBuilder.AppendLine("Поле с описанием услуги не может содержать более 400 символов;");

            if (!decimal.TryParse(PriceTBox.Text, out costService))
                errorBuilder.AppendLine("Стоимость услуги содержит некорреткные данные;");
            else if (costService <= 0)
                errorBuilder.AppendLine("Стоимость услуги не может быть указана 0 или меньше;");

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки: ");
            }
            return errorBuilder.ToString();
        }
    }
}
