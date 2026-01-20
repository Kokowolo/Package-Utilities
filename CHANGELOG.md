# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.0.145] - 2026-01-19
### Added
* `Alphabet` const string to `StringExtensions`

## [0.0.144] - 2026-01-16
### Added
* `BitmaskUtils.Contains` to check if a bit exists
### Changed
* `LayerMaskExtensions` to use `BitmaskUtils.Contains` when calculating `ContainsLayer`

## [0.0.143] - 2026-01-08
### Changed
* `InputManager` to be just `InputManager` and now require extending classes to override the `Instance` property
* singleton code to use the updated `FindAnyObjectByType` seen in the later versions of Unity
* `MonoBehaviourSingleton`'s `_Instance` field to be virtual

## [0.0.142] - 2025-12-22
### Fixed
* `InputManager` to have both a `InputManagerBase<T>` implementation and an `InputManager` to allow for `Singleton`

## [0.0.141] - 2025-12-22
### Added
* some test scripts and scene for `KokoMath` and general `Utilities`
### Changed
* all Utils class to be contained within Extension classes if there was an existing one
* `ReflectionUtils` to `ReflectionExtensions`
* `EditorUtils` to `EditorExtensions`
* `ScreenUtils` such that its class is easier to read
### Fixed
* `Kokowolo.Utilties` namespace not included in `BoundsIntExtensions`, `BoxCollider2DExtensions`, & `BoxColliderExtensions`

## [0.0.140] - 2025-12-19
### Added
* `ShowIf` `Attribute` and `PropertyDrawer` to show a specific member in the Editor if the condition is true
* `StringExtensions.Convert` methods to convert a value/string from a base-N number system 

## [0.0.139] - 2025-12-04
### Added
* overloaded methods to `ReflectionUtils`
### Changed
* `ReflectionUtils` to have optional params to allow for fetching the base class

## [0.0.138] - 2025-11-24
### Added
* `BoxCollider` and `BoxCollider2D` extensions scripts

## [0.0.137] - 2025-11-21
### Added
* safeguard for `Vector3Extensions.GetAxisValue()` and `Vector3Extensions.SetAxisValue()`

## [0.0.136] - 2025-10-29
### Added
* Copyright to `NewBehaviourSimple.cs.txt`
* Copyright to all scripts
* `#YEAR#` keyword to `KeywordReplace`

## [0.0.135] - 2025-10-28
### Added
* `Standalone` directory for any class that functions independently from other components/classes
* `Stat.cs` from previous Kokowolo projects
* `StatPropertyDrawer.cs` from previous Kokowolo (Editor) projects

## [0.0.134] - 2025-10-23
### Added
* `GC.SuppressFinalize(this);` to `IDisposable.Dispose()` within `Job`, `JobScheduler`, and `WaitForJob`

## [0.0.133] - 2025-10-21
### Added
* `GetStackTrace` to `Diagnostics`
* `Editor` only members to `Job` and `JobScheduler` to better draw `Job` objects within the `Editor`
### Changed
* `JobManagerEditor` to better draw `Job` objects within `the` Editor
* `ToString` method within `Job` and `JobScheduler`

## [0.0.132] - 2025-10-20
### Added
* `JobManagerEditor` within `JobManager.cs` to display the number of jobs in circulation for `JobManager`

## [0.0.131] - 2025-10-02
### Added
* overloaded function call to `EditorUtils`
* comments to `EditorUtils`
* `IListExtensions.GetRandomIndex`
### Changed
* call to `SerializedObject.Update` to `SerializedObject.UpdateIfRequiredOrScript` within `EditorUtils`

## [0.0.130] - 2025-09-17
### Fixed
* accidental `DOTween` reference include
* `Randomizer`'s `ISerializationCallbackReceiver` interface being included in builds

## [0.0.129] - 2025-09-03
### Added
* overloaded option to set the `JobScheduler` when adding/scheduling a new `Job` or `JobSequence`
* overloaded constructor for `Randomizer` to set its seed on init
* comment `JobSystem`'s get/set `JobScheduler` methods
### Changed
* `JobSystem`'s `Get()` to `Add()`
* `JobSystem`'s `WaitWhile()` to `AddWaitWhile()`

## [0.0.128] - 2025-08-29
### Added
* event `OnSeedSet` to `Randomizer`
* comment to `Randomizer.TryInit`

## [0.0.127] - 2025-07-31
### Added
* `KokoMath.Mod` for floats
* `Randomizer` to better manipulate calls to `UnityEngine.Random`

## [0.0.126] - 2025-07-30
### Added
* `FindFirstAssetByType` overloaded method to `EditorOnlyUtils`
### Changed
* `GetType(string typeName)` from `EditorUtils` to `EditorOnlyUtils`

## [0.0.125] - 2025-07-29
### Added
* `WorldCursor`, an instance focused evolution of `WorldCursorManager`
* additional information to `ScreenUtils` functions, better documenting the API
### Deprecated
* `WorldCursorManager` in favor of needing multiple `WorldCursors`

## [0.0.124] - 2025-07-27
### Added
* `AsyncOperationExtensions` to `Extensions`
### Changed 
* `ScriptableObjectSingleton` to initialize itself via the `EditorOnly` code first before checking the `PrefabManager`
* `ScriptTemplates` to longer use `#ROOTNAMESPACEBEGIN#` and `#ROOTNAMESPACEEND#`
* ordering of functions across all scripts

## [0.0.123] - 2025-07-25
### Added 
* static `TryGetComponent` to `WorldCursorManager`
* static `GetComponent` to `WorldCursorManager`
* EditorOnly initialization to `ScriptableObjectSingleton<T>` if `PrefabManager.Get<T>` fails
* `IntExtensions` for some mathematical extension functions
* `ClampIndex` and `ContainsIndex` to `IListExtensions`
### Changed
* `HasValidHitInfo` to `ValidHitInfo` in `WorldCursorManager`
* `PrefabManager` methods to assert that `T` is of type `UnityEngine.Object`
### Removed
* `DefaultExecutionOrder` from `WorldCursorManager`
### Fixed
* null reference bug within `PrefabManager.Get<T>`

