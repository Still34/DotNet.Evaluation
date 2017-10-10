using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Running;

namespace UriVsRegEx
{
    [MemoryDiagnoser]
    [RPlotExporter]
    public class UriVsRegex
    {
        private static readonly string[] UrlCollection =
        {
            "http://example.com/",
            "yes.http://example.com/",
            "no;http://example.com/",
            "helloWorld http://example.com/",
            "ye",
            "google.com/gmail"
        };

        private static readonly Regex UrlRegex =
            new Regex(@"(https?:\/\/)?(www\.)?[-a-z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-z0-9@:%_\+.~#?&//=]*)",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        [Benchmark]
        public static IEnumerable<bool> ContainsUrl_RegEx() => UrlCollection.Select(s => UrlRegex.IsMatch(s));

        [Benchmark]
        public static IEnumerable<bool> ContainsUrl_Uri() => UrlCollection
            .Select(s => s.Split(' ', ',', ';').Select(s1 => Uri.TryCreate(s1, UriKind.RelativeOrAbsolute, out _)))
            .Select(result => result.All(x => x));
    }
}