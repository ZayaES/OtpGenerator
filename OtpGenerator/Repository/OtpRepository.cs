using Microsoft.EntityFrameworkCore.Diagnostics;
using OtpGenerator.Data;
using OtpGenerator.Interfaces;
using OtpGenerator.Models;
using OtpGenerator.Utils;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using OtpGenerator.Services;

namespace OtpGenerator.Repository
{
    
    public class OtpRepository : IOtpRepository
    {
        private readonly EmailService _emailService;
        private readonly DataContext _context;
        public OtpRepository(DataContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public ICollection<Otp> GetOtps()
        {
            return _context.Otps2.OrderBy(o => o.Id).ToList();
        }

        public Otp GetOtpByUserId(int userId)
        {
            try
            {
                return _context.Otps2.Where(o => o.UserId == userId).FirstOrDefault();
            }
            catch (ArgumentNullException ex)
            {
                return null;
            }
        }

        public Otp CreateOtps(int userId)
        {
            OtpGeneratorUtil otpGenerator = new OtpGeneratorUtil();
            string otp = otpGenerator.GenerateOtp(6);
            string hashedOtp = OTPHasher.HashOTP(otp);
            Console.WriteLine(otp);


     
            Random random = new Random();
            var userOtp = new Otp()
            {
                UserId = userId,
                //Id = random.Next(1, 99),
                Value = hashedOtp,
                ExpiryTime = DateTime.Now.AddMinutes(1)
            };

            Console.WriteLine("email about to ve sent");

            try
            {
                _emailService.SendEmailAsync("gbolahanlao@gmail.com", "Your Requested Otp", $"Your Otp is {otp}");
                Console.WriteLine("email prolly sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            _context.Otps2.Add(userOtp);
            _context.SaveChanges();
            return userOtp;
        }

    }
}
/*public async Task StartCleanupAsync(CancellationToken cancellationToken)
{
    Console.WriteLine("enters startupcleanasync");
    while (!cancellationToken.IsCancellationRequested)
    {

        await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken); // Check every minute
        DeleteOtps();
    }
    Console.WriteLine("exits startupcleanasync");

}*/


/* public void DeleteOtps()
 {
     Console.WriteLine("enters deleteotps");

     DateTime Now = DateTime.UtcNow;
     var otpsToDelete =  _context.Otps2
     .Where(entry => entry.ExpiryTime <= Now)
     .ToList();
     Console.WriteLine("after choosing otps to delete");

     foreach (var entry in otpsToDelete)
     {
         Console.WriteLine("enters foreach");

         _context.Otps.Remove(entry);
     }
     Console.WriteLine("jst b4 save");

     _context.SaveChanges();
     Console.WriteLine("after savech");

 }*/
/* public void DeleteExpiredOtps()
 {
     // Logic to delete expired OTPs

     Console.WriteLine("Deleting expired OTPs...");
     DeleteOtps();

 }*/