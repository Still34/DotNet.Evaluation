using System;
using System.Threading.Tasks;

namespace AsyncMain
{
    internal class Program
    {
        /// <summary>
        ///     Conclusion: Async Main debugging is broken.
        ///     GetAwaiter().GetResult() will show the exception properly in Visual Studio, while Async Main will simply show that
        ///     the application is in break mode.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        //static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult();
        private static Task Main(string[] args) => new Program().StartAsync();
        //private static Task Main(string[] args) => ThrowAsync();
        //private static Task Main(string[] args)
        //{
        //    ThrowAsync();
        //    return Task.CompletedTask;
        //}

        public Task StartAsync() => ThrowAsync();

        public static Task ThrowAsync() =>
            throw new InvalidOperationException("boi");
    }
}