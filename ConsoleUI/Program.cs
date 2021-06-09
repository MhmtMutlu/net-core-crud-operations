using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            UserManager userManager = new UserManager(new EfUserDal());

            User user1 = new User();
            user1.Id = 3;
            user1.Name = "Hazal";
            user1.Surname = "Mutlu";
            user1.Email = "hazalmutlu@outlook.com";
            user1.BirthDate = new DateTime(day: 17, month: 03, year: 1997);
            user1.Phone = "05554443322";
            user1.Photo = "hazal.jpg";
            user1.Location = "Kayseri";

            userManager.Add(user1);

            var result = userManager.GetAll();

            if (result.Success == true)
            {
                foreach (var user in userManager.GetAll().Data)
                {
                    Console.WriteLine(user.Id + " - " + user.Name + "  " + user.Surname + " - " + user.Email);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}
