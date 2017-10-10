# Generics Switch Case
## Details
In C# 7.1, generics support for pattern matching with `switch` has been implemented; this makes the following code possible.
```cs
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
```

## Notes
Do keep in mind that this is a C# 7.1 specific feature, meaning that it will not compile with prior version of C# compiler.