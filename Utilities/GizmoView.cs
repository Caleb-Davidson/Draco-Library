using Sirenix.OdinInspector;
using UnityEngine;

namespace Draco.Utilities {
/// <summary>
/// Custom Component-based Gizmo visualization tools.
/// 
/// Usage:
/// - Attach this component to any GameObject to visualize it in the scene view.
/// - Configure the shape, color, and other properties in the inspector.
/// 
/// Properties:
/// - shape: The shape to draw (WireSphere, Sphere, WireCube, Cube, WireMesh, Mesh, Ray, Line, Icon)
/// - color: The color of the gizmo
/// - onlyWhenSelected: Whether to draw the gizmo only when the GameObject is selected
/// - radius: The radius of the sphere or wire sphere
/// - vector: The size of the cube or wire cube, or the direction of the ray or line
/// - mesh: The mesh to draw (for WireMesh and Mesh shapes)
/// - icon: The icon to draw (for Icon shape)
/// - allowScaling: Whether to allow scaling of the icon
/// </summary>
public class GizmoView : MonoBehaviour {
    #if UNITY_EDITOR
    private enum Shape {
        WireSphere, WireCube, WireMesh, Sphere, Cube, Mesh, Ray, Line, Icon
    }

    [SerializeField]
    private Shape shape;
    [SerializeField, ShowIf(nameof(showColor))]
    private Color color = Color.green;
    [SerializeField]
    private bool onlyWhenSelected = true;

    [SerializeField, ShowIf(nameof(isSphere))]
    private float radius = 0.5f;
    [SerializeField, ShowIf(nameof(isCubeOrLine)), LabelText("$" + nameof(vectorLabelText))]
    private Vector3 vector = Vector3.one;
    [SerializeField, ShowIf(nameof(isMesh))]
    private Mesh? mesh;
    [SerializeField, ShowIf(nameof(isIcon))]
    private string icon = "";
    [SerializeField, ShowIf(nameof(isIcon))]
    private bool allowScaling = true;
    
    private bool showColor => shape != Shape.Icon;
    private bool isSphere => shape is Shape.WireSphere or Shape.Sphere;
    private bool isCubeOrLine => shape is Shape.WireCube or Shape.Cube or Shape.Ray or Shape.Line;
    private bool isMesh => shape is Shape.WireMesh or Shape.Mesh;
    private bool isIcon => shape == Shape.Icon;
    private string vectorLabelText => shape switch {
        Shape.WireCube or Shape.Cube => "Size",
        Shape.Ray => "Direction",
        Shape.Line => "End Point",
        _ => ""
    };
    
    private void OnDrawGizmosSelected() {
        if (onlyWhenSelected) DrawGizmo();
    }

    private void OnDrawGizmos() {
        if (!onlyWhenSelected) DrawGizmo();
    }

    private void DrawGizmo() {
        Gizmos.color = color;
        switch (shape) {
            case Shape.WireSphere:
                Gizmos.DrawWireSphere(transform.position, radius);
                break;
            case Shape.Sphere:
                Gizmos.DrawSphere(transform.position, radius);
                break;
            case Shape.WireCube:
                Gizmos.DrawWireCube(transform.position, vector);
                break;
            case Shape.Cube:
                Gizmos.DrawCube(transform.position, vector);
                break;
            case Shape.WireMesh:
                if (mesh != null) Gizmos.DrawWireMesh(mesh, transform.position, transform.rotation, transform.lossyScale);
                break;
            case Shape.Mesh:
                if (mesh != null) Gizmos.DrawMesh(mesh, transform.position, transform.rotation, transform.lossyScale);
                break;
            case Shape.Ray:
                Gizmos.DrawRay(transform.position, vector);
                break;
            case Shape.Line:
                Gizmos.DrawLine(transform.position, transform.position + vector);
                break;
            case Shape.Icon:
                Gizmos.DrawIcon(transform.position, icon, allowScaling);
                break;
        }
    }
    #endif
}
}