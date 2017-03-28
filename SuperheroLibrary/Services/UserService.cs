using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperheroLibrary.Models;
using SuperheroLibrary.Models.ViewModels;

namespace SuperheroLibrary.Services
{
    public class UserService
    {
        public User GetUserById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            User user = null;
            using (var db = new AppContext())
            {
                user = db.Users.Find(id);
            }
            return user;
        }
        public User GetUserByName(string userName)
        {
            User user = null;
            using (var db = new AppContext())
            {
                user = db.Users.FirstOrDefault(u => u.Login == userName);
            }
            return user;
        }

        public int? GetUserIdByName(string userName)
        {
            int? userId = null;
            using (var db = new AppContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Login == userName);
                if (user != null)
                {
                    userId = user.Id;
                }
            }
            return userId;
        }

        public User FindUser(AccountBaseModel model)
        {
            User user = null;
            using (var db = new AppContext())
            {
                user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
            }
            return user;
        }

        public bool RegisterUser(AccountBaseModel model)
        {
            bool result = false;
            using (var db = new AppContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                if (user == null)
                {
                    result = true;
                    db.Users.Add(new User { Login = model.Login, Password = model.Password });
                    db.SaveChanges();
                }
            }
            return result;
        }
    }
}