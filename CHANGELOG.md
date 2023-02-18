# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.0.34] - 2023-02-18
### Added
* Math.WrapClamp01, Math.WrapClamp(float, float, float), and Math.WrapClamp(int, int, int)
* Math.GetPointOnCircle which gets a point on a circle of size `radius` in direction `normal` given a value `t`
### Changed
* changed package.json to have the preview tag
* MonoSingleton<T> API to be a little more convenient and intuitive to use
* Math.Normalize to Math.Remap01
### Fixed
* small bug within Math.Remap where the function assumed the min and max parameters

## [0.0.33] - 2023-02-16
### Added
* CursorManager prefab and script to Utilities' Managers
* MonoSingleton<T> which serves as an intermediate abstract class to handle Singleton code and redundancy 
* comment to GameObjectExtensions that may be useful later in improving the method
### Changed
* ListPool<T> to ListPool adding an internal Subclass that handles the generic typing of the given lists
### Fixed
* naming convention within the Utilities.RuntimeTests asmdef file

## [0.0.32] - 2023-01-29
### Changed
* `GameObjectExtensions.IsPrefab` to `IsGameObjectTheOriginalPrefab` to reduce confusion from `PrefabUtility` namespace

## [0.0.31] - 2023-01-26
### Changed
* Singleton.Get<T>'s defult parameter value from `true` to `false`
* BaseInputManager's and PrefabManager's Singleton settings back to allow them to call FindObjectOfType `from Get<T>()`

## [0.0.30] - 2023-01-25
### Added
* setting field to DontDestroyOnLoadScript to determine whether only one DontDestroyOnLoadScript GameObject can exist with its name
### Changed
* DontDestroyOnLoadScript to track unique singleton instance DontDestroyOnLoadScript GameObjects

## [0.0.29] - 2023-01-24
### Added
* a simple DontDestroyOnLoadScript to Components folder
* BaseInputManager's and PrefabManager's Singleton settings and prevented them from calling FindObjectOfType
* comment to ListExtensions containing useful code
### Changed
* Singleton<T> to static Singleton where it now uses a private subclass, SingletonInstance<T>, to handle instance data
* Singleton's default parameters, switching dontDestroyOnLoad from `true` to `false`
* Singleton's log messaging and severity
* Singleton from calling DestroyImmediate to Destroy
* BaseInputManager's and PrefabManager's scripts to handle to the new Singleton class structure

## [0.0.28] - 2023-01-23
### Added
* ColorExtensions class to add HDR intensity to a color
* namespace Kokowolo.Utilities to all classes within the Extensions folder
* class ColorUtils to get an HDR color with intensity 
* ReadOnlyAttribute and ReadOnlyDrawer to show semi-non-editable data within the Unity Editor
### Changed
* CreateNewGameObjectDivider to only add dividers with the `EditorOnly` tag

## [0.0.27] - 2023-01-17
### Added
* GameObjectExtensions class to Extensions directory
### Changed
* TransformExtensions's parameter name to match its type
### Fixed
* warning within CameraFacer by renaming camera field

## [0.0.26] - 2022-11-29
### Added
* ListExtensions function to get the ToString of all elements in list
### Changed
* OrbitScript such that all of its fields are public

## [0.0.25] - 2022-11-16
### Added
* ListExtensions to Extensions directory
* extension method to List<T> to swap List<T> indices
### Changed
* BaseInputManager to have an exposed IsMouseOverUI() as well as handle when UnityEngine.InputSystem is over UI

## [0.0.24] - 2022-11-15
### Added
* LogManager and foundational logging code to handle Kokowolo Diagnostics
### Changed
* Debug.Log calls to use LogManager instead

## [0.0.23] - 2022-11-08
### Changed
* Singleton<T>.Get() to have optional parameter findObjectOfType so that Singleton<T>.Get() can properly return null
* Singleton<T>.Set(T) to handle when Singleton<T>.instance and given parameter instance is null
### Fixed
* InputSystem bug where ENABLE_INPUT_SYSTEM_PACKAGE compilation directive was being used incorrectly

## [0.0.22] - 2022-10-31
### Changed
* InputManager to BaseInputManager since InputManager is already used by UnityEngine.InputSystem

## [0.0.21] - 2022-10-20
### Added
* public to OrbitCamera.target to expose the field
### Changed
* CameraFacer.mainCamera to camera and made the field public & in the event that field is not set, a warning is logged
### Fixed
* General.IsGameObjectInLayerMask function being private instead of public

## [0.0.20] - 2022-10-18
### Added
* Raycasting.cs to hold all Raycast code
* function to Raycast from an origin to a destination; RaycastToDestinationPoint()
* InputManager script to serve as a base manager for all Input code/classes
* General.IsGameObjectInLayerMask() to check if a GameObject is within a given LayerMask
### Removed
* any Raycast code from General.cs

## [0.0.19] - 2022-09-19
### Added
* two additional functions CreateNewScript and removed confusing code to allow for creation of a ScriptTemplate files
* Extensions folder, creating several script Extension files for more core UnityEngine/C# functionality
### Changed
* .gitignore file, updated it to current Kokowolo standard
* current ScriptTemplate files, updating to current Kokowolo standard
* EnumUtils, separating its functionality between its Utils and Extension functions
* location for Utilities script files, better organizing them
### Removed
* obsolete older file directories

