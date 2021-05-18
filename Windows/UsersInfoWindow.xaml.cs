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

        private int numOfPage ;

        private double countPages;

        private int countRows;
        public UsersInfoWindow()
        {
            InitializeComponent();
             
            dgUsersInfo.ItemsSource = Ent.Context.Users.ToList();
            roles = Ent.Context.Roles.ToList();
            roles.Insert(0, new Roles {Role = "Все роли"});
            cbSortRole.ItemsSource = roles;
            cbSortRole.DisplayMemberPath = "Role";
            cbSortRole.SelectedIndex = 0;
            cbNumOfRecords.SelectedIndex = 0;
            numOfRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
            numOfTakenRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
            numOfPages.Text = "1";
            numOfTakenPages.Text = "1";
            btnLeft.Visibility = Visibility.Hidden;
            btnFirst.Visibility = Visibility.Hidden;
            btnSecond.Visibility = Visibility.Hidden;
            btnThird.Visibility = Visibility.Hidden;
            btnRight.Visibility = Visibility.Hidden;
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
                    numOfTakenRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
                    cbNumOfRecords.SelectedIndex = 0;
                    numOfRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
                    countRows = dgUsersInfo.Items.Count;
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
            if (cbNumOfRecords.SelectedIndex != 0)
            {
                usersList = usersList.Skip(numOfPage * cbNumOfRecords.SelectedIndex * 15).ToList();
                usersList = usersList.Take(cbNumOfRecords.SelectedIndex * 15).ToList();
                
            }
            dgUsersInfo.ItemsSource = usersList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            AddInfoWindow addInfoWindow = new AddInfoWindow();
            addInfoWindow.ShowDialog();
            dgUsersInfo.ItemsSource = Ent.Context.Users.ToList();
            numOfTakenRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
            cbNumOfRecords.SelectedIndex = 0;
            numOfRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
            countRows = dgUsersInfo.Items.Count;
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
            numOfPage = 0;
            btnFirst.Content = "1";
            btnSecond.Content = "2";
            btnThird.Content = "3";
            cbNumOfRecords.SelectedIndex = 0;
            Filter();
            numOfTakenRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
            numOfRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
            countRows = dgUsersInfo.Items.Count;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            numOfPage = 0;
            btnFirst.Content = "1";
            btnSecond.Content = "2";
            btnThird.Content = "3";
            cbNumOfRecords.SelectedIndex = 0;
            Filter();
            numOfTakenRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
            numOfRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
            countRows = dgUsersInfo.Items.Count;
        }

        private void cbNumOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cbNumOfRecords.SelectedIndex == 1)
            {
                
                countPages = Math.Ceiling(Convert.ToDouble( countRows) / (cbNumOfRecords.SelectedIndex * 15));
                numOfPages.Text = Convert.ToString( countPages);
                btnLeft.Visibility = Visibility.Visible;
                btnFirst.Visibility = Visibility.Visible;
                btnSecond.Visibility = Visibility.Visible;
                btnThird.Visibility = Visibility.Visible;
                btnRight.Visibility = Visibility.Visible;
            }
            else if (cbNumOfRecords.SelectedIndex == 2)
            {
                
                countPages = Math.Ceiling(Convert.ToDouble(countRows) / (cbNumOfRecords.SelectedIndex * 15));
                numOfPages.Text = Convert.ToString(countPages);
                btnLeft.Visibility = Visibility.Visible;
                btnFirst.Visibility = Visibility.Visible;
                btnSecond.Visibility = Visibility.Visible;
                btnThird.Visibility = Visibility.Visible;
                btnRight.Visibility = Visibility.Visible;
            }
            else if (cbNumOfRecords.SelectedIndex == 0)
            {
                numOfTakenRecords.Text = numOfRecords.Text;
                btnLeft.Visibility = Visibility.Hidden;
                btnFirst.Visibility = Visibility.Hidden;
                btnSecond.Visibility = Visibility.Hidden;
                btnThird.Visibility = Visibility.Hidden;
                btnRight.Visibility = Visibility.Hidden;
            }
            
            numOfPage = 0;
            btnFirst.Content = "1";
            btnSecond.Content = "2";
            btnThird.Content = "3";
            Filter();
            numOfTakenRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (numOfPage > 0)
            {
                numOfPage -= 1;
                btnFirst.Content = numOfPage +1;
                btnSecond.Content = numOfPage + 2;
                btnThird.Content = numOfPage + 3;
                Filter();
            }
            numOfTakenRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {

            if (numOfPage != countPages - 1)
            {
                numOfPage += 1;
                btnFirst.Content = numOfPage + 1;
                btnSecond.Content = numOfPage + 2;
                btnThird.Content = numOfPage + 3;
                Filter();
            }
            else
            {
                MessageBox.Show("Листать больше некуда :(", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            numOfTakenRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            //if(numOfPage != Convert.ToInt32(btnFirst.Content)-1)
            //{
            //numOfPage += Convert.ToInt32(btnFirst.Content) - 1;
            //Filter();
            //}
            //numOfTakenRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
        }

        private void btnSecond_Click(object sender, RoutedEventArgs e)
        {
            if (numOfPage != countPages - 1)
            {
                numOfPage += 1;
                btnFirst.Content = numOfPage + 1;
                btnSecond.Content = numOfPage + 2;
                btnThird.Content = numOfPage + 3;
                Filter();
            }
            else
            {
                MessageBox.Show("Листать больше некуда :(", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            numOfTakenRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
        }

        private void btnThird_Click(object sender, RoutedEventArgs e)
        {
            if (numOfPage != countPages - 2)
            {
                numOfPage += 2;
                btnFirst.Content = numOfPage + 1;
                btnSecond.Content = numOfPage + 2;
                btnThird.Content = numOfPage + 3;
                Filter();
            }
            else
            {
                MessageBox.Show("Листать больше некуда :(", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            numOfTakenRecords.Text = Convert.ToString(dgUsersInfo.Items.Count);
        }
    }
}
