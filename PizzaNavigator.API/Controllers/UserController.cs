using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PizzaNavigator.API.Db;
using PizzaNavigator.API.Models;
using PizzaNavigator.API.Models.Dto;
using PizzaNavigator.API.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PizzaNavigator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly PizzaNavigatorContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserController(AuthenticationSettings authenticationSettings, PizzaNavigatorContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _authenticationSettings = authenticationSettings;
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            if (_dbContext.Users.FirstOrDefault(u => u.Email == dto.Email) != null) return BadRequest("Email in use");
           
            var newUser = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Username = dto.Username,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.Password = hashedPassword;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return Created();


        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null) return BadRequest("Invalid email or password.");
            
            var verityPassword = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (verityPassword == PasswordVerificationResult.Failed) BadRequest("Invalid email or password.");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("Username", user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.UtcNow.AddDays(_authenticationSettings.JwtExpireDays);

            var tokenSettings = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: exp,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenSettings);
            return Ok(token);
        }

        [Authorize]
        [HttpDelete]
        public ActionResult RemoveUser([FromBody]DeleteUserDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id.ToString() == userId);

            var verityPassword = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (verityPassword == PasswordVerificationResult.Failed) return BadRequest("Incorrect password.");

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
