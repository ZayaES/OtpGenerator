using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;
using System.Text;

namespace OtpGenerator.Utils
{
    public class OtpGeneratorUtil
    {
        public string GenerateOtp (int length)
        {
            const string validChars = "012345678";
            char[] chars = new char[length];
            byte[] data = new byte[length];

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            for (int i = 0; i < length; i++)
            {
                int index = data[i] % validChars.Length;
                chars[i] = validChars[index];
            }
            return new string(chars);
        }
    }
    public class OTPHasher
    {
        public static string HashOTP(string otp)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the OTP string to bytes
                byte[] otpBytes = Encoding.UTF8.GetBytes(otp);

                // Compute the hash of the OTP bytes
                byte[] hashBytes = sha256.ComputeHash(otpBytes);

                // Convert the hash bytes to a hexadecimal string
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2")); // Convert each byte to a two-digit hexadecimal representation
                }

                return stringBuilder.ToString();
            }
        }
        public static bool VerifyOTP(string enteredOTP, string hashedOTP)
        {
            // Hash the entered OTP using the same method
            string hashedEnteredOTP = HashOTP(enteredOTP);

            // Compare the hashed entered OTP with the stored hashed OTP
            return hashedEnteredOTP == hashedOTP;
        }
    }
}
