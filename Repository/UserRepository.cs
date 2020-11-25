using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task2.Models;
using Task2.Repository.IRepository;

namespace Task2.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ImageDbContext _db;

        public UserRepository(ImageDbContext db)
        {
            _db = db;
        }

        public string Fetch()
        {
            var user = _db.Users.SingleOrDefault(x => x.UserId == 1);

            //user not found
            if (user == null)
            {
                return null;
            }
            if (user.Choice == "Random")
            {
               List<ImageModel> imageList= _db.Images.FromSqlRaw("Select * from Images").ToList();
                System.Diagnostics.Debug.WriteLine(imageList);
                var rand = new Random();
                var num = rand.Next(0, imageList.Count);
                System.Diagnostics.Debug.WriteLine(imageList.ElementAt(num).Title);
                return imageList.ElementAt(num).Title;
            }
            return user.Choice;
        }

        public UserModel Authenticate(string email, string password)
        {
            var user = _db.Users.SingleOrDefault(x => x.Email == email && x.Password == password);

            //user not found
            if (user == null)
            {
                return null;
            }
            System.Diagnostics.Debug.WriteLine(user);
            return user;
        }


    }
}
