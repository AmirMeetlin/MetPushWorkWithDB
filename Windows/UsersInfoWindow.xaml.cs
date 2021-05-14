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

        private List<Roles> roles = new List<Roles>();

        private List<Users> usersList = new List<Users>();
        public UsersInfoWindow()
        {
            InitializeComponent();
             
            dgUsersInfo.ItemsSource = Ent.Context.Users.ToList();
            roles = Ent.Context.Roles.ToList();
            roles.Insert(0, new Roles {Role = "Все роли"});
            cbSortRole.ItemsSource = roles;
            cbSortRole.DisplayMemberPath = "Role";
            cbSortRole.SelectedIndex = 0;
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
        private void Filter()
        {
            usersList = Ent.Context.Users.ToList();
            if(cbSortRole.SelectedIndex != 0)
            {
                usersList = usersList.Where(i => i.IdRole == cbSortRole.SelectedIndex).ToList();

            }

            usersList = usersList.
                Where(i => i.LName.Contains(tbSearch.Text)
                || i.FName.Contains(tbSearch.Text)
                || i.MName.Contains(tbSearch.Text)
                )
                .ToList();
            dgUsersInfo.ItemsSource = usersList;
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

        private void cbSortRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
    }
}
