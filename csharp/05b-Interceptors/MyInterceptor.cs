﻿using System.Runtime.CompilerServices;

namespace Interceptors;
public static class MyInterceptor
{
    [InterceptsLocation(@"C:\githublearn\bastamainz2023\csharp\05b-Interceptors\Runner.cs", 7, 9)]
    public static void InterceptDotheMagic()
    {
        Console.WriteLine("Intercepting DotheMagic");
    }
}