## [0.0.122] - 2025-07-23
### Added 
* `OnFree` event to `JobScheduler`
* unique ids to `JobScheduler`, a ToString() method, and `IEquatable` interface with its methods 
* properties within `JobScheduler` and `JobSystem` to get the number of active `Job`s
* `StopAllJobs` to `JobScheduler`
* additional unit tests to verify `Scheduling` framework functionality 
### Fixed
* behavior within `JobManager` where a `JobScheduler` would be half-lazy-init (now fully lazy-init)
* bug within `WaitForJobScheduler` where a `JobScheduler` could be killed without `WaitForJobScheduler` handling 
### Removed
* `JobScheduler.Main` in favor of `JobSystem.GetScheduler()`
* `EventSystem` from `Scheduling`'s test scene

## [0.0.121] - 2025-07-23
### Added
* `Kokowolo.Utilities.Tests.Utils`
* `JobSystem` to replace JobManager as the core script controlling all things `Scheduling`
* `IsFree` to `JobSystem` which checks to see if all its `JobScheduler`s are free
* `SchedulersCount` to `JobSystem` to get the number of `JobScheduler`s
* lazy init to `JobManager`
* unit tests for multiple schedulers and lazy init
### Changed
* `JobManager` to be an internal class and hidden from the add component menu
* `MonoBehaviourSingleton` to have a `NOTE:` comment and a better `Header` for its Editor drawing
* `ScheduledEventManager`, `ScheduledEvent`, and prefab to be within `Runtime/Obsolete`
* `JobScheduler`'s constructor to be internal so `JobScheduler.Main` can never be a scheduler that is not `JobManager`'s
* unit test naming convention
* unit test function calling in `SetUp`, `TearDown`, etc.
### Removed
* `JobManager` prefab from package since `JobManager` now lazy inits
* `Utils` class within `Scheduling` test namespace in favor of its `TestController` and `Kokowolo.Utilities.Tests.Utils`

## [0.0.120] - 2025-07-22
### Added
* `JobScheduler` to replace `JobManager`'s code, allowing for `JobManager` to run concurrent `Job` lanes
* convenience function `Job.WaitForCompletion` which returns a `WaitForJob` `CustomYieldInstruction`
* `Scheduling.Utils.WaitWhile` to return an `IEnumerator` from a `WaitWhile`
* `Job.WaitWhile` to halt completion of a job until a `Func<bool>` parameter is false
* `DOTweenJob` script for `Job` and `JobSequence` `DOTween` `WaitWhile` functionality
* new tests to evaluate new `Job` system functionality and `DOTween` compatitbility
* `Abs(this float value)` extension to `FloatExtensions`
### Changed
* `JobManager` to run multiple `JobScheduler`s and listen to their `OnEnable`/`OnDispose` events
* `WaitForJobManager` to `WaitForJobScheduler`
* `JobSystem.IsFree` to `JobScheduler.Main.IsFree`
* `Job` and `JobSequence` to partial classes to allow for `DOTween` add ons to the classes
* `Scheduling.AdditionalTests` to `Scheduling.CoreTests`
* `Scheduling.Config` to be `Scheduling.TestController` and be a `MonoBehaviour` in the test scene
* `Analytics/Diagnostics` to use `Scheduling.Job` system
* `ScheduledEventManager` and `ScheduledEvent` directory to be within `Scripts/Scheduling/Obsolete`

## [0.0.119] - 2025-07-18
### Changed
* `JobManager`'s `IsFree` property to be static and updated all references

## [0.0.118] - 2025-07-18
### Added
* `EditorOnlyUtils` to Runtime Scripts
* replacement to `ScheduledEventManager`, i.e. the `Scheduling` namespace framework with most notibly `Scheduling.Job.Schedule(params)`
* `Tests/Runtime/Kokowolo.Utilities.Tests.asmdef` with working unit tests
* `Scheduling` namespace unit tests
* `JobManager` prefab
### Changed
* directory to `Editor/Scripts/ScriptTemplates` for all `ScriptTemplates` related Editor scripts
* directory of `ScheduledEventManager` to `Scripts/Scheduling`
### Deprecated
* `ScheduledEventManager` in favor of `Scheduling.Job` framework
### Removed
* `Rotator` scene within the `Tests` directory
### Fixed
* broken unit test architecture within the package
* `CHANGELOG.md` entry violations

## [0.0.117] - 2025-07-08
### Added
* `LoggerExtensions` to `LogManager` system
* overloaded methods to `LogManager`
* public setter for `LogManager.Profile`
* `DOTweenExtensions` to `Extensions` directory, which checks if `DOTween` is active in the project
* `ROOTNAMESPACEBEGIN` keyword to `ScriptTemplates`
### Changed
* `LogManager` and `LogManagerProfile` to adhere to modern Kokowolo scripting conventions
* `Kokowolo.Utilities.Analytics.dll`
* `WorldCursorManager` to check if its `visual` `GameObject` exists before setting its position
* some scripts, adhering to new scripting conventions (changed function orders, use of keyword `private`)

## [0.0.116] - 2025-07-07
### Changed
* `PrefabManager` to log an error if it cannot find a prefab
### Removed
* `Has<T>` function from `PrefabManager` since it was only checking if the reference existed

## [0.0.115] - 2025-07-04
### Added
* `AnimatorExtensions` along with its extension function `Play([...], bool prewarm)`

## [0.0.114] - 2025-07-01
### Added
* `GetPropertyType` to `SerializedPropertyExtensions` which gets the serialized type of a `SerializedProperty`

