using Data.Entity;
using Data.Entity.Drivers;
using Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Identity
{
    public  class User:IdentityUser<int>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => $"{FirstName} {LastName}";
        public string NationalId { get;private  set; }
        public DateTime CreatedAt { get; set; }=DateTime.Now;
        public DateOnly BirthDate { get; private  set; }
        public int Age =>DateTime.Now.Year - BirthDate.Year;
        public GenderType Gender { get; set; }

        public Driver ? Driver { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<Review> Reviews { get; set; }= new List<Review>();
        public ICollection<DriverApplication> DriverApplications { get; set; } = new List<DriverApplication>();
        public ICollection<Booking> Bookings { get; set; }=new List<Booking>();

      


        public void SetName (string F_name ,string L_name)
        {
            if (string.IsNullOrWhiteSpace(F_name) || string.IsNullOrWhiteSpace(L_name))
                throw new ArgumentException("");
            FirstName = F_name;
            LastName = L_name;
        }

        public void SetDateOfBith(DateOnly Birth)
        {
            if (Birth > DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Invalid BirthDate !");
            var age =DateTime.Now.Year - BirthDate.Year;
            if (age < 12)
                throw new ArgumentException("User Must be at 12 Years Old !");

            BirthDate = Birth;
        }

        public void SetNationalId(string  NationalId)
        {
            if (string.IsNullOrEmpty(NationalId))
                throw new ArgumentException("NationalId Is Required !");
            
            if (NationalId.Length != 14 || !NationalId.All(char.IsDigit))
                throw new ArgumentException("NationalId Must be 14 Digits !");
            
            this.NationalId = NationalId;
        }

        
    }
}
