using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Running;

namespace StringPerformance
{
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