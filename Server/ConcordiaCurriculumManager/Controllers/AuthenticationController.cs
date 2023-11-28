using AutoMapper;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace ConcordiaCurriculumManager.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : Controller
{
    private readonly IMapper _mapper;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IUserAuthenticationService _userAuthenticationService;

    public AuthenticationController(IMapper mapper, ILogger<AuthenticationController> logger, IUserAuthenticationService authenticationService)
    {
        _mapper = mapper;
        _logger = logger;
        _userAuthenticationService = authenticationService;
    }

    [HttpPost(nameof(Register))]
    [Consumes(typeof(RegisterDTO), MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "User created successfully", typeof(AuthenticationResponse), AuthenticationResponse.MediaType)]
    [SwaggerRequestExample(typeof(RegisterDTO), typeof(RegisterDTOExample))]
    public async Task<ActionResult> Register([FromBody, Required] RegisterDTO parameter)
    {
        if (parameter.Email.Equals("EmailExample@example.com"))
        {
            throw new InvalidInputException("Cannot use example values");
        }

        var parsedUserInput = _mapper.Map<User>(parameter);
        var accessToken = await _userAuthenticationService.CreateUserAsync(parsedUserInput);
        var response = new AuthenticationResponse() { AccessToken = accessToken };

        return Created($"/{nameof(Register)}", response);
    }

    [HttpPost(nameof(Login))]
    [Consumes(typeof(LoginDTO), MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "User logged in successfully", typeof(AuthenticationResponse), AuthenticationResponse.MediaType)]
    [SwaggerRequestExample(typeof(LoginDTO), typeof(LoginDTOExample))]
    public async Task<ActionResult> Login([FromBody, Required] LoginDTO parameter)
    {
        if (parameter.Email.Equals("EmailExample@example.com"))
        {
            throw new InvalidInputException("Cannot use example values");
        }

        var accessToken = await _userAuthenticationService.SigninUser(parameter.Email, parameter.Password);
        var response = new AuthenticationResponse() { AccessToken = accessToken };

        return Ok(response);
    }

    [Authorize]
    [HttpPost(nameof(Logout))]
    [SwaggerResponse(StatusCodes.Status200OK, "User logged out successfully")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerRequestExample(typeof(LoginDTO), typeof(LoginDTOExample))]
    public async Task<ActionResult> Logout()
    {
        await _userAuthenticationService.SignoutUser();
        return Ok();
    }
}
