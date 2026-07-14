# OzzMarkdown

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10-512BD4)](https://dotnet.microsoft.com/)

OzzMarkdown is a lightweight, modern Markdown reader and some editing functionality will be available in the future. It is designed to be fast, efficient, and easy to use.

It focuses on clean rendering, developer‑friendly utilities, and optional workflows for LLM enthusiasts.

## Features

- Fast, clean Markdown rendering
- Cross-project shared core library (`OzzMarkdown.Core`)
- MVVM-based WPF desktop frontend
- Built-in English and Turkish localization
- Bootstrap Icons integrated as native WPF resources

## Application Type

OzzMarkdown is a multi-frontend desktop utility targeting **.NET 10**.

| Project            | Status      | Technology     |
| :----------------- | :---------- | :------------- |
| `OzzMarkdown.Core` | ✅ Available | Shared Library |
| `OzzMarkdown.WPF`  | ✅ Available | WPF, MVVM      |
| `OzzMarkdown.MAUI` | 🔜 Planned  | .NET MAUI      |

The core functionality lives in `OzzMarkdown.Core`, which is **platform-agnostic** and shared by two frontends and [OzzContextGen](https://github.com/ozalpd/OzzContextGen). `OzzMarkdown.i18n` provides shared localization (English and Turkish) across all projects.

The WPF frontend uses MVVM and ships with a shared `Styles.xaml` resource dictionary and [Bootstrap Icons v1.13.1](https://icons.getbootstrap.com) (MIT) as WPF `Geometry` resources in `BootstrapIcons.xaml`. The MAUI frontend will mirror the same MVVM structure.

## Project Structure

```
OzzMarkdown/
├── OzzMarkdown.Core/         # Platform-agnostic Markdown rendering engine
│   ├── MarkdownHtmlRenderer.cs   # Renders Markdown to a temp HTML file / virtual-host URL
│   ├── MarkdownTheme.cs          # CSS theme record
│   └── MarkdownThemeProvider.cs  # Built-in theme registry (Light, etc.)
│
├── OzzWpf.Core/               # Shared WPF building blocks (used by WPF-based frontends/tools)
│   ├── Controls/
│   │   └── MarkdownViewer.xaml(.cs)  # WebView2-based Markdown viewer control
│   └── Models/
│       ├── AbstractAppSettings.cs    # Base class for persisted app settings
│       └── WindowPosition.cs         # Window geometry capture/restore helper
│
├── OzzMarkdown.WPF/            # WPF desktop frontend (MVVM)
│   ├── Commands/
│   │   └── RelayCommand.cs
│   ├── Models/
│   │   ├── AppSettings.cs        # Concrete, persisted app settings singleton
│   │   └── AppVersion.cs         # Assembly version/metadata accessor
│   ├── Services/
│   │   ├── IFileDialogService.cs     # Abstraction over native file dialogs
│   │   └── Win32FileDialogService.cs # Win32-backed implementation
│   ├── ViewModels/
│   │   ├── AbstractViewModel.cs
│   │   └── MainViewModel.cs
│   ├── Resources/
│   │   ├── Styles.xaml           # Shared control styles
│   │   └── BootstrapIcons.xaml   # Bootstrap Icons v1.13.1 as Geometry resources
│   └── MainWindow.xaml(.cs)
│
├── OzzMarkdown.i18n/           # Shared localization (English + Turkish .resx)
│
└── OzzMarkdown.MAUI/           # 🔜 Planned .NET MAUI frontend
```

## Getting Started

1. Clone the repository:

   ```powershell
   git clone https://github.com/ozalpd/OzzMarkdown.git
   ```

2. Open `OzzMarkdown.slnx` in Visual Studio 2022+ (or a compatible IDE) with the .NET 10 SDK installed.
3. Set `OzzMarkdown.WPF` as the startup project and run it.

## Contributing

Contributions, issues, and feature requests are welcome. Feel free to check the [issues page](https://github.com/ozalpd/OzzMarkdown/issues).

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for a history of notable changes.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.
