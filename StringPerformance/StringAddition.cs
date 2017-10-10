using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Running;

namespace StringPerformance
{
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
}