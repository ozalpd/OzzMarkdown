using Markdig;

namespace OzzMarkdown.Core
{
    public class MarkdownHtmlRenderer
    {
        private readonly MarkdownPipeline _pipeline;
        private readonly string VirtualHostName;
        private readonly string TempFolderName;
        private List<string> _tempHtmlFiles = new List<string>();

        public MarkdownHtmlRenderer(string virtualHostName, string tempFolderName)
        {
            _pipeline = new MarkdownPipelineBuilder()
           .UseAdvancedExtensions()
           .Build();

            VirtualHostName = virtualHostName;
            TempFolderName = tempFolderName;
        }

        public void CleanupTempFiles()
        {
            foreach (var file in _tempHtmlFiles)
            {
                try
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                catch
                {
                    //ignore any exceptions during cleanup
                }
            }
            _tempHtmlFiles.Clear();
        }

        public string TempFolder
        {
            get
            {
                string tmpFolder = Path.Combine(Path.GetTempPath(), TempFolderName);
                if (!Directory.Exists(tmpFolder))
                    Directory.CreateDirectory(tmpFolder);

                return tmpFolder;
            }
        }

        public string RenderToTempFileUrl(string markdown, MarkdownTheme theme)
        {
            string html = Markdown.ToHtml(markdown, _pipeline);
            string finalHtml = WrapHtml(html, theme.Css);

            string tempHtmlFile = Path.Combine(TempFolder, $"markdown_{Guid.NewGuid():N}.html");
            File.WriteAllText(tempHtmlFile, finalHtml, System.Text.Encoding.UTF8);
            _tempHtmlFiles.Add(tempHtmlFile);
            string fileName = Path.GetFileName(tempHtmlFile);

            return $"https://{VirtualHostName}/{fileName}";
        }

        private string WrapHtml(string html, string css)
        {
            return $@"<!DOCTYPE html>
<html>
<head>
<meta charset='utf-8'>
<style>{css}</style>
</head>
<body>{html}</body>
</html>";
        }
    }
}
