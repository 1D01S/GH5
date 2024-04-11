using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// Логика взаимодействия для CAPCA.xaml
    /// </summary>
    public partial class CAPCA : Page
    {
        public new bool IsVisible { get; set; }

        public string GetGeneratedCaptcha()
        {
            return "generated_captcha";
        }

        public void EnterCaptchaSolution(string solution)
        {
            if (txtEnteredCaptcha != null)
            {
                txtEnteredCaptcha.Text = solution; 
            }
            else
            {
                MessageBox.Show("Ошибка: текстовое поле для капчи не найдено.");
            }
        }
        private string generatedCaptcha;

        public CAPCA()
        {
            InitializeComponent();
            GenerateCaptchaText();
        }

        private void GenerateCaptchaText()
        {
            generatedCaptcha = GenerateRandomCaptcha(); // Генерация случайной капчи
            txtGeneratedCaptcha.Text = generatedCaptcha;
        }

        private string GenerateRandomCaptcha()
        {
            string allowChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] chars = allowChars.ToCharArray();
            Random rnd = new Random();
            string captcha = "";

            for (int i = 0; i < 6; i++)
            {
                captcha += chars[rnd.Next(chars.Length)];
            }

            return captcha;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string enteredCaptcha = txtEnteredCaptcha.Text;

            if (enteredCaptcha.Equals(txtGeneratedCaptcha.Text, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Капча верна. Доступ разрешен.");
                NavigationService.Navigate(new AuthPage());
            }
            else
            {
                MessageBox.Show("Неверная капча. Попробуйте еще раз.");
                GenerateCaptchaText(); // Генерация новой капчи
            }
        }

    }

}
   
