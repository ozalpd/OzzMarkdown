# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.1.8] - 2026-08-05

### Added
- Added an optional "Associate .md files with OzzMarkdown and set its icon" task to `OzzMarkdown.Setup.iss`, registering `.md` files under `HKA\Software\Classes` (ProgID `OzzMarkdown.MarkdownFile`) with `Assets\icon-doc-M-03.ico` as the file icon and `OzzMarkdown.WPF.exe "%1"` as the open command; refreshes Explorer's shell icon cache via `SHChangeNotify` after install when the task is selected.
- Added support for opening a Markdown file passed by the OS (e.g. via double-click on an associated `.md` file): `App.OnStartup` now reads the launch arguments and passes the file path to a new `MainWindow(string? filePathToOpen)` constructor overload, which loads it through a new `MainViewModel.LoadMarkdownFileAsync` method once the view model is initialized.

## [0.1.7] - 2026-08-04

### Added
- Added `Scripts\TagRelease.bat` to create and push a git tag derived from `OzzMarkdown.WPF.csproj`'s `<Version>`, with duplicate-tag detection and a confirmation prompt before tagging/pushing.
- Added Batch and Pascal language definitions to PrismJS for improved code block highlighting. Updated minified assets accordingly.
- Added update-checking to OzzMarkdown using new core types (`IReleaseSource`, `GitHubRelease`, `GitHubAsset`) and GitHub API integration. The app checks for updates at startup and prompts users when a new version is available.

### Changed
- Refined link colors in all built-in Markdown themes for better consistency and accessibility.
- Refreshed the SVG logo with new colors and gradients, replaced old icon and PNG assets with updated versions, and removed obsolete files. Updated the WPF project and `MainViewModel` to reference the new icon in the app and About dialog. No functional code changes—branding and resource updates only.
- Refactored `AbstractAppSettings` and `AppVersion` to `OzzMarkdown.Core.Models` for platform-agnostic access. Updated all affected files to use the new namespace. Moved `MainWindowPosition` to `AppSettings` to keep window geometry WPF-specific. Added `using static System.Environment` in `AbstractAppSettings` for environment utilities.

## [0.1.6] - 2026-07-26

### Added
- Wired the About toolbar button to a `ShowAboutCommand` `RelayCommand` in `MainViewModel`, which loads the app's high-resolution icon via `AboutDialog.LoadHighResolutionIcon` before showing the dialog.
- Added a runtime-check `[Code]` section to `OzzMarkdown.Setup.iss` that detects a missing .NET 10 Desktop Runtime and offers to open the official download page before continuing.
- Added `AppId`, `UninstallDisplayIcon`, `WizardStyle=modern`, `ArchitecturesAllowed`/`ArchitecturesInstallIn64BitMode`, and `Flags: ignoreversion` to the Inno Setup installer script for correct upgrade/uninstall behavior and 64-bit installs.

### Fixed
- Fixed `AboutDialog` throwing `DirectoryNotFoundException` when loading the app icon: changed `Assets\icon-M-02.ico` from a `Content` item (never copied to output) to a `Resource` item in `OzzMarkdown.WPF.csproj` so it resolves reliably via its pack URI.
- Fixed the "Generate TOC" `ToggleButton` showing a different background when checked vs. unchecked; `ToggleButtonStyle-AutoX24` now uses a custom `ControlTemplate` with a fixed transparent `Background`/`BorderBrush`, ignoring the default theme's checked-state chrome.

### Changed
- Removed the standalone `ShowAboutCommand` class (`OzzWpf.Core.Commands`) in favor of a `RelayCommand` defined directly in `MainViewModel`, since showing the dialog requires loading the icon first.

## [0.1.5] - 2026-07-25

### Added
- Added `AboutDialog` window to display application information, including version, author, and license details.

## [0.1.4] - 2026-07-25

### Added
- Added installer script for Inno Setup to create a Windows installer for the WPF frontend.

### Changed
- User control `MarkdownViewer` now uses a custom user data folder for WebView2 to improve isolation and persistence.
- Bumped `OzzMarkdown.WPF` version to 0.1.4 and `OzzWpf.Core` to 0.1.4 for consistency.

## [0.1.0] - 2026-07-25

### Added
- Added a localized "Generate TOC" toggle button to the toolbar with icon and tooltip, bound to a new `GenerateToc` property in MainViewModel (default true).

### Changed
- Moved `Styles.xaml` and `BootstrapIcons.xaml` to `OzzWpf.Core` for reuse across WPF-based frontends/tools.
- Updated `MainViewModel` to include `GenerateToc` property and bind it to the toolbar toggle button.

## [0.0.2] - 2026-07-15

### Added
- Embedded Prism.js and CSS themes as resources in `OzzMarkdown.Core` and added `bundleconfig.json` for minification of Prism assets.
- Added `ResourceLoader` for embedded asset loading.

### Changed
- Extended `MarkdownThemeProvider` for Prism CSS selection per theme.
- Updated `MarkdownHtmlRenderer` to inject Prism assets and highlight code blocks (default to C#).
- Improved `MarkdownViewer` background for theme consistency.

## [0.0.1] - 2026-07-14

### Added
- Initial project structure with `OzzMarkdown.Core` (shared library) and `OzzMarkdown.WPF` (WPF frontend).
- Implemented reusable `MarkdownViewer` control using WebView2 into WPF library project `OzzWpf.Core`.
- Functionality to open and render Markdown files in the WPF frontend via `MarkdownViewer`.
- Bootstrap Icons v1.13.1 bundled as WPF `Geometry` resources in `BootstrapIcons.xaml`.
- Localization support (English and Turkish) via `OzzMarkdown.i18n`.

[Unreleased]: https://github.com/ozalpd/OzzMarkdown/commits/main
[0.1.8]: https://github.com/ozalpd/OzzMarkdown/releases/tag/v0.1.8
[0.1.7]: https://github.com/ozalpd/OzzMarkdown/releases/tag/v0.1.7
[0.1.6]: https://github.com/ozalpd/OzzMarkdown/releases/tag/v0.1.6
[0.1.4]: https://github.com/ozalpd/OzzMarkdown/releases/tag/v0.1.4
...
