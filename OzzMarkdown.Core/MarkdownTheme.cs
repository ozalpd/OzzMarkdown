namespace OzzMarkdown.Core;

public class MarkdownTheme
{
    public MarkdownTheme(string name, string css)
    {
        Name = name;
        Css = css;
    }

    public string Name { get; set; }

    public string Css { get; set; }
}
