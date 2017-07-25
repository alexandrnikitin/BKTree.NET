using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BKTree.NET.Sandbox.Utils;

namespace BKTree.NET.Benchmarks
{
    [Config(typeof(Config))]
    [MemoryDiagnoser]
    public class SmallDictionaryBenchmarks
    {
        private readonly BKTree<string> _sut;
        private readonly string[] _queries;

        public SmallDictionaryBenchmarks()
        {
            _sut = new BKTree<string>(new DamerauLevenshteinStringDistanceMeasurer());
            foreach (var line in ResourcesUtils.Get("BKTree.NET.Sandbox.Resources.frequency_dictionary_en_82_765.txt"))
            {
                _sut.Add(new BKTreeNode<string>(line.Split(null)[0]));
            }

            _queries = ResourcesUtils.Get("BKTree.NET.Sandbox.Resources.noisy_query_en_1000.txt").Select(x => x.Split(null)[0]).ToArray();
        }

        [Benchmark(OperationsPerInvoke = 1000)]
        public void Bench()
        {
            foreach (var query in _queries)
            {
                _sut.Matches(query, 2);
            }
        }

        private class Config : ManualConfig
        {
            public Config()
            {
                Add(new Job(EnvMode.RyuJitX64, EnvMode.Clr, RunMode.Dry));
            }
        }
    }
}