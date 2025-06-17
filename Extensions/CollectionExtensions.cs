using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Draco.Extensions {
public static class CollectionExtensions {
    /** <summary>Executes action for each element in the collection.</summary> */
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
        foreach (var item in enumerable) action(item);
    }
    
    /** <summary>Executes action for each element in the collection with index.</summary> */
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action) {
        var index = 0;
        foreach (var item in enumerable) {
            action(item, index);
            index++;
        }
    }
    
    /** <summary>Filters out null elements from the collection.</summary> */
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> enumerable) => enumerable.Where(item => item != null).Cast<T>();

    /** <summary>Zips two collections into a sequence of tuples.</summary> */
    public static IEnumerable<ValueTuple<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second) {
        return first.Zip(second, (arg1, arg2) => new ValueTuple<T1, T2>(arg1, arg2));
    }

    /** <summary>Applies an action to each element and returns the original collection.</summary> */
    public static IEnumerable<T> Process<T>(this IEnumerable<T> enumerable, Action<T> action) {
        return enumerable.Select(value => {
            action(value);
            return value;
        });
    }
    
    /** <summary>Returns true if the collection has no elements.</summary> */
    public static bool Empty<T>(this IEnumerable<T> enumerable) => !enumerable.Any();

    /** <summary>Calculates the product of all elements in the collection.</summary> */
    public static float Product(this IEnumerable<float> enumerable) => enumerable.Aggregate(1f, (accumulator, value) => accumulator * value);
    
    /** <summary>Calculates the product of all elements in the collection.</summary> */
    public static int Product(this IEnumerable<int> enumerable) => enumerable.Aggregate(1, (accumulator, value) => accumulator * value);

    /** <summary>Unzips a sequence into two collections.</summary> */
    public static ValueTuple<TTarget1, TTarget2> Unzip<TSource, TTarget1, TTarget2>(this IEnumerable<TSource> enumerable,
                                                                                                      Action<TTarget1, TTarget2, TSource> unzipper) where TTarget1 : new() where TTarget2 : new() {
        return enumerable.Aggregate((new TTarget1(), new TTarget2()), (combiner, tuple) => {
            unzipper(combiner.Item1, combiner.Item2, tuple);
            return combiner;
        });
    }

    private static void Unzipper<T1, T2>(List<T1> list1, List<T2> list2, ValueTuple<T1, T2> tuple) {
        list1.Add(tuple.Item1);
        list2.Add(tuple.Item2);
    }
    
    /** <summary>Unzips a sequence into two collections.</summary> */
    public static ValueTuple<List<T1>, List<T2>> Unzip<T1, T2>(this IEnumerable<ValueTuple<T1, T2>> enumerable, Action<List<T1>, List<T2>, ValueTuple<T1, T2>> unzipper) {
        return Unzip<ValueTuple<T1, T2>, List<T1>, List<T2>>(enumerable, unzipper);
    }
    
    /** <summary>Unzips a sequence into two collections.</summary> */
    public static ValueTuple<List<T1>, List<T2>> Unzip<T1, T2>(this IEnumerable<ValueTuple<T1, T2>> enumerable) {
        return Unzip<ValueTuple<T1, T2>, List<T1>, List<T2>>(enumerable, Unzipper);
    }
    
    /** <summary>Unzips a sequence into two arrays.</summary> */
    public static ValueTuple<T1[], T2[]> UnzipToArray<T1, T2>(this IEnumerable<ValueTuple<T1, T2>> enumerable, Action<List<T1>, List<T2>, ValueTuple<T1, T2>> unzipper) {
        return Unzip<ValueTuple<T1, T2>, List<T1>, List<T2>>(enumerable, unzipper).Apply(tuple => (tuple.Item1.ToArray(), tuple.Item2.ToArray()));
    }
    
    /** <summary>Unzips a sequence into two arrays.</summary> */
    public static ValueTuple<T1[], T2[]> UnzipToArray<T1, T2>(this IEnumerable<ValueTuple<T1, T2>> enumerable) {
        return Unzip<ValueTuple<T1, T2>, List<T1>, List<T2>>(enumerable, Unzipper).Apply(tuple => (tuple.Item1.ToArray(), tuple.Item2.ToArray()));
    }
    
    /** <summary>Deconstructs an enumerable into its first element.</summary> */
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static void Deconstruct<T>(this IEnumerable<T> enumerable, out T first) {
        if (enumerable.Empty()) throw new IndexOutOfRangeException("Cannot deconstruct enumerable, it has less than 1 items!");
        first = enumerable.First();
    }

    /** <summary>Deconstructs an enumerable into its first two elements.</summary> */
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static void Deconstruct<T>(this IEnumerable<T> enumerable, out T first, out T second) {
        if (enumerable.Count() < 2) throw new IndexOutOfRangeException("Cannot deconstruct enumerable, it has less than 2 items!");
        first = enumerable.ElementAt(0);
        second = enumerable.ElementAt(1);
    }
    
    /** <summary>Deconstructs an enumerable into its first three elements.</summary> */
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static void Deconstruct<T>(this IEnumerable<T> enumerable, out T first, out T second, out T third) {
        if (enumerable.Count() < 3) throw new IndexOutOfRangeException("Cannot deconstruct enumerable, it has less than 3 items!");
        first = enumerable.ElementAt(0);
        second = enumerable.ElementAt(1);
        third = enumerable.ElementAt(2);
    }
    
    /** <summary>Deconstructs an enumerable into its first four elements.</summary> */
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static void Deconstruct<T>(this IEnumerable<T> enumerable, out T first, out T second, out T third, out T fourth) {
        if (enumerable.Count() < 4) throw new IndexOutOfRangeException("Cannot deconstruct enumerable, it has less than 4 items!");
        first = enumerable.ElementAt(0);
        second = enumerable.ElementAt(1);
        third = enumerable.ElementAt(2);
        fourth = enumerable.ElementAt(3);
    }
    
    /** <summary>Waits for all tasks in the enumerable to complete.</summary> */
    public static Task WhenAll(this IEnumerable<Task> enumerable) => Task.WhenAll(enumerable);
    
    /** <summary>Converts a sequence of tuples into a dictionary.</summary> */
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<ValueTuple<TKey, TValue>> enumerable) {
        return enumerable.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
    }
    
    /** <summary>Converts a sequence of tuples into a dictionary with reversed keys and values.</summary> */
    public static Dictionary<TValue, TKey> ToReverseDictionary<TKey, TValue>(this IEnumerable<ValueTuple<TKey, TValue>> enumerable) {
        return enumerable.ToDictionary(tuple => tuple.Item2, tuple => tuple.Item1);
    }
}
}