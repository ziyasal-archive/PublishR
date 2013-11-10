using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BookSleeve;
using NUnit.Framework;
using Ploeh.AutoFixture;
using PublishR.Redis;
using PublishR.Test.Entities;

namespace PublishR.Test.RedisClients
{
    [TestFixture]
    public class BookSleeeveTest
    {
        const string Localhost = "127.0.0.1";
        const int Million = 1000000;

        [Test]
        private static void Add_1M_Item_Test()
        {
            Fixture fixture = new Fixture();
            RedisConnection connection = RedisConnectionGateway.Current.GetConnection(Localhost);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < Million; i++)
            {
                Price p = fixture.Create<Price>();
                connection.Sets.Add(1, "urn:prc_z", p.ToBArray());
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            GetItemFrom();
            Console.WriteLine("Added 1m item");
        }

        private static void GetItemFrom()
        {
            RedisConnection connection = RedisConnectionGateway.Current.GetConnection(Localhost);

            Task<byte[][]> task = connection.Sets.GetAll(1, "urn:prc_z");
            foreach (byte[] bytes in task.Result)
            {
                Price typedEntity = bytes.ToTypedEntity<Price>();
                Console.WriteLine(typedEntity.Ticker + " Price:" + typedEntity.Value);
            }
        }
    }
}
