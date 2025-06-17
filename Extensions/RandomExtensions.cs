using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Draco.Extensions {
public static class RandomExtensions {
    /** <summary>Randomly selects an element from the array.</summary> */
    public static T RandomElement<T>(this T[] array) {
        if (array.Length == 0) throw new IndexOutOfRangeException();
        return array[Random.Range(0, array.Length)];
    }

    /** <summary>Randomly selects an element from the list.</summary> */
    public static T RandomElement<T>(this List<T> list) {
        if (list.Count == 0) throw new IndexOutOfRangeException();
        return list[Random.Range(0, list.Count)];
    }

    /** <summary>Randomly selects an element from the enumerable.</summary> */
    public static T RandomElement<T>(this IEnumerable<T> enumerable) {
        switch (enumerable) {
            case List<T> list:
                return list.RandomElement();
            case T[] array:
                return array.RandomElement();
            default: {
                var enumeration = enumerable.ToList();
                return enumeration.ElementAt(Random.Range(0, enumeration.Count));
            }
        }
    }
    
    /** <summary>Randomly selects an integer from the range excluding the upper bound.</summary> */
    public static int RandomValue(this Vector2Int range) => Random.Range(range.x, range.y);
    
    /** <summary>Randomly selects an integer from the range including the upper bound.</summary> */
    public static int RandomValueInclusive(this Vector2Int range) => Random.Range(range.x, range.y + 1);
    
    /** <summary>Randomly selects a float from the range.</summary> */
    public static float RandomValue(this Vector2 range) => Random.Range(range.x, range.y);
}
}