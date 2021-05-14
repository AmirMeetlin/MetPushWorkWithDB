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
using MetPushWorkWithDB.ClassHelper;
using System.IO;
using Microsoft.Win32;

namespace MetPushWorkWithDB.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddInfoWindow.xaml
    /// </summary>
    public partial class AddInfoWindow : Window
    {

        private bool isEdit;
        private Users editUser;
        private string pathPhoto;
        public AddInfoWindow()
        {
            InitializeComponent();
            cbRole.ItemsSource = Ent.Context.Roles.ToList();
            cbRole.DisplayMemberPath = "Role";
            cbRole.SelectedIndex = 2;
            cbGender.ItemsSource = Ent.Context.Genders.ToList();
            cbGender.DisplayMemberPath = "Gender";
            cbGender.SelectedIndex = 1;
        }
        public AddInfoWindow(Users user)
        {
            InitializeComponent();
            cbRole.ItemsSource = Ent.Context.Roles.ToList();
            cbRole.DisplayMemberPath = "Role";
            cbGender.ItemsSource = Ent.Context.Genders.ToList();
            cbGender.DisplayMemberPath = "Gender";
            tbName.Text = user.FName;
            tbLName.Text = user.LName;
            tbMName.Text = user.MName;
            tbLog.Text = user.Login;
            tbPas.Password = user.Password;
            cbRole.SelectedIndex = Int32.Parse(user.IdRole.ToString()) - 1;
            cbGender.SelectedIndex = Int32.Parse(user.IdGender.ToString()) - 1;
            //imgUsr.Source = Convert.FromBase64String(user.Photo); не могу понять, как из иассива чисел в картинку
            editUser = user;
            isEdit = true;
        }

        private void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Логин")
            {
                textBox.Text = "";
            }
            if (textBox.Text == "Имя")
            {
                textBox.Text = "";
            }
            if (textBox.Text == "Фамилия")
            {
                textBox.Text = "";
            }
            if (textBox.Text == "Отчество")
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
            if (textBox.Name == "tbName" && textBox.Text == "")
            {
                tbName.Text = "Имя";
            }
            if (textBox.Name == "tbLName" && textBox.Text == "")
            {
                tbLName.Text = "Фамилия";
            }
            if (textBox.Name == "tbMName" && textBox.Text == "")
            {
                tbMName.Text = "Отчество";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            
                if (string.IsNullOrWhiteSpace(tbLName.Text))
                {
                    MessageBox.Show("Запомните поле Фамилия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(tbLog.Text))
                {
                    MessageBox.Show("Запомните поле Логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(tbName.Text))
                {
                    MessageBox.Show("Запомните поле Имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(tbPas.Password))
                {
                    MessageBox.Show("Запомните поле Пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            //if()
            if (!isEdit)
            {
                try
                {
                    Users userAdd = new Users();
                    userAdd.FName = tbName.Text;
                    userAdd.LName = tbLName.Text;
                    if (!string.IsNullOrWhiteSpace(tbMName.Text))
                    {
                        userAdd.MName = tbMName.Text;
                    }
                    userAdd.Login = tbLog.Text;
                    userAdd.Password = tbPas.Password;
                    userAdd.IdRole = cbRole.SelectedIndex + 1;
                    userAdd.IdGender = cbGender.SelectedIndex + 1;
                    Ent.Context.Users.Add(userAdd);
                    Ent.Context.SaveChanges();
                    if(pathPhoto != null)
                    {
                        userAdd.Photo = File.ReadAllBytes(pathPhoto);
                        
                    }
                    MessageBox.Show("Пользователь добавлен");
                    Close();
                }
                catch
                {
                    MessageBox.Show("Неопознанная ошибка сервера");
                }
            }
            else
            {
                try
                {


                    editUser.FName = tbName.Text;
                    editUser.LName = tbLName.Text;
                    editUser.MName = tbMName.Text;
                    editUser.Login = tbLog.Text;
                    editUser.Password = tbPas.Password;
                    editUser.IdRole = cbRole.SelectedIndex + 1;
                    editUser.IdGender = cbGender.SelectedIndex + 1;
                    if (pathPhoto != null)
                    {
                        editUser.Photo = File.ReadAllBytes(pathPhoto);
                        
                    }
                    Ent.Context.SaveChanges();
                    MessageBox.Show("Пользователь изменен");
                    Close();
                }
                catch
                {
                    MessageBox.Show("Неопознанная ошибка сервера");
                }
            }
        }

        private void btnImg_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            if (openFile.ShowDialog() == true)
            {
                pathPhoto = openFile.FileName;
                imgUsr.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }

    }
}
