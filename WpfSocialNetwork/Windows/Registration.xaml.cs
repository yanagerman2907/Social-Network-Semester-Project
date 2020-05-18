using BLL;
using Neo4J;
using System.Windows;
using System.Windows.Input;

namespace WpfSocialNetwork.Windows
{
    public partial class Registration : Window
    {
        UserBLL _userBLL;
        PersonBLL _personBLL;

        public Registration()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            _personBLL = new PersonBLL();
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            if (firstName.Text != "" && lastName.Text != "" && login.Text != "")
            {
                if (_userBLL.CheckUserIdentity(login.Text))
                {
                    if (pwd.Password.ToString() == pwd2.Password.ToString() && pwd.Password.ToString() != "")
                    {
                        try
                        {
                            _userBLL.AddUser(firstName.Text, lastName.Text, login.Text, pwd.Password.ToString());
                            _personBLL.AddPerson(login.Text);
                            _userBLL.LoginWrite(login.Text);
                            HomePage main = new HomePage()
                            {
                                WindowStartupLocation = WindowStartupLocation.CenterScreen
                            };
                            main.Show();
                            this.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Error!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid password!");
                    }
                }
                else
                {
                    MessageBox.Show("This login already used!");
                }

            }
            else
            {
                MessageBox.Show("Empty fields!");
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            Login main = new Login();
            main.Show();
            this.Close();
        }

        private void login_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
