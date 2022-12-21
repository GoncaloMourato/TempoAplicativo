using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using TempoAplicativo.Data.Entities;
using TempoAplicativo.Helpers;

namespace TempoAplicativo.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();


            //Add User Admin when create db
            var userAdmin = await _userHelper.GetUserByEmailAsync("goncalomourato@gmail.com");
            if (userAdmin == null)
            {
                userAdmin = new User
                {
                    Email = "goncalomourato@gmail.com",
                    UserName = "goncalomourato@gmail.com",
                    Password = "penico123",
                    FirstName = "Goncalo",
                    LastName = "Mourato"
                };
                var result1 = await _userHelper.AddUserAsync(userAdmin, "penico123");
                if (result1 != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user  in seeder");
                }

            }

        }
    }
}
