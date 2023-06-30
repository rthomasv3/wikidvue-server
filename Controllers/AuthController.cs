using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using WikidVueServer.Services.Abstractions;

namespace WikidVueServer.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    #region Fields

    private readonly IAuthService _authService;

    #endregion

    #region Constructor

    public AuthController(IAuthService authService)
    {
        _authService = Guard.Against.Null(authService);   
    }

    #endregion

    #region Public Methods

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    #endregion
}
