using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Draco.Model {
/// <summary> 
/// Attribute to specify the path where a <see cref="ScriptableSingleton"/> should be saved.
/// PathType can be either Resources or Assets. Assets will be saved in the Assets folder and Resources will be saved in the Assets/Resources folder.
/// A PathType of Assets will not work in a build and should be used only in the editor.
/// </summary>
public class ScriptableSingletonPathAttribute : Attribute {
    public enum PathType {
        Resources, Assets
    }
    
    public string Path { get; }
    public PathType Type { get; }

    public ScriptableSingletonPathAttribute(string path, PathType type) {
        Path = path;
        Type = type;
    }
}

/// <summary> 
/// A ScriptableObject that is guaranteed to exist when accessed. 
/// Will create a new instance if none exists and save it to a file if a path is specified via <see cref="ScriptableSingletonPathAttribute"/>.
/// </summary>
public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T> {
    private static T? instance;
    public static T Instance {
        get {
            if (instance == null) EnsureInstanceExists();
            return instance!;
        }
    }

    private static void EnsureInstanceExists() {
        if (instance != null) return;
        var pathAttribute = typeof(T).GetCustomAttribute<ScriptableSingletonPathAttribute>();
        if (pathAttribute == null) {
            instance = CreateInstance<T>();
            return;
        }
        
        if (pathAttribute.Type == ScriptableSingletonPathAttribute.PathType.Resources) {
            instance = Resources.Load<T>(pathAttribute.Path);
            if (instance != null) return;
        }

        #if UNITY_EDITOR
        if (pathAttribute.Type == ScriptableSingletonPathAttribute.PathType.Assets) {
            var path = Path.Join("Assets/", pathAttribute.Path).Replace("\\", "/") + ".asset";
            instance = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            if (instance != null) return;
        }
        #endif

        instance = CreateInstance<T>();
        
        #if UNITY_EDITOR
        var assetPath = pathAttribute.Type switch {
            ScriptableSingletonPathAttribute.PathType.Resources => "Asset/Resources/" + pathAttribute.Path + ".asset",
            ScriptableSingletonPathAttribute.PathType.Assets => "Assets/" + pathAttribute.Path + ".asset",
            _ => throw new ArgumentOutOfRangeException()
        };
        if (!Directory.Exists(Path.GetDirectoryName(assetPath))) {
            Directory.CreateDirectory(Path.GetDirectoryName(assetPath)!);
        }
        UnityEditor.AssetDatabase.CreateAsset(instance, assetPath);
        #endif
    }

    private void Awake() {
        #if UNITY_EDITOR
        if (Application.isPlaying) return;
        if (instance == null || instance == this) return;
        Debug.LogError($"A second instance of {typeof(T)} was created! Destroying it.");
        DestroyImmediate(this);
        #endif
    }
}
}