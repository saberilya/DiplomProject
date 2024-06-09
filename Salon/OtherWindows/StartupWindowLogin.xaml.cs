using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Salon.OtherWindows
{
    /// <summary>
    /// Логика взаимодействия для StartupWindowLogin.xaml
    /// </summary>
    public partial class StartupWindowLogin : Window
    {
        
        public StartupWindowLogin()
        {
            InitializeComponent();
        }

        private void ClientClick_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void EmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            var userIsWorker = Classes.Context._context.Users.Where(emp => emp.Login == LoginBox.Text && emp.Password == PasswordBox.Text && emp.Role == 1).Any();
            string login = LoginBox.Text;
            string password = PasswordBox.Text;
            if (login.Length < 3)
            {
                LoginBox.ToolTip = "Поле 'Логин' введено некорректно";
                LoginBox.Background = Brushes.LightPink;
            }
            else if (password.Length < 5)
            {
                PasswordBox.ToolTip = "Поле 'Пароль' введено некорректно";
                PasswordBox.Background = Brushes.LightPink;
            }
            else
            {
                if (userIsWorker)
                {

                    AdminCategory.AdminWindow adminWindow = new AdminCategory.AdminWindow();
                    adminWindow.Show();

                }
            }
        }
    }
}
