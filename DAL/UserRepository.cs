using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DataModels;

namespace DAL
{
    interface IUserRepository
    {
        bool Add(UserVM user);
    }
    public class UserRepository: IUserRepository
    {
        LibrarianEntities db = new LibrarianEntities();
        public bool Add(UserVM user)
        {
            if (user != null)
            {
                User obj = new User();
                obj.FName = user.FName;
                obj.LName = user.LName;
                obj.E_mail = user.E_mail;
                obj.Password = user.Password;
                obj.Country = user.Country;
                obj.Gender = user.Gender;
                db.User.Add(obj);
                db.SaveChanges();
                return true;
            }
            else
                return false;

        }
    }
}
