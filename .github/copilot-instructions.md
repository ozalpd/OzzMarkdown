# Copilot Instructions for OzzMarkdown

## Project Overview

OzzMarkdown is a lightweight, modern Markdown reader (with editing features planned) targeting **.NET 10**. It focuses on fast, clean rendering and developer-friendly utilities, with optional workflows for LLM enthusiasts. The project has multiple frontends backed by a shared, platform-agnostic core.

## Architecture

### Projects

| Project | Role |
|---|---|
| `OzzMarkdown.Core` | Platform-agnostic Markdown rendering engine. No UI or platform dependencies. Shared by all frontends and by [OzzContextGen](https://github.com/ozalpd/OzzContextGen). |
| `OzzWpf.Core` | Shared WPF building blocks: the `MarkdownViewer` user control, `AbstractAppSettings`, and `WindowPosition`. Depends on `OzzMarkdown.Core` and `Microsoft.Web.WebView2`. |
| `OzzMarkdown.WPF` | WPF desktop frontend using MVVM. References `OzzWpf.Core`, `OzzMarkdown.Core`, and `OzzMarkdown.i18n`. |
| `OzzMarkdown.MAUI` | *(Planned)* .NET MAUI cross-platform frontend. Will mirror the WPF frontend's MVVM structure. |
| `OzzMarkdown.i18n` | Shared localization (English + Turkish `.resx` files) used across all frontends. |

### Core Types

| Type | Responsibility |
|---|---|
| `MarkdownHtmlRenderer` (`OzzMarkdown.Core`) | Renders Markdown to a temporary HTML file and returns a virtual-host URL for display in a `WebView2` control (bypasses the `NavigateToString` size limit). Also handles temp-file cleanup. |
| `MarkdownTheme` (`OzzMarkdown.Core`) | Record describing a named CSS theme (e.g., name + stylesheet body) applied when rendering Markdown to HTML. |
| `MarkdownThemeProvider` (`OzzMarkdown.Core`) | Static registry of built-in themes (`Light`, etc.). `GetTheme(name)` falls back to `Light` if the name is not found. `GetAllThemeNames()` supports UI population (e.g., a theme picker ComboBox). |
| `MarkdownViewer` (`OzzWpf.Core.Controls`) | WPF `UserControl` wrapping a `WebView2` browser. Exposes `MarkdownContent` as a `DependencyProperty` (bindable) that re-renders via `MarkdownHtmlRenderer` whenever it changes. Constructor requires an `AbstractAppSettings` instance to resolve the settings folder name and initial theme. |
| `AbstractAppSettings` (`OzzWpf.Core.Models`) | Abstract base for app settings: `MainWindowPosition`, `UiCulture`, `SelectedTheme`, and JSON persistence helpers (`Save`, `GetSettingsFilePath`). Subclasses provide `GetSettingsFolderName()` and app-specific settings file naming/singleton logic (see `OzzMarkdown.WPF.Models.AppSettings`). |
| `WindowPosition` (`OzzWpf.Core.Models`) | Holds window geometry (`Top`, `Left`, `Width`, `Height`). `GetWindowPositions(window)` captures current state; `SetWindowPositions(window)` restores it. |

## Key Constraints

- **`OzzMarkdown.Core` must stay platform-agnostic.** Never add WPF, MAUI, Windows-only APIs, or any UI framework reference to it.
- **`OzzWpf.Core` is WPF-specific** but shared across WPF-based frontends/tools; keep it free of app-specific (i.e., `OzzMarkdown.WPF`-only) logic so it stays reusable.
- **New reusable rendering/theme logic goes into `OzzMarkdown.Core`**, not into a frontend project.
- **Each frontend owns its own MVVM layer** (ViewModels, Commands, Views/Pages). Keep ViewModels thin — push logic down to `OzzMarkdown.Core` / `OzzWpf.Core`.
- When adding a feature that applies to both WPF and the future MAUI project, implement the shared parts in `OzzMarkdown.Core` (or a future `OzzMaui.Core`) and expose a clean API.
- Keep ViewModels free of direct Win32/WPF dialog or file-system API usage — abstract such interactions behind an interface (e.g., `IFileDialogService`) with a concrete implementation living in the frontend project, so ViewModels stay testable and portable.

## Coding Conventions

- Prefer `record` types for immutable data/state models.
- Use `async/await` for all file I/O operations.
- Prefer short, concise type names.
- User-facing strings go into `OzzMarkdown.i18n` (`LocalizedStrings.resx` for English, `LocalizedStrings.tr.resx` for Turkish). Access via the generated `LocalizedStrings` static class.
- Expose control state intended for XAML/code binding as WPF `DependencyProperty`, not plain CLR properties (plain CLR properties cannot be used as `Binding`/`SetBinding` targets).

## WPF Frontend Notes (`OzzMarkdown.WPF`)

- Base ViewModel: `AbstractViewModel` (implements `INotifyPropertyChanged`).
- `RelayCommand` (`Commands/RelayCommand.cs`) supports sync and async delegates with optional `CanExecute`.
- `MainViewModel` takes an `IFileDialogService` via constructor injection (with a parameterless overload defaulting to `Win32FileDialogService` for XAML/DataContext convenience).
- `AppSettings` (`Models/AppSettings.cs`): Singleton (thread-safe lazy init) extending `AbstractAppSettings`; persists settings as JSON to `%AppData%/OzzMarkdown/wpfsettings.json`. Call `GetAppSettings()` to read, `Save()` to write.
- `AppVersion` (`Models/AppVersion.cs`): Internal static class; exposes version/product metadata read from assembly attributes.
- Shared WPF resources for the frontend live in `Resources/Styles.xaml` and `Resources/BootstrapIcons.xaml` (Bootstrap Icons v1.13.1, MIT, as `Geometry` resources for use in `Path` elements).
- Controls that require constructor arguments (like `MarkdownViewer`) cannot be declared directly in XAML with a parameterless tag; instantiate them in code-behind and use `FrameworkElement.SetBinding` to bind their `DependencyProperty` values to the `DataContext` ViewModel.

## MAUI Frontend Notes (Planned)

- Must mirror the WPF frontend's MVVM structure so ViewModels can eventually be shared or easily ported.
- Do not use WPF-specific types (`PresentationCore`, `System.Windows.*`, etc.) anywhere that MAUI will also reference.
