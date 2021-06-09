using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            UserManager userManager = new UserManager(new EfUserDal());

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
