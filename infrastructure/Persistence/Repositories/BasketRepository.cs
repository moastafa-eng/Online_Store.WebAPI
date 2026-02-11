using Domain.Contracts;
using Domain.Entities.Baskets;
using StackExchange.Redis;
using System.Text.Json;
namespace Persistence.Repositories
{
    #region Comment
    //IConnectionMultiplexer => from StackExChange.Redis Library.
    //IConnectionMultiplexer is a connection manager responsible for opening connection with Redis server 
    #endregion
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        #region Comment

        // IConnectionMultiplexer is responsible for managing the connection to the Redis server.
        // It provides access to the database through the GetDatabase() method.

        // IDatabase is returned from IConnectionMultiplexer.
        // It exposes APIs like StringSet, StringGet, KeyDelete, etc.
        // These methods allow us to manipulate application data stored in Redis (in-memory database)
        // such as Add, Update, Delete, and Retrieve operations. 
        #endregion
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket, TimeSpan duration)
        {
            // Convert basket object to JSON string before storing in Redis
            var redisValue = JsonSerializer.Serialize(basket);

            // StringSetAsync returns a boolean indicating whether the operation succeeded
            var flag = await _database.StringSetAsync(basket.Id, redisValue, duration);


            // If storing fails, return null; otherwise, retrieve and return the basket data
            if (!flag) return null;
            return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string Id)
        {
            // Delete Basket from Redis
            return await _database.KeyDeleteAsync(Id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            // Retrieve basket data from Redis using the basket Id
            var redisValue = await _database.StringGetAsync(Id);


            // If no value is found, return null; otherwise deserialize the JSON string into CustomerBasket
            if (redisValue.IsNullOrEmpty) return null;
            var Basket = JsonSerializer .Deserialize<CustomerBasket>(redisValue);

            // If deserialization fails, return null; otherwise return the basket object
            if (Basket is null) return null;
            return Basket;
        }
    }
}
