# Styleguide for this project
Below is the folder and styleguide for this project, this outlines the naming conventions and folder structure for our game

## Development Environment Setup
To ensure consistent code style across contributors and files, please configure your development environment to the specification below:

### Code Style Configuration (.editorconfig)
A shared `.editorconfig` file has been created to enforce consistency code formatting across different editors.

#### Rider
1. Open the Spin Game project in **Jetbrains Rider**.
2. The `.editorconfig` file sshould be automatically detected due to it being located within the root directory of the project.
3. To Verify:
- Go to **Preferences / Settings -> Editor -> Code Style -> General**.
- Ensure **"Enable EditorConfig support"** is enabled.

#### Visual Studio Code
1. Install the **EditorConfig for VS Code** extension.
2. Open the project folder in **Visual Studio Code**.
3. The `.editorconfig` file should be automatically detected due to it being located within the root directory of the project.
4. It may be neccessary to reload the window for the `.editorconfig` to take effect. To do this press `Ctrl+Shift+P` ***-> "Reload Window"***.


### Folder Structure
Note that these folders use PascalCase, this styling also applies to subfolders

| Folder              | Contents Description                                                                               |
| ------------------- | -------------------------------------------------------------------------------------------------- |
| `Config`            | JSON, XML, or ScriptableObject configuration files that define game rules, constants, or settings. |
| `Editor`            | Editor scripts and custom inspector tools. Only compiled in the Unity Editor.                      |
| `Materials`         | Material assets (`.mat`) for rendering meshes and sprites.                                         |
| `Prefabs`           | Pre-configured GameObjects saved as `.prefab` files for reuse in scenes or instantiation via code. |
| `Resources`         | Assets loaded at runtime using `Resources.Load()`. Use sparingly as it bypasses Addressables.      |
| `Scenes`            | Unity scene files (`.unity`). Each major game level or UI view should be a separate scene.         |
| `ScriptableObjects` | ScriptableObject assets used for shared data containers, configs, event systems, etc.              |
| `Scripts`           | C# script files that contain gameplay logic, components, utilities, and MonoBehaviours.            |
| `Settings`          | Unity project or subsystem settings (e.g., input settings, render pipeline assets).                |
| `Sounds`            | Audio clips (`.wav`, `.mp3`, `.ogg`) for music, sound effects, and UI audio.                       |
| `Sprites`           | 2D sprite textures used in the UI or game visuals (`.png`, `.jpg`).                                |
| `UI`                | UI prefabs, canvases, fonts, and layout components used for HUDs or menus.                         |
| `VFX`               | Visual effects such as particle systems, shaders, VFX Graph assets.        												 |


### Naming Conventions
Please ensure that members and variables are named accordingly, this should be enforced by `.editorconfig` but might not be always enforced.

| Symbol Type             | Scope / Accessibility | Naming Convention | Example                    |
|-------------------------|-----------------------|-------------------|----------------------------|
| **Interfaces**          | `any`                 | `IPascalCase`     | `IInteractable`, `IGrabableObject` |
| **Classes (concrete)**  | `any`                 | `PascalCase`      | `ItemBehaviour`, `PlayerInputManager` |
| **Classes (abstract)**  | `any`                 | `AbstractPascalCase` | `AbstractEnemy`, `AbstractPlayerState` |
| **Properties (public)** | `public`              | `PascalCase`      | `Health`, `Speed`          |
| **Fields (public)**     | `public`              | `PascalCase`      | `MaxHealth`, `IsAlive`     |
| **Fields (private)**    | `private`             | `_camelCase`      | `_health`, `_speed`        |
| **Variables**						| `local`								|	`camelCase`				| `normalizedDir`, `i`			 |
| **Methods (public)**    | `public`              | `PascalCase`      | `TakeDamage()`, `MoveTo()` |
| **Methods (private)**   | `private`             | `_camelCase`      | `_takeDamage()`, `_moveTo()` |
