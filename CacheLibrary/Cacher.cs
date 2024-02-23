using System.Runtime.Caching;

public class CacheManager
{
    private static MemoryCache _cache = new MemoryCache("UserCache");

    public static void AddOrUpdateCache(string userId, object responseObject)
    {
        // Define a cache item policy with a 1-hour expiration time
        var cacheItemPolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
        };

        // Add or update the cache item with the specified key and policy
        _cache.Set(userId, responseObject, cacheItemPolicy);
    }

    public static void AddOrUpdateCache2(string userId, object responseObject)
    {
        // Define a cache item policy with a 45 seconds expiration time
        var cacheItemPolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(45)
        };

        // Add or update the cache item with the specified key and policy
        _cache.Set(userId, responseObject, cacheItemPolicy);
    }

    public static void AddOrUpdateCache3(string userId, object responseObject)
    {
        // Define a cache item policy with a 10-hour expiration time
        var cacheItemPolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(10)
        };

        // Add or update the cache item with the specified key and policy
        _cache.Set(userId, responseObject, cacheItemPolicy);
    }

    public static object GetFromCache(string userId)
    {
        // Try to retrieve the cached object using the user ID as the key
        return _cache.Get(userId);
    }

    public static void RemoveFromCache(string userId)
    {
        // Remove the cached item for the specified user ID
        _cache.Remove(userId);
    }
}