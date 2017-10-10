using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Running;

namespace AsyncAwait.Vs.TaskRun
{
    internal class Program
    {
        /// <summary>
        ///     Conclusion: Wrapping <see cref="Task.Run" /> at execution for synchronous method is better than a useless
        ///     <see cref="Task.FromResult" />.
        /// </summary>
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<PrimeNumberTest>();
        }
    }

    [MemoryDiagnoser]
    [RPlotExporter]
    public class PrimeNumberTest
    {
        private static readonly int[] TargetNums =
        {
            1000,
            1111,
            2222,
            3333,
            4444
        };

        [Benchmark]
        public async Task FindPrimeNumber_TaskRunAsync()
        {
            foreach (int targetNum in TargetNums)
                await Task.Run(() => FindPrimeNumber(targetNum)).ConfigureAwait(false);
        }

        [Benchmark]
        public async Task FindPrimeNumber_TaskResultAsync()
        {
            foreach (int targetNum in TargetNums)
                await FindPrimeNumberAsync(targetNum).ConfigureAwait(false);
        }

        public long FindPrimeNumber(int n)
        {
            int count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                int prime = 1;
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                    count++;
                a++;
            }
            return --a;
        }

        /// <summary>
        ///     This doesn't provide any meaningful way of executing the method asynchronously, rather, it is executing the method
        ///     synchronously and returning its result in a <see cref="Task" /> object.
        /// </summary>
        public Task<long> FindPrimeNumberAsync(int n) => Task.FromResult(FindPrimeNumber(n));
    }
}