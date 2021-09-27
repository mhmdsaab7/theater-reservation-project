using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TheaterPrjt.Entities;

namespace TheaterPrjt.Data
{
    public class UserDataManager : BaseSQLDataManager
    {
        public User GetUserByUsername(string username)
        {
            return GetSPItem("sp_User_GetByUserName", UserMapper, username);
        }
        public User GetUser(long userId)
        {
            return GetSPItem("sp_User_Get", UserMapper, userId);
        }
        private User UserMapper(IDataReader dataReader)
        {
            return new User
            {
                UserId = (long)dataReader["ID"],
                Username = (string)dataReader["Username"],
                Password = (string)dataReader["Password"],
                IsAdmin = (bool)dataReader["IsAdmin"]
            };
        }
    }
}
