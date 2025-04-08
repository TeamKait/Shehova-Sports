using Sports.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.ComponentModel;
using System.Collections;

namespace Sports.Pages
{
    public partial class CataloguePage : Page
    {
        public ObservableCollection<Product> Products { get; set; }

        public CataloguePage()
        {
            InitializeComponent();
            Products = new ObservableCollection<Product>(Classes.Manager.DB.Product.ToList());
            ListView.ItemsSource = Products;
        }

        private void HandleBuyButton(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Product product)
            {
                if (!Classes.Manager.IsGuest)
                {
                    MessageBox.Show("Оформить заказ?", "Покупка", MessageBoxButton.YesNo, MessageBoxImage.Question);
                }
                else
                {
                    MessageBox.Show("Войдите, чтобы оформить заказ", "Покупка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        // Обработчик изменения текста в поисковом поле
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListCollectionView view = (ListCollectionView)CollectionViewSource.GetDefaultView(Products);
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                view.CustomSort = null;
            }
            else
            {
                view.CustomSort = new ProductFuzzyComparer(SearchBox.Text);
            }
            view.Refresh();
        }

        // Класс для сравнения товаров по методу нечеткого поиска
        public class ProductFuzzyComparer : IComparer
        {
            private readonly string _query;

            public ProductFuzzyComparer(string query)
            {
                _query = query.ToLower().Trim();
            }

            public int Compare(object x, object y)
            {
                if (!(x is Product a) || !(y is Product b))
                {
                    return 0;
                }

                // Чем ниже общий нормализованный балл – тем лучше совпадение
                double scoreA = GetCombinedScore(a);
                double scoreB = GetCombinedScore(b);

                return scoreA.CompareTo(scoreB);
            }

            private double GetCombinedScore(Product product)
            {
                // Вычисляем нормализованное расстояние для каждого поля.
                // Если поле null или пустое, считаем максимальное несходство (1.0).
                double nameScore = NormalizedLevenshteinDistance(_query, product.Name);
                double manufacturerScore = NormalizedLevenshteinDistance(_query, product.ManufacturerId.ToString());
                double priceScore = NormalizedLevenshteinDistance(_query, product.Price.ToString());

                // Применяем весовые коэффициенты (подберите веса под задачу):
                // Например, имя имеет наибольший приоритет, потом производитель, затем цена.
                double weightedName = nameScore * 1.0;
                double weightedManufacturer = manufacturerScore * 0.5;
                double weightedPrice = priceScore * 0.2;

                return weightedName + weightedManufacturer + weightedPrice;
            }

            private double NormalizedLevenshteinDistance(string query, string target)
            {
                if (string.IsNullOrEmpty(target))
                    return 1.0;

                query = query.ToLower();
                target = target.ToLower();

                int lev = LevenshteinDistance(query, target);
                // Делим на максимум из длин сравниваемых строк, чтобы нормализовать значение
                int maxLength = Math.Max(query.Length, target.Length);
                return maxLength == 0 ? 0 : (double)lev / maxLength;
            }

            private int LevenshteinDistance(string s, string t)
            {
                int n = s.Length;
                int m = t.Length;
                int[,] d = new int[n + 1, m + 1];

                if (n == 0) return m;
                if (m == 0) return n;

                for (int i = 0; i <= n; i++)
                    d[i, 0] = i;
                for (int j = 0; j <= m; j++)
                    d[0, j] = j;

                for (int i = 1; i <= n; i++)
                {
                    for (int j = 1; j <= m; j++)
                    {
                        int cost = s[i - 1] == t[j - 1] ? 0 : 1;

                        d[i, j] = Math.Min(
                            Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                            d[i - 1, j - 1] + cost);
                    }
                }

                return d[n, m];
            }
        }
    }
}