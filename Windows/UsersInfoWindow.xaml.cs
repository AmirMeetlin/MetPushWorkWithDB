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
using MetPushWorkWithDB.Entities;

namespace MetPushWorkWithDB.Windows
{
    /// <summary>
    /// Логика взаимодействия для UsersWindow.xaml
    /// </summary>
    public partial class UsersInfoWindow : Window
    {
        public UsersInfoWindow()
        {
            InitializeComponent();
             
            dgUsersInfo.ItemsSource = Ent.Context.Users.ToList();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                if (dgUsersInfo.SelectedItem is Users userDel)
                {
                    Ent.Context.Users.Remove(userDel);
                    Ent.Context.SaveChanges();
                    dgUsersInfo.ItemsSource = Ent.Context.Users.ToList();
                    MessageBox.Show("Запись успешно удалена","Удаление",MessageBoxButton.OK,MessageBoxImage.Information);
                }
               
            }
            else
            {
                return;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            AddInfoWindow addInfoWindow = new AddInfoWindow();
            addInfoWindow.ShowDialog();
            dgUsersInfo.ItemsSource = Ent.Context.Users.ToList();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if(dgUsersInfo.SelectedItem is Users user )
            {
                AddInfoWindow addInfoWindow = new AddInfoWindow(user);
                addInfoWindow.ShowDialog();
                dgUsersInfo.ItemsSource = Ent.Context.Users.ToList();


            }
        }
    }
}
