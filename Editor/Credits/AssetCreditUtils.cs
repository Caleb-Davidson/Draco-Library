using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Draco.Editor.Credits {
public static class AssetCreditUtils {
    public static HashSet<Object> GetAllCreditedAssets() {
        var credited = new HashSet<Object>();

        var general = AssetDatabase.LoadAssetAtPath<AssetCreditGeneral>("Assets/Credits/General.asset");
        if (general != null) {
            foreach (var credit in general.Credits) {
                foreach (var obj in credit.Assets) {
                    if (obj != null)
                        credited.Add(obj);
                }
            }
        }

        var guids = AssetDatabase.FindAssets("t:AssetCreditCategory", new[] { "Assets/Credits" });
        foreach (var guid in guids) {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var creditCategory = AssetDatabase.LoadAssetAtPath<AssetCreditCategory>(path);
            if (creditCategory == null) continue;

            foreach (var asset in creditCategory.Assets)
                if (asset != null)
                    credited.Add(asset);
        }

        return credited;
    }
}
}