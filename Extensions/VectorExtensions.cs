using UnityEngine;

namespace Draco.Extensions {
public static class VectorExtensions {
    /** <summary>Creates a new Vector3 with the specified x component.</summary> */
    public static Vector3 WithX(this Vector3 vector, float x) {
        vector.x = x;
        return vector;
    }
    
    /** <summary>Creates a new Vector3 with the specified y component.</summary> */
    public static Vector3 WithY(this Vector3 vector, float y) {
        vector.y = y;
        return vector;
    }
    
    /** <summary>Creates a new Vector3 with the specified z component.</summary> */
    public static Vector3 WithZ(this Vector3 vector, float z) {
        vector.z = z;
        return vector;
    }
    
    /** <summary>Creates a new Vector3 with the specified x and y components.</summary> */
    public static Vector3 WithXY(this Vector3 vector, float x, float y) {
        vector.x = x;
        vector.y = y;
        return vector;
    }
    
    /** <summary>Creates a new Vector3 with the specified x and z components.</summary> */
    public static Vector3 WithXZ(this Vector3 vector, float x, float z) {
        vector.x = x;
        vector.z = z;
        return vector;
    }
    
    /** <summary>Creates a new Vector3 with the specified y and z components.</summary> */
    public static Vector3 WithYZ(this Vector3 vector, float y, float z) {
        vector.y = y;
        vector.z = z;
        return vector;
    }
    
    /** <summary>Creates a new Vector3Int with the specified x component.</summary> */
    public static Vector3Int WithX(this Vector3Int vector, int x) {
        vector.x = x;
        return vector;
    }
    
    /** <summary>Creates a new Vector3Int with the specified y component.</summary> */
    public static Vector3Int WithY(this Vector3Int vector, int y) {
        vector.y = y;
        return vector;
    }
    
    /** <summary>Creates a new Vector3Int with the specified z component.</summary> */
    public static Vector3Int WithZ(this Vector3Int vector, int z) {
        vector.z = z;
        return vector;
    }
    
    /** <summary>Creates a new Vector3Int with the specified x and y components.</summary> */
    public static Vector3Int WithXY(this Vector3Int vector, int x, int y) {
        vector.x = x;
        vector.y = y;
        return vector;
    }
    
    /** <summary>Creates a new Vector3Int with the specified x and z components.</summary> */
    public static Vector3Int WithXZ(this Vector3Int vector, int x, int z) {
        vector.x = x;
        vector.z = z;
        return vector;
    }
    
    /** <summary>Creates a new Vector3Int with the specified y and z components.</summary> */
    public static Vector3Int WithYZ(this Vector3Int vector, int y, int z) {
        vector.y = y;
        vector.z = z;
        return vector;
    }
    
    /** <summary>Creates a new Vector2 with the specified x component.</summary> */
    public static Vector2 WithX(this Vector2 vector, float x) {
        vector.x = x;
        return vector;
    }
    
    /** <summary>Creates a new Vector2 with the specified y component.</summary> */
    public static Vector2 WithY(this Vector2 vector, float y) {
        vector.y = y;
        return vector;
    }
    
    /** <summary>Creates a new Vector2Int with the specified x component.</summary> */
    public static Vector2Int WithX(this Vector2Int vector, int x) {
        vector.x = x;
        return vector;
    }
    
    /** <summary>Creates a new Vector2Int with the specified y component.</summary> */
    public static Vector2Int WithY(this Vector2Int vector, int y) {
        vector.y = y;
        return vector;
    }

    /** <summary>Converts a Vector3 to a Vector2 using the specified plane.</summary> */
    public static Vector2 ToVector2XY(this Vector3 vector3) {
        return new Vector2(vector3.x, vector3.y);
    }
    
    /** <summary>Converts a Vector3 to a Vector2 using the specified plane.</summary> */
    public static Vector2 ToVector2XZ(this Vector3 vector3) {
        return new Vector2(vector3.x, vector3.z);
    }
    
    /** <summary>Converts a Vector3 to a Vector2 using the specified plane.</summary> */
    public static Vector2 ToVector2YZ(this Vector3 vector3) {
        return new Vector2(vector3.y, vector3.z);
    }

