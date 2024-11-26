using System.Security.Claims;

namespace RoadMech.Mechanic.API.Models.Utility
{
    public class SessionHelper
    {
        public string GenerateOTP()
        {
            Random r = new Random();
            string OTP = r.Next(100000, 999999).ToString();
            return OTP;
          //  return "1234";
        }
    }
     
}