## [0.0.113] - 2025-06-30
### Added
* public `Editor.General.MenuItemPriority` constant field and updated any reference
* overloaded functions for `EditorUtils.DrawSerializeFields<T>`

## [0.0.112] - 2025-06-25
### Added
* `GetRandomElement` to `IListExtensions`

## [0.0.111] - 2025-06-08
### Changed
* `MenuItem` `priority` of `CreateNewMonoBehaviour`
* divider characters within `CreateNewMonoBehaviour`

## [0.0.110] - 2025-06-05
### Added 
* `ScriptableObjectSingleton` which uses `PrefabManager`
* `KokoRandom` to handle all functions relating to random
* shuffle parameter option to `IListExtensions.Shuffle`
* `TransformExtensions.GetComponentInHierarchy` to search for a component upwards in the hierarchy
* `Bezier.ConstructQuadraticPath`
### Changed
* directory for `General`, `Raycasting`, and `Singleton` scripts
* `MonoSingleton` to `MonoBehaviourSingleton` and updated all references
* `MathKoko` to `KokoMath`
* some of former `MathKoko`'s functions to now reside in new script `KokoRandom`
* `NewBehaviourSimple` and `NewBehaviourVerbose` ScriptTemplates to use new designs and dividers
* all script dividers to use new standard
* `IListExtensions.ToString` to not erroneously call `ToString` within its function
* `TransformExtensions`'s `GetGrandparent` to `GetHierarchyRoot`

## [0.0.109] - 2024-12-04
### Added
* `DebugUtils.DrawWireBox`

## [0.0.108] - 2024-12-02
### Added
* utility and extension method `ReduceBy` to `Vector3`, which reduces a vector's magnitude

## [0.0.107] - 2024-11-19
### Fixed
* `ScreenUtils.GetMouseWorldPoint` returning a `Vector2` as oppose to a `Vector3`

## [0.0.106] - 2024-11-13
### Added
* `GetGreatestCommonFactor` to `MathKoko`

## [0.0.105] - 2024-11-05
### Added
* `BoundsUtils` with `CreateBounds` to create a `Bounds` from a min position and a max size
* `Vector3Utils.Abs` to take the absolute value of a vector's components
* overloaded `DrawBounds` functions to `GizmosExtensions` and `DebugUtils`
* `forestGreen` color to `ColorUtils`
### Fixed
* bug within `GizmosExtensions` and `DebugUtils` where created `Bounds` was incorrect

## [0.0.104] - 2024-10-23
### Added
* `BoundsExtensions` to convert `Bounds` to `BoundsInt`

## [0.0.103] - 2024-10-21
### Added
* `DebugUtils` to draw custom shapes within the Editor
* `DrawBounds` to `GizmosExtensions`
### Changed
* `GizmosExtensions` to only compile its code when within the Editor

## [0.0.102] - 2024-10-18
### Added
* `BoundsIntExtensions` to include an `Encapsulate` function within `BoundsInt` 
* `EditorUtils.RenderStaticPreview` and `EditorUtils.GetType` from Unity's `RuleTile` script

## [0.0.101] - 2024-10-11
### Added
* overloaded `GetMouseWorldPoint` functions to `ScreenUtils`
### Fixed
* incorrect `ScreenUtils` depth calculation from `ScreenPointToWorld`

## [0.0.100] - 2024-09-19
### Added
* `SerializedPropertyExtensions` for changing a `SerializedProperty` into its owning class
### Changed
* `HeaderLineDecoratorDrawer`'s `GetHeight()` to be half of what it was

## [0.0.99] - 2024-09-12
### Fixed
* error within `MonoSingleton` where `Awake` would not get called if `FindInstance()` was called before it

## [0.0.98] - 2024-09-11
### Changed
* `Singleton.TrySet` to log a warning rather than error when passed a `null` instance 

## [0.0.97] - 2024-07-23
### Added
* `Color` property to `VirtualCursor`

## [0.0.96] - 2024-07-22
### Added
* `PropertyAttribute` `HeaderLine` to draw horizontal line dividers in the `Editor`
* `enum` `ColorEnum` to have a compile-time constant representation of `Color`
* `SetCursorWorldPosition` and `SetCursorScreenPosition` to `InputManager`
* basic outline for `VirtualCursor`
* convenience function `GetMouseScreenPoint` to `ScreenUtils` and also `ScreenPoint01ToScreenPoint`
* `ScreenUtils.ScreenPointToWorld`
* some convenience methods to `FloatExtensions`
### Changed
* `BaseInputManager` to `InputManager` and slightly refactored its class
* references to `ScreenPoint01` to `ScreenPointNormalized`
* `CursorWorldManager` to `WorldCursorManager` updating its script and prefab
* `WorldCursorManager.SetCursorPosition` to `SetCursorWorldPosition` and now uses `InputManager`
* `BaseInputManager.GetMouseWorldPosition` to `WorldCursorManager.GetMouseWorldPosition`
* `MonoSingleton`'s fields from `private` to `protected`
### Fixed
* incorrect naming for `ScheduledEventManager`'s prefab
* bug within `FloatExtensions.RemapTo01`

## [0.0.95] - 2024-07-14
### Added
* `SetComponentInNormalizedDirection` and `GetComponentInNormalizedDirection` to `Vector3Utils`

## [0.0.94] - 2024-07-03
### Added
* `Vector3IntExtensions.ToVector3`
* overloaded `QuaternionExtensions.ToDirectionVector`
* `Vector3.Utils.SetComponentInDirection`

## [0.0.93] - 2024-06-28
### Added
* `Axis.Opposite` which gets the oppose axis
### Removed
* `Tween.cs` from packge in favor of `DG.DoTween`

## [0.0.92] - 2024-06-26
### Added
* `DotInt` to `MathKoko` to get a dot product between vectors as an `int`

