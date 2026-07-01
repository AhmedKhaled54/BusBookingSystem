using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.TicketService
{
    public  interface ITicketServices
    {
        Task<Ticket> GenerateTicket(int BookingId,int bookingnumber);
        Task<Ticket> GetTicketByBookingId(int BookingId);
        Task<Ticket> GetTicketById (int TicketId);
        byte[] GeneratePdf(Ticket ticket);

    }
}
