using System;

namespace Draco.Attributes
{
/// <summary>
/// Indicates that a numeric value must be strictly greater than zero.
/// Applicable to int, float, and double fields or properties.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class PositiveAttribute : Attribute {}

/// <summary>
/// Indicates that a numeric value must be strictly less than zero.
/// Applicable to int, float, and double fields or properties.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class NegativeAttribute : Attribute {}

/// <summary>
/// Indicates that a numeric value must be non-zero (either positive or negative).
/// Applicable to int, float, and double fields or properties.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class NonZeroAttribute : Attribute {}

/// <summary>
/// Indicates that a numeric value must be greater than or equal to zero.
/// Useful for disallowing negative values while still allowing zero.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class NonNegativeAttribute : Attribute {}

/// <summary>
/// Indicates that a numeric value must be less than or equal to zero.
/// Useful for disallowing positive values while still allowing zero.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class NonPositiveAttribute : Attribute {}
}