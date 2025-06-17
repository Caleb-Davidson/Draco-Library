#if UNITY_EDITOR
using System;
using System.Collections.Generic;

namespace Draco.Utilities {
/** <summary>Utility class for registering and invoking draw gizmo events.</summary> */
public static class DrawGizmoUtil {
    private static readonly Dictionary<Type, Delegate> drawSelectedGizmos = new();
    private static readonly Dictionary<Type, Delegate> drawGizmos = new();
    
    /** <summary>Registers a draw gizmo action for the specified type.</summary> */
    public static void RegisterDrawGizmosSelected<T>(Action<T> drawGizmosAction) {
        var type = typeof(T);
        if (drawSelectedGizmos.TryGetValue(type, out var drawGizmosDelegate)) {
            drawSelectedGizmos[type] = Delegate.Combine(drawGizmosDelegate, drawGizmosAction);
        } else {
            drawSelectedGizmos[type] = drawGizmosAction;
        }
    }
    
    /** <summary>Invokes the draw gizmo action for the specified type and target.</summary> */
    public static void OnDrawGizmosSelected<T>(T target) {
        if (drawSelectedGizmos.TryGetValue(typeof(T), out var drawGizmosDelegate)) {
            drawGizmosDelegate.DynamicInvoke(target);
        }
    }
    
    /** <summary>Registers a draw gizmo action for the specified type.</summary> */
    public static void RegisterDrawGizmos<T>(Action<T> drawGizmosAction) {
        var type = typeof(T);
        if (drawGizmos.TryGetValue(type, out var drawGizmosDelegate)) {
            drawGizmos[type] = Delegate.Combine(drawGizmosDelegate, drawGizmosAction);
        } else {
            drawGizmos[type] = drawGizmosAction;
        }
    }

    /** <summary>Invokes the draw gizmo action for the specified type and target.</summary> */
    public static void OnDrawGizmos<T>(T target) {
        if (drawGizmos.TryGetValue(typeof(T), out var drawGizmosDelegate)) {
            drawGizmosDelegate.DynamicInvoke(target);
        }
    }
}
}
#endif
