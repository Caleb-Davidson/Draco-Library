using System;

namespace Draco.Attributes {
/// <summary>
/// Indicates that a Vector value must be not at the origin (i.e. not all zeroes in its constituent values)
/// Applicable to Vector2, Vector3, Vector2Int, and Vector3Int fields or properties.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class IsNotOrigin : Attribute { }
}