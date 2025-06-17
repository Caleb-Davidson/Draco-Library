using System;

namespace Draco.Extensions {
public static class ObjectExtensions {
    /** <summary>Applies a function to an object and returns the result.</summary> */
    public static TR? Apply<T, TR>(this T? obj, Func<T?, TR?> func) where T : notnull {
        return func(obj);
    }
}
}