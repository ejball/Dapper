using System.ComponentModel;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Faithlife.Data;

namespace Dapper.Tests.Performance
{
    [Description("FaithlifeData")]
    public class FaithlifeDataBenchmarks : BenchmarkBase
    {
        private DbConnector _connector;

        [GlobalSetup]
        public void Setup()
        {
            BaseSetup();
            _connector = DbConnector.Create(_connection, new DbConnectorSettings { NoDispose = true });
        }

        [Benchmark(Description = "Query<T> (buffered)")]
        public Post QueryBuffered()
        {
            Step();
            return _connector.Command("select * from Posts where Id = @Id", ("Id", i)).Query<Post>().First();
        }

        [Benchmark(Description = "Query<dynamic> (buffered)")]
        public dynamic QueryBufferedDynamic()
        {
            Step();
            return _connector.Command("select * from Posts where Id = @Id", ("Id", i)).Query<dynamic>().First();
        }

        [Benchmark(Description = "Query<T> (unbuffered)")]
        public Post QueryUnbuffered()
        {
            Step();
            return _connector.Command("select * from Posts where Id = @Id", ("Id", i)).Enumerate<Post>().First();
        }

        [Benchmark(Description = "Query<dynamic> (unbuffered)")]
        public dynamic QueryUnbufferedDynamic()
        {
            Step();
            return _connector.Command("select * from Posts where Id = @Id", ("Id", i)).Enumerate<dynamic>().First();
        }

        [Benchmark(Description = "QueryFirstOrDefault<T>")]
        public Post QueryFirstOrDefault()
        {
            Step();
            return _connector.Command("select * from Posts where Id = @Id", ("Id", i)).QueryFirstOrDefault<Post>();
        }

        [Benchmark(Description = "QueryFirstOrDefault<dynamic>")]
        public dynamic QueryFirstOrDefaultDynamic()
        {
            Step();
            return _connector.Command("select * from Posts where Id = @Id", ("Id", i)).QueryFirstOrDefault<dynamic>();
        }
    }
}
