using System.Threading.Tasks;
using BookSleeve;
using PublishR.MessageStore;

namespace PublishR.Redis
{
    public class RedisMessageStore : IMessageStore
    {
        private readonly string _connectionString;

        public RedisMessageStore(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Store<T>(string key, T message)
        {
            RedisConnection redisConnection = RedisConnectionGateway.Current.GetConnection(_connectionString);
            redisConnection.Sets.Add(2, key, message.ToBArray());
        }

        public T Get<T>(string key)
        {
            RedisConnection redisConnection = RedisConnectionGateway.Current.GetConnection(_connectionString);
            Task<byte[]> task = redisConnection.Strings.Get(2, key);
            return task.Result.ToTypedEntity<T>();
        }
    }
}
