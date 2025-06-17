using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Draco.Editor.Credits {
public class AssetCreditCategory : ScriptableObject, ICreditCategory {
    public string Name => name;
    [field: SerializeField]
    public string Url { get; set; } = "";
    [field: SerializeField, Required]
    public string License { get; set; } = "";
    [field: SerializeField, TextArea(2, 5)]
    public string Notes { get; set; } = "";
    [field: SerializeField, RequiredListLength(1, int.MaxValue), Required]
    public List<string> Authors { get; set; } = new();

    [field: SerializeField]
    public List<Object> Assets { get; set; } = new();

    [field: SerializeField, HideInInspector]
    public List<SubCategory> SubCategories { get; set; } = new();
    
    public IEnumerable<ICreditCategory> GetCategories() => SubCategories;
}

[Serializable]
public class SubCategory : ICreditCategory {
    [field: SerializeField, Required]
    public string Name { get; set; } = "";
    [field: SerializeField, Required]
    public List<Object> Assets { get; set; } = new();
    
    [field: SerializeField]
    public List<AssetCreditCategory> SubCategories { get; set; } = new();
    
    public IEnumerable<ICreditCategory> GetCategories() => SubCategories;
}

public interface ICreditCategory {
    public string Name { get; }
    public IEnumerable<ICreditCategory> GetCategories();
}
}