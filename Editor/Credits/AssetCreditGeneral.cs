using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Draco.Editor.Credits {
public class AssetCreditGeneral : ScriptableObject {
    [field: SerializeField]
    public List<AssetCreditSimple> Credits { get; set; } = new();
}

[Serializable]
public class AssetCreditSimple {
    [field: SerializeField]
    public string Url { get; set; } = "";
    [field: SerializeField, Required]
    public string License { get; set; } = "";
    [field: SerializeField, Required]
    public string Authors { get; set; } = "";

    [field: SerializeField]
    public List<Object> Assets { get; set; } = new();
}
}