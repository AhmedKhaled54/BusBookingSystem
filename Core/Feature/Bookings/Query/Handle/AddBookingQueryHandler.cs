using AutoMapper;
using Core.Base;
using Core.Feature.Booking.Query.Models;
using Core.Feature.Booking.Query.Results;
using Core.Wrappers;
using Data.Entity;
using MediatR;
using Services.Services.BookingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Feature.Booking.Query.Handle
{
    public class AddBookingQueryHandler : ResponseHandler, 
        IRequestHandler<GetMyBookingQuery, Response<Pagination<GetMyBookingQueryResult>>>
    {
        #region Feild
        private readonly IBookingServices _Services;
        private readonly IMapper _mapper;
        #endregion
        #region Ctor 
        public AddBookingQueryHandler(IBookingServices services, IMapper mapper)
        {
            _Services = services;
            _mapper = mapper;
        }
        #endregion
        public async  Task<Response<Pagination<GetMyBookingQueryResult>>> Handle(GetMyBookingQuery request, CancellationToken cancellationToken)
        {
            
            var data = _Services.GetMyBookings(request.UserId, request.Status);
            if (data == null)
                return NotFound<Pagination<GetMyBookingQueryResult>>("No bookings found for the user.");
         
            var result =await data.ToPaginationListAsync<Data.Entity.Booking,GetMyBookingQueryResult>(request.PageSize,request.PageNumber,_mapper);
            return Success(result);
        }
    }
}
