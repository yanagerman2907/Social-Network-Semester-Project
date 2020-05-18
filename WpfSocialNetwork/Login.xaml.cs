using BLL;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfSocialNetwork.Windows;

namespace WpfSocialNetwork
{
    public partial class Login : Window
    {
        UserBLL _userBLL;

        public Login()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void LOGIN(object sender, RoutedEventArgs e)
        {
            if (_userBLL.CheckPassword(txtLogin.Text, Password.Password.ToString()))
            {
                _userBLL.LoginWrite(txtLogin.Text);
                HomePage main = new HomePage()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid password!");
            }
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Registration main = new Registration()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.Show();
            this.Close();
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
