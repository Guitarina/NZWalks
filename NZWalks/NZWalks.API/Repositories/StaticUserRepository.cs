using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> users = new List<User>
        {
            //new User()
            //{
            //    FirstName = "Read Only",
            //    LastName = "User",
            //    EmailAdress = "readonly@user.com",
            //    Id = Guid.NewGuid(),
            //    Username ="ReadOnlyUser",
            //    Password = "ReadOnlyUser",
            //    Roles = new List<string>{ "reader"}
            //},
            //new User()
            //{
            //    FirstName = "Read Write",
            //    LastName = "User",
            //    EmailAdress = "readwrite@user.com",
            //    Id = Guid.NewGuid(),
            //    Username ="ReadWriteUser",
            //    Password = "ReadWriteUser",
            //    Roles = new List<string>{ "reader","writer"}
            //}
        };
        public async Task<User> AuthenticateAsync(string username, string password)
        {
           var user =  users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && 
            x.Password == password);
            return user;
        }
    }
}
