using StackExchange.Redis;
using System;
using System.Text;
using WeFramework.Core.Configuration;

namespace WeFramework.Core.Caching
{
    public class RedisCacheManager : ICacheManager, IDisposable
    {
        #region Fields

        private readonly string redisConnectionString;

        private volatile ConnectionMultiplexer redisConnection;

        private readonly object redisConnectionLockHelper = new object();

        #endregion

        #region Ctor

        public RedisCacheManager(ApplicationConfig config)
        {
            if (String.IsNullOrEmpty(config.RedisCacheConfig.ConnectionString))
            {
                throw new Exception("Redis connection string is empty");
            }

            this.redisConnectionString = config.RedisCacheConfig.ConnectionString;
            redisConnection = GetRedisConnection();
        }


        #endregion

        #region Methods

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key)
        {
            var value = redisConnection.GetDatabase().StringGet(key);
            if (value.HasValue)
            {
                return Deserialize<T>(value);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public virtual void Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                redisConnection.GetDatabase().StringSet(key, Serialize(value), cacheTime);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public virtual bool Contains(string key)
        {
            return redisConnection.GetDatabase().KeyExists(key);
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
            redisConnection.GetDatabase().KeyDelete(key);
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            foreach (var endPoint in this.GetRedisConnection().GetEndPoints())
            {
                var server = this.GetRedisConnection().GetServer(endPoint);
                foreach (var key in server.Keys())
                {
                    redisConnection.GetDatabase().KeyDelete(key);
                }
            }
        }

        public void Dispose()
        {
            if (redisConnection != null)
            {
                redisConnection.Dispose();
            }
        }

        #endregion

        #region Utilities

        private ConnectionMultiplexer GetRedisConnection()
        {

            if (redisConnection != null && redisConnection.IsConnected)
            {
                return redisConnection;
            }

            lock (redisConnectionLockHelper)
            {
                if (redisConnection != null && redisConnection.IsConnected)
                {
                    return redisConnection;
                }

                if (redisConnection != null)
                {
                    redisConnection.Dispose();
                }

                redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
            }

            return redisConnection;
        }

        protected virtual byte[] Serialize(object item)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        protected virtual T Deserialize<T>(byte[] serializedObject)
        {
            if (serializedObject == null)
            {
                return default(T);
            }

            var jsonString = Encoding.UTF8.GetString(serializedObject);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
        }

        #endregion
    }
}
