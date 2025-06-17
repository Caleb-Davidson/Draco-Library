using UnityEngine;

namespace Draco.Extensions {
public static class ColorExtensions {
    /** <summary>Creates a new color with the specified red value.</summary> */
    public static Color WithRed(this Color color, float red) {
        return new Color(red, color.g, color.b, color.a);
    }

    /** <summary>Creates a new color with the specified green value.</summary> */
    public static Color WithGreen(this Color color, float green) {
        return new Color(color.r, green, color.b, color.a);
    }

    /** <summary>Creates a new color with the specified blue value.</summary> */
    public static Color WithBlue(this Color color, float blue) {
        return new Color(color.r, color.g, blue, color.a);
    }
    
    /** <summary>Creates a new color with the specified alpha value.</summary> */
    public static Color WithAlpha(this Color color, float alpha) {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
}