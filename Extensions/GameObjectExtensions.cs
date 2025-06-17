using System.Collections.Generic;
using UnityEngine;

namespace Draco.Extensions {
public static class GameObjectExtensions {
    /** <summary>Returns all child GameObjects as enumerable.</summary> */
    public static IEnumerable<GameObject> GetChildren(this GameObject parent) {
        var parentTransform = parent.transform;
        for (var i = 0; i < parentTransform.childCount; i++) {
            yield return parentTransform.GetChild(i).gameObject;
        }
    }

    /** <summary>Destroys all child GameObjects.</summary> */
    public static void DestroyAllChildren(this GameObject parent) {
        var parentTransform = parent.transform;
        for (var i = parentTransform.childCount - 1; i >= 0; i--) {
            Object.Destroy(parentTransform.GetChild(i).gameObject);
        }
    }
}
}