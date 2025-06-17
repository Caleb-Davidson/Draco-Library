using System;
using UnityEngine;

namespace Draco.Extensions {
public static class NumberExtensions {
    /**
     * <summary>Returns the largest integer smaller to or equal to value.</summary>
     * <param name="value"></param>
     */
    public static int FloorToInt(this float value) {
        return (int)Math.Floor(value);
    }
    
    /**
     * <summary>Returns the largest integer smaller to or equal to value.</summary>
     * <param name="value"></param>
     */
    public static int FloorToInt(this double value) {
        return (int)Math.Floor(value);
    }
    
    /**
     * <summary>Returns the nearest integer to value.</summary>
     * <param name="value"></param>
     */
    public static int RoundToInt(this float value) {
        return (int)Math.Round(value);
    }
    
    /**
     * <summary>Returns the nearest integer to value.</summary>
     * <param name="value"></param>
     */
    public static int RoundToInt(this double value) {
        return (int)Math.Round(value);
    }

    /**
     * <summary>Returns the smallest integer greater to or equal to value.</summary>
     * <param name="value"></param>
     */
    public static int CeilingToInt(this float value) {
        return (int)Math.Ceiling(value);
    }
    
    /** <summary>Returns the smallest integer greater to or equal to value.</summary> */
    public static int CeilingToInt(this double value) {
        return (int)Math.Ceiling(value);
    }
    
    /** <summary>Returns the integer part of value.</summary> */
    public static int Truncate(this float value) {
        return (int)value;
    }
    
    /** <summary>Returns the integer part of value.</summary> */
    public static int Truncate(this double value) {
        return (int)value;
    }

    /**
     * <summary>Clamps the value to be in the range defined by min and max inclusive.</summary>
     */
    public static int Clamp(this int value, int min, int max) {
        return Mathf.Clamp(value, min, max);
    }
    
    /**
     * <summary>Clamps the value to be in the range defined by min and max inclusive.</summary>
     */
    public static float Clamp(this float value, float min, float max) {
        return Mathf.Clamp(value, min, max);
    }
}
}