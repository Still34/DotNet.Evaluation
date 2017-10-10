# Async State Machine Impact
## Details
When the keyword `async` is introduced to a method, the compiler introduces a set of async state machine to the code, adding unnecessary overhead for simple `Task` methods that don't necessarily require an `await`.
## Test Results
``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i7-4790K CPU 4.00GHz (Haswell), ProcessorCount=8
Frequency=3917579 Hz, Resolution=255.2597 ns, Timer=TSC
.NET Core SDK=2.0.0
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT


```
 |                Method |     Mean |     Error |    StdDev | Allocated |
 |---------------------- |---------:|----------:|----------:|----------:|
 |        DoThings_Async | 15.62 ms | 0.0812 ms | 0.0759 ms |     528 B |
 | DoThings_WithoutAsync | 15.61 ms | 0.0883 ms | 0.0826 ms |     312 B |

![Bar Chart](https://i.imgur.com/7gN6hcK.png)

The method marked with `async` modifier performed almost the same as the one without; however, the memory allocation sees a ~70% increment.
## Solution
Simply do not add the `async` modifier if your method matches the following criteria,

1) Does not have multiple `Task` that need to be awaited (i.e. only one).
2) The `Task` returns the same value type as the main method.