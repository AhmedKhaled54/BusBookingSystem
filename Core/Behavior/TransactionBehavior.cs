using Infrastracture.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Behavior
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUnitOfWork _UOW;
        public TransactionBehavior(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var Trans = await _UOW.TransactionAsync();
            try
            {
                var Response = await next();
                await _UOW.Complete();
                await Trans.CommitAsync();
                return Response;

            }
            catch 
            {
                await Trans.RollbackAsync();
                throw;
            }
        }
    }
}
