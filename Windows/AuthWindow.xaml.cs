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
using MetPushWorkWithDB.Entities;
using MetPushWorkWithDB.Windows;

namespace MetPushWorkWithDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var user = Ent.Context.Users.ToList().
                Where(i => i.Login == tbLog.Text && i.Password == tbPas.Password).FirstOrDefault();
            if (user != null)
            {
                ClassHelper.UserData.users = user;
                switch (user.IdRole)
                {
                    case 1:
                        tbLog.Text = "Логин";
                        tbPas.Password = "";
                        AdminWindow adminWindow = new AdminWindow();
                        Hide();
                        adminWindow.ShowDialog();
                        Show();
                        break;
                    case 2:
                        tbLog.Text = "Логин";
                        tbPas.Password = "";
                        ManagerWindow managerWindow = new ManagerWindow();
                        Hide();
                        managerWindow.ShowDialog();
                        Show();
                        break;
                    case 3:
                        tbLog.Text = "Логин";
                        tbPas.Password = "";
                        UserWindow userWindow = new UserWindow();
                        Hide();
                        userWindow.ShowDialog();
                        Show();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Пользователь не нвйден","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Логин")
            {
                textBox.Text = "";
            }
        }
        private void tb_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Name == "tbLog" && textBox.Text == "")
            {
                tbLog.Text = "Логин";
            }
        }
    }
}
