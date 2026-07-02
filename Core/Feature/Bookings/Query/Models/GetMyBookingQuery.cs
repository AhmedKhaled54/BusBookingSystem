using Core.Base;
using Core.Feature.Booking.Query.Results;
using Core.Wrappers;
using Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Booking.Query.Models
{
    public class GetMyBookingQuery : IRequest<Response<Pagination<GetMyBookingQueryResult>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int UserId { get; set; }
        public  BookingStatus Status { get; set; }
    }
}
