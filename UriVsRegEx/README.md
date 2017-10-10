# Scanning for Links: Uri vs. Regular Expression
## Background
I've been making Discord bots with Discord.NET for a while for my server and a public bot. One of our channels has a requirement that only image attachments or links should go through.

There are several ways to determine whether a string contains a link or not, but I've gone with `RegEx` after several experiementations.

## Methodlogy
The benchmark tests the performance differences between `Uri.TryCreate` and `RegEx.IsMatch`.

Against:
```cs
private static readonly string[] UrlCollection =
{
    "http://example.com/",
    "yes.http://example.com/",
    "no;http://example.com/",
    "helloWorld http://example.com/",
    "ye",
    "google.com/gmail"
};
```

`RegEx.IsMatch`:
```cs
private static readonly Regex UrlRegex =
    new Regex(@"(https?:\/\/)?(www\.)?[-a-z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-z0-9@:%_\+.~#?&//=]*)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

public static IEnumerable<bool> ContainsUrl_RegEx() => UrlCollection.Select(s => UrlRegex.IsMatch(s));
```

`Uri`:
```cs
public static IEnumerable<bool> ContainsUrl_Uri() => UrlCollection
    .Select(s => s.Split(' ', ',', ';').Select(s1 => Uri.TryCreate(s1, UriKind.RelativeOrAbsolute, out _)))
    .Select(result => result.All(x => x));
```

## Test Results
``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-4790K CPU 4.00GHz (Haswell), ProcessorCount=8
Frequency=3917579 Hz, Resolution=255.2597 ns, Timer=TSC
.NET Core SDK=2.0.0
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT


```
 |            Method |      Mean |     Error |    StdDev |  Gen 0 | Allocated |
 |------------------ |----------:|----------:|----------:|-------:|----------:|
 | ContainsUrl_RegEx |  42.37 ns | 0.2922 ns | 0.2440 ns | 0.0114 |      48 B |
 |   ContainsUrl_Uri | 118.55 ns | 1.5465 ns | 1.3709 ns | 0.0455 |     192 B |


![Bar Chart](https://i.imgur.com/nmkHgPA.png)

## Comments / Conclusion

The `Uri` methodlogy seems inferior no matter how it is tested. It first requires the string to be split with delimiters like `,`, `;`...etc. which is highly exploitable.
Then comes the performance and memory allocation differences. 

A `RegEx` with `RegexOptions.Compiled` performs far better than initial expectations and can be configured whether to use filter absolute and relative scheme much like `Uri` if you add/remove the parenthesis around `http`.