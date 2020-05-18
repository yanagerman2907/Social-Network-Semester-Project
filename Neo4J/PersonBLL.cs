using DAL;
using Entity;
using System.Collections.Generic;

namespace Neo4J
{
    public class PersonBLL
    {
        PersonDAL _DAL;
        UserDAL userDAL;

        public PersonBLL()
        {
            _DAL = new PersonDAL();
            userDAL = new UserDAL();
        }

        public bool IsUserFollowing(string user, string friend)
        {
            User u = new User();
            u = userDAL.SelectByLogin(user);
            var following = _DAL.GetFollowingRelations(user);

            if (u != null && following != null)
            {
                foreach (var f in following)
                {
                    if (f.Login == friend)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public int Connections(string user, string friend)
        {
            return _DAL.GetShortestPath(user, friend);
        }

        public List<string> GetWhoCommon(string user, string friend)
        {
            List<string> common = new List<string>();
            try
            {
                var people = _DAL.GetWhoCommon(user, friend);
                foreach (var p in people)
                {
                    common.Add(p.Login);
                }
                return common;
            }
            catch
            {
                return common;
            }
        }

        public bool UpdatePerson(string name, string new_name)
        {
            try
            {
                _DAL.UpdatePerson(name, new_name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddPerson(string login)
        {
            User user = new User();
            user.Login = login;
            _DAL.CreatePerson(user);
        }

        public List<string> GetFollowers(string login)
        {
            List<string> followers = new List<string>();
            try
            {
                var people = _DAL.GetFollowerRelations(login);
                foreach (var p in people)
                {
                    followers.Add(p.Login);
                }
                return followers;
            }
            catch
            {
                return followers;
            }
        }

        public List<string> GetFollowing(string login)
        {
            List<string> following = new List<string>();
            try
            {
                var people = _DAL.GetFollowingRelations(login);
                foreach (var p in people)
                {
                    following.Add(p.Login);
                }
                return following;
            }
            catch
            {
                return following;
            }
        }

        public void Follow(string user, string friend)
        {
            _DAL.CreateRelations(user, friend);
        }

        public void Unfollow(string user, string friend)
        {
            _DAL.DeleteRelations(user, friend);
        }
    }
}