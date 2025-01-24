using System.ComponentModel.DataAnnotations;

namespace AuthServiceBulgakov.Api.Contracts
{
    public record LoginModel([Required]string UserName, [Required]string Password);
}
