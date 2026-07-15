# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).



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
