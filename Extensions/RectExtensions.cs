using UnityEngine;

namespace Draco.Extensions {
public static class RectExtensions {
    /** <summary>Returns a rect with specified width and is left aligned to the original rect.</summary> */
    public static Rect TakeLeft(this Rect rect, float width) {
        rect.width = width;
        return rect;
    }

    /** <summary>Returns a rect with specified width and is right aligned to the original rect.</summary> */
    public static Rect TakeRight(this Rect rect, float width) {
        rect.x += rect.width - width;
        rect.width = width;
        return rect;
    }

    /** <summary>Returns a rect with specified height and is top aligned to the original rect.</summary> */
    public static Rect TakeTop(this Rect rect, float height) {
        rect.height = height;
        return rect;
    }

    /** <summary>Returns a rect with specified height and is bottom aligned to the original rect.</summary> */
    public static Rect TakeBottom(this Rect rect, float height) {
        rect.y += rect.height - height;
        rect.height = height;
        return rect;
    }

    /** <summary>Returns a rect with the specified width trimmed from the left side.</summary> */
    public static Rect TrimLeft(this Rect rect, float width) {
        rect.x += width;
        rect.width -= width;
        return rect;
    }
    
    /** <summary>Returns a rect with the specified width trimmed from the right side.</summary> */
    public static Rect TrimRight(this Rect rect, float width) {
        rect.width -= width;
        return rect;
    }
    
    /** <summary>Returns a rect with the specified height trimmed from the top side.</summary> */
    public static Rect TrimTop(this Rect rect, float height) {
        rect.y += height;
        rect.height -= height;
        return rect;
    }
    
    /** <summary>Returns a rect with the specified height trimmed from the bottom side.</summary> */
    public static Rect TrimBottom(this Rect rect, float height) {
        rect.height -= height;
        return rect;
    }
    
    /** <summary>Returns a rect with the specified amount trimmed from all sides.</summary> */
    public static Rect Shrink(this Rect rect, float amount) {
        rect.x += amount;
        rect.y += amount;
        rect.width -= amount * 2;
        rect.height -= amount * 2;
        return rect;
    }
    
    /** <summary>Returns a rect with the specified width trimmed from both sides.</summary> */
    public static Rect ShrinkHorizontal(this Rect rect, float width) {
        rect.x += width;
        rect.width -= width * 2;
        return rect;
    }
    
    /** <summary>Returns a rect with the specified height trimmed from both sides.</summary> */
    public static Rect ShrinkVertical(this Rect rect, float height) {
        rect.y += height;
        rect.height -= height * 2;
        return rect;
    }
    
    /** <summary>Splits the rect into multiple rect with the specified width and returns the rect at the specified index (0-based).</summary> */
    public static Rect SplitWidth(this Rect rect, float width, int index) {
        return new Rect(rect.x + width * index, rect.y, width, rect.height);
    }
    
    /** <summary>Splits the rect into multiple rect with the specified height and returns the rect at the specified index (0-based).</summary> */
    public static Rect SplitHeight(this Rect rect, float height, int index) {
        return new Rect(rect.x, rect.y + height * index, rect.width, height);
    }
}
}