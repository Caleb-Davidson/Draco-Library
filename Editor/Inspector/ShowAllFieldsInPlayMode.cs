using System;
using System.Collections.Generic;
using System.Reflection;
using Draco.Attributes;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Draco.Editor.Inspector {
[UsedImplicitly]
public class ShowAllFieldsInPlayModeAttributeProcessor : OdinAttributeProcessor {
    public override bool CanProcessSelfAttributes(InspectorProperty property) => false;

    public override bool CanProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member) {
        if (parentProperty.GetAttribute<ShowAllFieldsInPlayModeAttribute>() == null || !(member is FieldInfo field))
            return false;

        // Check if field is a delegate type or a collection of delegate types
        var fieldType = field.FieldType;
        if (typeof(Delegate).IsAssignableFrom(fieldType) || 
            (fieldType.IsGenericType && typeof(Delegate).IsAssignableFrom(fieldType.GetGenericArguments()[0])))
            return false;

        // Check if field is a collection of delegates
        if ((typeof(IEnumerable<>).IsAssignableFrom(fieldType) || fieldType.IsArray) && 
            fieldType.IsGenericType && typeof(Delegate).IsAssignableFrom(fieldType.GetGenericArguments()[0]))
            return false;

        return true;
    }

    public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes) {
        attributes.Add(new ShowInInspectorAttribute());
        attributes.Add(new HideInEditorModeAttribute());
    }
}

public class ShowAllFieldsInPlayModeDrawer : OdinAttributeDrawer<ShowAllFieldsInPlayModeAttribute> {
    protected override void DrawPropertyLayout(GUIContent? label) {
        CallNextDrawer(label);
        if (!Application.isPlaying) {
            EditorGUILayout.LabelField("More data will be shown after entering play mode.");
        }
    }
}
}