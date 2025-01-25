namespace AuthServiceBulgakov.Application.Dto
{
    public record LoginResponse(string UserName, string AccessToken, string RefreshToken, bool IsActive);
}
