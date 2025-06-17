using System;
using System.Collections.Generic;
using System.Linq;
using Draco.Extensions;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Draco.Editor {
public class AssetTracker : AssetPostprocessor {
    public static event Action? OnAssetsProcessed;
    public static event Action<string[]>? OnAssetsImported;
    public static event Action<string[]>? OnAssetsDeleted;
    public static event Action<string[], string[]>? OnAssetsMoved;
    private static readonly List<ITracker> trackers = new();

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
        AssetsImported(importedAssets);
        OnAssetsImported?.Invoke(importedAssets);
        
        AssetsDeleted(deletedAssets);
        OnAssetsDeleted?.Invoke(deletedAssets);
        
        AssetsMoved(movedAssets, movedFromAssetPaths);
        OnAssetsMoved?.Invoke(movedAssets, movedFromAssetPaths);
        
        OnAssetsProcessed?.Invoke();
    }

    private static void AssetsImported(string[] paths) {
        if (trackers.Empty()) return;

        var (validPaths, assets) = paths
            .Select(path => (path, asset: AssetDatabase.LoadAssetAtPath<Object>(path)))
            .Where(tuple => tuple.asset != null)
            .UnzipToArray();

        foreach (var tracker in trackers) {
            tracker.AssetsImported(validPaths, assets);
        }
    }
    
    private static void AssetsDeleted(string[] paths) {
        if (trackers.Empty()) return;
        foreach (var tracker in trackers) {
            tracker.AssetsDeleted(paths);
        }
    }
    
    private static void AssetsMoved(string[] moveToPaths, string[] moveFromPaths) {
        if (trackers.Empty()) return;
        AssetsImported(moveToPaths);
        AssetsDeleted(moveFromPaths);
    }

    private interface ITracker {
        public void AssetsImported(string[] paths, Object[] assets);
        public void AssetsDeleted(string[] paths);
    }
    
    public class Tracker<T> : ITracker where T : Object {
        private static readonly Dictionary<string, T> assets = new();
        public static IReadOnlyDictionary<string, T> Assets => assets;
        public static event Action<string[], T[]>? OnAssetsImported;
        public static event Action<string[]>? OnAssetsDeleted;

        public static void Start() {
            if (trackers.Any(tracker => tracker is Tracker<T>)) return;
            trackers.Add(new Tracker<T>());

            AssetDatabase.FindAssets($"t:{typeof(T).Name}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .ForEach(path => { assets[path] = AssetDatabase.LoadAssetAtPath<T>(path); });
        }

        public void AssetsImported(string[] paths, Object[] assets) {
            var (validPaths, validAssets) = paths.Zip(assets)
                .Select(tuple => (tuple.Item1, tuple.Item2 as T))
                .Where(tuple => tuple.Item2 != null)
                .Process(tuple => Tracker<T>.assets[tuple.Item1] = tuple.Item2!)
                .UnzipToArray();
            
            OnAssetsImported?.Invoke(validPaths, validAssets!);
        }

        public void AssetsDeleted(string[] paths) {
            var validPaths = paths.Where(path => assets.Remove(path)).ToArray();
            if (validPaths.Empty()) return;
            OnAssetsDeleted?.Invoke(validPaths);
        }
    } 
}
}