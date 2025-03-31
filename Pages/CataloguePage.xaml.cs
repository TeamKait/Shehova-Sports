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

namespace Sports.Pages
{
    /// <summary>
    /// Логика взаимодействия для CataloguePage.xaml
    /// </summary>
    public partial class CataloguePage : Page
    {
        public CataloguePage()
        {
            InitializeComponent();
            ListView.ItemsSource = Classes.Manager.DB.Product.ToList();
        }

        private void HandleBuyButton(object sender, RoutedEventArgs e)
        {
            if(sender is Button button && button.DataContext is Product product)
            {
                if(!Classes.Manager.IsGuest)
                {
                    MessageBox.Show("Оформить заказ?", "Покупка", MessageBoxButton.YesNo, MessageBoxImage.Question);
                }
                else
                {
                    MessageBox.Show("Войдите, чтобы оформить заказ", "Покупка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