    /** <summary>Converts a Vector3 to a Vector2Int using the specified plane by flooring the coordinates.</summary> */
    public static Vector2Int FloorToVector2IntXY(this Vector3 vector3) {
        return new Vector2Int(Mathf.FloorToInt(vector3.x), Mathf.FloorToInt(vector3.y));
    }
    
    /** <summary>Converts a Vector3 to a Vector2Int using the specified plane by flooring the coordinates.</summary> */
    public static Vector2Int FloorToVector2IntXZ(this Vector3 vector3) {
        return new Vector2Int(Mathf.FloorToInt(vector3.x), Mathf.FloorToInt(vector3.z));
    }
    
    /** <summary>Converts a Vector3 to a Vector2Int using the specified plane by flooring the coordinates.</summary> */
    public static Vector2Int FloorToVector2IntYZ(this Vector3 vector3) {
        return new Vector2Int(Mathf.FloorToInt(vector3.y), Mathf.FloorToInt(vector3.z));
    }

    /** <summary>Converts a Vector3 to a Vector2Int using the specified plane by ceiling the coordinates.</summary> */
    public static Vector2Int CeilToVector2IntXY(this Vector3 vector3) {
        return new Vector2Int(Mathf.CeilToInt(vector3.x), Mathf.CeilToInt(vector3.y));
    }
    
    /** <summary>Converts a Vector3 to a Vector2Int using the specified plane by ceiling the coordinates.</summary> */
    public static Vector2Int CeilToVector2IntXZ(this Vector3 vector3) {
        return new Vector2Int(Mathf.CeilToInt(vector3.x), Mathf.CeilToInt(vector3.z));
    }
    
    /** <summary>Converts a Vector3 to a Vector2Int using the specified plane by ceiling the coordinates.</summary> */   
    public static Vector2Int CeilToVector2IntYZ(this Vector3 vector3) {
        return new Vector2Int(Mathf.CeilToInt(vector3.y), Mathf.CeilToInt(vector3.z));
    }

