using Draco.Attributes;
using Draco.Editor.Validators;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(IsNotOriginValidatorVector2))]
[assembly: RegisterValidator(typeof(IsNotOriginValidatorVector3))]
[assembly: RegisterValidator(typeof(IsNotOriginValidatorVector2Int))]
[assembly: RegisterValidator(typeof(IsNotOriginValidatorVector3Int))]

namespace Draco.Editor.Validators {
#region IsNotOrigin Validators
public class IsNotOriginValidatorVector2 : AttributeValidator<IsNotOrigin, Vector2> {
    protected override void Validate(ValidationResult result) {
        if (Value == Vector2.zero) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be origin.";
        }
    }
}

public class IsNotOriginValidatorVector3 : AttributeValidator<IsNotOrigin, Vector3> {
    protected override void Validate(ValidationResult result) {
        if (Value == Vector3.zero) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be origin.";
        }
    }
}

public class IsNotOriginValidatorVector2Int : AttributeValidator<IsNotOrigin, Vector2Int> {
    protected override void Validate(ValidationResult result) {
        if (Value == Vector2Int.zero) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be origin.";
        }
    }
}

public class IsNotOriginValidatorVector3Int : AttributeValidator<IsNotOrigin, Vector3Int> {
    protected override void Validate(ValidationResult result) {
        if (Value == Vector3Int.zero) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be origin.";
        }
    }
}
#endregion
}