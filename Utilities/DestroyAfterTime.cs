using Draco.Attributes;
using UnityEngine;

namespace Draco.Utilities {
/// <summary>
/// Destroys the GameObject this component is attached to after a specified amount of time.
/// This is commonly used for temporary visual effects (VFX) or other transient game objects
/// that should not persist indefinitely in the scene.
/// </summary>
public class DestroyAfterTime : MonoBehaviour {
    [SerializeField, Positive, Tooltip("The amount of time to wait before destroying the object.")]
    private float time;
    
    private void Start() {
        Destroy(gameObject, time);
    }
}
}