## [0.0.91] - 2024-06-20
### Added
* `ToVector3` and `ToVector3Int` to `Axis`
### Changed
* `AxisExtensions` methods to always return a value

## [0.0.90] - 2024-06-18
### Added
* `AxisExtensions` to `Axis.cs` to get the `Positive` and `Negative` `enum` from a defined `Axis`
* `GetAxisValue`, `SetAxisValue`, and `AddAxisValue` to `VectorExtensions`
### Changed
* `VectorUtils` and `VectorExtensions` to be broken up into respective `Vector3`, `Vector2`, `Vector3Int` classes

## [0.0.89] - 2024-06-17
### Added
* `GetMouseWorldPoint` to `ScreenUtils`
* `Enums` directory and `enum` `Axis`
### Fixed
* `GizmosExtensions.DrawBoxCast` where overloaded function did not include `maxDistance`

## [0.0.88] - 2024-06-12
### Added
* `ColorExtensions.ToNegative` to flip a color
* commented-out methods within `RectTransformExtensions`
* `WorldToScreenPoint` to `ScreenUtils`
### Fixed
* `EditorUtils` not saving after drawing properties
* `ScenesInBuildManager` being in the wrong namespace
* `SchedulingEventManager`'s prefab naming violation

## [0.0.87] - 2024-05-28
### Added
* `MathKoko.RemapFrom01`
* overloaded `MathKoko` `Remap` functions to allow for `Vector2` ranges
* extension `Remap` functions to `FloatExtensions`
* `ScreenUtils` for `Screen` utility functions
* `RectTransformExtensions` for `RectTransform` extensions
### Changed
* `MathKoko.Remap01` to `MathKoko.RemapTo01` and changed additional references (`Tween.cs`)
### Fixed
* error within `MathKoko.Remap` where it was not allowing negative range remapping

## [0.0.86] - 2024-05-01
### Added
* `lime` and `pink` to `ColorUtils`
### Fixed
* mistakenly left out parameter within `Singleton.Get<T>`

## [0.0.85] - 2024-04-27
### Added
* `FindInstance()` to `MonoSingleton`
* static readonly field `orange` to `ColorUtils`
### Changed
* `BaseInputManager` and `PrefabManager` to extend from `MonoSingleton`

## [0.0.84] - 2024-04-27
### Added
* `BitmaskUtils` to handle bitmask operations; probably should add `LayerMaskUtils`/`Extensions` code at some point
* `EditorUtils` to handle various `CustomEditor` utilities
* `ReflectionUtils` to handle various `Reflection` utilities and also as an index on how to use `Reflection`

## [0.0.83] - 2024-04-21
### Added
* `ToSceneEnum` extension function to `ScenesInBuildManager`'s auto-generated class

## [0.0.82] - 2024-04-21
### Added
* `IListExtensions` to expand accessibility of `ListExtensions`
* *Fisher-Yates* `Shuffle` method to `ListExtensions`/`IListExtensions`
### Changed
* `ScenesInBuild` auto-generated file to use a struct pointer to better serialize Scene data
* `ScenesInBuild` auto-generated file to use a build scene's GUID as its underlying serialization
### Fixed
* `ScenesInBuild` auto-generated file invalid `Kokowolo.Utilities.Editor` reference

## [0.0.81] - 2024-04-20
### Added
* some `EditorBuildSettingsScene` functions to `Utilities.Editor.General`
### Changed
* `ScenesInBuild` file making the script easier to read
* `ScenesInBuild` auto-generated file making its reserialize and retain its scene object better (not perfect though)

## [0.0.80] - 2024-04-19
### Added
* `EditorOnly` `PropertyDrawer` to `ScenesInBuildManager`'s auto-generated class
* SceneAttribute and SceneDrawer to draw strings within the Editor as Scenes
### Changed
* `ScenesInBuildManager` to work with `buildIndex`/`strings` as oppose to runtime `Scene` structs
* `ScenesInBuildManager`'s code, cleaning up the class a bit

## [0.0.79] - 2024-04-18
### Added
* `Kokowolo.Utilities.Editor.ScenesInBuildManager` which auto-generates a script file for the `Build Index`
* `Kokowolo.Utilities.Editor.ScenesInBuildManagerEditor` to draw a custom editor for `ScenesInBuildManager`
* `Kokowolo.Utilities.Editor.General` with function `GetPackageInfo`
* comment within `FloatExtensions` to indicate how to convert a `float` to a string

## [0.0.78] - 2024-04-12
### Added
* `GizmosExtensions.DrawBoxCast`
### Fixed
* `GameObjectExtensions` being in the wrong namespace and assembly

## [0.0.77] - 2024-04-09
### Added
* `LayerMaskExtensions.ContainsLayerMask` to remove ambiguity within the class's `Contains` functions
* `LayerMaskExtensions.ToLayer` allowing for conversion from `LayerMask` to `Layer`
* `LayerMaskUtils` to perform various `LayerMask` operations and conversions
### Changed 
* `LayerAttribute` to only work on an `Integer` and `LayerMask` `SerializedProperty`
* `LayerMaskExtensions.Contains` to `ContainsLayer` and `ContainsGameObject` to remove ambiguity
* `GameObjectExtensions` to match aforementioned changes
### Fixed
* missing namespace from `LayerMaskExtensions`

## [0.0.76] - 2024-04-08
### Added 
* `GizmosExtensions` to provide additional options for drawing `Gizmos`
* `Editor/Scripts/Drawer` directory to store `PropertyDrawer`s
* `LayerAttribute` & `LayerAttributeDrawer` to force a `LayerMask` to only have one `Layer` from the `Editor`
### Changed
* `Editor` scripts directory from `Editor/` to `Editor/Scripts`

## [0.0.75] - 2024-03-31
### Added
* `VectorUtils.GetDirection` & `GetDirectionNormalized` (because I never want to use my brain again)
### Changed
* `VectorUtils.GetVectorComponentInDirection` to VectorUtils.GetComponentInDirection & its parameter names

