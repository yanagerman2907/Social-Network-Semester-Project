using System.Collections.Generic;
using System.Windows;

namespace WpfSocialNetwork.Windows
{
    public partial class People : Window
    {
        public People(List<string> people)
        {
            InitializeComponent();
            if (people != null && people.Count > 0)
            {
                foreach (var p in people)
                {
                    lbxPeople.Items.Add(p);
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
