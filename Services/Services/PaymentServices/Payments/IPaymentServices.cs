using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PaymentServices.Payment
{
    public  interface IPaymentServices
    {
        Task<Data.Entity.Payment> GetPaymentById(int Id);
        Task<Data.Entity.Payment> GetPaymentByBookingId(int BookingId);
        Task<Data.Entity.Payment> Add(Data.Entity.Payment payment);
        bool Update (Data.Entity.Payment payment);


    }
}
