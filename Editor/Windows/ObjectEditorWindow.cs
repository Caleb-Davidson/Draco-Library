using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Draco.Editor.Windows {
public class ObjectEditorWindow<TObject, TWindow> : OdinEditorWindow where TWindow : ObjectEditorWindow<TObject, TWindow> {
    [ShowInInspector]
    protected TObject? Target;
    
    public static void ShowTarget(TObject target, string? title = null) {
        var window = GetWindow<TWindow>();
        window.Target = target;
        window.titleContent = new GUIContent(title ?? typeof(TWindow).Name);
        window.Show();
    }
}
}