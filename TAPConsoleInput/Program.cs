using System;
using System.Threading.Tasks;

namespace TAPConsoleInput
{
    internal class Program
    {
        /// <summary>
        ///     Conclusion:
        ///     It is possible to take console input while the main thread is executing other tasks;
        ///     however, whether this is justifiable is not yet known.
        /// </summary>
        private static Task Main(string[] args) => new Program().StartAsync();

        public Task StartAsync()
        {
            _ = Task.Run(AcceptInputAsync);
            return DoSomethingAsync();
        }

        private static Task AcceptInputAsync()
        {
            while (true)
            {
                var input = Console.ReadKey();
                Console.WriteLine(input.Key);
            }
        }

        private static async Task DoSomethingAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
            Console.WriteLine("Finished.");
        }
    }
}