using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Running;

namespace AsyncStateImpact
{
    internal class Program
    {
        /// <summary>
        ///     Conclusion: Returning an awaited <see cref="Task" /> causes unnecessary overhead.
        ///     Albeit the performance impact is very minimal, the memory allocation is considerably higher.
        /// </summary>
        private static void Main(string[] args) => BenchmarkRunner.Run<AsyncImpact>();
    }

    [MemoryDiagnoser]
    [RPlotExporter]
    public class AsyncImpact
    {
        [Benchmark]
        public async Task DoThings_Async() => await Task.Delay(10);

        [Benchmark]
        public Task DoThings_WithoutAsync() => Task.Delay(10);
    }
}