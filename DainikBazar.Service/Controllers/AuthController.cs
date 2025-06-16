using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuth0Manager authService) : ControllerBase
{
    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var success = await authService.ChangePassword(userId, request.NewPassword);
        return success ? Ok() : BadRequest();
    }

    [Authorize]
    [HttpGet("userinfo")]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await authService.GetUserInfo(userId);
        return Ok(user);
    }
}