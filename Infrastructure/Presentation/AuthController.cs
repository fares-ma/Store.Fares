using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager serviceManager ) : ControllerBase
    {
        // Login
        [HttpPost("login")] // POST : /api/auth/login
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await serviceManager.AuthService.LoginAsync(loginDto);
            return Ok(result);

        }
        // Register

        [HttpPost("register")] // Post : /api/auth/register
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await serviceManager.AuthService.RegisterAsync(registerDto);
            return Ok(result);
        }

        [HttpGet("EmailExists")] // GET : /api/auth/EmailExists
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var result = await serviceManager.AuthService.CheckEmailExistsAsync(email);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.AuthService.GetCurrentUserAsync(email);
            return Ok(result);
        }

        [HttpGet("Address")]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthService.GetCurrentUserAddressAsync(email);
            return Ok(result);
        }

        [HttpPut("Address")]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthService.UpdateCurrentUserAddressesAsync(addressDto, email);
            return Ok(result);
        }



    }
}
