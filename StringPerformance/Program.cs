using BenchmarkDotNet.Running;

namespace StringPerformance
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<StringAddition>();
            BenchmarkRunner.Run<StringCompare>();
            BenchmarkRunner.Run<StringContain>();
        }
    }
}