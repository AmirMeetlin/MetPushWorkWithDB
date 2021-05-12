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
using System.Windows.Shapes;

namespace MetPushWorkWithDB.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            tbAdminInfo.Text = $"{ClassHelper.UserData.users.LName} {ClassHelper.UserData.users.FName} {ClassHelper.UserData.users.MName}";
        }

        private void btnViewUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersInfoWindow usersInfoWindow = new UsersInfoWindow();
            usersInfoWindow.ShowDialog();
        }
    }
}
