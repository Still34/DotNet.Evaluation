using System;

namespace GenericsSwitchCase
{
    public class Program
    {
        /// <summary>
        ///     <para>
        ///         Methodology: See <see cref="DoSomething_UsingPatternMatching{T}" />.
        ///     </para>
        ///     <para>
        ///         Conclusion: N/A
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     Pattern matching for generics is not implemented until C# 7.1, therefore, the code will not compile in prior
        ///     versions.
        /// </remarks>
        private static void Main(string[] args) => new Program().Start();

        public void Start()
        {
            DoSomething_UsingPatternMatching(1);
            DoSomething_UsingPatternMatching("something");
            DoSomething_UsingPatternMatching(DateTime.Now);
            Console.ReadKey();
        }

        public void DoSomething_UsingPatternMatching<T>(T param)
        {
            switch (param)
            {
                case int _:
                    Console.WriteLine("Is Int");
                    break;
                case string _:
                    Console.WriteLine("Is string");
                    break;
                default:
                    Console.WriteLine("Is other item");
                    break;
            }
        }
    }
}