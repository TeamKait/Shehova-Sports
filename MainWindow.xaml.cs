using Sports.Data;
using Sports.Pages;
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

namespace Sports
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Manager.MainFrame = MainFrame;
            Manager.DB = new SportEntities1();
            Manager.Roles = Manager.DB.Roles.ToArray();

            VisibilityController.SetButtonVisibility<LoginPage>(LoginButton, () => Manager.IsGuest);
            VisibilityController.SetButtonVisibility<CataloguePage>(CatalogueButton, () => true);
            VisibilityController.SetButtonVisibility<AdminPage>(AdminButton, () => LoginPage.FindRole(Manager.User)?.Name == "Администратор");

            MainFrame.Navigate(new CataloguePage());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new LoginPage());
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Page page)
            {
                Manager.currentPage = page;
            }
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            VisibilityController.CheckButtons();
        }
    }
}
