using System.ComponentModel.DataAnnotations;

namespace AuthServiceBulgakov.Api.Contracts
{
    public record ChangeStatusUserRequest([Required]Guid UserId, bool IsActive);
}
