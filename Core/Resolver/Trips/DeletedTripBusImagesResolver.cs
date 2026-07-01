using AutoMapper;
using Core.Feature.ApplicationDriver.Query.Result;
using Core.Feature.Trips.Query.Results;
using Data.Entity;
using Services.Services.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Resolver.Trips
{
    public class DeletedTripBusImagesResolver : IValueResolver<Trip, GetDeletedTripQueryResult, string>
    {
        private readonly IFileService _fileService;
        public DeletedTripBusImagesResolver(IFileService fileService)
        {
            _fileService = fileService;
        }
        public string Resolve(Trip source, GetDeletedTripQueryResult destination, string destMember, ResolutionContext context)
            => _fileService.GetUrl(source.Bus.Image);
    }
}
