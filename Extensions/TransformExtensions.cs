using System.Collections.Generic;
using UnityEngine;

namespace Draco.Extensions {
public static class TransformExtensions {
    /** <summary>Resets the transform's local position, rotation, and scale.</summary> */
    public static void ResetLocal(this Transform transform) {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    /** <summary>Resets the transform's local position.</summary> */
    public static void ResetLocalPosition(this Transform transform) {
        transform.localPosition = Vector3.zero;
    }

    /** <summary>Resets the transform's local rotation.</summary> */
    public static void ResetLocalRotation(this Transform transform) {
        transform.localRotation = Quaternion.identity;
    }

    /** <summary>Resets the transform's local scale.</summary> */
    public static void ResetLocalScale(this Transform transform) {
        transform.localScale = Vector3.one;
    }

    /** <summary>Resets the transform's world position and rotation.</summary> */
    public static void ResetWorld(this Transform transform) {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    /** <summary>Resets the transform's world position.</summary> */
    public static void ResetWorldPosition(this Transform transform) {
        transform.position = Vector3.zero;
    }

    /** <summary>Resets the transform's world rotation.</summary> */
    public static void ResetWorldRotation(this Transform transform) {
        transform.rotation = Quaternion.identity;
    }

    /** <summary>Resets the transform's local and world position and rotation as well as local scale.</summary> */
    public static void ResetLocalAndWorld(this Transform transform) {
        ResetLocal(transform);
        ResetWorld(transform);
    }

    /** <summary>Returns all child transforms as enumerable.</summary> */
    public static IEnumerable<Transform> GetChildren(this Transform parent) {
        for (var i = 0; i < parent.childCount; i++) {
            yield return parent.GetChild(i);
        }
    }

    /** <summary>Destroys all child GameObjects.</summary> */
    public static void DestroyAllChildren(this Transform parent) {
        for (var i = parent.childCount - 1; i >= 0; i--) {
            Object.Destroy(parent.GetChild(i).gameObject);
        }
    }
}
}