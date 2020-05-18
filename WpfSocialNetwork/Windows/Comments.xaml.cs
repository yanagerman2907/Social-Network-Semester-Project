using System.Collections.Generic;
using System.Windows;

namespace WpfSocialNetwork.Windows
{
    public partial class Comments : Window
    {
        public Comments(List<string> comments)
        {
            InitializeComponent();
            if (comments != null && comments.Count > 0)
            {
                foreach (var c in comments)
                {
                    lbxComments.Items.Add(c);
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
