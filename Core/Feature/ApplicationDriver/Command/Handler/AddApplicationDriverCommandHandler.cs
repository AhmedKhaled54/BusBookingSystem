using AutoMapper;
using Core.Base;
using Core.Feature.ApplicationDriver.Command.Models;
using Core.Feature.Notification.Events.EventDriverStatus;
using Data.Entity.Drivers;
using Data.Enums;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using Services.RealTimeServices.NotificationServices;
using Services.Services.DriverApplicationServices;
using Services.Services.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Core.Feature.ApplicationDriver.Command.Handler
{
    public class AddApplicationDriverCommandHandler : ResponseHandler,
        IRequestHandler<AddApplicationDriverCommand, Response<string>>,
        IRequestHandler<ApproveApplicationDriverCommand, Response<string>>,
        IRequestHandler<RejectedApplicationDriverCommand, Response<string>>
    {
        #region Feild
        private readonly UserManager<User> _userManager;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IDriverApplicationService _service;
        private readonly IHttpContextAccessor _http;
        private readonly INotificaionService _notificaionService;
        private readonly IMediator _mediator;


        #endregion
        #region Ctor
        public AddApplicationDriverCommandHandler(UserManager<User> userManager, IFileService fileService, IMapper mapper, IDriverApplicationService service, IHttpContextAccessor http, INotificaionService notificaionService, IMediator mediator)
        {
            _userManager = userManager;
            _fileService = fileService;
            _mapper = mapper;
            _service = service;
            _http = http;
            _notificaionService = notificaionService;
            _mediator = mediator;
        }
        #endregion
        public async Task<Response<string>> Handle(AddApplicationDriverCommand request, CancellationToken cancellationToken)
        {
           
           // var userid = GetUserId(); 
            var map = _mapper.Map<DriverApplication>(request);
       //     map.UserId = userid;
            var url =await _fileService.Upload(request.LicenceImageUrl, "ApplicationDriver/Images");
            map.LicenceImageUrl = url;
            var result = await _service.AddDriverApp(map);
            return Success("Created");

        }

        public async Task<Response<string>> Handle(ApproveApplicationDriverCommand request, CancellationToken cancellationToken)
        {
            
            var application = await _service.AproveApplication(request.AppId, request.AdminComment);
            
            var user =await _userManager.Users.FirstOrDefaultAsync(c=>c.Id== application.UserId);
            if (await _userManager.IsInRoleAsync(user,"Driver"))
                return BadRequest<string>("User Already has a driver !");

            var driver = await _service.CreateDriverFromApp(application);
            await _userManager.AddToRoleAsync(user, RolesPermession.Driver.ToString());
            //save notification in db 
            await _notificaionService.SendNotification(application.UserId,
                "Your Application has been Approved", NotificationType.DriverApplicationApproved);
            //notify using event 
            await _mediator.Publish(new AppStatusChangeEvents(application.UserId,
                NotificationType.DriverApplicationApproved.ToString(), "Your Application has been Approved "));

            return Success("Driver application approved successfully");
        }

        public async Task<Response<string>> Handle(RejectedApplicationDriverCommand request, CancellationToken cancellationToken)
        {
            var application =await _service.RejectApplication(request.AppId, request.AdminComment);

            await _notificaionService.SendNotification(application.UserId, "Application has been  Rejected ",NotificationType.DriverApplicationRejected);
            await _mediator.Publish(new AppStatusChangeEvents(application.UserId,
                NotificationType.DriverApplicationRejected.ToString(), "Application has been Rejected "));
            return Success("Driver application rejected");
        }

        private int GetUserId()
        {
            ClaimsPrincipal claim = _http.HttpContext.User;
            var userid = int.Parse(_userManager.GetUserId(claim));
            return userid;

        }
    }
}
