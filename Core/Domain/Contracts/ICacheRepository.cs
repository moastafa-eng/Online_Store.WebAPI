namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string Key, object value, TimeSpan duration);
    }
}
