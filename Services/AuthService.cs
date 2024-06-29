using M10Backend.DAL;
using M10Backend.DTOs;
using M10Backend.Entities;
using M10Backend.Exceptions;
using M10Backend.Exceptions.Auth;
using M10Backend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace M10Backend.Services
{
    public class AuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly IHttpContextAccessor _http;
        private readonly AppDbContext _context;
        private readonly string _userName;

        public AuthService(UserManager<AppUser> userManager, TokenService tokenService, IHttpContextAccessor http, AppDbContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _http = http;
            _context = context;
            _userName = http.HttpContext.User.Identity.Name;
        }
        public async Task RegisterAsync(RegisterUserDto req)
        {
            AppUser? user = await _userManager.Users.Where(u => u.PhoneNumber == req.Phone).Include(u => u.OTPCodes).FirstOrDefaultAsync();

            if (user is null)
            {
                user = new AppUser
                {
                    PhoneNumber = req.Phone,
                    UserName = req.Phone
                };
                var res = await _userManager.CreateAsync(user);
                if (!res.Succeeded)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (IdentityError err in res.Errors)
                    {
                        sb.AppendLine(err.Description);
                    }
                    throw new UserCreateException(sb.ToString());
                }
            }
            OTPCode? loginOtp = user.OTPCodes.FirstOrDefault();
            if (loginOtp is not null)
            {
                if (DateTime.UtcNow < loginOtp.ValidTo) throw new AlreadyExistsException("You already have an active OTP code!");
                _context.OTPCodes.Remove(loginOtp);
                await _context.SaveChangesAsync();

            }
            user.OTPCodes.Add(new OTPCode
            {
                Code = 1111,
                ValidTo = DateTime.UtcNow.AddMinutes(1)
            });

            await _context.SaveChangesAsync();
        }

        public async Task<TokenResponseDto> ConfirmOTPAsync(ConfirmOTPDto dto)
        {
            AppUser? user = await _userManager.Users.Include(u => u.OTPCodes).FirstOrDefaultAsync(u => u.PhoneNumber == dto.Phone)
                ?? throw new NotFoundException($"User didnt found!");
            OTPCode? activeOTP = user.OTPCodes
                .Where(otp => otp.Code == dto.OTP && otp.User.Email == user.Email && otp.ValidTo.AddMinutes(1) >= DateTime.UtcNow)
                .FirstOrDefault()
               ?? throw new NotFoundException($"Invalid OTP!");

            TokenResponseDto tokens = await _tokenService.GenerateTokensAsync(user);
            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiresAt = tokens.RefreshTokenExpiresAt;
            await _userManager.UpdateAsync(user);
            _context.OTPCodes.Remove(activeOTP);
            await _context.SaveChangesAsync();
            return tokens;

        }

        public async Task<string> CheckAuth()
        {
            AppUser user = await _userManager.FindByNameAsync(_userName)
                ?? throw new NotFoundException($"User not found!");
            return user.PhoneNumber;

        }
    }
}
