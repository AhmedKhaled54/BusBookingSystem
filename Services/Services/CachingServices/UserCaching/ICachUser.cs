using Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CachingServices.UserCaching
{
    public  interface ICachUser
    {
        void LoadUsers(IEnumerable<User> users);
        User GetById(int id);
    }
}
