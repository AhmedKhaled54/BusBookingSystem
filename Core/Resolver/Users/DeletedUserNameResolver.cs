using AutoMapper;
using Core.Feature.ApplicationDriver.Query.Result;
using Data.Entity;
using Services.Services.CachingServices.UserCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Resolver.Users
{
    public class DeletedUserNameResolver : IValueResolver<Bus, GetDeletedBusesQueryResult, string>
    {
        private readonly ICachUser _cachUser;

        public DeletedUserNameResolver(ICachUser cachUser)
        {
            _cachUser = cachUser;
        }

        public string Resolve(Bus source, GetDeletedBusesQueryResult destination, string destMember, ResolutionContext context)
        {
            var user = _cachUser.GetById(source.DeletedBy ?? 0);
            return user.FullName;
        }
    }
}
