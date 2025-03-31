using Microsoft.Win32;
using Sports.Data;
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

namespace Sports.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditProductPage.xaml
    /// </summary>
    public partial class EditProductPage : Page
    {
        private Product _product;
        private byte[] _imageData;

        public EditProductPage(Product product)
        {
            InitializeComponent();
            _product = product;
            LoadProductData();
        }

        private void LoadProductData()
        {
            NameTextBox.Text = _product.Name;
            CategoryIdTextBox.Text = _product.CategoryId.ToString();
            DiscountTextBox.Text = _product.Discount.ToString();
            StockTextBox.Text = _product.Stock.ToString();
            PriceTextBox.Text = _product.Price.ToString();
            ManufacturerIdTextBox.Text = _product.ManufacturerId.ToString();
            _imageData = _product.Image;

            if (_imageData != null)
            {
                using (var ms = new MemoryStream(_imageData))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    PreviewImage.Source = image;
                }
            }
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png" };
            if (openFileDialog.ShowDialog() == true)
            {
                _imageData = File.ReadAllBytes(openFileDialog.FileName);
                PreviewImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                !int.TryParse(CategoryIdTextBox.Text, out int categoryId) ||
                !int.TryParse(DiscountTextBox.Text, out int discount) ||
                !int.TryParse(StockTextBox.Text, out int stock) ||
                !decimal.TryParse(PriceTextBox.Text, out decimal price) ||
                !int.TryParse(ManufacturerIdTextBox.Text, out int manufacturerId))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _product.Name = NameTextBox.Text;
            _product.Image = _imageData;
            _product.CategoryId = categoryId;
            _product.Discount = discount;
            _product.Stock = stock;
            _product.Price = price;
            _product.ManufacturerId = manufacturerId;

            MessageBox.Show("Продукт успешно обновлён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }
    }
}
