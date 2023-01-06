namespace Books.Services
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Save<T>(string key, T payload);
        bool Exists(string key);
    }
}
