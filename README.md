# Draco Library

A comprehensive utility library for Unity projects, providing solutions to common development challenges and quality-of-life improvements.

## Table of Contents

- [Draco Library](#draco-library)
  - [Table of Contents](#table-of-contents)
  - [Features](#features)
    - [Attributes](#attributes)
      - [Numeric Validators](#numeric-validators)
      - [Resource Validators](#resource-validators)
      - [Vector Validators](#vector-validators)
      - [Inspector Attributes](#inspector-attributes)
    - [Editor Tools](#editor-tools)
    - [Extensions](#extensions)
      - [Boolean Extensions](#boolean-extensions)
      - [Collection Extensions](#collection-extensions)
      - [Color Extensions](#color-extensions)
      - [GUI Scopes](#gui-scopes)
      - [GameObject Extensions](#gameobject-extensions)
      - [Number Extensions](#number-extensions)
      - [Random Extensions](#random-extensions)
      - [Transform Extensions](#transform-extensions)
      - [Object Extensions](#object-extensions)
      - [Rect Extensions](#rect-extensions)
      - [Task Extensions](#task-extensions)
      - [Vector Extensions](#vector-extensions)
    - [Model](#model)
      - [ScriptableList](#scriptablelist)
      - [ScriptableSingleton](#scriptablesingleton)
      - [SerializableHashSet](#serializablehashset)
    - [Utilities](#utilities)
  - [Contributing](#contributing)
  - [License](#license)

## Features

### Attributes

#### Numeric Validators
Enforce numeric constraints on fields with these attributes:
- `[Positive]`: Ensures value is > 0
- `[Negative]`: Ensures value is < 0
- `[NonNegative]`: Ensures value is ≥ 0
- `[NonPositive]`: Ensures value is ≤ 0
- `[NonZero]`: Ensures value is not zero

#### Resource Validators
Validate resource references in the Unity editor:
- `[ResourceFolder]`: Validates folder paths in Resources
  - `prependPath`: Optional path to prepend
  - `appendPath`: Optional path to append
  - `allowEmptyPath`: Whether empty paths are allowed
- `[ResourceFile]`: Validates file paths in Resources
  - `prependPath`: Optional path to prepend
  - `extension`: Expected file extension
  - `allowEmptyPath`: Whether empty paths are allowed
  - `requiredStartCharacter`: Optional character the path must start with

#### Vector Validators
- `[IsNotOrigin]`: Ensures a vector is not at the origin (0,0) or (0,0,0)

#### Inspector Attributes
Control field visibility in the Unity inspector:
- `[ShowAllFieldsInPlayMode]`: Shows all fields of a class in the inspector while in play mode

### Editor Tools
- **Asset Tracker**: Monitor and track assets being imported, deleted, or moved
- **Editor Icons**: Quick access to Unity's built-in editor icons
- **Hidden GameObject Tools**: Menu Items for showing/hiding hidden GameObjects

### Extensions

#### Boolean Extensions
- `IfTrue(Action)`: Executes action if true
- `IfFalse(Action)`: Executes action if false

**Examples:**
```csharp
// Execute only if condition is true
bool isPlayerAlive = true;
isPlayerAlive.IfTrue(() => Debug.Log("Player is alive!"));

// Execute only if condition is false
bool hasKey = false;
hasKey.IfFalse(() => ShowKeyMissingMessage());

// Chaining
bool isGamePaused = false;
isGamePaused
    .IfTrue(() => PauseGame())
    .IfFalse(() => ResumeGame());
```

#### Collection Extensions
- **Iteration**
  - `ForEach(Action<T>)`: Executes an action for each element
  - `ForEach(Action<T, int>)`: Executes an action for each element with index
  - `Process(Action<T>)`: Applies an action to each element and returns the original collection

- **Querying**
  - `WhereNotNull()`: Filters out null elements
  - `Empty()`: Returns true if the collection has no elements
  - `Product()`: Calculates the product of all elements (for int and float)

- **Transformation**
  - `Zip<T1, T2>(IEnumerable<T2>)`: Zips two sequences into tuples
  - `ToDictionary()`: Creates a dictionary from a sequence of tuples
  - `ToReverseDictionary()`: Creates a dictionary with keys and values swapped
  - `RandomElement<T>()`: Returns a random element from any IEnumerable
  - `Shuffle<T>()`: Returns a shuffled copy of the collection

- **Deconstruction**
  - `Deconstruct(out T first)`: Extracts the first element
  - `Deconstruct(out T first, out T second)`: Extracts first two elements
  - `Deconstruct(out T first, out T second, out T third)`: Extracts first three elements
  - `Deconstruct(out T first, out T second, out T third, out T fourth)`: Extracts first four elements

- **Tuple Operations**
  - `Unzip<TSource, TTarget1, TTarget2>(Action<TTarget1, TTarget2, TSource>)`: Unzips a sequence into two collections
  - `UnzipToArray<T1, T2>()`: Unzips a sequence into two arrays
  - `WhenAll()`: Awaits all tasks in a sequence

- **Examples**
  ```csharp
  // Basic iteration
  myList.ForEach(item => Debug.Log(item));
  
  // Deconstruction
  var (first, second) = someCollection;
  
  // Zipping collections
  var zipped = collection1.Zip(collection2);
  
  // Dictionary creation
  var dict = new[] { ("key1", 1), ("key2", 2) }.ToDictionary();
  ```

#### Color Extensions
- `WithRed(g, b, a)`: Creates new color with modified red component
- `WithGreen(r, b, a)`: Creates new color with modified green component
- `WithBlue(r, g, a)`: Creates new color with modified blue component
- `WithAlpha(r, g, b)`: Creates new color with modified alpha

**Examples:**
```csharp
// Create a semi-transparent red
var red = Color.red.WithAlpha(0.5f);

// Create a dark blue
var darkBlue = Color.blue.WithBlue(0.5f);

// Create a custom color by chaining
var customColor = Color.white
    .WithRed(0.8f)
    .WithGreen(0.6f)
    .WithBlue(0.9f)
    .WithAlpha(0.7f);
```

#### GUI Scopes
- `ColorScope`: Temporarily change GUI color
- `BackgroundColorScope`: Temporarily change GUI background color

**Examples:**
```csharp
// Change text color temporarily
using (new GUIScopes.ColorScope(Color.red)) {
    GUILayout.Label("Warning: This is important!");
}

// Change background color temporarily
using (new GUIScopes.BackgroundColorScope(new Color(0.2f, 0.2f, 0.2f))) {
    GUILayout.Button("Dark Button");
}

// Nested scopes
using (new GUIScopes.ColorScope(Color.blue))
using (new GUIScopes.BackgroundColorScope(Color.white)) {
    GUILayout.Label("Blue text on white background");
}
```

#### GameObject Extensions
- `GetChildren()`: Returns all child GameObjects as enumerable
- `EnsureComponent<T>()`: Gets or adds component
- `DestroyAllChildren()`: Removes all child GameObjects
- `InLayerMask(LayerMask)`: Checks if the GameObject is in the specified layer mask

**Examples:**
```csharp
// Get all children
foreach (var child in gameObject.GetChildren()) {
    Debug.Log($"Child: {child.name}");
}

// Get or add a component (avokes GetComponent/AddComponent pattern)
var rigidbody = gameObject.EnsureComponent<Rigidbody>();

// Safe destruction with null check
gameObject.DestroyAllChildren();

// Check if object is in a specific layer
if (gameObject.InLayerMask(groundLayer)) {
    // Handle ground collision
}
```

#### Number Extensions
- `FloorToInt()`: Math.Floor as int
- `RoundToInt()`: Math.Round as int
- `CeilToInt()`: Math.Ceiling as int
- `Truncate()`: Truncates decimal places
- `Clamp(min, max)`: Clamps value between min and max

**Examples:**
```csharp
// Basic math operations
float number = 3.7f;
int floored = number.FloorToInt();  // 3
int rounded = number.RoundToInt();  // 4
int ceiled = number.CeilToInt();    // 4

// Truncation
float precise = 5.98765f;
float truncated = precise.Truncate(2);  // 5.98

// Clamping
int value = 15;
int clamped = value.Clamp(0, 10);  // 10

// Chaining
float result = 7.8f
    .FloorToInt()      // 7
    .Clamp(5, 10)      // 7
    .ToFloat();         // 7.0f
```

#### Random Extensions
- `RandomElement()`: Get random element from array/collection
- `RandomValue(Vector2/Vector2Int)`: Get random value in range
- `RandomValueInclusive(Vector2Int)`: Inclusive random int in range

**Examples:**
```csharp
// Get random element from array
string[] fruits = { "Apple", "Banana", "Cherry" };
string randomFruit = fruits.RandomElement();

// Get random value in range (float)
float randomFloat = new Vector2(1.5f, 5.5f).RandomValue();

// Get random int in range (exclusive)
int randomInt = new Vector2Int(1, 11).RandomValue();  // 1-10

// Get random int in range (inclusive)
int randomDice = new Vector2Int(1, 7).RandomValueInclusive();  // 1-6

// Get random element from List
List<GameObject> enemies = GetEnemies();
GameObject randomEnemy = enemies.RandomElement();

// Get random enum value
Difficulty randomDifficulty = Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>().RandomElement();
```

#### Transform Extensions
- `ResetLocal()`: Resets local position/rotation/scale
- `GetChildren()`: Gets all children as enumerable
- `DestroyChildren()`: Destroys all child objects

**Examples:**
```csharp
// Reset transform to identity
transform.ResetLocal();

// Iterate through all children
foreach (Transform child in transform.GetChildren()) {
    child.gameObject.SetActive(true);
}

// Destroy all children
transform.DestroyChildren();
```

#### Object Extensions
- `Apply<T, TR>(Func<T?, TR?> func)`: Applies a function to the object and returns the result

**Examples:**
```csharp
// Simple transformation
string name = "hello";
string upperName = name.Apply(s => s?.ToUpper());  // "HELLO"

// Null-conditional chaining
GameObject player = FindObjectOfType<Player>();
string playerName = player?.Apply(p => p.name.ToUpper()) ?? "No Player";

// Method chaining
float distance = player
    ?.Apply(p => p.transform.position)
    .Apply(pos => Vector3.Distance(Vector3.zero, pos)) ?? 0f;
```

#### Rect Extensions
- **Taking Parts of a Rect**
  - `TakeLeft(width)`: Takes left portion with specified width
  - `TakeRight(width)`: Takes right portion with specified width
  - `TakeTop(height)`: Takes top portion with specified height
  - `TakeBottom(height)`: Takes bottom portion with specified height
- **Trimming**
  - `TrimLeft(width)`, `TrimRight(width)`: Trims from sides
  - `TrimTop(height)`, `TrimBottom(height)`: Trims from top/bottom
  - `Shrink(amount)`: Shrinks from all sides
  - `ShrinkHorizontal(width)`, `ShrinkVertical(height)`: Shrinks from sides
- **Splitting**
  - `SplitWidth(width, index)`: Splits horizontally
  - `SplitHeight(height, index)`: Splits vertically

**Examples:**
```csharp
// Create a basic rect
var rect = new Rect(0, 0, 100, 50);

// Take portions
var leftSide = rect.TakeLeft(30);      // Rect from (0,0) to (30,50)
var rightSide = rect.TakeRight(30);    // Rect from (70,0) to (100,50)
var topBar = rect.TakeTop(10);         // Rect from (0,0) to (100,10)

// Trimming
var innerRect = rect.Shrink(10);       // Rect from (10,10) to (90,40)
var horizontal = rect.ShrinkVertical(5); // Keeps full width, trims 5 from top/bottom

// Splitting for UI layout
var columns = 3;
for (int i = 0; i < columns; i++) {
    var column = rect.SplitWidth(rect.width / columns, i);
    GUI.Button(column, $"Column {i + 1}");
}

// Complex layout
var header = rect.TakeTop(30);
var content = rect.TrimTop(30).Shrink(5);
var buttonArea = content.TakeBottom(25);

// Centered element
var buttonWidth = 100;
var buttonHeight = 30;
var buttonRect = new Rect(
    (rect.width - buttonWidth) / 2,
    (rect.height - buttonHeight) / 2,
    buttonWidth,
    buttonHeight
);
```

#### Task Extensions
- `Forget()`: Suppresses the "unawaited task" warning when you intentionally don't await a task
  ```csharp
  // Example:
  SomeAsyncMethod().Forget(); // No warning about not awaiting the task
  ```

#### Vector Extensions
- **Component Access/Modification**
  - `WithX/Y/Z()`: Returns new vector with modified component (works with Vector2, Vector2Int, Vector3, Vector3Int)
  - `WithXY/YZ/XZ()`: Returns new vector with multiple modified components
- **Vector Conversion**
  - `ToVector2XY()`, `ToVector2XZ()`, `ToVector2YZ()`: Converts from Vector3/Vector3Int to Vector2/Vector2Int using specified planes
  - `ToVector3XY(z)`, `ToVector3XZ(y)`, `ToVector3YZ(x)`: Converts from Vector2/Vector2Int to Vector3/Vector3Int with optional fill value
  - `FloorToVector2IntXY()`, `CeilToVector2IntXY()`, `RoundToVector2IntXY()`: Converts with different rounding modes (also XZ and YZ variants)
- **Range Checking**
  - `Contains(value)`: Checks if value is in range (exclusive)
  - `ContainsInclusive(value)`: Checks if value is in range (inclusive)

**Examples:**
```csharp
// Basic component modification
Vector3 position = transform.position;
Vector3 groundPosition = position.WithY(0);  // Flatten Y

// Chained modifications
Vector3 offset = Vector3.zero
    .WithX(5)          // (5, 0, 0)
    .WithY(10)         // (5, 10, 0)
    .WithZ(15);        // (5, 10, 15)

// Grid-based positioning
Vector3 worldPos = transform.position;
Vector2Int gridPos = worldPos.RoundToVector2IntXZ(); // Convert to grid coordinates

// Range checking (useful for validations)
Vector2 healthRange = new Vector2(0, 100);
float currentHealth = 75f;
bool isValidHealth = healthRange.Contains(currentHealth);  // true
bool isMaxHealth = healthRange.ContainsInclusive(100);      // true

// 3D to 2D conversions (useful for 2D games with 3D physics)
Vector3 worldPos = transform.position;
Vector2 screenPos = worldPos.ToVector2XY();  // Top-down view (drops Z)
Vector2 sideView = worldPos.ToVector2XZ();   // Side-scroller view (drops Y)

// 2D to 3D with elevation
Vector2 input = new Vector2(1, 1);
Vector3 worldInput = input.ToVector3XZ(0);  // Creates (1, 0, 1)

// Character movement with fixed Y (platformer)
Vector3 movement = new Vector2(Input.GetAxisRaw("Horizontal"), 0)
    .ToVector3XZ()
    .normalized * speed * Time.deltaTime;
```

### Model

#### ScriptableList
A powerful `ScriptableObject`-based list that provides a reorderable, serializable collection with editor integration. Automatically handles runtime vs. editor mode data separation to prevent accidental modification of serialized data during play mode.

**Key Features:**
- Reorderable list in the Unity inspector
- Automatic runtime data isolation (changes in play mode don't affect serialized data)
- Implements `IList<T>` for seamless integration with standard C# collections
- Editor utilities for marking objects dirty when modified

**Usage:**
```csharp
[CreateAssetMenu(menuName = "Game/Items/Item Database")]
public class ItemDatabase : ScriptableList<Item> {}
```

#### ScriptableSingleton
A robust singleton implementation for `ScriptableObject`s that ensures only one instance exists in the project. Can automatically create and save instances to specified paths.

**Key Features:**
- Automatic instance management
- Configurable save paths (Resources or Assets folder)
- Thread-safe initialization
- Prevents duplicate instances in the editor

**Usage:**
```csharp
[ScriptableSingletonPath("Settings/GameSettings", ScriptableSingletonPath.PathType.Resources)]
public class GameSettings : ScriptableSingleton<GameSettings> {
    public float musicVolume = 0.8f;
    public float sfxVolume = 1.0f;
}

// Access anywhere:
float volume = GameSettings.Instance.musicVolume;
```

#### SerializableHashSet
A fully serializable `HashSet<T>` implementation that works with Unity's serialization system. Perfect for when you need set operations with guaranteed unique elements that persist between editor sessions.

**Key Features:**
- Implements `ISet<T>` interface
- Supports all standard set operations (Union, Intersect, Except, etc.)
- Properly handles serialization/deserialization
- Maintains element uniqueness

**Usage:**
```csharp
[Serializable]
public class StringSet : SerializableHashSet<string> {}

// In your MonoBehaviour or ScriptableObject
[SerializeField] private StringSet uniqueTags = new();

void Start() {
    uniqueTags.Add("player");
    uniqueTags.Add("enemy");
    
    if (uniqueTags.Contains("player")) {
        // Do something with the tag
    }
}
```

### Utilities
- **DoNotDestroyOnLoad**: Simple Component to enable marking DontDestroyOnLoad from the inspector
- **Gizmo Utilities**: Adds the ability to emit and subscribe to events to draw gizmos
- **Gizmo View**: Custom Component-based Gizmo visualization tools

## Contributing
Contributions are welcome! Please feel free to submit a Pull Request.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
