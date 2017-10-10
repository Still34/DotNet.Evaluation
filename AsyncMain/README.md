# Async Main Exception Handling in Visual Studio
## Details
When C#7.1 introduced the ability to make the Main method a `Task` and asynchronous, Visual Studio debugger didn't adjust to this change properly.

The following code will cause Visual Studio to enter break mode rather than the usual exception handling screen,
```cs
private static Task Main(string[] args) => 
	new Program().StartAsync();

// OR

private static Task Main(string[] args) => ThrowAsync();

public Task StartAsync() => ThrowAsync();

public static Task ThrowAsync() => 
	throw new InvalidOperationException("boi");
```

This makes asynchronous `Main` rather hard to diagnose.

This has not been fixed as of Visual Studio 15.3.3.