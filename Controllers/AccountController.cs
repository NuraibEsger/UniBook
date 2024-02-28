using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using UniBook.DTOs.Account;
using UniBook.Entities;
using UniBook.Services.Abstract;
using UniBook.Services.Concrete;

namespace UniBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, [FromServices] IJwtTokenService tokenService)
        {
            if (!ModelState.IsValid)
            {
                var messages = ModelState
                  .SelectMany(modelState => modelState.Value!.Errors)
                  .Select(err => err.ErrorMessage)
                  .ToList();

                return BadRequest(messages);
            }

            var user = await _userManager.FindByEmailAsync(dto.Email!);
            if (user is null) return NotFound();

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, dto.Password!);
            if (!isPasswordCorrect) return NotFound();

            if(dto.Password != dto.ConfirmPassword)
            {
                ModelState.AddModelError(nameof(LoginDto.ConfirmPassword), "Confirm password doesn't match. Type again!");

                var validationErrors = ModelState
                .SelectMany(modelState => modelState.Value!.Errors)
                .Select(err => err.ErrorMessage)
                .ToList();

                return BadRequest(validationErrors);
            }

            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            var token = tokenService.GenerateToken(user.Name!, user.Surname!, user.UserName!, roles, user.Id);

            if (token is null) return NotFound();

            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto, [FromServices] ISendEmailService sendEmail)
        {
            if(ModelState.IsValid)
            {
                if(_userManager.Users.Any(x=>x.Email == dto.Email)) return BadRequest("User alredy exists");

                var user = new AppUser
                {
                    Name = dto.Name,
                    Surname = dto.Surname,
                    Email = dto.Email,
                    UserName = dto.Email,
                };


                if (dto.Password != dto.ConfirmPassword)
                {
                    ModelState.AddModelError(nameof(LoginDto.ConfirmPassword), "Confirm password doesn't match. Type again!");

                    var validationErrors = ModelState
                    .SelectMany(modelState => modelState.Value!.Errors)
                    .Select(err => err.ErrorMessage)
                    .ToList();

                    return BadRequest(validationErrors);
                }

                var result = await _userManager.CreateAsync(user, dto.Password!);

                if (!result.Succeeded) return BadRequest();

                await _userManager.AddToRoleAsync(user, "User");

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = $"http://localhost:5173/EmailConfirmation?token={token}&email={dto.Email}";

                bool IsSendEmail = await sendEmail.EmailSend(dto.Email!, confirmationLink!);

                if (IsSendEmail)
                    return Ok("User registered. Confirmation email sent.");
                else
                    return BadRequest("Failed to send confirmation email");

            }
            return BadRequest();
        }
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return BadRequest("Email not found");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return Ok("Email confirmed successfully");
            else
                return BadRequest($"Failed to confirm email: {email}");
        }
    }
}
