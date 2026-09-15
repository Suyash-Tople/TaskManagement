using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Security;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JwtTokenService _jwtTokenService;

        public AuthService(AppDbContext context, IPasswordHasher<User> passwordHasher, JwtTokenService jwtTokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if(user == null)
            {
                throw new NotFoundException("User not found or Email/Password is incorrect.");
            }

            if(!user.IsActive)
            {
                throw new BusinessException("User is not Active.");
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if(passwordResult == PasswordVerificationResult.Failed)
            {
                throw new ValidationException("Incorrect Email and Password.");
            }

            return _jwtTokenService.GenerateToken(user);
        }

    }
}
