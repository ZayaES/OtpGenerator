using OtpGenerator.Models;

namespace OtpGenerator.Interfaces
{
    public interface IOtpRepository
    {
        ICollection<Otp> GetOtps();

        Otp GetOtpByUserId(int userId);
        Otp CreateOtps(int userId);
        //void ValidateOtps(int otp);
        //Task StartCleanupAsync(CancellationToken cancellationToken);
        //void DeleteOtps();
    }
}
