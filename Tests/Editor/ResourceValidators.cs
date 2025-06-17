using System.IO;
using Draco.Attributes;
using Draco.Editor.Validators;
using NUnit.Framework;
using Sirenix.OdinInspector.Editor.Validation;

namespace Draco.Tests.Editor {
public class ResourceFileValidatorTests {
    private string tempDir;

    [SetUp]
    public void Setup() {
        // Create a temp directory mimicking a Resources folder layout
        tempDir = Path.Combine(Path.GetTempPath(), "TestResources");
        Directory.CreateDirectory(tempDir);

        // Subdirectories
        Directory.CreateDirectory(Path.Combine(tempDir, "Characters/Hero"));
        Directory.CreateDirectory(Path.Combine(tempDir, "Characters/NPC/Animations"));

        // Files
        File.WriteAllText(Path.Combine(tempDir, "Characters/Hero/Idle.png"), "fake");
        File.WriteAllText(Path.Combine(tempDir, "Characters/NPC/Animations/Idle.anim"), "fake");

        // Inject this test resources path
        ResourceValidatorUtils.SetCustomResourcesDirectories(new[] { tempDir });
    }

    [TearDown]
    public void TearDown() {
        ResourceValidatorUtils.SetCustomResourcesDirectories(null);
        Directory.Delete(tempDir, recursive: true);
    }

    [Test]
    public void Valid_File_Should_Pass() {
        var attr = new ResourceFileAttribute("Characters", ".png", false);
        var validationResult = new ValidationResult();
        ResourceFileValidator.Validate("Hero/Idle", attr, validationResult);
        AssertIsValid(validationResult);
    }

    [Test]
    public void Missing_File_Should_Fail() {
        var attr = new ResourceFileAttribute("Characters", ".png", false);
        var validationResult = new ValidationResult();
        ResourceFileValidator.Validate("Hero/Walk", attr, validationResult);
        AssertIsInvalid(validationResult);
    }

    [Test]
    public void Wildcard_Directory_Should_Find_File() {
        var attr = new ResourceFileAttribute("Characters/*/Animations", ".anim", false);
        var validationResult = new ValidationResult();
        ResourceFileValidator.Validate("Idle", attr, validationResult);
        AssertIsValid(validationResult);
    }
    
    [Test]
    public void Multiple_Wildcards_Should_Fail() {
        var attr = new ResourceFileAttribute("Characters/*/Animations/*", ".anim", false);
        var validationResult = new ValidationResult();
        ResourceFileValidator.Validate("Idle", attr, validationResult);
        AssertIsInvalid(validationResult);
    }

    [Test]
    public void Empty_Path_Allowed_Should_Pass() {
        var attr = new ResourceFileAttribute("Characters", ".png", true);
        var validationResult = new ValidationResult();
        ResourceFileValidator.Validate("", attr, validationResult);
        AssertIsValid(validationResult);
    }

    [Test]
    public void Empty_Path_Disallowed_Should_Fail() {
        var attr = new ResourceFileAttribute("Characters", ".png", false);
        var validationResult = new ValidationResult();
        ResourceFileValidator.Validate("", attr, validationResult);
        AssertIsInvalid(validationResult);
    }

    [Test]
    public void Invalid_Value_Characters_Should_Fail() {
        var attr = new ResourceFileAttribute("Characters", ".png", false);
        var validationResult = new ValidationResult();
        ResourceFileValidator.Validate("Hero/Idl|e", attr, validationResult);
        AssertIsInvalid(validationResult);
    }
    
    [Test]
    public void Invalid_Path_Characters_Should_Fail() {
        var attr = new ResourceFileAttribute("Characters|", ".png", false);
        var validationResult = new ValidationResult();
        ResourceFileValidator.Validate("Hero/Idle", attr, validationResult);
        AssertIsInvalid(validationResult);
    }
    
    [Test]
    public void Invalid_Extension_Characters_Should_Fail() {
        var attr = new ResourceFileAttribute("Characters", ".|.png", false);
        var validationResult = new ValidationResult();
        ResourceFileValidator.Validate("Hero/Idle", attr, validationResult);
        AssertIsInvalid(validationResult);
    }

    [Test]
    public void Missing_Required_Start_Character_Should_Skip_Validation() {
        var attr = new ResourceFileAttribute("Characters", ".png", false, '$');
        var validationResult = new ValidationResult();
        ResourceFileValidator.Validate("Invalid", attr, validationResult);
        AssertIsValid(validationResult);
    }

    [Test]
    public void Required_Start_Character_Valid_When_Present() {
        var attr = new ResourceFileAttribute("Characters", ".png", false, '$');
        var validationResult = new ValidationResult();
        ResourceFileValidator.Validate("$Hero/Idle", attr, validationResult);
        AssertIsValid(validationResult);
    }
    
    private void AssertIsValid(ValidationResult result) {
        AssertExtensions.IsOneOf(result.ResultType, ValidationResultType.Valid, ValidationResultType.IgnoreResult);
    }

    private void AssertIsInvalid(ValidationResult result) {
        AssertExtensions.IsOneOf(result.ResultType, ValidationResultType.Error);
    }
}
}