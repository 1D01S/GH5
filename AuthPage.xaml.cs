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
    public partial class AuthPage : Page
    {
        public string Username
        {
            get { return txtUsername.Text; }
            set { txtUsername.Text = value; }
        }

        public string Password
        {
            get { return txtPassword.Password; }
            set { txtPassword.Password = value; }
        }

        public AuthPage()
        {
            InitializeComponent();
        }
        public bool IsCaptchaDisplayed()
        {
            if (NavigationService.Content is CAPCA captchaPage)
            {
                return captchaPage.IsVisible;
            }

            return false;
        }

        public string GetCaptchaTask()
        {
            if (NavigationService.Content is CAPCA captchaPage)
            {
                return captchaPage.GetGeneratedCaptcha();
            }

            return string.Empty;
        }

        public void SolveCaptcha(string solution)
        {
            if (NavigationService.Content is CAPCA captchaPage)
            {
                captchaPage.EnterCaptchaSolution(solution);
            }
        }

        public void SetLoginCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void Login_Click(object sender, RoutedEventArgs e)
        {
            bool loginSuccess = Auth(txtUsername.Text, txtPassword.Password);

            if (loginSuccess)
            {
                MessageBox.Show("Успешный вход!");
            }
            else
            {
                loginAttempts++;
                if (loginAttempts >= 3)
                {
                    CAPCA captchaPage = new CAPCA(); 
                    this.NavigationService.Navigate(captchaPage); 
                }
            }
        }
        public void Login()
        {
            string enteredUsername = Username;
            string enteredPassword = Password;

            if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
            {
                MessageBox.Show("Please enter both username and password");
                return;
            }

            // Here you can write the actual authentication logic, checking against a database or any authentication service
            bool isAuthenticated = Auth(enteredUsername, enteredPassword);

            if (isAuthenticated)
            {
                MessageBox.Show("Login successful! Redirecting to home page...");
                // You can redirect the user to another page or perform other actions upon successful login
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
                // You may also increment the login attempts counter or show a captcha entry after multiple failed attempts
            }
        }

        public bool Auth(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return false;
            }

            using (var db = new Entities1())
            {
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                    return false;
                }

                if (user.Role == "Удален") // Check if the user has the role "Удален"
                {
                    MessageBox.Show("Доступ запрещен! Пользователь удален.");
                    return false;
                }

                MessageBox.Show("Работает!");
                txtUsername.Clear();
                txtPassword.Clear();
                loginAttempts = 0;

                return true;
            }
        }
        public int loginAttempts = 0;
    }
}
