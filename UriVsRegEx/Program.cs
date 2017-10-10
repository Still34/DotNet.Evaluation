using BenchmarkDotNet.Running;

namespace UriVsRegEx
{
    internal class Program
    {
        private static void Main(string[] args) => BenchmarkRunner.Run<UriVsRegex>();
    }
}