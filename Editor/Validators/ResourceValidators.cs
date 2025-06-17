using System.IO;
using System.Linq;
using Draco.Attributes;
using Draco.Editor.Validators;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEditor;
using UnityEngine;

[assembly: RegisterValidator(typeof(ResourceFolderValidator))]
[assembly: RegisterValidator(typeof(ResourceFileValidator))]

namespace Draco.Editor.Validators {
public class ResourceFolderValidator : AttributeValidator<ResourceFolderAttribute, string> {
    private static string[]? cachedResourcesDirectories;

    protected override void Validate(ValidationResult result) {
        if (string.IsNullOrWhiteSpace(Value)) {
            if (Attribute.AllowEmptyPath) {
                return;
            }
            result.ResultType = ValidationResultType.Error;
            result.Message = "Folder path must not be empty.";
            return;
        }

        if (Value.IndexOfAny(Path.GetInvalidPathChars()) >= 0) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "Folder path contains invalid characters.";
            return;
        }

        var fullRelativePath = Path.Combine(Attribute.PrependPath, Value, Attribute.AppendPath).Replace("\\", "/");
        var found = ResourceValidatorUtils.EnsureResourcesCache().Any(resourcesDir =>
            Directory.Exists(Path.Combine(resourcesDir, fullRelativePath))
        );

        if (!found) {
            result.ResultType = ValidationResultType.Error;
            result.Message = $"Could not find folder at relative path '{fullRelativePath}' under any Resources directory.";
        }
    }
}

public class ResourceFileValidator : AttributeValidator<ResourceFileAttribute, string> {
    protected override void Validate(ValidationResult result) {
        Validate(Value, Attribute, result);
    }

    public static void Validate(string value, ResourceFileAttribute attribute, ValidationResult result) {
        if (string.IsNullOrWhiteSpace(value)) {
            if (attribute.AllowEmptyPath) {
                return;
            }
            result.ResultType = ValidationResultType.Error;
            result.Message = "File path must not be empty.";
            return;
        }

        if (attribute.RequiredStartCharacter.HasValue) {
            if (value[0] != attribute.RequiredStartCharacter.Value) {
                return;
            } else {
                value = value[1..];
            }
        }

        if (value.IndexOfAny(Path.GetInvalidPathChars()) >= 0) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "File value contains invalid characters.";
            return;
        }
        
        if (attribute.PrependPath.IndexOfAny(Path.GetInvalidPathChars().Where(c => c != '*').ToArray()) >= 0) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "File path contains invalid characters.";
            return;
        }
        
        if (attribute.Extension.IndexOfAny(Path.GetInvalidPathChars()) >= 0) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "File extension contains invalid characters.";
            return;
        }
        
        var relativePath = Path.Combine(attribute.PrependPath, value).Replace("\\", "/") + attribute.Extension;
        
        var wildcardCount = relativePath.Count(c => c == '*');
        if (wildcardCount > 1) {
            result.ResultType = ValidationResultType.Error;
            result.Message = "File path contains more than one wildcard.";
            return;
        }

        var found = false;
        var wildcardPresent = relativePath.Contains('*');
        foreach (var resourcesDir in ResourceValidatorUtils.EnsureResourcesCache()) {
            if (!wildcardPresent) {
                var candidateFile = Path.Combine(resourcesDir, relativePath);
                if (File.Exists(candidateFile)) {
                    found = true;
                    break;
                }
            } else {
                var pathParts = relativePath.Split('*');
                var absoluteSearchRoot = Path.Combine(resourcesDir, pathParts[0]);
                if (!Directory.Exists(absoluteSearchRoot))
                    continue;

                var subDirectories = Directory.GetDirectories(absoluteSearchRoot);
                foreach (var subDir in subDirectories) {
                    var candidateFile = Path.Combine(subDir, pathParts[1].TrimStart('/', '\\'));
                    if (File.Exists(candidateFile)) {
                        found = true;
                        break;
                    }
                }
            }

            if (found) {
                break;
            }
        }

        if (!found) {
            result.ResultType = ValidationResultType.Error;
            result.Message = $"Could not find file at relative path '{relativePath}' under any Resources directory.";
        }
    }
}

public static class ResourceValidatorUtils {
    private static string[]? cachedResourcesDirectories;

    public static string[] EnsureResourcesCache() {
        return cachedResourcesDirectories ??= Directory.GetDirectories(Application.dataPath, "Resources", SearchOption.AllDirectories);
    }

    [InitializeOnLoadMethod]
    private static void HookAssetPostprocessor() {
        DracoAssetPostProcessor.OnPostProcessAllAssets += ClearCache;
    }

    private static void ClearCache() {
        cachedResourcesDirectories = null;
    }

    public static void SetCustomResourcesDirectories(string[]? testDirectories) {
        cachedResourcesDirectories = testDirectories;
    }
}
}