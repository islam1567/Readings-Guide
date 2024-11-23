using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Readings_Guide.Cores.Dtos;
using Readings_Guide.Cores.Interfaces;
using System.Security.Claims;

namespace Readings_Guide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth service;

        public AuthController(IAuth service)
        {
            this.service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModelDto model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await service.RegisterAsync(model);
            if(!result.IsAuthntecation)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModelDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await service.LoginAsync(model);
            if (!result.IsAuthntecation)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePaswword(ChangePasswordDto model)
        {
            var userd =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await service.ChangePasswordAsync(userd, model);
            if(success)
                return Ok("Password changed successfully");

            return BadRequest();
        }
    }
}
