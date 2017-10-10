using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Running;

namespace StringPerformance
{
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
}