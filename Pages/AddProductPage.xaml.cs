using Microsoft.Win32;
using Sports.Data;
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
using System.IO;
using Sports.Classes;

namespace Sports.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        private byte[] _imageData;

        public AddProductPage()
        {
            InitializeComponent();
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
                !int.TryParse(CategoryIdTextBox.Text, out int categoryId) || categoryId < 0 ||
                !int.TryParse(DiscountTextBox.Text, out int discount) || discount < 0 || discount > 100 ||
                !int.TryParse(StockTextBox.Text, out int stock) || stock < 0 ||
                !decimal.TryParse(PriceTextBox.Text, out decimal price) || price < 0 ||
                !int.TryParse(ManufacturerIdTextBox.Text, out int manufacturerId) || manufacturerId < 0)
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Product newProduct = new Product
            {
                Id = Manager.DB.Product.Max(item => item.Id) + 1,
                Name = NameTextBox.Text,
                Image = _imageData,
                CategoryId = categoryId,
                Discount = discount,
                Stock = stock,
                Price = price,
                ManufacturerId = manufacturerId
            };

            try
            {
                Manager.DB.Product.Add(newProduct);
                Manager.DB.SaveChanges();
                MessageBox.Show("Продукт успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.Navigate(new AdminPage());
            }
            catch(Exception ex)
            {
                MessageBox.Show($"При добавлении товара произошла ошибка.\nСодержание:\n{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
