using System.IO;

namespace OzzMarkdown.Core.Helpers;

public static class ResourceLoader
{
    public static string Load(string name)
    {
        var asm = typeof(ResourceLoader).Assembly;
        using var stream = asm.GetManifestResourceStream(name);
        if (stream == null)
            throw new FileNotFoundException($"Resource '{name}' not found in assembly '{asm.FullName}'.");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}