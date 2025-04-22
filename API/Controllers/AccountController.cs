using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using API.Services.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
namespace API.Controllers;
public class AccountController : BaseApiController
{
    private readonly IUsersService _usersService;


    public AccountController(IUsersService usersService)
    {
        this._usersService = usersService;
 
    }
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var response = await _usersService.Register(registerDto);
        return StatusCode(response.StatusCode, response);

    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        var response = await _usersService.login(loginDto);
        return StatusCode(response.StatusCode, response);
    }



}
