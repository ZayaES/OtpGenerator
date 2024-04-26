using Microsoft.AspNetCore.Mvc;
using OtpGenerator.Interfaces;
using OtpGenerator.Models;
using OtpGenerator.Utils;
using System.ComponentModel;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OtpGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : Controller
    {
        private readonly IOtpRepository _otpRepository;
        public OtpController(IOtpRepository otpRepository)
        {
            Console.WriteLine("otpcontroller(rep) ran");
            _otpRepository = otpRepository;
        }
        // GET: api/<OtpController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Otp>))]
        public IActionResult GetOtps()
        {
            var otp = _otpRepository.GetOtps();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(otp);
        }

        // GET api/<OtpController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OtpController>
        [HttpPost]
        public IActionResult CreateOtps([FromBody] int value)
        {
            if (value == null)
            {
                return( BadRequest(ModelState));
            }
            var otp = _otpRepository.GetOtpByUserId(value);
            if (otp != null) 
            {
                ModelState.AddModelError("", "User still has an Otp");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState); 
            }
            var newOtp = _otpRepository.CreateOtps(value);
            if (newOtp != null)
            {
                Console.WriteLine(newOtp.Value);
            }
            
           // ;
            //Console.WriteLine("Otp delete ran");

            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

           // _otpRepository.StartCleanupAsync(cancellationTokenSource.Token);

            Console.WriteLine("just b4 suc return");
            return Ok("Successfully created otp");

            //_otpRepository.StartCleanupAsync(newOtp);
        }

        [HttpPost("validate")]
        public IActionResult ValidateOtps([FromBody] int id, int otp)
        {
            if (id == null || otp == null)
            {
                return (BadRequest(ModelState));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string trueHashedOtp = _otpRepository.GetOtpByUserId(id).Value;
            string stringOtp = otp.ToString();
            bool value = OTPHasher.VerifyOTP(stringOtp, trueHashedOtp);
            if (value == true)
            {
                return Ok("Otp matches");
            }
            else
            {
                ModelState.AddModelError("", "Otp incorrect");
                return StatusCode(404, ModelState);
            }
         }
            // PUT api/<OtpController>/5
        [HttpPut("{id}")]
            public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OtpController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
