using AutoMapper;
using Core.Feature.ApplicationDriver.Query.Result;
using Core.Feature.Trips.Query.Results;
using Data.Entity;
using Services.Services.CachingServices.UserCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Resolver.Users
{
    public class DeletedUserNameTripResolver : IValueResolver<Trip, GetDeletedTripQueryResult, string>
    {
        private readonly ICachUser _cachUser;
        public DeletedUserNameTripResolver(ICachUser cachUser)
        {
            _cachUser = cachUser;
        }
        public string Resolve(Trip source, GetDeletedTripQueryResult destination, string destMember, ResolutionContext context)
        {
            var user = _cachUser.GetById(source.DeletedBy ?? 0);
            return user.FullName;
        }
    }
}
