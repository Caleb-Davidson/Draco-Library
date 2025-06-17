using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Draco.Model {
/** <summary>
 * A ScriptableObject for tracking a collection of data. Special handling is in place to maintain the collection set when entering/exiting play mode, however
 * any changes to the values of the entries would be maintained when exiting play mode.
 * </summary>
 */
public abstract class ScriptableList<TItem> : ScriptableObject, IList<TItem> {
    // When in the editor and in play mode create a copy of the data and run from that
    // so that it can be changed without affecting the serialized data
    #if UNITY_EDITOR
    [TableList(AlwaysExpanded = true), HideLabel, HideInEditorMode, ShowInInspector]
    private List<TItem>? runtimeData;
    public List<TItem> Data {
        get {
            if (Application.isPlaying) {
                runtimeData ??= new List<TItem>(data);
                return runtimeData;
            } else {
                return data;
            }
        }
    }
    #else
    public List<TItem> Data => data;
    #endif
    
    [SerializeField, TableList(AlwaysExpanded = true), HideLabel, HideInPlayMode]
    private List<TItem> data = new();

    #region IList<TItem> Methods
    public int Count => Data.Count;
    bool ICollection<TItem>.IsReadOnly => ((ICollection<TItem>)Data).IsReadOnly;
    
    public int IndexOf(TItem item) {
        return Data.IndexOf(item);
    }

    public void Insert(int index, TItem item) {
        MarkDirtyInEditor();
        Data.Insert(index, item);
    }

    public void RemoveAt(int index) {
        MarkDirtyInEditor();
        Data.RemoveAt(index);
    }

    public TItem this[int index] {
        get => Data[index];
        set => Data[index] = value;
    }

    public IEnumerator<TItem> GetEnumerator() {
        return Data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return ((IEnumerable)Data).GetEnumerator();
    }

    public virtual void Add(TItem item) {
        MarkDirtyInEditor();
        Data.Add(item);
    }

    public void Clear() {
        MarkDirtyInEditor();
        Data.Clear();
    }

    public bool Contains(TItem item) {
        return Data.Contains(item);
    }

    public void CopyTo(TItem[] array, int arrayIndex) {
        Data.CopyTo(array, arrayIndex);
    }

    public virtual bool Remove(TItem experienceGroup) {
        MarkDirtyInEditor();
        return Data.Remove(experienceGroup);
    }

    [Conditional("UNITY_EDITOR")]
    private void MarkDirtyInEditor() {
        #if UNITY_EDITOR
        if (!Application.isPlaying) {
            UnityEditor.EditorUtility.SetDirty(this);
        }
        #endif
    }
    #endregion
}
}