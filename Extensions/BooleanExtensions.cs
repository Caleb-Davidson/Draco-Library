using System;
using System.Runtime.CompilerServices;

namespace Draco.Extensions {
public static class BooleanExtensions {
    /** <summary>Executes action if condition is true.</summary> */
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IfTrue(this bool condition, Action action) {
        if (condition) {
            action();
        }
        return condition;
    }
    
    /** <summary>Executes action if condition is false.</summary> */
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IfFalse(this bool condition, Action action) {
        if (!condition) {
            action();
        }
        return condition;
    }
}
}