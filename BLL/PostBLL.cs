using DAL;
using Entity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class PostBLL
    {
        PostDAL postDAL;
        UserDAL userDAL;
        UserBLL userBLL;

        public PostBLL()
        {
            postDAL = new PostDAL();
            userDAL = new UserDAL();
            userBLL = new UserBLL();
        }

        public void AddPost(string text)
        {
            Post post = new Post();
            post.Text = text;
            string n = userBLL.LoginRead();
            ObjectId id = userDAL.GetUserId(n);
            post.OwnerId = id;
            post.DateTime = DateTime.Now.ToString();
            postDAL.InsertPost(post);
        }

        public void EditPost(string newText, ObjectId postId)
        {
            try
            {
                postDAL.UpdatePost(postId, newText);
            }
            catch { }
        }

        public bool DeletePost(ObjectId postId)
        {
            try
            {
                postDAL.DeleteById(postId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DidUserLikePost(string userLogin, ObjectId postId)
        {
            Post post = new Post();
            post = postDAL.SelectById(postId);
            if (post != null)
            {
                if (post.WhoLiked != null && post.WhoLiked.Count > 0)
                {
                    foreach (var login in post.WhoLiked)
                    {
                        if (login == userLogin)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void AddLike(string userLogin, ObjectId postId)
        {
            try
            {
                postDAL.AddLike(userLogin, postId);
            }
            catch { }
        }

        public void DismissLike(string userLogin, ObjectId postId)
        {
            try
            {
                postDAL.DismissLike(userLogin, postId);
            }
            catch { }
        }

        public int GetNumOfLikes(ObjectId postId)
        {
            try
            {
                return postDAL.GetNumOfLikes(postId);
            }
            catch
            {
                return 0;
            }
        }

        public bool AddComment(string text, ObjectId postId)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.OwnerId = userDAL.GetUserId(userBLL.LoginRead());
            comment.DateTime = DateTime.Now.ToString();
            try
            {
                postDAL.AddComment(comment, postId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteComment(string text, ObjectId postId)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.OwnerId = userDAL.GetUserId(userBLL.LoginRead());
            comment.DateTime = DateTime.Now.ToString();
            try
            {
                postDAL.DeleteComment(comment, postId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Post> GetNewPosts(string timeOfLastUserLogin, List<string> following)
        {
            List<ObjectId> postIds = new List<ObjectId>();
            if (following != null)
            {
                foreach (var user in following)
                {
                    postIds.Add(userDAL.GetUserId(user));
                }
                return postDAL.SelectNewPosts(timeOfLastUserLogin, postIds);
            }
            return new List<Post>();
        }

        public List<Post> GetAllPosts(string login)
        {
            List<Post> posts = new List<Post>();
            try
            {
                posts = postDAL.SelectAllPosts(userDAL.GetUserId(login));
                return posts;
            }
            catch
            {
                return posts;
            }
        }

        public List<Post> GetAllPosts(List<string> following)
        {
            List<ObjectId> postIds = new List<ObjectId>();
            if (following != null)
            {
                foreach (var user in following)
                {
                    postIds.Add(userDAL.GetUserId(user));
                }
                return postDAL.SelectAllPosts(postIds).OrderByDescending(p => p.DateTime).ToList();
            }
            return new List<Post>();
        }

        public Post GetPost(ObjectId postId)
        {
            Post post = new Post();
            try
            {
                post = postDAL.SelectById(postId);
                return post;
            }
            catch
            {
                return post;
            }
        }

        public List<string> GetWhoCommented(ObjectId postId)
        {
            List<Comment> comments = new List<Comment>();
            try
            {
                comments = postDAL.GetComments(postId);
                if (comments != null)
                {
                    List<string> output = new List<string>();
                    foreach (var comment in comments)
                    {
                        output.Add(userDAL.SelectById(comment.OwnerId).Login + ":\n" + comment.Text + "\n" + comment.DateTime + "\n");
                    }
                    return output;
                }
            }
            catch
            {
                return new List<string>();
            }
            return new List<string>();
        }

        public List<string> GetWhoLiked(ObjectId postId)
        {
            List<string> likers = new List<string>();
            try
            {
                likers = postDAL.GetWhoLiked(postId);
                return likers;
            }
            catch
            {
                return likers;
            }
        }
    }
}