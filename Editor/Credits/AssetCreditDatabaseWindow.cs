using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Draco.Editor.Credits {
public class AssetCreditDatabaseWindow : OdinMenuEditorWindow {
    private const string CREDIT_FOLDER_PATH = "Assets/Credits";
    private const string GENERAL_CREDITS_ASSET_PATH = CREDIT_FOLDER_PATH + "/General.asset";

    [MenuItem("Tools/Draco/Asset Credit Database")]
    private static void OpenWindow() {
        var window = GetWindow<AssetCreditDatabaseWindow>();
        window.titleContent = new GUIContent("Asset Credits Database");
        window.Show();
    }

    protected override OdinMenuTree BuildMenuTree() {
        var tree = new OdinMenuTree(true);

        if (!Directory.Exists(CREDIT_FOLDER_PATH))
            Directory.CreateDirectory(CREDIT_FOLDER_PATH);

        if (!File.Exists(GENERAL_CREDITS_ASSET_PATH)) {
            var asset = CreateInstance<AssetCreditGeneral>();
            asset.name = "General";
            AssetDatabase.CreateAsset(asset, GENERAL_CREDITS_ASSET_PATH);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();       
        }
        
        var general = AssetDatabase.LoadAssetAtPath<AssetCreditGeneral>(GENERAL_CREDITS_ASSET_PATH);
        tree.Add(general.name, general);

        var guids = AssetDatabase.FindAssets("t:" + nameof(AssetCreditCategory), new[] { CREDIT_FOLDER_PATH });
        foreach (var guid in guids) {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var so = AssetDatabase.LoadAssetAtPath<AssetCreditCategory>(path);
            if (so == null) {
                Debug.LogWarning("Failed to load asset credit category at " + path);
                continue;
            }
            
            RegisterCategoryInViewDeeply(tree, so, "");
        }

        tree.Add("Create New Category", new CreateCategoryView());
        tree.Add("Find Uncredited Assets", new UncreditedAssetScannerView());

        return tree;
    }

    private static void RegisterCategoryInViewDeeply(OdinMenuTree tree, ICreditCategory category, string path) {
        if (!string.IsNullOrEmpty(path)) {
            path += "/";
        }
        path += category.Name;
        tree.Add(path, category);
        
        foreach (var subCategory in category.GetCategories()) {
            RegisterCategoryInViewDeeply(tree, subCategory, path);
        }
        
        tree.Add(path + "/Create New Category", new CreateCategoryView(path));
    }

    protected override void OnBeginDrawEditors() {
        SirenixEditorGUI.Title("Asset Credits", null, TextAlignment.Left, true);
    }

    [Serializable]
    private class CreateCategoryView {
        private string path = "";
        
        [SerializeField, PropertyOrder(-10)]
        private string newCategoryName = "New Credit Category";

        public CreateCategoryView() { }
        public CreateCategoryView(string path) {
            this.path = path;
        }

        [Button("Create")]
        private void Create() {
            var assetDirectory = CREDIT_FOLDER_PATH + (string.IsNullOrWhiteSpace(path) ? "" : $"/{path}");
            if (!Directory.Exists(assetDirectory))
                Directory.CreateDirectory(assetDirectory);
            
            var asset = CreateInstance<AssetCreditCategory>();
            asset.name = newCategoryName;
            var assetPath = $"{assetDirectory}/{asset.name}.asset";

            if (File.Exists(assetPath)) {
                Debug.LogWarning($"Asset already exists at {assetPath}");
                return;           
            }

            AssetDatabase.CreateAsset(asset, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
    
    [Serializable]
    private class UncreditedAssetScannerView {
        [SerializeField, ReadOnly, PropertyOrder(1)]
        private List<Object> uncreditedAssets = new();
        
        [ShowInInspector, EnableGUI, InlineEditor, PropertySpace(20), PropertyOrder(2)]
        private AssetScannerSettings settings => AssetScannerSettings.Instance;

        [Button("Scan For Uncredited Assets"), PropertyOrder(0)]
        private void Scan() {
            var credited = AssetCreditUtils.GetAllCreditedAssets();

            string[] allGuids = AssetScannerSettings.SearchPaths
                .SelectMany(folder => AssetDatabase.FindAssets("", new[] { folder }))
                .Distinct()
                .ToArray();

            uncreditedAssets = allGuids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(path => !AssetDatabase.IsValidFolder(path))
                .Where(IsAssetTypeThatNeedsCredit)
                .Select(AssetDatabase.LoadAssetAtPath<Object>)
                .Where(asset => asset != null && !credited.Contains(asset))
                .ToList();
        }

        private bool IsAssetTypeThatNeedsCredit(string path) {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return !AssetScannerSettings.ExcludedExtensions.Contains(ext);
        }
    }
}
}