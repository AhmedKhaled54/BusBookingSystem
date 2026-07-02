using Data.Entity.Drivers;
using Data.Enums;
using Infrastracture.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.DriverApplicationServices
{
    public class DriverApplicationService : IDriverApplicationService
    {
        private readonly IUnitOfWork _UOW;
        public DriverApplicationService(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }

        public async Task<DriverApplication> AddDriverApp(DriverApplication application)
        {
            var driver =await _UOW.Repository<DriverApplication>()
                .IsAny(c=>c.UserId==application.UserId&&c.Status==ApplicationDriverStatus.pending);

            if (driver)
                throw new Exception("driver already pending Application");

            await _UOW.Repository<DriverApplication>().AddAsync(application);
            await _UOW.Complete();
            return application;

        }

        public async Task<DriverApplication> AproveApplication(int AppId, string? CommentAdmin)
        {
            //change status 
            var app =await _UOW.Repository<DriverApplication>().FindAsync(c=>c.Id==AppId);
            if (app == null)
                throw new Exception("App Not Found !");
            if (app.Status != ApplicationDriverStatus.pending)
                throw new Exception("Only Pending Application Can Be Approved");
            app.Status = ApplicationDriverStatus.Approved;
            app.ReviewAt = DateTime.Now;
            app.AdminComment = CommentAdmin;
            _UOW.Repository<DriverApplication>().Update(app);
            return app;
        }

        public async Task<Driver> CreateDriverFromApp(DriverApplication application)
        {
            var driver = new Driver
            {
                FullName = application.FullName,
                Email = application.Email,
                LicenceExprireYear = application.LicenceExprireYear,
                NationalId = application.NationalId,
                PhoneNumber = application.PhoneNumber,
                LicenceNumber = application.LicenceNumber,
                LicenceImageUrl = application.LicenceImageUrl,
                UserId = application.UserId
            };

            await _UOW.Repository<Driver>().AddAsync(driver);
            return driver;
        }

        public async Task<DriverApplication> GetDriverAppBtId(int id)
            =>await _UOW.Repository<DriverApplication>().FindAsync(c=>c.Id==id);

        public async  Task<DriverApplication> RejectApplication(int AppId, string? CommentAdmin)
        {
            var app = await _UOW.Repository<DriverApplication>().FindAsync(c => c.Id == AppId);
            if (app == null)
                throw new Exception("App Not Found !");
            if (app.Status != ApplicationDriverStatus.pending)
                throw new Exception("Only Pending Application Can Be Rejected");
            app.Status = ApplicationDriverStatus.Rejected;
            app.ReviewAt = DateTime.Now;
            app.AdminComment = CommentAdmin;
            _UOW.Repository<DriverApplication>().Update(app);
            return app;


        }
    }
}
