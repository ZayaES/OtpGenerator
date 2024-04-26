using Microsoft.EntityFrameworkCore;
using OtpGenerator.Data;
using OtpGenerator.Interfaces;
using OtpGenerator.Repository;

namespace OtpGenerator
{
    public class BackgroundTask
    {
        private readonly IOtpRepository otpRepository;
        private DataContext _context;

        public void DeleteExpiredOtps(DataContext context)
        {
            _context = context;
            // Logic to delete expired OTPs
            Console.WriteLine("enters deleteotps");

            DateTime Now = DateTime.UtcNow;
            var otpsToDelete = _context.Otps2
            .Where(entry => entry.ExpiryTime <= Now)
            .ToList();
            Console.WriteLine("after choosing otps to delete");

            foreach (var entry in otpsToDelete)
            {
                Console.WriteLine("enters foreach");

                _context.Otps2.Remove(entry);
            }
            Console.WriteLine("jst b4 save");

            _context.SaveChanges();
            Console.WriteLine("after savech");

            Console.WriteLine("Deleting expired OTPs...");

        }
    }
}
