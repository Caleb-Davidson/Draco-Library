using Draco.Attributes;
using Draco.Editor.Validators;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(PositiveValidatorInt))]
[assembly: RegisterValidator(typeof(PositiveValidatorFloat))]
[assembly: RegisterValidator(typeof(PositiveValidatorDouble))]

[assembly: RegisterValidator(typeof(NegativeValidatorInt))]
[assembly: RegisterValidator(typeof(NegativeValidatorFloat))]
[assembly: RegisterValidator(typeof(NegativeValidatorDouble))]

[assembly: RegisterValidator(typeof(NonZeroValidatorInt))]
[assembly: RegisterValidator(typeof(NonZeroValidatorFloat))]
[assembly: RegisterValidator(typeof(NonZeroValidatorDouble))]

[assembly: RegisterValidator(typeof(NonNegativeValidatorInt))]
[assembly: RegisterValidator(typeof(NonNegativeValidatorFloat))]
[assembly: RegisterValidator(typeof(NonNegativeValidatorDouble))]

[assembly: RegisterValidator(typeof(NonPositiveValidatorInt))]
[assembly: RegisterValidator(typeof(NonPositiveValidatorFloat))]
[assembly: RegisterValidator(typeof(NonPositiveValidatorDouble))]

namespace Draco.Editor.Validators {
#region Positive Validators

public class PositiveValidatorInt : AttributeValidator<PositiveAttribute, int> {
    protected override void Validate(ValidationResult result) {
        if (Value <= 0) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must be positive.";
        }
    }
}

public class PositiveValidatorFloat : AttributeValidator<PositiveAttribute, float> {
    protected override void Validate(ValidationResult result) {
        if (Value <= 0f) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must be positive.";
        }
    }
}

public class PositiveValidatorDouble : AttributeValidator<PositiveAttribute, double> {
    protected override void Validate(ValidationResult result) {
        if (Value <= 0d) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must be positive.";
        }
    }
}

#endregion

#region Negative Validators

public class NegativeValidatorInt : AttributeValidator<NegativeAttribute, int> {
    protected override void Validate(ValidationResult result) {
        if (Value >= 0) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must be negative.";
        }
    }
}

public class NegativeValidatorFloat : AttributeValidator<NegativeAttribute, float> {
    protected override void Validate(ValidationResult result) {
        if (Value >= 0f) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must be negative.";
        }
    }
}

public class NegativeValidatorDouble : AttributeValidator<NegativeAttribute, double> {
    protected override void Validate(ValidationResult result) {
        if (Value >= 0d) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must be negative.";
        }
    }
}

#endregion

#region Non-Zero Validators

public class NonZeroValidatorInt : AttributeValidator<NonZeroAttribute, int> {
    protected override void Validate(ValidationResult result) {
        if (Value == 0) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be zero.";
        }
    }
}

public class NonZeroValidatorFloat : AttributeValidator<NonZeroAttribute, float> {
    protected override void Validate(ValidationResult result) {
        if (UnityEngine.Mathf.Approximately(Value, 0f)) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be zero.";
        }
    }
}

public class NonZeroValidatorDouble : AttributeValidator<NonZeroAttribute, double> {
    protected override void Validate(ValidationResult result) {
        if (System.Math.Abs(Value) < 1e-10) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be zero.";
        }
    }
}

#endregion

#region Non-Negative Validators

public class NonNegativeValidatorInt : AttributeValidator<NonNegativeAttribute, int> {
    protected override void Validate(ValidationResult result) {
        if (Value < 0) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be negative.";
        }
    }
}

public class NonNegativeValidatorFloat : AttributeValidator<NonNegativeAttribute, float> {
    protected override void Validate(ValidationResult result) {
        if (Value < 0f) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be negative.";
        }
    }
}

public class NonNegativeValidatorDouble : AttributeValidator<NonNegativeAttribute, double> {
    protected override void Validate(ValidationResult result) {
        if (Value < 0d) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be negative.";
        }
    }
}

#endregion

#region Non-Positive Validators

public class NonPositiveValidatorInt : AttributeValidator<NonPositiveAttribute, int> {
    protected override void Validate(ValidationResult result) {
        if (Value > 0) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be positive.";
        }
    }
}

public class NonPositiveValidatorFloat : AttributeValidator<NonPositiveAttribute, float> {
    protected override void Validate(ValidationResult result) {
        if (Value > 0f) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be positive.";
        }
    }
}

public class NonPositiveValidatorDouble : AttributeValidator<NonPositiveAttribute, double> {
    protected override void Validate(ValidationResult result) {
        if (Value > 0d) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Value must not be positive.";
        }
    }
}

#endregion
}