using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Running;

namespace AsyncStateImpact
{
    public  class Program
    {
        /// <summary>
        ///     Conclusion: Returning an awaited <see cref="Task" /> causes unnecessary overhead.
        ///     Albeit the performance impact is very minimal, the memory allocation is considerably higher.
        /// </summary>
        public static void Main(string[] args) => new AsyncException().StartAsync().GetAwaiter().GetResult();
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

    public class AsyncException
    {
        public async Task DoThings_Async() => await ThrowAsync();
        public Task DoThings_WithoutAsync() => ThrowAsync();

        private static async Task ThrowAsync()
        {
            await Task.Delay(10).ConfigureAwait(false);
            throw new Exception("Bad.");
        }

        public async Task StartAsync()
        {
            try
            {
                await DoThings_Async().ConfigureAwait(false);
                Console.ReadKey();
                await DoThings_WithoutAsync().ConfigureAwait(false);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}