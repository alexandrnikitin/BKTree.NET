using BenchmarkDotNet.Running;

namespace BKTree.NET.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SmallDictionaryBenchmarks>();
        }
    }
}
