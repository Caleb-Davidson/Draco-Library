using System;

namespace Draco.Attributes {
/// <summary>
/// Validates that a string represents a valid folder path within a Unity Resources directory.
/// You can optionally prepend or append sub-paths to the field value before searching.
/// Useful for organizing and validating folder-based resources like textures, audio, or prefabs.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ResourceFolderAttribute : Attribute {
    /// <summary>
    /// A path that will be prepended to the field value when searching for the folder.
    /// For example, if PrependPath is "Sprites/NPCs" and the field value is "Rival",
    /// the validator will check in "Resources/Sprites/NPCs/Rival".
    /// </summary>
    public string PrependPath { get; }
    
    /// <summary>
    /// A path that will be appended to the field value when searching for the folder.
    /// For example, if AppendPath is "/Variants" and the field value is "Rival",
    /// the validator will check in "Resources/Rival/Variants".
    /// </summary>
    public string AppendPath { get; }
    
    /// <summary>
    /// If true, allows the field value to be empty or null without triggering a validation error.
    /// </summary>
    public bool AllowEmptyPath { get; }

    public ResourceFolderAttribute(string prependPath = "", string appendPath = "", bool allowEmptyPath = true) {
        PrependPath = prependPath;
        AppendPath = appendPath;
        AllowEmptyPath = allowEmptyPath;
    }
}

/// <summary>
/// Validates that a string represents a valid file path within a Unity Resources directory.
/// Optionally allows configuring a path to prepend or a file extension to append during validation.
/// Can also restrict validation to strings that start with a specific character.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ResourceFileAttribute : Attribute {
    /// <summary>
    /// A path that will be prepended to the field value when searching for the file.
    /// For example, if PrependPath is "Dialogues" and the field value is "Intro",
    /// the validator will check in "Resources/Dialogues/Intro".
    /// </summary>
    public string PrependPath { get; }
    
    /// <summary>
    /// A file extension (e.g., ".asset", ".txt") that will be appended to the field value.
    /// Useful for ensuring files are of the correct type and match Unity's Resources loading pattern.
    /// </summary>
    public string Extension { get; }
    
    /// <summary>
    /// If true, allows the field value to be empty or null without triggering a validation error.
    /// </summary>
    public bool AllowEmptyPath { get; }
    
    /// <summary>
    /// If specified, validation will only occur if the field value starts with this character.
    /// This is useful for special cases where only tagged inputs should be validated (e.g., '@name').
    /// </summary>
    public char? RequiredStartCharacter { get; }

    public ResourceFileAttribute(string prependPath = "", string extension = "", bool allowEmptyPath = true, char requiredStartCharacter = '\0') {
        PrependPath = prependPath;
        Extension = extension;
        AllowEmptyPath = allowEmptyPath;
        RequiredStartCharacter = requiredStartCharacter == '\0' ? null : requiredStartCharacter;
    }
}
}