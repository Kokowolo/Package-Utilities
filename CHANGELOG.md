# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.0.5] - 2022-5-3
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

## [0.0.4] - 2022-4-29
### Added
* Singleton class to handle singleton instance management
### Changed
* Runtime .asmdef file to include Cinemachine and TextMeshPro dependencies
* GenUtils class to General
* MathUtils class to Math
* Bezier functions to better indicate functionality
### Removed
* dependency on Unity Recorder package

## [0.0.3] - 2022-4-27
### Removed
* URP settings; new projects can use the URP Templated settings

## [0.0.2] - 2022-4-26
### Added
* Script Templates Folder
* functionality to create new C# Scripts that adhere to Kokowolo Coding Conventions

## [0.0.1] - 2022-4-25
### Added
* initial commit of package

