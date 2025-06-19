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

    /** <summary>Gets or adds a component to the GameObject.</summary> */
    public static T EnsureComponent<T>(this GameObject gameObject) where T : Component {
        var component = gameObject.GetComponent<T>();
        if (component == null) {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }

    /** <summary>Destroys all child GameObjects.</summary> */
    public static void DestroyAllChildren(this GameObject parent) {
        var parentTransform = parent.transform;
        for (var i = parentTransform.childCount - 1; i >= 0; i--) {
            Object.Destroy(parentTransform.GetChild(i).gameObject);
        }
    }
    
    /** <summary>Returns true if the GameObject is in the specified layer mask.</summary> */
    public static bool InLayerMask(this GameObject gameObject, LayerMask layerMask) {
        return layerMask == (layerMask | (1 << gameObject.layer));
    }
}
}