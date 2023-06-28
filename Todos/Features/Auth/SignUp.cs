using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Exceptions;
using Todos.Database.Models;
using Todos.Features.Todos;

namespace Todos.Features.Auth;

public class SignUp
{
    public class Command : IRequest<Result>
    {
        public required string Username { get; init; }
        public required string Email { get; init; }
        public required string PhoneNumber { get; init; }
        public required string Password { get; init; }
        public required string ConfirmPassword { get; init; }
    }

    public class Result
    {
        public required AuthDto Auth { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Username).NotEmpty();
            RuleFor(c => c.Email).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(c => c.PhoneNumber).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.ConfirmPassword).NotEmpty().Equal(c => c.Password);
        }
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<SignUp> _logger;
        private readonly JwtService _jwtService;

        public Handler(UserManager<AppUser> userManager, ILogger<SignUp> logger, JwtService jwtService)
        {
            _userManager = userManager;
            _logger = logger;
            _jwtService = jwtService;
        }

        public async Task<Result> Handle(Command request, CancellationToken ct)
        {
            if (await UserExcist(request))
            {
                throw new PlatformException(PlatformError.SignUpExcist);
            }

            var appUser = new AppUser
            {
                UserName = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            var identity = await _userManager.CreateAsync(appUser, request.Password);

            if (!identity.Succeeded)
            {
                _logger.LogError("Create Idendity Error {Errors}", identity.Errors);
                // TODO: return errors to user
                throw new PlatformException(PlatformError.SignUpError);
            }

            var token = _jwtService.GenerateJWTToken(appUser);

            return new Result { Auth = new AuthDto { Jwt = token } };
        }

        private async Task<bool> UserExcist(Command request)
        {
            var userByUsername = await _userManager.FindByNameAsync(request.Username);
            if (userByUsername != null) return true;

            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userByEmail != null) return true;

            return false;
        }
    }
}
