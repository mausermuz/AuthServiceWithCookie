using AuthServiceBulgakov.Application.Dto;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AuthServiceBulgakov.Application.UseCases.Users.Commands
{
    public record RefreshTokenCommand([Required] string UserName, [Required] string RefreshToken) : IRequest<RefreshTokenResponse>;
}
