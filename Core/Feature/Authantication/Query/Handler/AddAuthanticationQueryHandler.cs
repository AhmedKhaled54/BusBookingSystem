using Core.Base;
using Core.Feature.Authantication.Query.Models;
using MediatR;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto.Utilities;
using Services.Services.Authantication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.Query.Handler
{
    public class AddAuthanticationQueryHandler : ResponseHandler,
        IRequestHandler<ConfirmEmailQuery, Response<string>>
    {
        #region Feild 
        private readonly IAuthanticationServices _authanticationServices;

        #endregion
        #region Ctor 
        public AddAuthanticationQueryHandler(IAuthanticationServices authanticationServices)
        {
            _authanticationServices = authanticationServices;
        }
        #endregion
        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _authanticationServices.ConfirmEmail(request.UserId,request.Code);
            if (!result.Success)
                return BadRequest<string>(result.Message);
            return Success(result.Message);
        }
    }
}