    /** <summary>Converts a Vector3 to a Vector2Int using the specified plane by rounding the coordinates.</summary> */
    public static Vector2Int RoundToVector2IntXY(this Vector3 vector3) {
        return new Vector2Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));
    }
    
    /** <summary>Converts a Vector3 to a Vector2Int using the specified plane by rounding the coordinates.</summary> */   
    public static Vector2Int RoundToVector2IntXZ(this Vector3 vector3) {
        return new Vector2Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.z));
    }
    
    /** <summary>Converts a Vector3 to a Vector2Int using the specified plane by rounding the coordinates.</summary> */  
    public static Vector2Int RoundToVector2IntYZ(this Vector3 vector3) {
        return new Vector2Int(Mathf.RoundToInt(vector3.y), Mathf.RoundToInt(vector3.z));
    }
    
    /** <summary>Converts a Vector3Int to a Vector2Int using the specified plane.</summary> */
    public static Vector2Int ToVector2XY(this Vector3Int vector3) {
        return new Vector2Int(vector3.x, vector3.y);
    }
    
    /** <summary>Converts a Vector3Int to a Vector2Int using the specified plane.</summary> */
    public static Vector2Int ToVector2XZ(this Vector3Int vector3) {
        return new Vector2Int(vector3.x, vector3.z);
    }
    
    /** <summary>Converts a Vector3Int to a Vector2Int using the specified plane.</summary> */
    public static Vector2Int ToVector2YZ(this Vector3Int vector3) {
        return new Vector2Int(vector3.y, vector3.z);
    }
    
    /** <summary>Converts a Vector2 to a Vector3 using the specified plane.</summary> */
    public static Vector3 ToVector3XY(this Vector2 vector2, float z = 0.0f) {
        return new Vector3(vector2.x, vector2.y, z);
    }
    
    /** <summary>Converts a Vector2 to a Vector3 using the specified plane.</summary> */
    public static Vector3 ToVector3XZ(this Vector2 vector2, float y = 0.0f) {
        return new Vector3(vector2.x, y, vector2.y);
    }
    
    /** <summary>Converts a Vector2 to a Vector3 using the specified plane.</summary> */
    public static Vector3 ToVector3YZ(this Vector2 vector2, float x = 0.0f) {
        return new Vector3(x, vector2.x, vector2.y);
    }
    
    /** <summary>Converts a Vector2Int to a Vector3Int using the specified plane.</summary> */
    public static Vector3Int ToVector3XY(this Vector2Int vector2, int z = 0) {
        return new Vector3Int(vector2.x, vector2.y, z);
    }
    
    /** <summary>Converts a Vector2Int to a Vector3Int using the specified plane.</summary> */
    public static Vector3Int ToVector3XZ(this Vector2Int vector2, int y = 0) {
        return new Vector3Int(vector2.x, y, vector2.y);
    }
    
    /** <summary>Converts a Vector2Int to a Vector3Int using the specified plane.</summary> */
    public static Vector3Int ToVector3YZ(this Vector2Int vector2, int x = 0) {
        return new Vector3Int(x, vector2.x, vector2.y);
    }

    /** <summary>Converts a Vector3 to a Vector3Int by flooring the coordinates.</summary> */
    public static Vector3Int FloorToVector3Int(this Vector3 vector3) {
        return new Vector3Int(Mathf.FloorToInt(vector3.x), Mathf.FloorToInt(vector3.y), Mathf.FloorToInt(vector3.z));
    }
    
    /** <summary>Converts a Vector3 to a Vector3Int by ceiling the coordinates.</summary> */
    public static Vector3Int CeilToVector3Int(this Vector3 vector3) {
        return new Vector3Int(Mathf.CeilToInt(vector3.x), Mathf.CeilToInt(vector3.y), Mathf.CeilToInt(vector3.z));
    }
    
    /** <summary>Converts a Vector3 to a Vector3Int by rounding the coordinates.</summary> */
    public static Vector3Int RoundToVector3Int(this Vector3 vector3) {
        return new Vector3Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y), Mathf.RoundToInt(vector3.z));
    }

    /** <summary>Converts a Vector2 to a Vector2Int by flooring the coordinates.</summary> */
    public static Vector2Int FloorToVector2Int(this Vector2 vector2) {
        return new Vector2Int(Mathf.FloorToInt(vector2.x), Mathf.FloorToInt(vector2.y));
    }
    
    /** <summary>Converts a Vector2 to a Vector2Int by ceiling the coordinates.</summary> */   
    public static Vector2Int CeilToVector2Int(this Vector2 vector2) {
        return new Vector2Int(Mathf.CeilToInt(vector2.x), Mathf.CeilToInt(vector2.y));
    }
    
    /** <summary>Converts a Vector2 to a Vector2Int by rounding the coordinates.</summary> */ 
    public static Vector2Int RoundToVector2Int(this Vector2 vector2) {
        return new Vector2Int(Mathf.RoundToInt(vector2.x), Mathf.RoundToInt(vector2.y));
    }

    /** <summary>Checks if a value is within the range of a Vector2Int (exclusive).</summary> */
    public static bool Contains(this Vector2Int vector, int value) {
        return vector.x < value && value < vector.y;
    }
    
    /** <summary>Checks if a value is within the range of a Vector2Int (inclusive).</summary> */
    public static bool ContainsInclusive(this Vector2Int vector, int value) {
        return vector.x <= value && value <= vector.y;
    }
    
    /** <summary>Checks if a value is within the range of a Vector2 (exclusive).</summary> */
    public static bool Contains(this Vector2 vector, int value) {
        return vector.x < value && value < vector.y;
    }
    
    /** <summary>Checks if a value is within the range of a Vector2 (exclusive).</summary> */
    public static bool Contains(this Vector2 vector, float value) {
        return vector.x < value && value < vector.y;
    }
    
    /** <summary>Checks if a value is within the range of a Vector2 (inclusive).</summary> */
    public static bool ContainsInclusive(this Vector2 vector, int value) {
        return vector.x <= value && value <= vector.y;
    }
    
    /** <summary>Checks if a value is within the range of a Vector2 (inclusive).</summary> */
    public static bool ContainsInclusive(this Vector2 vector, float value) {
        return vector.x <= value && value <= vector.y;
    }
}
}