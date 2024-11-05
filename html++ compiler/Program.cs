using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

class CustomHtmlCompiler
{
    static void Main(string[] args)
    {
        for (int i = 0; i < 200; i++)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the input file path.");
                string inputFilePath = "G:\\fri3nds\\v-category-projects\\html++\\html++ compiler\\html++ compiler\\HtmlInput.tpp";
                string inputCode = File.ReadAllText(inputFilePath);

                string filename = "output";
                bool runInBrowser = args.Length > 1 && args[1] == "--run";

                string htmlOutput = CompileToHtml(inputCode);
                File.WriteAllText($"{filename}.html", htmlOutput);

                Console.WriteLine($"HTML file generated: {filename}.html");

                if (!runInBrowser)
                {
                    OpenInBrowser($"{filename}.html");
                }
                return;
            }
            else
            {
                string inputFilePath = args[0];
                string inputCode = File.ReadAllText(inputFilePath);

                string filename = "output";
                bool runInBrowser = args.Length > 1 && args[1] == "--run";

                string htmlOutput = CompileToHtml(inputCode);
                File.WriteAllText($"{filename}.html", htmlOutput);

                Console.WriteLine($"HTML file generated: {filename}.html");

                if (!runInBrowser)
                {
                    OpenInBrowser($"{filename}.html");
                }
            }
            Console.WriteLine("Press any key to redo it... ");
            Console.ReadLine();
        }
    }

    static string CompileToHtml(string inputCode)
    {
        var lines = inputCode.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        var htmlOutput = new StringBuilder();
        var indentLevel = 0;
        var tagStack = new Stack<string>();
        var inLoopOrCondition = false;
        var loopOrConditionContent = new StringBuilder();
        var scriptOrStyleContent = new StringBuilder();
        var inScriptOrStyle = false;

        htmlOutput.AppendLine("<!DOCTYPE html>");
        htmlOutput.AppendLine("<html>");

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (string.IsNullOrEmpty(trimmedLine)) continue;

            // Handle loops
            if (trimmedLine.StartsWith("@for "))
            {
                var loopMatch = Regex.Match(trimmedLine, @"@for\s+(\w+):(\w+)\s+in\s+(\d+)\.\.(\d+)\s*{");
                if (loopMatch.Success)
                {
                    var variableName = loopMatch.Groups[1].Value;
                    var start = int.Parse(loopMatch.Groups[3].Value);
                    var end = int.Parse(loopMatch.Groups[4].Value);

                    for (int i = start; i <= end; i++)
                    {
                        var loopContent = loopOrConditionContent.ToString();
                        loopContent = loopContent.Replace(variableName, i.ToString());
                        htmlOutput.AppendLine($"{GetIndent(indentLevel)}{loopContent}");
                    }
                    continue;
                }
            }

            // Handle while loops
            if (trimmedLine.StartsWith("@while "))
            {
                var whileMatch = Regex.Match(trimmedLine, @"@while\s*\((.*?)\)\s*{");
                if (whileMatch.Success)
                {
                    var condition = whileMatch.Groups[1].Value;

                    // Handle while loop (this is a simplified version and may not handle complex conditions)
                    while (EvaluateCondition(condition))
                    {
                        var whileContent = loopOrConditionContent.ToString();
                        htmlOutput.AppendLine($"{GetIndent(indentLevel)}{whileContent}");
                        break; // This should be replaced with actual condition evaluation and loop continuation
                    }
                    continue;
                }
            }

            // Handle conditions
            if (trimmedLine.StartsWith("@if "))
            {
                var ifMatch = Regex.Match(trimmedLine, @"@if\s*\((.*?)\)\s*{");
                if (ifMatch.Success)
                {
                    var condition = ifMatch.Groups[1].Value;
                    htmlOutput.AppendLine($"{GetIndent(indentLevel)}<div class='if-block'>");
                    indentLevel++;
                    continue;
                }
            }
            else if (trimmedLine.StartsWith("else if "))
            {
                var elseIfMatch = Regex.Match(trimmedLine, @"else\s+if\s*\((.*?)\)\s*{");
                if (elseIfMatch.Success)
                {
                    var condition = elseIfMatch.Groups[1].Value;
                    htmlOutput.AppendLine($"{GetIndent(indentLevel)}</div>");
                    htmlOutput.AppendLine($"{GetIndent(indentLevel)}<div class='else-if-block'>");
                    indentLevel++;
                    continue;
                }
            }
            else if (trimmedLine.StartsWith("else"))
            {
                htmlOutput.AppendLine($"{GetIndent(indentLevel)}</div>");
                htmlOutput.AppendLine($"{GetIndent(indentLevel)}<div class='else-block'>");
                indentLevel++;
                continue;
            }
            else if (trimmedLine == "}")
            {
                if (inLoopOrCondition)
                {
                    htmlOutput.AppendLine($"{GetIndent(indentLevel)}</div>");
                    loopOrConditionContent.Clear();
                    inLoopOrCondition = false;
                }
                else if (inScriptOrStyle)
                {
                    htmlOutput.AppendLine($"{GetIndent(indentLevel)}{scriptOrStyleContent.ToString()}</{(inScriptOrStyle ? "script" : "style")}>");
                    scriptOrStyleContent.Clear();
                    inScriptOrStyle = false;
                }
                else if (tagStack.Count > 0)
                {
                    var lastTag = tagStack.Pop();
                    indentLevel = Math.Max(0, indentLevel - 1);
                    htmlOutput.AppendLine($"{GetIndent(indentLevel)}</{lastTag}>");
                }
                continue;
            }

            if (trimmedLine.StartsWith("script() {"))
            {
                htmlOutput.AppendLine($"{GetIndent(indentLevel)}<script>");
                inScriptOrStyle = true;
                continue;
            }

            if (trimmedLine.StartsWith("style() {"))
            {
                htmlOutput.AppendLine($"{GetIndent(indentLevel)}<style>");
                inScriptOrStyle = true;
                continue;
            }

            // Handle script and style content
            if (inScriptOrStyle)
            {
                if (trimmedLine.StartsWith("\"\""))
                {
                    inScriptOrStyle = false;
                    htmlOutput.AppendLine($"{GetIndent(indentLevel)}{scriptOrStyleContent.ToString()}</{(inScriptOrStyle ? "script" : "style")}>");
                    scriptOrStyleContent.Clear();
                }
                else
                {
                    scriptOrStyleContent.AppendLine(trimmedLine);
                }
                continue;
            }

            // Handle tag and content
            var tagMatch = Regex.Match(trimmedLine, @"(\w+)\s*\{\s*(.*?)\s*\}");
            if (tagMatch.Success)
            {
                var tagName = tagMatch.Groups[1].Value;
                var tagContent = tagMatch.Groups[2].Value;

                if (tagName == "style")
                {
                    htmlOutput.AppendLine($"{GetIndent(indentLevel)}<{tagName}>{tagContent}</{tagName}>");
                }
                else if (tagName == "script")
                {
                    //htmlOutput.AppendLine($"{GetIndent(indentLevel)}<{tagName}>{tagContent}</{tagName}>");
                }
                else
                {
                    htmlOutput.AppendLine($"{GetIndent(indentLevel)}<{tagName}>{tagContent}</{tagName}>");
                }
            }
        }

        // Close any remaining open tags
        while (tagStack.Count > 0)
        {
            var lastTag = tagStack.Pop();
            indentLevel = Math.Max(0, indentLevel - 1);
            htmlOutput.AppendLine($"{GetIndent(indentLevel)}</{lastTag}>");
        }

        //htmlOutput.AppendLine("</html>");
        return htmlOutput.ToString();
    }

    static string GetIndent(int level)
    {
        return new string(' ', Math.Max(0, level * 2));
    }

    static bool EvaluateCondition(string condition)
    {
        // Implement a simple condition evaluator for demo purposes
        // This is a placeholder and should be replaced with real logic
        return true;
    }

    static void OpenInBrowser(string filename)
    {
        try
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = Path.GetFullPath(filename),
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while trying to open the file: {ex.Message}");
        }
    }
}
