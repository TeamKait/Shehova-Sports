using Sports.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;

namespace Sports.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private string login => LoginTextBox.Text;
        private string password => PasswordBox.Password;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            TryLogin();
        }

        public static Roles FindRole(Users user)
        {
            return Classes.Manager.Roles.FirstOrDefault(findRole => findRole.Id == user?.RoleId);
        }

        private void HandleKeyDown(object sender,  KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                TryLogin();
            }
        }

        private void TryLogin()
        {
            try
            {
                if (login == "" || password == "")
                {
                    throw new Exception("Данные введены неправильно");
                }
                Users user = Classes.Manager.DB.Users.FirstOrDefault(findUser => findUser.Login == login);
                if (user == null)
                {
                    throw new Exception("Пользователь не найден!");
                }
                if (user.Password != password)
                {
                    throw new Exception("Неверный пароль!");
                }

                Roles role = FindRole(user);

                if (role == null)
                {
                    throw new Exception("Данные о пользователе не найдены!");
                }
                Classes.Manager.User = user;
                Classes.Manager.MainFrame.Navigate(new CataloguePage());
                MessageBox.Show($"Здравствуй, {role.Name} {user.FirstName}!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new CataloguePage());
            MessageBox.Show("Вы вошли как гость", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
