using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreWebAPI.Entities;
using StoreWebAPI.Exceptions;
using StoreWebAPI.Models;
using StoreWebAPI.Services;

namespace StoreWebAPI.Controllers;

[ApiController]
[Route("/api/auth")]
[AllowAnonymous]
[Produces(MediaTypeNames.Application.Json)]
public class SignInController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly JwtService _jwtService;
    private readonly IUserService _userService;

    public SignInController(IAuthenticationService authenticationService, JwtService jwtService,
        IUserService userService)
    {
        _authenticationService = authenticationService;
        _jwtService = jwtService;
        _userService = userService;
    }

    [HttpPost("signin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<ActionResult<TokenInfo>> SignIn([FromBody] SignInModel signInModel)
    {
        User? user = await _authenticationService.Authenticate(signInModel.Username, signInModel.Password);
        if (user is null) return Unauthorized();

        // Build JWT
        string token = _jwtService.GenerateToken(user);
        var tokenInfo = new TokenInfo
        {
            Token = token,
            ExpirationDateUtc = _jwtService.GetExpirationDate(token)
        };

        // Return JWT Model
        return Ok(tokenInfo);
    }

    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> SignUp([FromBody] NewUserModel newUser)
    {
        try
        {
            User createdUser = await _userService.CreateUser(
                username: newUser.Username,
                password: newUser.Password,
                role: newUser.Role,
                email: newUser.Email,
                firstName: newUser.FirstName,
                middleName: newUser.MiddleName,
                lastName: newUser.LastName);
            return NoContent();
        }
        catch (InvalidUserRoleException e)
        {
            return BadRequest(e.Message);
        }
        catch (UserNotCreatedExcpetion e)
        {
            ModelState.TryAddModelException("", e);
            return StatusCode(500, "Internal server error"); // TODO: Change this logic
        }
    }
}
