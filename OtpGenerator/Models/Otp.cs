namespace OtpGenerator.Models
{
    public class Otp
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime ExpiryTime { get; set; }
        public int UserId { get; set; }
    }
}
