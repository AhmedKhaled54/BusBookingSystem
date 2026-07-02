using AutoMapper;
using Core.Feature.ApplicationDriver.Query.Result;
using Data.Entity;
using Services.Services.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Resolver.Buses
{
    public class DeletedBusImageResolver : IValueResolver<Bus, GetDeletedBusesQueryResult, string>
    {
        private readonly IFileService _fileService;
        public DeletedBusImageResolver(IFileService fileService)
        {
            _fileService = fileService;
        }
        public string Resolve(Bus source, GetDeletedBusesQueryResult destination, string destMember, ResolutionContext context)
        => _fileService.GetUrl(source.Image);


    }
}
