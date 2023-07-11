using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Exceptions;
using System.Text.Json.Serialization;
using Todos.Database.Models;
using Todos.Features.Auth;

namespace Todos.Features.Todos;

public class SignIn
{
    public class Command : IRequest<Result>
    {
        public required string UsernameEmail { get; init; }
        [JsonIgnore]
        public required string Password { get; init; }
    }

    public class Result
    {
        public required string Jwt { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.UsernameEmail).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<SignIn> _logger;
        private readonly JwtService _jwtService;

        public Handler(UserManager<AppUser> userManager, ILogger<SignIn> logger, JwtService jwtService)
        {
            _userManager = userManager;
            _logger = logger;
            _jwtService = jwtService;
        }

        public async Task<Result> Handle(Command request, CancellationToken ct)
        {
            var command = new Command
            {
                UsernameEmail = request.UsernameEmail.Trim(),
                Password = request.Password.Trim(),
            };

            var appUser = await GetUser(command);

            if (!await _userManager.CheckPasswordAsync(appUser, command.Password))
            {
                throw new PlatformException(PlatformError.SignInInvalidPassword);
            }

            var token = _jwtService.GenerateJWTToken(appUser);

            return new Result { Jwt = token };

        }

        private async Task<AppUser> GetUser(Command request)
        {
            var userByUsername = await _userManager.FindByNameAsync(request.UsernameEmail);
            if (userByUsername != null) return userByUsername;

            var userByEmail = await _userManager.FindByEmailAsync(request.UsernameEmail);
            if (userByEmail != null) return userByEmail;

            throw new PlatformException(PlatformError.SignInNotFound);
        }
    }
}
