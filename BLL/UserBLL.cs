using Entity;
using DAL;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using MongoDB.Bson;

namespace BLL
{
    public class UserBLL
    {
        UserDAL userDAL;

        public UserBLL()
        {
            userDAL = new UserDAL();
        }

        public bool CheckPassword(string login, string password)
        {
            User user = new User();
            user = userDAL.SelectByLogin(login);
            if (user != null)
            {
                if (user.HashedPassword == GetHash(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool UpdatePassword(string oldPwd, string newPwd)
        {
            if (CheckPassword(LoginRead(), (oldPwd)))
            {
                userDAL.UpdateByField(LoginRead(), "HashedPassword", GetHash(newPwd));
                return true;
            }
            else
            {
                return false;
            }

        }

        public string GetHash(string str)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
            string result = "";
            foreach (byte b in hashPassword)
            {
                result += b.ToString();
            }
            return result;
        }

        public bool CheckUserIdentity(string login)
        {
            List<User> users = new List<User>();
            users = userDAL.SelectAllUsers();
            foreach (var user in users)
            {
                if (user.Login == login)
                {
                    return false;
                }
            }
            return true;
        }

        public void LoginWrite(string login)
        {
            var p = new LogInfo();

            p.Login = login;
            if (p != null)
            {
                using (FileStream fs = new FileStream("LogInfo.json", FileMode.Create))
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(LogInfo));
                    jsonFormatter.WriteObject(fs, p);
                }
            }
            else
            {
                using (FileStream fs = new FileStream("LogInfo.json", FileMode.Create))
                {
                    p = new LogInfo { Login = "" };
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(LogInfo));
                    jsonFormatter.WriteObject(fs, p);
                }
            }
        }

        public string LoginRead()
        {
            var p = new LogInfo();
            using (FileStream fs = new FileStream("LogInfo.json", FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(LogInfo));
                if (fs.Length != 0)
                {
                    p = (LogInfo)jsonFormatter.ReadObject(fs);
                }
            }
            return p.Login;
        }

        public bool IsUserExist(string login)
        {
            User user = new User();
            user = userDAL.SelectByLogin(login);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool UpdateByField(string login, string fieldName, string value)
        {
            try
            {
                userDAL.UpdateByField(login, fieldName, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void UpdateByDateTime()
        {
            try
            {
                userDAL.UpdateByDateTime(LoginRead());
            }
            catch { }
        }

        public void AddUser(string firstName, string lastName, string login, string pwd)
        {
            User user = new User();
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Login = login;
            user.HashedPassword = GetHash(pwd);
            userDAL.InsertUser(user);
        }

        public User GetUser()
        {
            try
            {
                return userDAL.SelectByLogin(LoginRead());
            }
            catch
            {
                return new User();
            }
        }

        public ObjectId GetUserId()
        {
            try
            {
                return userDAL.GetUserId(LoginRead());
            }
            catch
            {
                return new ObjectId();
            }
        }

        public User GetUser(string login)
        {
            try
            {
                return userDAL.SelectByLogin(login);
            }
            catch
            {
                return new User();
            }
        }

        public User GetUser(ObjectId id)
        {
            try
            {
                return userDAL.SelectById(id);
            }
            catch
            {
                return new User();
            }
        }
    }
}