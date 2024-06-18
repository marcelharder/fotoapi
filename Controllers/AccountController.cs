using fotoservice.data;
using fotoservice.data.interfaces;

namespace api.Controllers;

    public class AccountController : BaseApiController
    {

        private readonly ITokenService _ts;
        private static readonly HttpClient client = new HttpClient();
        private readonly UserManager<AppUser> _manager;
        private readonly SignInManager<AppUser> _signIn;
        private readonly IConfiguration _config;
        private IUsers _users;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AccountController(
            ITokenService ts,
            IConfiguration config,
            UserManager<AppUser> manager,
            IUsers users,
            IWebHostEnvironment hostEnvironment,
            SignInManager<AppUser> signIn)
        {
            _config = config;
            _manager = manager;
            _signIn = signIn;
            _ts = ts;
            _users = users;
            _hostEnvironment = hostEnvironment;



        }

        [HttpGet("checkIfUserExists/{email}")]
        public async Task<int> userexists(string email)
        {
            var result = 0;
            var user = await _manager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null) { result = 1; }
            return result;
        }

       

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserForLoginDto ufl)
        {
            var user = await _manager.Users.SingleOrDefaultAsync(x => x.UserName == ufl.UserName.ToLower());
            if (user == null) return Unauthorized();

            var result = await _signIn.CheckPasswordSignInAsync(user, ufl.password, false);
            if (!result.Succeeded) return Unauthorized();


            return new UserDto
            {
                UserName = user.UserName,
                Token = await _ts.CreateToken(user),
                UserId = user.Id,
                paidTill = user.PaidTill
            };
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> CChangePwd([FromBody] ChangePasswordDto ufl)
        {
            var user = await _manager.FindByEmailAsync(ufl.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            var resultp = await _signIn.CheckPasswordSignInAsync(user, ufl.CurrentPassword, false);
            if (!resultp.Succeeded) return BadRequest("Pls use correct password ...");

            var result = await _manager.ChangePasswordAsync(user, ufl.CurrentPassword, ufl.Password);
            if (!result.Succeeded) return BadRequest("Changing password failed ...");

            return Ok("Password changed");
        }

      

     


    }

