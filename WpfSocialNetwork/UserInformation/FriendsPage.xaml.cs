using BLL;
using Entity;
using Neo4J;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfSocialNetwork.Windows;

namespace WpfSocialNetwork.UserInformation
{
    public partial class FriendsPage : UserControl
    {
        string _friendLogin;
        UserBLL _userBLL;
        PersonBLL _personBLL;
        PostBLL _postBLL;
        User _user;
        Post _current_post;
        int _conn;
        List<Post> _posts;
        int _index_of_post = 0;
        bool _is_any_posts = false;
        bool _tempLike = false;

        public FriendsPage(string friendLogin)
        {
            InitializeComponent();

            _friendLogin = friendLogin;
            _userBLL = new UserBLL();
            _personBLL = new PersonBLL();
            _postBLL = new PostBLL();

            _user = _userBLL.GetUser(_friendLogin);
            info.Content = _user.FirstName + "  " + _user.LastName + "\nLogin:   " + _user.Login + "\nActive:\n      " + _user.LastLogin;
            if (_personBLL.IsUserFollowing(_userBLL.LoginRead(), _friendLogin))
            {
                btnFollow.BorderBrush = Brushes.MediumPurple;
            }
            else
                btnFollow.BorderBrush = Brushes.Transparent;

            _current_post = new Post();
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;

            Refresh();
        }
        private void Follow_Click(object sender, RoutedEventArgs e)
        {
            if (!_personBLL.IsUserFollowing(_userBLL.LoginRead(), _friendLogin))
            {
                _personBLL.Follow(_userBLL.LoginRead(), _friendLogin);
                btnFollow.BorderBrush = Brushes.MediumPurple;
            }
            else
            {
                _personBLL.Unfollow(_userBLL.LoginRead(), _friendLogin);
                btnFollow.BorderBrush = Brushes.Transparent;
            }
            Refresh();
        }

        private void Like(object sender, RoutedEventArgs e)
        {
            if (_posts.Count > 0)
            {
                if (_tempLike == false)
                {
                    btnLike.BorderBrush = Brushes.WhiteSmoke;
                    _tempLike = true;
                    _postBLL.AddLike(_userBLL.LoginRead(), _current_post.Id);
                }
                else
                {
                    btnLike.BorderBrush = Brushes.Transparent;
                    _tempLike = false;
                    _postBLL.DismissLike(_userBLL.LoginRead(), _current_post.Id);
                }
                btnLikers.Content = "Liked by: " + _postBLL.GetNumOfLikes(_current_post.Id).ToString();
                btnComments.Content = "View comments " + _postBLL.GetWhoCommented(_current_post.Id).Count;
            }
            else
            {
                btnLike.BorderBrush = Brushes.Transparent;
                _tempLike = false;
            }
        }

        private void Prev(object sender, RoutedEventArgs e)
        {
            _posts = _postBLL.GetAllPosts(_friendLogin);
            if (_is_any_posts)
            {
                if (_index_of_post > 0)
                {
                    _index_of_post--;
                    Refresh();

                    if (_index_of_post == 0)
                    {
                        btnPrev.IsEnabled = false;
                        btnNext.IsEnabled = true;
                    }
                    else
                    {
                        btnPrev.IsEnabled = true;
                    }
                }
            }
        }

        private void Comment(object sender, RoutedEventArgs e)
        {
            AddComment main = new AddComment(_current_post.Id)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.ShowDialog();
            this.btnComments.Content = "View comments: " + _postBLL.GetWhoCommented(_current_post.Id).Count;
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            _posts = _postBLL.GetAllPosts(_friendLogin);
            if (_is_any_posts)
            {
                if (_index_of_post + 1 < _posts.Count)
                {
                    _index_of_post++;
                    Refresh();

                    if (_index_of_post + 1 == _posts.Count)
                    {
                        btnNext.IsEnabled = false;
                        btnPrev.IsEnabled = true;
                    }
                    else
                    {
                        btnNext.IsEnabled = true;
                        btnPrev.IsEnabled = true;
                    }
                }
            }
        }

        private void WhoLiked(object sender, RoutedEventArgs e)
        {
            People main = new People(_postBLL.GetWhoLiked(_current_post.Id))
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.Show();
        }

        private void WhoCommented(object sender, RoutedEventArgs e)
        {
            Comments main = new Comments(_postBLL.GetWhoCommented(_current_post.Id))
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.ShowDialog();
        }

        private void WhoCommon(object sender, RoutedEventArgs e)
        {
            People main = new People(_personBLL.GetWhoCommon(_userBLL.LoginRead(), _friendLogin))
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.ShowDialog();
        }

        private void Following(object sender, RoutedEventArgs e)
        {
            People main = new People(_personBLL.GetFollowing(_friendLogin))
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.ShowDialog();
        }

        private void Followers(object sender, RoutedEventArgs e)
        {
            People main = new People(_personBLL.GetFollowers(_friendLogin))
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.ShowDialog();
        }

        public void Refresh()
        {
            _posts = _postBLL.GetAllPosts(_friendLogin);
            if (_posts != null && _posts.Count > 0)
            {
                btnLike.IsEnabled = true;
                btnComment.IsEnabled = true;
                btnComments.IsEnabled = true;
                btnLikers.IsEnabled = true;
                btnNext.IsEnabled = true;

                _current_post = _posts[_index_of_post];
                Header.Content = "Posts in " + _user.FirstName + "'s Timeline:   " + _posts.Count;
                Timeline.Content = _current_post.Text;
                date.Content = "Posted on:  " + _current_post.DateTime;
                _is_any_posts = true;

                if (_posts.Count == 1 || _index_of_post == _posts.Count - 1) btnNext.IsEnabled = false;

                if (_postBLL.DidUserLikePost(_userBLL.LoginRead(), _current_post.Id))
                {
                    btnLike.BorderBrush = Brushes.WhiteSmoke;
                    _tempLike = true;
                }
                else
                {
                    btnLike.BorderBrush = Brushes.Transparent;
                    _tempLike = false;
                }
                btnLikers.Content = "Liked by: " + _postBLL.GetNumOfLikes(_current_post.Id).ToString();
                btnComments.Content = "View comments " + _postBLL.GetWhoCommented(_current_post.Id).Count;
            }
            else
            {
                Header.Content = "No posts in " + _user.FirstName + "'s Timeline";
                Timeline.Content = "";
                btnLike.IsEnabled = false;
                btnComment.IsEnabled = false;
                btnComments.IsEnabled = false;
                btnLikers.IsEnabled = false;
            }

            _conn = _personBLL.Connections(_userBLL.LoginRead(), _friendLogin);
            if (_conn == 0)
            {
                btnConnections.IsEnabled = false;
                lblConn.Content = "No connection";
            }
            else if (_conn == 1)
            {
                btnConnections.IsEnabled = false;
                lblConn.Content = "1st connection";
            }
            else if (_conn == 2)
            {
                btnConnections.IsEnabled = true;
                lblConn.Content = "2nd connection";
            }
            else if (_conn == 3)
            {
                btnConnections.IsEnabled = false;
                lblConn.Content = "3rd connection";
            }
            else
            {
                btnConnections.IsEnabled = false;
                lblConn.Content = _conn + "th connection";
            }
        }
    }
}
