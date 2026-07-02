using Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CachingServices.UserCaching
{
    public class CachUser : ICachUser
    {
        private Dictionary<int, User> _users = new Dictionary<int, User>();
        public User GetById(int id)
            => _users.TryGetValue(id, out var user) ? user : null;

        public void LoadUsers(IEnumerable<User> users)
            => _users = users.ToDictionary(c => c.Id);
    }
}
