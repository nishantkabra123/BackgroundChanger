using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task2.Models;

namespace Task2.Repository.IRepository
{
    public interface IUserRepository 
    {
        public UserModel Authenticate(string email, string password);
        public string Fetch();
    }
}
