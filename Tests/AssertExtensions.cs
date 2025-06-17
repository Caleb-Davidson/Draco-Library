using System.Linq;
using NUnit.Framework;

namespace Draco.Tests {
public static class AssertExtensions {
    public static void IsOneOf<T>(T actual, params T[] expected) {
        if (!expected.Contains(actual)) {
            Assert.Fail($"Expected value to be one of: {string.Join(", ", expected)}, but was: {actual}");
        }
    }
}
}