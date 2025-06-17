using System;
using System.Collections.Generic;
using System.Linq;
using Draco.Extensions;
using UnityEditor;

namespace Draco.Editor {
public abstract class AssetProcessorPlugin {
    public abstract bool AppliesToPath(string path);
    public virtual void OnAssetImported(string path) { }
    public virtual void OnAssetDeleted(string path) { }
    public virtual void OnAssetMoved(string fromPath, string toPath) { }
    public virtual void OnAssetProcessed(string path) { }
}

public sealed class DracoAssetPostProcessor : AssetPostprocessor {
    private static readonly List<AssetProcessorPlugin> plugins;
    public static event Action? OnPostProcessAllAssets;
    
    static DracoAssetPostProcessor() {
        plugins = TypeCache.GetTypesDerivedFrom<AssetProcessorPlugin>()
            .Where(type => !type.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<AssetProcessorPlugin>()
            .ToList();
    }
    
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
        foreach (var path in importedAssets) {
            foreach (var plugin in plugins) {
                if (plugin.AppliesToPath(path)) {
                    plugin.OnAssetImported(path);
                    plugin.OnAssetProcessed(path);
                }
            }
        }
        
        foreach (var path in deletedAssets) {
            foreach (var plugin in plugins) {
                if (plugin.AppliesToPath(path)) {
                    plugin.OnAssetDeleted(path);
                    plugin.OnAssetProcessed(path);
                }
            }
        }

        foreach (var (path, fromPath) in movedAssets.Zip(movedFromAssetPaths)) {
            foreach (var plugin in plugins) {
                if (plugin.AppliesToPath(path) || plugin.AppliesToPath(fromPath)) {
                    plugin.OnAssetMoved(fromPath, path);
                    plugin.OnAssetProcessed(path);
                }
            }
        }
        
        OnPostProcessAllAssets?.Invoke();
    }
}
}
