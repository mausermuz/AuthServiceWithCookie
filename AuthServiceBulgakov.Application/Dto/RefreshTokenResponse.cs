namespace AuthServiceBulgakov.Application.Dto
{
    public record RefreshTokenResponse(string UserName, string AccessToken, string RefreshToken);
}