## [0.0.18] - 2022-09-07
### Added
* DefaultExecutionOrder attribute to PrefabManager with value of -100
### Changed
* log message in Singleton<T>.Get() and now has slightly more information
### Removed
* test PrefabManager scene

## [0.0.17] - 2022-08-31
### Added
* default parameter to Singleton<T>.Get()
* GetValues<T> to EnumUtils
### Removed
* erroneous code from within CameraFacer
### Fixed
* Kokowolo.Utilities namespace within OrbitCamera, OrbitScript, Rotator, and EnumUtils

## [0.0.16] - 2022-08-29
### Added
* PrefabManager class to handle static serialization of any UnityEngine.Object prefabs
* RuntimeTests PrefabManager scene
* PrefabManager prefab
### Changed
* Math.GetPercentRoll to TryProbabilityOfSuccess where its parameter now is between 0 and 1
* warning to regular log message in Singleton class for calling Get() before Set()

## [0.0.15] - 2022-07-28
### Added
* CreateNewGameObjectDivider class for creating empty GameObject dividers in the Editor's Hierarchy
### Changed
* CreateNewScript and CreateNewMonoBehaviour are now static classes

## [0.0.14] - 2022-06-29
### Added
* EnumUtils containing basic functionality for getting a string potentially associated to an Enum
### Changed
* Rotator class's speed field from private to public
* ListPool.Add to ListPool.Release
### Deprecated
* Singleton.Release() as the singleton MonoBehaviour should be destroyed like any other GameObject

## [0.0.13] - 2022-05-31
### Added
* Rotator class for rotating a transform
### Fixed
* Singleton class calling DestroyImmediate() on current instance multiple Set() occurred

## [0.0.12] - 2022-05-27
### Added
* OrbitCamera Distance and Target properties to edit class's fields
* General.MouseScreenPointToRaycastHit()
* warning logs to Singleton when dangerous functionality is run
* RuntimeTests asmdef files
### Changed
* changelogUrl within package.json file
* Math properties to adhere to current naming convention
### Deprecated
* General.MouseScreenPointToRay() in favor of General.MouseScreenPointToRaycastHit()
### Removed
* erroneous inclusion of removed textures (now in VFX package)
### Fixed
* asmdef files including Editor only files in build
* General.CacheGetComponent bug where input parameter did not include ref keyword
* typos within General.cs

## [0.0.11] - 2022-05-19
### Changed
* CHANGELOG.md due to missed documentation of features added in v0.0.10
### Removed
* Debug.Log from General.RecursiveFind()
### Fixed
* incorrect GraphicsSettingsManager.cs file location to Editor folder

## [0.0.10] - 2022-05-17
### Added
* General.RecursiveFind(string) function to recursively search for a grandchild by name n
### Fixed
* OrbitCamera syntax bug

## [0.0.9] - 2022-05-17
### Added
* default target position value for OrbitCamera in the event that the target transform is not set
* Math.GetPercentRoll(float) function to check if a percent chance roll event occurs

## [0.0.8] - 2022-05-11
### Added
* hexagons texture
* temp function under Utilities.General
* GraphicsSettingsManager Editor Script
* Circle and Pixel default Sprites (might need to be moved into Images/Sprites folder in the future)
* functionality for OrbitCamera to have no target transform (probably should cache this default value)
### Fixed
* odd hierarchy and default transform for OrbitCamera prefab
* erroneous MonoBehaviour declaration within NewMonoBehaviour.cs

## [0.0.7] - 2022-05-09
### Added
* new WorldSpaceDisplay class with an associated display Prefab (see class for more info)
* new input to OrbitCamera which allows for a user to change the distance of the orbit
* functionality to hide mouse when using the OrbitCamera class/prefab
### Changed
* OrbitScript class to orbit around a target transform using an orbit distance and speed
### Removed
* OrbitScript orbiting function where relative rotation would be a factor of the orbiting

## [0.0.6] - 2022-05-03
### Added
* new folder for scripts called Additional, which will include helper utility scripts
* helper script OrbitScript.cs which is a simple example orbit script
* helper script OrbitCamera.cs which orbits the camera around a target Transform w/ user input
* Orbit Camera prefab 
### Changed
* comments for Math.cs
* location of CameraFacer.cs to Additional

## [0.0.5] - 2022-05-03
### Added
* CameraFacer class to force a GameObject's orientation towards a Camera
* GetCacheComponent() function to General.cs
* DontDestroyOnLoad option for Singleton class
### Changed
* ScriptTemplates for generating a new Script
* asmdef files to account for better naming conventions
* simplified Math.cs's Perturb functions
### Removed
* Rendering folder from Utilities

## [0.0.4] - 2022-04-29
### Added
* Singleton class to handle singleton instance management
### Changed
* Runtime .asmdef file to include Cinemachine and TextMeshPro dependencies
* GenUtils class to General
* MathUtils class to Math
* Bezier functions to better indicate functionality
### Removed
* dependency on Unity Recorder package

## [0.0.3] - 2022-04-27
### Removed
* URP settings; new projects can use the URP Templated settings

## [0.0.2] - 2022-04-26
### Added
* Script Templates Folder
* functionality to create new C# Scripts that adhere to Kokowolo Coding Conventions

## [0.0.1] - 2022-04-25
### Added
* initial commit of package

