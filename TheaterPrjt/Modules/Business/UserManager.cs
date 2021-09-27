using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TheaterPrjt.Data;
using TheaterPrjt.Entities;

namespace TheaterPrjt.Business
{
    public class UserManager
    {
        UserDataManager _dataManager = new UserDataManager();
        public static long currentUserId;
        public static bool isAdmin;
        public static long GetLoggedInUserId()
        {
            return currentUserId;
        }
        public User GetLoggedInUser(string userName)
        {
            return _dataManager.GetUserByUsername(userName);
        }
        public User GetUser(long userId)
        {
            return _dataManager.GetUser(userId);
        }

        public User Authenticate(string userName, string password)
        {
            var loggedInUser = GetLoggedInUser(userName);
            if (loggedInUser == null)
                return null;
            var hashedPassword = Encryptor.MD5Hash(password);
            if (hashedPassword.ToLower() != loggedInUser.Password.ToLower())
                return null;
            currentUserId = loggedInUser.UserId;
            isAdmin = loggedInUser.IsAdmin;
            return loggedInUser;
        }

        public static bool IsLoggedInUserAdmin()
        {
            return isAdmin;
        }
    }

    public static class Encryptor
    {
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
