using AutoMapper;
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
    public class TripsWithBusImageResolver : IValueResolver<Trip, _GetTripQueryResult, string>
    {
        private readonly IFileService _fileService;

        public TripsWithBusImageResolver(IFileService fileService)
        {
            _fileService = fileService;
        }
        public string Resolve(Trip source, _GetTripQueryResult destination, string destMember, ResolutionContext context)
            => _fileService.GetUrl(source.Bus.Image);
    }
}
