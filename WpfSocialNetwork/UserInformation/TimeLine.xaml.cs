using BLL;
using DAL;
using Entity;
using Neo4J;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfSocialNetwork.Windows;

namespace WpfSocialNetwork.UserInformation
{
    public partial class TimeLine : UserControl
    {
        PostDAL _postDAL;
        PostBLL _postBLL;
        UserBLL _userBLL;
        PersonBLL _personBLL;
        User _user;
        List<string> _followings;
        List<Post> _posts;
        List<Post> _new_posts;
        bool _is_any_posts = false;
        bool _tempLike = false;
        Post _current_post;
        int _index_of_post = 0;

        public TimeLine()
        {
            InitializeComponent();

            _postDAL = new PostDAL();
            _postBLL = new PostBLL();
            _userBLL = new UserBLL();
            _personBLL = new PersonBLL();

            _user = _userBLL.GetUser(_userBLL.LoginRead());

            _current_post = new Post();
            _posts = new List<Post>();
            _new_posts = new List<Post>();

            Refresh();
        }

        private void Like(object sender, RoutedEventArgs e)
        {
            if (_posts.Count > 0)
            {
                if (_tempLike == false)
                {
                    btnLike.BorderBrush = Brushes.MediumPurple;
                    _tempLike = true;
                    _postDAL.AddLike(_userBLL.LoginRead(), _current_post.Id);
                }
                else
                {
                    btnLike.BorderBrush = Brushes.Transparent;
                    _tempLike = false;
                    _postDAL.DismissLike(_userBLL.LoginRead(), _current_post.Id);
                }
                btnLikers.Content = "View likers: " + _postBLL.GetNumOfLikes(_current_post.Id).ToString();
            }
            else
            {
                btnLike.BorderBrush = Brushes.Transparent;
                _tempLike = false;
            }
        }

        private void Prev(object sender, RoutedEventArgs e)
        {
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
            btnComments.Content = "View comments: " + _postBLL.GetWhoCommented(_current_post.Id).Count;
        }

        private void Next(object sender, RoutedEventArgs e)
        {
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

        public void Refresh()
        {
            _followings = _personBLL.GetFollowing(_user.Login);
            _posts = _postBLL.GetAllPosts(_followings);
            _new_posts = _postBLL.GetNewPosts(_user.LastLogin, _followings);
            if (_posts != null && _posts.Count > 0)
            {
                btnLike.IsEnabled = true;
                btnComment.IsEnabled = true;
                btnComments.IsEnabled = true;
                btnLikers.IsEnabled = true;
                btnNext.IsEnabled = true;
                btnPrev.IsEnabled = false;

                _is_any_posts = true;
                _current_post = _posts[_index_of_post];
                Header.Content = _userBLL.GetUser(_current_post.OwnerId).Login;
                Timeline.Content = _current_post.Text;
                date.Content = "Time:  " + _current_post.DateTime;
                if (_index_of_post < _new_posts.Count)
                {
                    newpost.Content = "!";
                    newpost.Background = Brushes.MediumPurple;
                }
                else
                {
                    newpost.Content = "";
                    newpost.Background = Brushes.Transparent;
                }

                if (_posts.Count == 1 || _index_of_post == _posts.Count - 1)
                    btnNext.IsEnabled = false;

                if (_postBLL.DidUserLikePost(_userBLL.LoginRead(), _current_post.Id))
                {
                    btnLike.BorderBrush = Brushes.MediumPurple;
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
                Header.Content = "No posts in your friends' Timeline";
                Timeline.Content = "";
                btnLike.IsEnabled = false;
                btnComment.IsEnabled = false;
                btnComments.IsEnabled = false;
                btnLikers.IsEnabled = false;
                btnNext.IsEnabled = false;
                btnPrev.IsEnabled = false;
                newpost.Content = "";
                newpost.Background = Brushes.Transparent;
            }
        }
    }
}
