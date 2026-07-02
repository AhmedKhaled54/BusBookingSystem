using Data.Entity;
using Infrastracture.Abstract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.PaymentServices.Payment
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitOfWork _UOW;

        public PaymentServices(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }
        public async Task<Data.Entity.Payment> GetPaymentById(int Id)
            => await _UOW.Repository<Data.Entity.Payment>().FindAsync(c => c.Id == Id);

        public async Task<Data.Entity.Payment> GetPaymentByBookingId(int BookingId)
         => await _UOW.Repository<Data.Entity.Payment>().FindAsync(c => c.BookingId == BookingId);


        public async Task<Data.Entity.Payment> Add(Data.Entity.Payment payment)
        {
            await _UOW.Repository<Data.Entity.Payment>().AddAsync(payment);
            await _UOW.Complete();
            return payment;
        }

        public bool Update(Data.Entity.Payment payment)
        {
            _UOW.Repository<Data.Entity.Payment>().Update(payment);
            return true;
        }
    }
}
