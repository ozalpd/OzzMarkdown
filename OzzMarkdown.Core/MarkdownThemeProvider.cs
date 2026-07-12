namespace OzzMarkdown.Core;

public static class MarkdownThemeProvider
{
    /// <summary>
    /// Returns all available theme names for UI population (e.g., a ComboBox).
    /// </summary>
    public static IEnumerable<string> GetAllThemeNames() => _themes.Keys;

    /// <summary>
    /// Returns the theme by name, or the Default theme if not found.
    /// </summary>
    public static MarkdownTheme GetTheme(string name)
    {
        return _themes.TryGetValue(name, out var theme) ? theme : _themes["Light"];
    }


    private static readonly Dictionary<string, MarkdownTheme> _themes = new Dictionary<string, MarkdownTheme> {
    { "Light",
            new MarkdownTheme("Light",  @"
body {
    font-family: 'Segoe UI', sans-serif;
    background: #ffffff;
    color: #222;
    margin: 20px;
    line-height: 1.6;
}

h1, h2, h3, h4 {
    color: #111;
    margin-top: 24px;
    font-weight: 600;
}

a {
    color: #0067c0;
    text-decoration: none;
}

code {
    background: #f2f2f2;
    padding: 3px 5px;
    border-radius: 4px;
    font-family: Consolas, monospace;
}

pre code {
    display: block;
    padding: 12px;
    overflow-x: auto;
    background: #f2f2f2;
}

table {
    border-collapse: collapse;
    margin-top: 16px;
}

th, td {
    border: 1px solid #ccc;
    padding: 6px 10px;
}

blockquote {
    border-left: 4px solid #ccc;
    padding-left: 12px;
    color: #555;
    margin-left: 0;
}") },
    { "Dark",
            new MarkdownTheme("Dark", @"
body {
	font-family: 'Segoe UI', sans-serif;
	background: #1e1e1e;
	color: #ddd;
	margin: 20px;
	line-height: 1.6;
}

h1, h2, h3, h4 {
	color: #fff;
	margin-top: 24px;
	font-weight: 600;
}

a {
	color: #4ea1ff;
	text-decoration: none;
}

code {
	background: #252526;
	padding: 3px 5px;
	border-radius: 4px;
	font-family: Consolas, monospace;
}

pre code {
	display: block;
	padding: 12px;
	overflow-x: auto;
}

table {
	border-collapse: collapse;
	margin-top: 16px;
}

th, td {
	border: 1px solid #444;
	padding: 6px 10px;
}

blockquote {
	border-left: 4px solid #555;
	padding-left: 12px;
	color: #aaa;
	margin-left: 0;
}") },
    { "WarmLight",
            new MarkdownTheme("WarmLight", @"
body {
    font-family: 'Segoe UI', sans-serif;
    background: #F7F4F1;
    color: #2a2a2a;
    margin: 20px;
    line-height: 1.6;
}

h1, h2, h3, h4 {
    color: #1e1e1e;
    margin-top: 24px;
    font-weight: 600;
}

a {
    color: #005a9e;
    text-decoration: none;
}

code {
    background: #e8e5e1;
    padding: 3px 5px;
    border-radius: 4px;
    font-family: Consolas, monospace;
}

pre code {
    display: block;
    padding: 12px;
    overflow-x: auto;
    background: #e8e5e1;
}

table {
    border-collapse: collapse;
    margin-top: 16px;
}

th, td {
    border: 1px solid #bfb9b3;
    padding: 6px 10px;
}

blockquote {
    border-left: 4px solid #bfb9b3;
    padding-left: 12px;
    color: #5a5a5a;
    margin-left: 0;
}") }
};
}
