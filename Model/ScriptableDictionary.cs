using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Draco.Model {
    /// <summary>
    /// A ScriptableObject for tracking a dictionary of data. Special handling is in place to maintain the collection set when entering/exiting play mode, however
    /// any changes to the values of the entries would be maintained when exiting play mode.
    /// </summary>
    public abstract class ScriptableDictionary<TKey, TValue> : SerializedScriptableObject, IDictionary<TKey, TValue> {
        // When in the editor and in play mode create a copy of the data and run from that
        // so that it can be changed without affecting the serialized data
#if UNITY_EDITOR
        [ShowInInspector, HideLabel, HideInEditorMode, OnCollectionChanged("OnCollectionChanged")]
        private Dictionary<TKey, TValue>? runtimeData;
        public Dictionary<TKey, TValue> Data {
            get {
                if (Application.isPlaying) {
                    runtimeData ??= new Dictionary<TKey, TValue>(data);
                    return runtimeData;
                } else {
                    return data;
                }
            }
        }
#else
        public Dictionary<TKey, TValue> Data => data;
#endif

        [SerializeField, HideLabel, HideInPlayMode, OnCollectionChanged("OnCollectionChanged")]
        private Dictionary<TKey, TValue> data = new();

        #region IDictionary<TKey, TValue> Methods
        public int Count => Data.Count;
        public bool IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>)Data).IsReadOnly;
        public ICollection<TKey> Keys => Data.Keys;
        public ICollection<TValue> Values => Data.Values;

        public TValue this[TKey key] {
            get => Data[key];
            set {
                MarkDirtyInEditor();
                Data[key] = value;
            }
        }

        public void Add(TKey key, TValue value) {
            MarkDirtyInEditor();
            Data.Add(key, value);
        }

        public bool ContainsKey(TKey key) => Data.ContainsKey(key);

        public bool Remove(TKey key) {
            MarkDirtyInEditor();
            return Data.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value) => Data.TryGetValue(key, out value);

        public void Add(KeyValuePair<TKey, TValue> item) {
            MarkDirtyInEditor();
            ((ICollection<KeyValuePair<TKey, TValue>>)Data).Add(item);
        }

        public void Clear() {
            MarkDirtyInEditor();
            Data.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) => ((ICollection<KeyValuePair<TKey, TValue>>)Data).Contains(item);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            ((ICollection<KeyValuePair<TKey, TValue>>)Data).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item) {
            MarkDirtyInEditor();
            return ((ICollection<KeyValuePair<TKey, TValue>>)Data).Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Data.GetEnumerator();

        [Conditional("UNITY_EDITOR")]
        private void MarkDirtyInEditor() {
#if UNITY_EDITOR
            if (!Application.isPlaying) {
                UnityEditor.EditorUtility.SetDirty(this);
            }
#endif
        }
        #endregion

        #if UNITY_EDITOR
        public event System.Action? OnDataChangedViaInspector;
        private void OnCollectionChanged() => OnDataChangedViaInspector?.Invoke();
        #endif
    }
}
