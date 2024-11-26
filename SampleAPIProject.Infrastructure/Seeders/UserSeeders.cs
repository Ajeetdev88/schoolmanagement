using MLMProject.Domain.Entities;
using MLMProject.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Infrastructure.Seeders
{
    internal class UserSeeders(MLMProjectDbContext dbContext) : IUserSeeders
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.UserAuths.Any())
                {
                    var User = GetUser();
                    dbContext.UserAuths.AddRange(User);
                    dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<UserAuth> GetUser()
        {
            List<UserAuth> users = [new()
            {
                UserName = "Arshad",
                PhoneNumber = "7890236595",
                Name = "Diljeet Singh",
                //DOB = "12/10/2000",
                EmailAddress = "arshad@antheminfotech.com",
                Password = "Anthem#11",
                UserCode = "AU1003",
                UserTypeId = 2,
                TPassword = "Anthem@123"
            },
            ];
            return users;
        }
    }
}
