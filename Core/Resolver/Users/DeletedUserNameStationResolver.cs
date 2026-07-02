using AutoMapper;
using Core.Feature.Stations.Query.Result;
using Data.Entity;
using Services.Services.CachingServices.UserCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Resolver.Users
{
    public  class DeletedUserNameStationResolver : IValueResolver<Stations, GetDeleteStationQueryResult, string>
    {
        private readonly ICachUser _cachUser;

        public DeletedUserNameStationResolver(ICachUser cachUser)
        {
            _cachUser = cachUser;
        }

        public string Resolve(Stations source, GetDeleteStationQueryResult destination, string destMember, ResolutionContext context)
        {
            var user = _cachUser.GetById(source.DeletedBy ?? 0);
            return user.FullName;
            
        }
    }
}
