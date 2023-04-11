using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace to_become_junior.Controllers;

[ApiController]
[Route(Path)]
public class AuthController : ApiControllerBase
{
    private const string Path = "api/v1/auth";

    [HttpPost, Route("login")]
    public async Task<IActionResult> Login()
    {
        var message = "Login";
        return Ok(new { message });
    }
}