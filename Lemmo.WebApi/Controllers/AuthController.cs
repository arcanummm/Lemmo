using Lemmo.WebApi.Application.Commands.Auth.Login;
using Lemmo.WebApi.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lemmo.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto, [FromHeader(Name = "User-Agent")] string? device)
        {
            var result = await mediator.Send(new LoginCommand(dto.PhoneNumber, dto.Password, device));

            return Ok(result);
        }
    }
}
