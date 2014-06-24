using System;
using System.Net.Sockets;
using BookSleeve;

namespace PublishR.Redis
{
    public sealed class RedisConnectionGateway
    {
        private const string REDIS_CONNECTION_FAILED = "Redis connection failed.";
        public static string RedisIp = "127.0.0.1";/*ConfigurationManager.AppSettings["publishr_Redis_adress"];*/
        private RedisConnection _connection;
        private static volatile RedisConnectionGateway _instance;

        private static readonly object SyncLock = new object();
        private static readonly object SyncConnectionLock = new object();

        public static RedisConnectionGateway Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RedisConnectionGateway();
                        }
                    }
                }

                return _instance;
            }
        }

        private RedisConnectionGateway()
        {
            _connection = GetNewConnection(RedisIp);
        }

        private static RedisConnection GetNewConnection(string ip)
        {
            RedisIp = ip;
            return new RedisConnection(ip, syncTimeout: 5000, ioTimeout: 5000);
        }

        public RedisConnection GetConnection(string ip)
        {
            lock (SyncConnectionLock)
            {
                if (_connection == null)
                    _connection = GetNewConnection(ip);

                if (_connection.State == RedisConnectionBase.ConnectionState.Opening)
                    return _connection;

                if (_connection.State == RedisConnectionBase.ConnectionState.Closing || _connection.State == RedisConnectionBase.ConnectionState.Closed)
                {
                    try
                    {
                        _connection = GetNewConnection(ip);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(REDIS_CONNECTION_FAILED, ex);
                    }
                }

                if (_connection.State == RedisConnectionBase.ConnectionState.Shiny)
                {
                    try
                    {
                        var openAsync = _connection.Open();
                        _connection.Wait(openAsync);
                    }
                    catch (SocketException ex)
                    {
                        throw new Exception(REDIS_CONNECTION_FAILED, ex);
                    }
                }

                return _connection;
            }
        }
    }
}