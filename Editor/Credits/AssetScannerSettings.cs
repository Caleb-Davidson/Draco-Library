using System.Collections.Generic;
using System.Linq;
using Draco.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Draco.Editor.Credits {
[ScriptableSingletonPath("Credits/Asset Scanner Settings", ScriptableSingletonPathAttribute.PathType.Assets)]
public class AssetScannerSettings : ScriptableSingleton<AssetScannerSettings> {
    [SerializeField]
    private List<string> searchPaths = new();
    
    [SerializeField, OnCollectionChanged(After = nameof(AfterExcludedExtensionsChanged))]
    private List<string> excludedExtensions = new() {
        ".asset", ".meta", ".prefab", "spriteatlas", ".spriteatlasv2", ".unity", ".cs", ".rsp", ".asmdef", ".inputactions"
    };
    
    private void AfterExcludedExtensionsChanged() => excludedExtensionsSet = null;

    public static IEnumerable<string> SearchPaths => Instance.searchPaths;
    private static HashSet<string>? excludedExtensionsSet;
    public static HashSet<string> ExcludedExtensions => excludedExtensionsSet ??= Instance.excludedExtensions.ToHashSet();
}
}