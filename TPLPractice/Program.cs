using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TPLPractice
{
    public class Program
    {
        /// <summary>
        ///     Conclusion: <see cref="Parallel.ForEach" /> is a very nice way to tackle bunch of data that may require heavy
        ///     CPU-bound processing.
        ///     <para>
        ///         The code used to test this, however, was not exactly ideal. A re-run of this test with improved code would be
        ///         preferred.
        ///     </para>
        /// </summary>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public static Task Main(string[] args) => new Program().StartAsync();

        public async Task StartAsync()
        {
            Console.Clear();
            var nums = new List<int>();
            var result = new List<long>();
            var r = new Random();
            var sw = new Stopwatch();
            var taskQueue = new List<Task<long>>();
            for (int i = 0; i < 20; i++)
                nums.Add(r.Next(10000, 500000));
            Console.WriteLine(string.Join(", ", nums));
            Console.WriteLine(
                "The program will attempt to calculate the prime number for each of the numbers listed above.");
            Console.WriteLine(
                "The purpose of the test is to simulate heavy CPU workload and demonstrate each concurrent APIs.");
            Console.WriteLine("Press any key to start.");
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();

            Console.WriteLine("Prime number calculation (Task.WhenAll)");
            sw.Start();
            taskQueue.AddRange(nums.Select(FindPrimeNumberAsync));
            result.AddRange(await Task.WhenAll(taskQueue).ConfigureAwait(false));
            Console.WriteLine();
            taskQueue.Clear();
            sw.Stop();
            PrintResult(result, sw);
            Console.WriteLine();
            sw.Reset();

            Console.WriteLine("Prime number calculation (Task.WhenAll, with each task wrapped in Task.Run)");
            sw.Start();
            taskQueue.AddRange(nums.Select(num => Task.Run(() => FindPrimeNumberAsync(num))));
            result.AddRange(await Task.WhenAll(taskQueue).ConfigureAwait(false));
            taskQueue.Clear();
            sw.Stop();
            PrintResult(result, sw);
            Console.WriteLine();
            sw.Reset();

            Console.WriteLine("Prime number calculation (Parallel.ForEach)");
            sw.Start();
            result.AddRange(await CrunchNumbersForEachAsync(nums).ConfigureAwait(false));
            sw.Stop();
            PrintResult(result, sw);
            Console.WriteLine();
            sw.Reset();

            Console.WriteLine(
                "Prime number calculation (Parallel.ForEach, MaxDegreeOfParallelism: Environment.ProcessorCount)");
            sw.Start();
            result.AddRange(await CrunchNumbersForEachAsync(nums,
                new ParallelOptions {MaxDegreeOfParallelism = Environment.ProcessorCount}).ConfigureAwait(false));
            sw.Stop();
            PrintResult(result, sw);
            Console.WriteLine();
            sw.Reset();

            Console.WriteLine("Prime number calculation (Parallel.ForEach, MaxDegreeOfParallelism: 4)");
            sw.Start();
            result.AddRange(await CrunchNumbersForEachAsync(nums,
                new ParallelOptions {MaxDegreeOfParallelism = 4}).ConfigureAwait(false));
            sw.Stop();
            PrintResult(result, sw);
            Console.WriteLine();
            sw.Reset();

            Console.ReadKey();
        }

        public List<long> PrintResult(List<long> results, Stopwatch sw)
        {
            Console.WriteLine(string.Join(", ", results));
            Console.WriteLine($"Finished after {sw.Elapsed}.");
            results.Clear();
            return results;
        }

        public Task<List<long>> CrunchNumbersForEachAsync(IEnumerable<int> nums, ParallelOptions options = null)
        {
            options = options ?? new ParallelOptions();
            var concurrentBag = new ConcurrentBag<long>();
            Parallel.ForEach(nums, options,
                async num => { concurrentBag.Add(await FindPrimeNumberAsync(num).ConfigureAwait(false)); });
            return Task.FromResult(concurrentBag.ToList());
        }

        public Task<long> FindPrimeNumberAsync(int n)
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
            return Task.FromResult(--a);
        }
    }
}