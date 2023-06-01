using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CompanySystem.BL;
using CompanySystem.DAL;

namespace WebApiApp.Controllers
{

    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IInstructorManager instructorManager;
        public UserController(IConfiguration configuration,IInstructorManager _instructorManager)
        {
            _configuration = configuration;
            instructorManager = _instructorManager;
        }

        #region static login
        [HttpPost]
        [Route("StaticLogin")]
        public ActionResult<string> Login(LoginDto credentials)
        {
            if (credentials.UserName == "admin" && credentials.Password == "pass")
            {
                #region Claims
                var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, credentials.UserName), //default type
                    new Claim(ClaimTypes.Email,$"{credentials.UserName}@gmail.com"),
                    new Claim("Nationality","Egyptian") //custom type
                };
                #endregion

                #region Key
                var SecretKey = _configuration.GetValue<string>("SecretKey");
                var SecretKeyInByte = Encoding.ASCII.GetBytes(SecretKey);
                var key = new SymmetricSecurityKey(SecretKeyInByte);
                #endregion

                #region Determine how to generate HASHING RESULT using microsoft.identitymodel.tokens liberary
                var methodUsedInGeneratingToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                #endregion

                #region Generate Token using System.IdentityModel.Tokens.Jwt liberary
                var jwt = new JwtSecurityToken(
                        claims: userClaims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddMinutes(15),
                        //issuer:"admin",
                        //audience: "admin",
                        signingCredentials: methodUsedInGeneratingToken
                        );

                var tokenHandler = new JwtSecurityTokenHandler();
                string tokenString = tokenHandler.WriteToken(jwt);
                #endregion

                return Ok(tokenString);
            }
            return Unauthorized("Wrong credintials");
        } 
        #endregion

        [HttpPost]
        [Route("StaticRegister")]
        public ActionResult<string> Register(RegisterDto registerDto)
        {
            var newInstructor = new Instructor
            {
                Name = registerDto.UserName,
                Email = registerDto.Email,              
            };
            return null;
        }

    }
}
