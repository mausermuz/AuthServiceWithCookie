using System.Text.Json;
using StackExchange.Redis;

namespace AuthServiceBulgakov.Application.Extensions
{
    public static class RedisExtension
    {
        public static async Task<T?> GetAsync<T>(this IDatabase database, string key)
        {
            string? raw = await database.StringGetAsync(key);
            return raw == null ? default : JsonSerializer.Deserialize<T>(raw);
        }

        public static async Task SetAsync<T>(this IDatabase database, string key, T value, TimeSpan? expiry = null)
        {
            var raw = JsonSerializer.Serialize(value);
            await database.StringSetAsync(key, raw, expiry);
        }
    }
}
