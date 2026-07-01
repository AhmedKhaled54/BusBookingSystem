using Data.Entity;
using Data.Enums;
using Infrastracture.Abstract;
using Infrastracture.Specifications.TicketsSpecification;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using QRCoder;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.TicketService
{
    public class TicketServices : ITicketServices
    {
        private readonly IUnitOfWork _UOW;

        public TicketServices(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }

        public byte[] GeneratePdf(Ticket ticket)
        {
            var document = new TicketDecument(ticket);
            return document.GeneratePdf();
        }

        public async Task<Ticket> GenerateTicket(int BookingId, int bookingnumber)
        {

            var token = $"Ticket-Booking-{bookingnumber}-{Guid.NewGuid()}";
            var qr = GenerateQRCode(token);
            var ticket = new Ticket
            {
                BookingId = BookingId,
                Number = GenerateTicketNumber(),
                Status = TicketStatus.Active,
                QRCode = qr
            };
            await _UOW.Repository<Ticket>().AddAsync(ticket);
            return ticket;

        }

        public async Task<Ticket> GetTicketByBookingId(int BookingId)
        {
            var specs = new TicketWIthBookingAndTripSpecification(BookingId);
            var ticket = await _UOW.Repository<Ticket>().GetEntityByIdSepecification(specs);
            return ticket;
        }

        public async  Task<Ticket> GetTicketById(int TicketId)
        {
            var specs = new TicketByIdSpecification(TicketId);
            var ticket = await _UOW.Repository<Ticket>().GetEntityByIdSepecification(specs);
            return ticket;
        }

        private string GenerateQRCode(string text)
        {
            var generate = new QRCodeGenerator();
            var data = generate.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            var QRCode = new QRCode(data);
            using var map = QRCode.GetGraphic(20);
            using var ms = new MemoryStream();

            map.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            return Convert.ToBase64String(ms.ToArray());

        }
        private string GenerateTicketNumber()
        {
            var guidNumbers = new string(Guid.NewGuid().ToString("N").Where(char.IsDigit).ToArray());
            var eightDigits = guidNumbers[..8].PadRight(8, '0');
            return $"00{eightDigits}"; //00 + 8 number 
        }
    };
}

