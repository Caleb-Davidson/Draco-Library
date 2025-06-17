using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Draco.Editor {
public static class HiddenGameObjectTools {
    private static readonly List<GameObject> hiddenGameObjects = new();
    
    [MenuItem("Tools/Draco/Hidden GameObjects/Reveal")]
    private static void RevealHiddenGameObjects()
    {
        var scene = SceneManager.GetActiveScene();
        foreach(var gameObject in scene.GetRootGameObjects())
        {
            RevealHiddenGameObject(gameObject);
        }
    }

    [MenuItem("Tools/Draco/Hidden GameObjects/Reveal", true)]
    private static bool ValidateRevealHiddenGameObjects() {
        return hiddenGameObjects.Count == 0;
    }
 
    private static void RevealHiddenGameObject(GameObject gameObject)
    {
        if(gameObject.hideFlags.HasFlag(HideFlags.HideInHierarchy))
        {
            Debug.Log($"Revealing hidden GameObject {gameObject.name}", gameObject);
            hiddenGameObjects.Add(gameObject);
            gameObject.hideFlags &= ~HideFlags.HideInHierarchy;
        }
 
        foreach(Transform child in gameObject.transform)
        {
            RevealHiddenGameObject(child.gameObject);
        }
    }
    
    [MenuItem("Tools/Draco/Hidden GameObjects/Hide")]
    private static void HideHiddenGameObjects()
    {
        foreach (var gameObject in hiddenGameObjects.Where(gameObject => gameObject != null)) {
            gameObject.hideFlags &= HideFlags.HideInHierarchy;
        }
        
        hiddenGameObjects.Clear();
    }

    [MenuItem("Tools/Draco/Hidden GameObjects/Hide", true)]
    private static bool ValidateHideHiddenGameObjects() {
        return hiddenGameObjects.Count != 0;
    }
}
}