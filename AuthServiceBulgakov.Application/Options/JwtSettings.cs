namespace AuthServiceBulgakov.Application.Options
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int MinutesToExpirationAccessToken { get; set; }

        public int DaysToExpirationRefreshToken { get; set; }

        public TimeSpan ExpireAccessToken => TimeSpan.FromMinutes(MinutesToExpirationAccessToken);
        //public TimeSpan ExpireRefreshTokenToken => TimeSpan.FromDays(DaysToExpirationRefreshToken);

    }
}