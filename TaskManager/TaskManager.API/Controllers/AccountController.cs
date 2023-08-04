using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.API.DTOs;
using TaskManager.Core.Models;
using TaskManager.Data.Configurations;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager
            , JwtSettings jwtSettings
            , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                // Retorna um BadRequest com erros do ModelState
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                // Retorna um BadRequest com erros do ModelState
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                // Retorna Unauthorized com uma mensagem específica
                return Unauthorized(new { error = "Usuário não encontrado." });
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!validPassword)
            {
                // Retorna Unauthorized com uma mensagem específica
                return Unauthorized(new { error = "Senha inválida." });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    // você pode adicionar mais claims se necessário
                }),
                Expires = DateTime.UtcNow.AddDays(7), // validade do token, você pode definir conforme suas necessidades
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }

    }
}
