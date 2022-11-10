using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DataServiceLib;
using DataServiceLib.DBObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectPortfolio2_Group11.Authentication.Services;
using ProjectPortfolio2_Group11.Model;
using System.Threading.Tasks;


namespace ProjectPortfolio2_Group11.Controller
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DataServiceFacade _dataServiceFacade;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsersController(DataServiceFacade dataServiceFacade, IMapper mapper, IConfiguration configuration)
        {
            _dataServiceFacade = dataServiceFacade;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        
        [HttpGet("{userId}/{password}")]
        public IActionResult GetUser(int userId,string password)
        {
            var user = _dataServiceFacade.UsersDs.GetUser(userId,password);

            if (user == null)
            {
                return NotFound("user not found");
            }
            return Ok(_mapper.Map<UsersDto>(user));
        }
        
        [HttpPost]
        public IActionResult CreateUsers(UsersForCreationDto usersForCreationDto)
        {
            var user = _mapper.Map<Users>(usersForCreationDto);
            _dataServiceFacade.UsersDs.CreateUser(user);
            return Created("", user);
        }
        
        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, UsersForCreationDto usersForCreationDto)
        {
            var user = _mapper.Map<Users>(usersForCreationDto);
            if (!_dataServiceFacade.UsersDs.UpdateUser(userId, user))
            {
                return NotFound("user not found");
            }
            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            
            var response = " user not found";
            if (!_dataServiceFacade.UsersDs.DeleteUser(userId))
            {
                return NotFound(response);
            }
            response = " user deleted succesfully";
            return CreatedAtRoute(null, userId + response);
        }
        
        //For Authentication
        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            if (_dataServiceFacade.UsersDs.AuthenticationGetUser(registerDto.Username) != null)
            {
                return BadRequest("User already exists");
            }

            int.TryParse(_configuration.GetSection("Auth:PasswordSize").Value, out int pwdSize);

            if (pwdSize == 0)
            {
                throw new ArgumentException("No password size");
            }

            var salt = PasswordService.GenerateSalt(pwdSize);
            var pwd = PasswordService.HashPassword(registerDto.Password, salt, pwdSize);

            _dataServiceFacade.UsersDs.AuthenticationCreateUser(registerDto.Name, registerDto.Username, pwd, salt);

            return CreatedAtRoute(null, new { registerDto.Username });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _dataServiceFacade.UsersDs.AuthenticationGetUser(dto.Username);
            if (user == null)
            {
                return BadRequest("user not found");
            }

            int.TryParse(_configuration.GetSection("Auth:PasswordSize").Value, out int pwdSize);

            if (pwdSize == 0)
            {
                throw new ArgumentException("No password size");
            }

            string secret = _configuration.GetSection("Auth:Secret").Value;
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("No secret");
            }

            var password = PasswordService.HashPassword(dto.Password, user.Salt, pwdSize);

            if (password != user.Password)
            {
                return BadRequest("wrong password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(secret);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.Now.AddSeconds(600),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var token = tokenHandler.WriteToken(securityToken);

            return Ok(new { dto.Username, token });
        }

    }
}