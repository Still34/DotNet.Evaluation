using System;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
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

    [MemoryDiagnoser]
    [RPlotExporter]
    public class StringAddition
    {
        private const int LoopCount = 100;
        private const string Word = "Hi";

        [Benchmark]
        public string AddStringByConcat()
        {
            string testString = string.Empty;
            for (int i = 0; i < LoopCount; i++)
                testString += Word;
            return testString;
        }

        [Benchmark]
        public string AddStringByStringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < LoopCount; i++)
                sb.Append(Word);
            return sb.ToString();
        }
    }

    [MemoryDiagnoser]
    [RPlotExporter]
    public class StringCompare
    {
        private const string StringA = "helloWorld";
        private const string StringB = "HeLLoWorlD";

        [Benchmark]
        public bool CompareStringByToLower() => StringA.ToLower() == StringB.ToLower();

        [Benchmark]
        public bool CompareStringByStringEquals() => StringA.Equals(StringB, StringComparison.OrdinalIgnoreCase);

        [Benchmark]
        public bool CompareStringByIndexOf() => StringA.IndexOf(StringB, StringComparison.OrdinalIgnoreCase) > -1;
    }

    [MemoryDiagnoser]
    [RPlotExporter]
    public class StringContain
    {
        private const string OriginalString = "Hello World! I have a cat!";
        private const string TargetString = "wOrLd!";

        [Benchmark]
        public bool ContainsStringByIndexOf() =>
            OriginalString.IndexOf(TargetString, StringComparison.OrdinalIgnoreCase) > -1;

        [Benchmark]
        public bool ContainsStringByToLower() => OriginalString.ToLower().Contains(TargetString.ToLower());
    }
}