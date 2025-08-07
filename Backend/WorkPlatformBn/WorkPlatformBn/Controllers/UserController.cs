using Microsoft.AspNetCore.Mvc;
using WorkPlatformBn.Interface;
using WorkPlatformBn.Model.Request;

namespace WorkPlatformBn.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("/register")]
    public async Task<IActionResult> Register([FromBody]UserRequestModel model)
    {
        var result = await _userService.RegisterUser(model);

        if (result.StatusCode == StatusCodes.Status200OK)
        {
            return new OkObjectResult(result);
        }
        else
        {
            return new BadRequestObjectResult(result);
        }
    }

    [HttpPost]
    [Route("/login")]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        var result = await _userService.Login(model);

        if (result.StatusCode == StatusCodes.Status200OK)
        {
            return new OkObjectResult(result);
        }
        else
        {
            return new BadRequestObjectResult(result);
        }
    }
}
