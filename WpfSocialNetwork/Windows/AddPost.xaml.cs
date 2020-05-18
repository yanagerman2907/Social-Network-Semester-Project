using BLL;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfSocialNetwork.Windows
{
    public partial class AddPost : Window
    {
        PostBLL _postBLL;

        public AddPost()
        {
            InitializeComponent();
            _postBLL = new PostBLL();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _postBLL.AddPost(Text.Text);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
