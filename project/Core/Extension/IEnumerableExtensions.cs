﻿using System.Linq;

namespace System.Collections.Generic;

public static class IEnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    {
        return source == null || !source.Any();
    }
}