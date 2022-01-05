using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WooingApi.Data;
using WooingApi.Dtos;
using WooingApi.Interfaces;
using WooingApi.Models;

namespace WooingApi.Controllers;
public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    public AccountController(DataContext context, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await this.UserExists(registerDto.UserName)) return BadRequest("Username is taken");

        using var hmac = new HMACSHA512();

        var user = new User
        {
            UserName = registerDto.UserName.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDto{
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(loginDto.UserName));

        if (user == null) return Unauthorized("Invalid Username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computeHash.Length; i++)
        {
            if (computeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }

        return new UserDto{
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string userName)
    {
        return await _context.Users.AnyAsync(u => u.UserName.Equals(userName));
    }
}