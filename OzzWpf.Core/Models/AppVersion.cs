using System.Reflection;

namespace OzzWpf.Core.Models;

/// <summary>
/// Provides application version information.
/// </summary>
public static class AppVersion
{
    /// <summary>
    /// Gets the assembly whose version information is reported (the entry
    /// assembly, i.e. the running application, falling back to this assembly).
    /// </summary>
    private static Assembly TargetAssembly =>
        Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

    /// <summary>
    /// Gets the version number (e.g., "1.1.0").
    /// </summary>
    public static string Version =>
        TargetAssembly.GetName().Version?.ToString(3) ?? "1.0.0";

    /// <summary>
    /// Gets the full informational version.
    /// </summary>
    public static string FullVersion =>
        TargetAssembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? Version;

    /// <summary>
    /// Gets the product name.
    /// </summary>
    public static string Product =>
        TargetAssembly
            .GetCustomAttribute<AssemblyProductAttribute>()?
            .Product ?? string.Empty;

    /// <summary>
    /// Gets the copyright information.
    /// </summary>
    public static string Copyright =>
        TargetAssembly
            .GetCustomAttribute<AssemblyCopyrightAttribute>()?
            .Copyright ?? "Copyright © 2026";

    /// <summary>
    /// Gets the description.
    /// </summary>
    public static string Description =>
        TargetAssembly
            .GetCustomAttribute<AssemblyDescriptionAttribute>()?
            .Description ?? string.Empty;
}