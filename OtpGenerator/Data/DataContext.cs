using Microsoft.EntityFrameworkCore;
using OtpGenerator.Models;

namespace OtpGenerator.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Otp> Otps2 { get; set; }
    }
}
