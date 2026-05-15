using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enums
{
    public  enum NotificationType
    {
        BookingCreated = 1,
        BookingConfirmed = 2,
        BookingCancelled = 3,
        PaymentSucceeded = 4,
        PaymentFailed = 5,
        PaymentRefunded = 6,
        TripReminder = 7,
        TripStarted = 8,
        TripDelayed = 9,
        TripCancelled = 10,
        TripCompleted = 11,
        SeatReserved = 12,
        DriverAssigned = 13,
        DriverApplicationSubmitted = 14,
        DriverApplicationApproved = 15,
        DriverApplicationRejected = 16,
        NewReview = 17,
        BusArrivingSoon = 18,
        SystemAnnouncement = 19 

    }
}
