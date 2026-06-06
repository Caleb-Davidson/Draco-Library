using UnityEngine;

namespace Draco.Extensions {
public static class UnityObjectExtensions {
    /** <summary>Destroys the Object.</summary> */
    public static void Destroy(this Object gameObject) {
        #if UNITY_EDITOR
        if (!Application.isPlaying) {
            Object.DestroyImmediate(gameObject);
            return;
        }
        #endif
        Object.Destroy(gameObject);
    }
    
    /** <summary>Moves the Object to the Don't Destroy On Load Scene</summary> */
    public static void DontDestroyOnLoad(this Object gameObject) {
        #if UNITY_EDITOR
        if (!Application.isPlaying) {
            return;
        }
        #endif
        Object.DontDestroyOnLoad(gameObject);
    }
}
}