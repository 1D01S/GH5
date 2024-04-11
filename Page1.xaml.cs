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

namespace GH5
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        public class User
        {
            public string FIO { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Gender { get; set; }
            public string Role { get; set; }
            public string PhoneNumber { get; set; }
            public string PhotoUrl { get; set; }
        }

        public void Register_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxFIO.Text) || string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBox.Password)
                || CmbGender.SelectedItem == null || CmbRole.SelectedItem == null || string.IsNullOrEmpty(TextBoxPhoneNumber.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля!");
                return;
            }

            string login = TextBoxLogin.Text;

            using (var db = new Entities1())
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Login == login);

                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь был успешно зарегестрирован!");
                    return;
                }

                GH5.Users newUser = new GH5.Users
                {
                    FIO = TextBoxFIO.Text,
                    Login = login,
                    Password = PasswordBox.Password,
                    Sex = (CmbGender.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Role = (CmbRole.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Number = TextBoxPhoneNumber.Text,
                    Photo = TextBoxPhotoUrl.Text
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                MessageBox.Show("New user successfully registered!");
            }
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            TextBoxFIO.Text = string.Empty;
            TextBoxLogin.Text = string.Empty;
            PasswordBox.Password = string.Empty;
            CmbGender.SelectedItem = null;
            CmbRole.SelectedItem = null;
            TextBoxPhoneNumber.Text = string.Empty;
            TextBoxPhotoUrl.Text = string.Empty;
        }
    }
}