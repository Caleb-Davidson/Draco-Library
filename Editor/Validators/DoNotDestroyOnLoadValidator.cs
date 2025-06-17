using Draco.Editor.Validators;
using Draco.Utilities;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEditor;

[assembly: RegisterValidator(typeof(DoNotDestroyOnLoadValidator))]

namespace Draco.Editor.Validators {
public class DoNotDestroyOnLoadValidator : ValueValidator<DoNotDestroyOnLoad> {
    protected override void Validate(ValidationResult result) {
        if (Value.transform.parent == null) return;
        ref var errorRef = ref result.AddError("DoNotDestroyOnLoad must not have a parent.");
            
        if (!PrefabUtility.IsPartOfPrefabAsset(Value)) {
            errorRef.WithFix("Move to Scene Root", () => {
                Undo.SetTransformParent(Value.transform, null, true, "Move to Scene Root");
            });
        }
    }
}
}