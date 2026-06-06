using Draco.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Draco.Utilities {
/// <summary>
/// Applies continuous rotation to the GameObject this component is attached to.
/// This component supports various rotation behaviors, including single-run, looping,
/// and yoyo (alternating direction) rotations, making it suitable for visual effects
/// or dynamic environment elements.
/// </summary>
public class RotationOverTime : MonoBehaviour {
    [SerializeField, Tooltip("The number of rotations to apply to the object per second.")]
    private Vector3 rotation;
    [SerializeField, Tooltip("Whether the rotation should be applied in a loop.")]
    private bool loop;
    [SerializeField, HideIf(nameof(loop)), Tooltip("The amount of time to spend rotating the object in seconds.")]
    private float duration = 1;
    [SerializeField, ShowIf(nameof(loop)), Tooltip("Whether the rotation should alternate between clockwise and counterclockwise.")]
    private bool yoyo;
    [SerializeField, ShowIf(nameof(yoyo)), Tooltip("The amount of time to spend rotating the object in each direction in seconds.")]
    private float yoyoDuration = 1;
    [SerializeField, HideIf(nameof(loop)), Tooltip("Whether the object should be destroyed when the rotation is complete.")]
    private bool destroyOnFinish;

    private float elapsedTime;
    private bool hasRun;
    private bool reverse;

    private void Update() {
        if (!loop) HandleSingleRun();
        else if (yoyo) HandleYoyo();
        else HandleLoop();
    }

    private void HandleSingleRun() {
        if (hasRun) return;
        
        elapsedTime += Time.deltaTime;
        if (elapsedTime > duration) {
            if (destroyOnFinish) gameObject.Destroy();
            hasRun = true;
            return;
        }
        
        transform.Rotate(rotation * (360 * Time.deltaTime));
    }
    
    private void HandleYoyo() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > yoyoDuration) {
            reverse = !reverse;
            elapsedTime = 0f;
        }

        if (reverse) {
            transform.Rotate(rotation * (-360 * Time.deltaTime));
        } else {
            transform.Rotate(rotation * (360 * Time.deltaTime));
        }
    }
    
    private void HandleLoop() {
        transform.Rotate(rotation * (360 * Time.deltaTime));
    }
}
}