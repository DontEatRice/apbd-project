using blazor_project.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                return BadRequest("User does not exist");

            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!singInResult.Succeeded)
                return BadRequest("Invalid password");

            await _signInManager.SignInAsync(user, request.RememberMe);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register body)
        {
            var user = new ApplicationUser();
            user.UserName = body.UserName;
            var result = await _userManager.CreateAsync(user, body.Password);
            if (!result.Succeeded) 
                return BadRequest(result.Errors.FirstOrDefault()?.Description);

            return await Login(new Login
            {
                UserName = body.UserName,
                Password = body.Password
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public CurrentUser CurrentUser() {
            return new CurrentUser {
                UserName = User.Identity?.Name,
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value),
                IsAuthenticated = User.Identity is null ? false : User.Identity.IsAuthenticated
            };
        }
    }
}