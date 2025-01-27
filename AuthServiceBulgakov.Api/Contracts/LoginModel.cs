using System.ComponentModel.DataAnnotations;

namespace AuthServiceBulgakov.Api.Contracts
{
    public record LoginModel(string UserName, string Password);
}
