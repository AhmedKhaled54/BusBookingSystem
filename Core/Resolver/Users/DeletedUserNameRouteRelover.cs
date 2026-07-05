using AutoMapper;
using Core.Feature.Route.Query.Results;
using Data.Entity;
using MimeKit.Tnef;
using Services.Services.CachingServices.UserCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Resolver.Users
{
    public class DeletedUserNameRouteRelover : IValueResolver<Routes, GetDeletedRoutesQueryResult, string>
    {
        private readonly ICachUser _CachUser;
        public DeletedUserNameRouteRelover(ICachUser cachUser)
        {
            _CachUser = cachUser;
        }
        public string Resolve(Routes source, GetDeletedRoutesQueryResult destination, string destMember, ResolutionContext context)
        {
           var user =_CachUser.GetById(source.DeletedBy??0);
            return user.FullName;
        }
    }
}
