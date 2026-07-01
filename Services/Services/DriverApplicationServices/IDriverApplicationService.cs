using Data.Entity.Drivers;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.DriverApplicationServices
{
    public  interface IDriverApplicationService
    {
        Task<DriverApplication> AddDriverApp(DriverApplication application);
        Task<DriverApplication> AproveApplication(int AppId, string? CommentAdmin);
        Task<DriverApplication> RejectApplication(int AppId, string? CommentAdmin);

        Task<DriverApplication> GetDriverAppBtId(int id);

        Task<Driver> CreateDriverFromApp(DriverApplication application);
    }
}
