using M10Backend.DTOs;
using M10Backend.Entities;
using M10Backend.Services;
using M10Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace M10Backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            Console.WriteLine("ok");
            await _service.RegisterAsync(dto);
            return Ok();
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> CheckAuth()
        {
            return Ok(await _service.CheckAuth());
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmOTP(ConfirmOTPDto dto)
        {
            return Ok(await _service.ConfirmOTPAsync(dto));
        }
    }
}
