﻿using Sports.Data;
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
using Sports.Classes;

namespace Sports.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {

        public AdminPage()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            ProductsDataGrid.ItemsSource = null;
            ProductsDataGrid.ItemsSource = Manager.DB.Product.ToList();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is Product product)
            {
                // Реализуйте переход на страницу редактирования, передавая выбранный продукт
                NavigationService?.Navigate(new EditProductPage(product));
            }
        }

        // Обработчик кнопки "Добавить"
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу добавления нового продукта
            NavigationService?.Navigate(new AddProductPage());
        }

        // Обработчик кнопки "Удалить"
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProducts = ProductsDataGrid.SelectedItems.Cast<Product>().ToList();
            if (!selectedProducts.Any())
            {
                MessageBox.Show("Пожалуйста, выберите один или несколько продуктов для удаления.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Запрос подтверждения
            var result = MessageBox.Show($"Вы действительно хотите удалить выбранные {selectedProducts.Count} продукт(ов)?",
                                          "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                foreach (var product in selectedProducts)
                {
                    Manager.DB.Product.Remove(product);
                }
                Classes.Manager.DB.SaveChanges();
                UpdateDataGrid();
            }
        }
    }
}