## [0.0.74] - 2024-03-26
### Added
* again the function `General.CacheGetComponent<T>`; changed decision on whether it should be deprecated

## [0.0.73] - 2024-03-21
### Added
* `VectorExtensions.ToVector3(Vector2)` as a convenience casting method
### Changed
* whitespace indentation within `ScriptTemplate` files

## [0.0.72] - 2024-03-19
### Removed
* `RuntimeTests` assembly definition file to remove warning

## [0.0.71] - 2024-03-18
### Added
* `VectorExtensions` class, including extension function `ToVector3Int()`
* `VectorUtils` class from Project-HAT with a couple of utility functions
### Deprecated
* `General.CacheGetComponent<T>` as writing an encapsulated property is easier to read

## [0.0.70] - 2024-03-03
### Added
* `LayerMaskExtensions` file for `LayerMask` convenience functions
### Deprecated
* `GameObjectExtensions.IsInLayerMask(LayerMask)` in favor of `LayerMaskExtensions.Contains(GameObject)`

## [0.0.69] - 2024-02-26
### Added
* `GraphicsSettingsManager` toggle for the last graphics setting that was selected (cleared whenever there's a domain reload)
### Removed
* whitespace from `ScriptTemplates` files
### Fixed
* `GraphicsSettingsManager`'s incorrect ordering of graphics options
* `ReadOnlyAttribute` being in the wrong folder, moved from `Editor` to `Scripts/Attributes`

## [0.0.68] - 2024-02-25
### Changed
* naming conventions for existing assets to adhere to new Kokowolo naming
* `CursorManager` to be `CursorWorldManager` to prepare for upcoming `CursorVirtualManager`
* `PrefabManager` prefab, hopefully solving its serialization errors that it was throwing

## [0.0.67] - 2024-02-11
### Added
* `LayoutGroup3D` from `Project-LÜTRO`
* `QuaternionExtensions` with its function `ToDirectionVector`
* `Ballistics` from `Project-LÜTRO` to Math dir
* `GetLastValue` and `GetCount` `EnumUtils`
### Changed
* `EnumUtils`'s `GetValues` to return `T[]` as oppose to `IEnumerable<T>`

## [0.0.66] - 2024-01-29
### Added
* `TransformExtensions`.`TransformRotation` and `InverseTransformRotation` to convert a rotation from one transform to another

## [0.0.65] - 2024-01-28
### Added
* `TransformExtensions`.`SetLossyScale` effectively adding a set method to `transform`.`lossyScale`
### Changed
* `General`.`RecursiveFind` to `TransformExtensions`.`FindRecursively` 

## [0.0.64] - 2024-01-13
### Added
* event `OnHitInfoTransformChanged` to `CursorManager`
### Changed
* input listening on `SimpleCameraController` to respond to when the left alt button is pressed
* `SimpleCameraController` to have a debug setting for `canQuitApplication`

## [0.0.63] - 2023-12-19
### Added
* `IPoolable` base interface to `IPoolable<T>`
* `IPoolable.GenerateInstanceFrom` which uses an instance to generate a new instance from `PoolSystem` unless overridden
* `IPoolable.Release` which auto adds a `IPoolable` to `PoolSystem` unless overridden
### Changed
* `IPoolable<T>` to enforce that it inherits from `IPoolable` where `T` is `IPoolable<T>`

## [0.0.62] - 2023-12-18
### Fixed
* error caused by updating Unity to `2022.3.15f1` found within `KeywordReplace.OnWillCreateAsset`

## [0.0.61] - 2023-11-11
### Added 
* `SetCursorPosition` to CursorManager to set the position of the cursor, and increased the complexity of the manager a little
### Changed
* `BaseInputManager` to use `Input.mousePosition` over `InputSystem` due to the bug in `Mouse.current.WarpCursorPosition(Vector2)`

## [0.0.60] - 2023-11-01
### Added
* `LineRendererExtensions` to handle a list when calling `SetPositions`
* groundwork within `LineRendererExtensions` for converting a line to Bezier as commented-out code, but it'll likely be removed
* `GetGrandparent` to `TransformExtensions` to get the highest parent within the current hierarchy (which might be self)

## [0.0.59] - 2023-10-29
### Added
* cancelable events to `SchedulingManager` (now `ScheduledEventManager`)
* `Tween` script that uses the new `ScheduledEventManager` to Lerp and logarithmically Lerp any float
### Changed
* `ScriptTemplates`, updating their base outline
* `SchedulingManager` to `ScheduledEventManager`
### Removed
* `CreateNewMonoBehaviour`'s Verbose script option within the Editor

## [0.0.58] - 2023-10-21
### Added
* `ListPool.Release<T>(ref _)` to avoid mistakingly re-assigning a list in the future
* `TransformFacer` and moved related `CameraFacer`'s code to the class
### Changed
* `CameraFacer` to extend from `TransformFacer`

## [0.0.57] - 2023-09-10
### Changed
* `KokowoloUtilitiesProject` folder name to `Kokowolo`.`Utilities`
* `LogLibrary` directory and dll name to `Kokowolo`.`Utilities`.`Analytics`

## [0.0.56] - 2023-09-07
### Added
* `virtual` `OnLoad` function to `LogManagerProfile` that gets called when `LogManager` initializes
### Changed
* `LogManager`.`TryInitializeUnityLogger` to `TryInitialize` which now inits both `unityLogger` and `LogManagerProfile`

## [0.0.55] - 2023-09-06
### Added
* `LogManager`.`TryInitializeUnityLogger` to ensure that `unityLogger` is initialized before it is replaced
### Changed
* `LogManager`.`LogManagerProfileString` from `public` to `internal`
* `LogManager`.`LogManagerProfile` from `private` to `public`
* `LogManager`.`defaultLogger` to `unityLogger` and exposed it via `public` property `UnityLogger`
* `LogManagerProfile`.`ThrowWhenLoggingException` default value to `false`
### Fixed
* error when setting `Debug`.`unityLogger` where the logger would not get set properly

## [0.0.54] - 2023-09-05
### Added
* `Runtime/Scripts/Analytics/LogLibrary.dll` which contains `LogManager` and the new `LogManagerProfile`
* colorized logging options and the ability to log to various `LogHandler`s through the `LogManager`
* commented-out code to `ColorUtils` and `ColorExtensions` to serve as a reminder to where some useful code lies
* `Exception` logging to `LogManager` where the class can now log and throw `Exception`s given to it
### Changed
* `Runtime/Scripts/Diagnostics` to `Runtime/Scripts/Analytics`
### Removed
* `LogManager.cs` from the project since it is now included within the `Runtime/Scripts/Analytics/LogLibrary.dll`

## [0.0.53] - 2023-08-31
### Added
* `SchedulingManager`.`IsApplicationQuitting` to detect if the game/app is quitting (might relocate property in the future)

## [0.0.52] - 2023-08-30
### Changed
* `SchedulingManager`.`AddAsynchronousEvent` such that it triggers `function` next frame even when `time <= 0`

## [0.0.51] - 2023-08-24
### Added
* functionality to time `Coroutines` within `Diagnostics`
* optional parameter to `ListExtensions.ToString` to allow for elements to be printed on new lines
### Changed
* Diagnostics to be more readable/concise

## [0.0.50] - 2023-08-16
### Changed
* naming of `Math` to `MathKoko` to avoid conflict with `System`.`Math`

## [0.0.49] - 2023-08-11
### Added
* mathematically correct modulus function to `Math`

## [0.0.48] - 2023-07-22
### Added
* `SchedulingManager` to asynchronously and synchronously schedule functions and routines
* `SchedulingManager` `MonoSingleton` prefab 

## [0.0.47] - 2023-07-10
### Added
* `HasIPoolable<T>` to `PoolSystem` to check if Poolable exists
* `Has<T>` to `PrefabManager` to check if prefab exists
* arguments option to `IPoolable`'s static `Create()` within the PoolSystem
* null check within `ListPool`.`Add<T>`

## [0.0.46] - 2023-06-16
### Added
* description containers to `PrefabManager` allowing for descriptive bundling of Prefabs
* optional parameters to `IPoolable`'s `static` `Create()` and `PoolSystem`'s `CreateIPoolable()`
### Fixed
* incorrect optional parameter handling within `IPoolable` and `PoolSystem`

## [0.0.45] - 2023-06-15
### Added
* slight optimization to `ListPool`
* optional parameters to `IPoolable`
* optional parameter handling to `PoolSystem`
### Changed
* `CursorManager.Update` to `CursorManager.LateUpdate`

## [0.0.44] - 2023-04-12
### Added
* `PoolSystem` and `IPoolable<T>` which serves as a similar framework to `ListPool`
### Changed
* `ListPool` directory to Pooling
* `ListPool`.`Release` to `Add`

## [0.0.43] - 2023-04-06
### Added
* reference NOTE comment to `ListPool`
* optional parameters to `Math`.`Peturb` to apply/restrict noise in given axes
* convenience extension method to `ColorExtensions` to change the alpha value

## [0.0.42] - 2023-03-22
### Added
* `FileManager` from Project-Lich

## [0.0.41] - 2023-03-13
### Added
* `TransformExtensions`.`DestroyImmediateChildren` (which is useful for UI transforms and their items)

## [0.0.40] - 2023-03-12
### Added
* `Math`.`GetRadiansOnUnitCircle` which converts a `Vector2` from x, y to a value `[0, 2π]` on the unit circle
### Changed
* `Math`.`GetPointOnCircle` to match the values on the unit circle
* `Math`.`TryProbabilityOfSuccess` to `Math`.`TryChanceOfSuccess`
### Fixed
* `Math`.`TryChanceOfSuccess` always returning `true`
* clamped `Bezier`.`GetQuadraticDerivative` returning an incorrect value

## [0.0.39] - 2023-03-03
* `MonoSingleton_OnEnable` to `MonoSingleton` to remove confusion as to specifically specify what its API supports
### Changed
* `ListExtensions`.`ToStringForElements<T>` to `ToString<T>` to adhere to how most `ToString` implementations operate
* `CursorManager` to a `MonoSingleton` and set its `DefaultExecutionOrder` to -100
### Fixed
* `MonoSingleton`'s `OnDisable` `private` declaration to `protected` so it'll thrown an error now when an extending class uses it

## [0.0.38] - 2023-03-02
### Changed
* `General`.`IsGameObjectInLayerMask` to `GameObjectExtensions`.`IsInLayerMask`
* `GameObjectExtensions`.`IsGameObjectTheOriginalPrefab` to `GameObjectExtensions`.`IsTheOriginalPrefab`

## [0.0.37] - 2023-03-01
### Added
* static `Diagnostics` class for diagnostics functions like `TimeFunctionWithStopwatch`
### Changed
* directory for `LogManager` to Diagnostics

## [0.0.36] - 2023-02-28
### Added
* `Cast<TIn, TOut>()` to`ListExtensions` to convert a `List` from one type to another
### Removed
* `IsReady` property from `Singleton<T>`

## [0.0.35] - 2023-02-23
### Added
* `IsReady` property to `MonoSingleton<T>`
* unparenting code and optional parameter to `Singleton` and `MonoSingleton<T>`
* unparenting code and optional parameter to `DontDestroyOnLoadScript`
### Changed
* `DontDestroyOnLoadScript`'s parameter names and uses
* `Singleton`'s `Destroy()` call back to `DestroyImmediate()`
* formatting of CHANGELOG's references to code
### Fixed
* bug where `DontDestroyOnLoadScript` was not removing singleton instance names after destruction

## [0.0.34] - 2023-02-18
### Added
* `Math.WrapClamp01`, `Math.WrapClamp(float, float, float)`, and `Math.WrapClamp(int, int, int)`
* `Math.GetPointOnCircle` which gets a point on a circle of size `radius` in direction `normal` given a value `t`
### Changed
* changed package.json to have the preview tag
* `MonoSingleton<T>` API to be a little more convenient and intuitive to use
* `Math.Normalize` to `Math.Remap01`
### Fixed
* small bug within Math.Remap where the function assumed the min and max parameters

## [0.0.33] - 2023-02-16
### Added
* `CursorManager` prefab and script to Utilities' Managers
* `MonoSingleton<T>` which serves as an intermediate abstract class to handle Singleton code and redundancy 
* comment to `GameObjectExtensions` that may be useful later in improving the method
### Changed
* `ListPool<T>` to `ListPool` adding an internal Subclass that handles the generic typing of the given lists
### Fixed
* naming convention within the `Utilities`.`RuntimeTests` asmdef file

## [0.0.32] - 2023-01-29
### Changed
* `GameObjectExtensions.IsPrefab` to `IsGameObjectTheOriginalPrefab` to reduce confusion from `PrefabUtility` namespace

## [0.0.31] - 2023-01-26
### Changed
* `Singleton`.`Get<T>'`s defult parameter value from `true` to `false`
* `BaseInputManager`'s and `PrefabManager`'s `Singleton` settings back to allow them to call `FindObjectOfType` `from Get<T>()`

## [0.0.30] - 2023-01-25
### Added
* setting field to `DontDestroyOnLoadScript` to determine whether only one `DontDestroyOnLoadScript` GameObject can exist w/ its name
### Changed
* `DontDestroyOnLoadScript` to track unique singleton instance `DontDestroyOnLoadScript` GameObjects

## [0.0.29] - 2023-01-24
### Added
* a simple `DontDestroyOnLoadScript` to `Components` folder
* `BaseInputManager`'s and `PrefabManager`'s `Singleton` settings and prevented them from calling `FindObjectOfType`
* comment to ListExtensions containing useful code
### Changed
* `Singleton<T>` to static `Singleton` where it now uses a private subclass, `SingletonInstance<T>`, to handle instance data
* `Singleton`'s default parameters, switching `dontDestroyOnLoad` from `true` to `false`
* `Singleton`'s log messaging and severity
* `Singleton` from calling `DestroyImmediate` to `Destroy`
* `BaseInputManager`'s and `PrefabManager`'s scripts to handle to the new `Singleton` class structure

## [0.0.28] - 2023-01-23
### Added
* `ColorExtensions` class to add HDR intensity to a color
* namespace `Kokowolo.Utilities` to all classes within the `Extensions` folder
* class `ColorUtils` to get an HDR color with intensity 
* `ReadOnlyAttribute` and `ReadOnlyDrawer` to show semi-non-editable data within the Unity Editor
### Changed
* `CreateNewGameObjectDivider` to only add dividers with the `EditorOnly` tag

## [0.0.27] - 2023-01-17
### Added
* `GameObjectExtensions` class to `Extensions` directory
### Changed
* `TransformExtensions`'s parameter name to match its type
### Fixed
* warning within `CameraFacer` by renaming camera field

## [0.0.26] - 2022-11-29
### Added
* `ListExtensions` function to get the ToString of all elements in list
### Changed
* `OrbitScript` such that all of its fields are public

## [0.0.25] - 2022-11-16
### Added
* `ListExtensions` to `Extensions` directory
* extension method to `List<T>` to swap `List<T>` indices
### Changed
* `BaseInputManager` to have an exposed `IsMouseOverUI()` as well as handle when `UnityEngine.InputSystem` is over UI

## [0.0.24] - 2022-11-15
### Added
* `LogManager` and foundational logging code to handle `Kokowolo` `Diagnostics`
### Changed
* `Debug.Log` calls to use `LogManager` instead

## [0.0.23] - 2022-11-08
### Changed
* `Singleton<T>.Get()` to have optional parameter `findObjectOfType` so that `Singleton<T>.Get()` can properly return null
* `Singleton<T>.Set(T)` to handle when `Singleton<T>.instance` and given parameter instance is null
### Fixed
* `InputSystem` bug where `ENABLE_INPUT_SYSTEM_PACKAGE` compilation directive was being used incorrectly

## [0.0.22] - 2022-10-31
### Changed
* `InputManager` to `BaseInputManager` since `InputManager` is already used by `UnityEngine.InputSystem`

## [0.0.21] - 2022-10-20
### Added
* public to `OrbitCamera.target` to expose the field
### Changed
* `CameraFacer.mainCamera` to camera and made the field public & in the event that field is not set, a warning is logged
### Fixed
* `General.IsGameObjectInLayerMask` function being private instead of public

## [0.0.20] - 2022-10-18
### Added
* `Raycasting.cs` to hold all `Raycast` code
* function to `Raycast` from an origin to a destination; `RaycastToDestinationPoint()`
* `InputManager` script to serve as a base manager for all `Input` code/classes
* `General.IsGameObjectInLayerMask()` to check if a `GameObject` is within a given `LayerMask`
### Removed
* any `Raycast` code from `General.cs`

## [0.0.19] - 2022-09-19
### Added
* two additional functions `CreateNewScript` and removed confusing code to allow for creation of a `ScriptTemplate` files
* Extensions folder, creating several script `Extension` files for more core `UnityEngine/C#` functionality
### Changed
* `.gitignore` file, updated it to current Kokowolo standard
* current `ScriptTemplate` files, updating to current Kokowolo standard
* `EnumUtils`, separating its functionality between its `Utils` and `Extension` functions
* location for `Utilities` script files, better organizing them
### Removed
* obsolete older file directories

## [0.0.18] - 2022-09-07
### Added
* `DefaultExecutionOrder` attribute to `PrefabManager` with value of `-100`
### Changed
* log message in `Singleton<T>.Get()` and now has slightly more information
### Removed
* test `PrefabManager` scene

## [0.0.17] - 2022-08-31
### Added
* default parameter to `Singleton<T>.Get()`
* `GetValues<T>` to `EnumUtils`
### Removed
* erroneous code from within `CameraFacer`
### Fixed
* `Kokowolo.Utilities` namespace within `OrbitCamera`, `OrbitScript`, `Rotator`, and `EnumUtils`

## [0.0.16] - 2022-08-29
### Added
* `PrefabManager` class to handle static serialization of any `UnityEngine.Object` prefabs
* `RuntimeTests` `PrefabManager` scene
* PrefabManager prefab
### Changed
* `Math.GetPercentRoll` to `TryProbabilityOfSuccess` where its parameter now is between `0` and `1`
* warning to regular log message in `Singleton` class for calling `Get()` before `Set()`

## [0.0.15] - 2022-07-28
### Added
* `CreateNewGameObjectDivider` class for creating empty `GameObject` dividers in the Editor's hierarchy
### Changed
* `CreateNewScript` and `CreateNewMonoBehaviour` are now static classes

## [0.0.14] - 2022-06-29
### Added
* `EnumUtils` containing basic functionality for getting a string potentially associated to an `Enum`
### Changed
* `Rotator` class's speed field from private to public
* `ListPool.Add` to `ListPool.Release`
### Deprecated
* `Singleton.Release()` as the singleton `MonoBehaviour` should be destroyed like any other `GameObject`

## [0.0.13] - 2022-05-31
### Added
* `Rotator` class for rotating a transform
### Fixed
* `Singleton` class calling `DestroyImmediate()` on current instance multiple `Set()` occurred

## [0.0.12] - 2022-05-27
### Added
* `OrbitCamera` `Distance` and `Target` properties to edit class's fields
* `General.MouseScreenPointToRaycastHit()`
* warning logs to Singleton when dangerous functionality is run
* `RuntimeTests` asmdef files
### Changed
* `changelogUrl` within `package.json` file
* `Math` properties to adhere to current naming convention
### Deprecated
* `General.MouseScreenPointToRay()` in favor of `General.MouseScreenPointToRaycastHit()`
### Removed
* erroneous inclusion of removed textures (now in VFX package)
### Fixed
* asmdef files including Editor only files in build
* `General.CacheGetComponent` bug where input parameter did not include ref keyword
* typos within `General.cs`

## [0.0.11] - 2022-05-19
### Changed
* `CHANGELOG.md` due to missed documentation of features added in v0.0.10
### Removed
* `Debug.Log` from `General.RecursiveFind()`
### Fixed
* incorrect `GraphicsSettingsManager.cs` file location to Editor folder

## [0.0.10] - 2022-05-17
### Added
* `General.RecursiveFind(string)` function to recursively search for a grandchild by name n
### Fixed
* `OrbitCamera` syntax bug

## [0.0.9] - 2022-05-17
### Added
* default target position value for `OrbitCamera` in the event that the target transform is not set
* `Math.GetPercentRoll(float)` function to check if a percent chance roll event occurs

## [0.0.8] - 2022-05-11
### Added
* hexagons texture
* temp function under `Utilities.General`
* `GraphicsSettingsManager` Editor script
* `Circle` and `Pixel` default Sprites (might need to be moved into `Images/Sprites` folder in the future)
* functionality for `OrbitCamera` to have no target transform (probably should cache this default value)
### Fixed
* odd hierarchy and default transform for `OrbitCamera` prefab
* erroneous `MonoBehaviour` declaration within `NewMonoBehaviour.cs`

## [0.0.7] - 2022-05-09
### Added
* new `WorldSpaceDisplay` class with an associated display `Prefab` (see class for more info)
* new input to `OrbitCamera` which allows for a user to change the distance of the orbit
* functionality to hide mouse when using the `OrbitCamera` class/prefab
### Changed
* `OrbitScript` class to orbit around a target transform using an orbit distance and speed
### Removed
* `OrbitScript` orbiting function where relative rotation would be a factor of the orbiting

## [0.0.6] - 2022-05-03
### Added
* new folder for scripts called `Additional`, which will include helper utility scripts
* helper script `OrbitScript.cs` which is a simple example orbit script
* helper script `OrbitCamera.cs` which orbits the camera around a target `Transform` w/ user input
* `Orbit Camera` prefab 
### Changed
* comments for `Math.cs`
* location of `CameraFacer.cs` to `Additional`

## [0.0.5] - 2022-05-03
### Added
* `CameraFacer` class to force a `GameObject`'s orientation towards a `Camera`
* `GetCacheComponent()` function to `General.cs`
* `DontDestroyOnLoad` option for `Singleton` class
### Changed
* `ScriptTemplates` for generating a new `Script`
* `.asmdef` files to account for better naming conventions
* simplified `Math.cs`'s `Perturb` functions
### Removed
* `Rendering` folder from `Utilities`

## [0.0.4] - 2022-04-29
### Added
* `Singleton` class to handle singleton instance management
### Changed
* `Runtime` `.asmdef` file to include `Cinemachine` and `TextMeshPro` dependencies
* `GenUtils` class to `General`
* `MathUtils` class to `Math`
* `Bezier` functions to better indicate functionality
### Removed
* dependency on `Unity Recorder package`

## [0.0.3] - 2022-04-27
### Removed
* `URP` settings; new projects can use the `URP Templated` settings

## [0.0.2] - 2022-04-26
### Added
* `Script Templates Folder`
* functionality to create new C# scripts that adhere to Kokowolo Coding Conventions

## [0.0.1] - 2022-04-25
### Added
* initial commit of package

