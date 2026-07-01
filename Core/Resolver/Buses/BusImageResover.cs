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
    public class BusImageResover : IValueResolver<Bus, GetAllBusesQueryResult, string>
    {
        private readonly IFileService _fileService;

        public BusImageResover(IFileService fileService)
        {
            _fileService = fileService;
        }

        public string Resolve(Bus source, GetAllBusesQueryResult destination, string destMember, ResolutionContext context)
            => _fileService.GetUrl(source.Image);
    }
}
