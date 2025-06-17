using UnityEngine;

namespace Draco.Utilities {
/** <summary>Keeps the GameObject from being destroyed when loading a new scene.</summary> */
public class DoNotDestroyOnLoad : MonoBehaviour {
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
}