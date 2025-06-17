using System;
using UnityEngine;

namespace Draco.Extensions {
public static class GUIScopes {
    /** <summary>Scope for temporarily setting the GUI color.</summary> */
    // Code copied from an internal Unity class to avoid reflection
    // https://github.com/Unity-Technologies/UnityCsReference/blob/master/Modules/IMGUI/GUI.cs
    public struct ColorScope : IDisposable {
        private bool disposed;
        private readonly Color previousColor;

        public ColorScope(Color newColor) {
            disposed = false;
            previousColor = GUI.color;
            GUI.color = newColor;
        }

        public ColorScope(float r, float g, float b, float a = 1f)
            : this(new Color(r, g, b, a)) { }

        public void Dispose() {
            if (disposed) return;
            disposed = true;
            GUI.color = previousColor;
        }
    }

    /** <summary>Scope for temporarily setting the GUI background color.</summary> */
    // Code copied from an internal Unity class to avoid reflection
    // https://github.com/Unity-Technologies/UnityCsReference/blob/master/Modules/IMGUI/GUI.cs
    public struct BackgroundColorScope : IDisposable {
        private bool disposed;
        private readonly Color previousColor;

        public BackgroundColorScope(Color newColor) {
            disposed = false;
            previousColor = GUI.backgroundColor;
            GUI.backgroundColor = newColor;
        }

        public BackgroundColorScope(float r, float g, float b, float a = 1.0f) : this(new Color(r, g, b, a)) { }

        public void Dispose() {
            if (disposed)
                return;
            disposed = true;
            GUI.backgroundColor = previousColor;
        }
    }
}
}