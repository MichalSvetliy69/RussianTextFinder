using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class RussianTextFinder
{
    static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: app.exe <path to .cs file>");
            return 1;
        }

        string filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found: " + filePath);
            return 2;
        }

        try
        {
            foreach (var lineNumber in FindLinesWithRussianText(filePath))
            {
                Console.WriteLine(lineNumber);
            }
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return 3;
        }
    }

    static IEnumerable<int> FindLinesWithRussianText(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            string line;
            int lineNumber = 0;
            bool inMultiLineComment = false;

            Regex stringWithRussianTextRegex = new Regex(@"[""'].*?[\u0400-\u04FF]+.*?[""']", RegexOptions.Compiled);
            Regex singleLineCommentRegex = new Regex(@"//.*");
            Regex attributeRegex = new Regex(@"\[\s*\w+\s*\(.*?\)\s*\]", RegexOptions.Singleline);

            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;

                // Skip multi-line comments
                if (inMultiLineComment)
                {
                    int endCommentIndex = line.IndexOf("*/");
                    if (endCommentIndex != -1)
                    {
                        line = line.Substring(endCommentIndex + 2);
                        inMultiLineComment = false;
                    }
                    else
                    {
                        continue;
                    }
                }

                while (true)
                {
                    int startCommentIndex = line.IndexOf("/*");
                    if (startCommentIndex != -1)
                    {
                        int endCommentIndex = line.IndexOf("*/", startCommentIndex + 2);
                        if (endCommentIndex != -1)
                        {
                            line = line.Remove(startCommentIndex, endCommentIndex - startCommentIndex + 2);
                        }
                        else
                        {
                            line = line.Substring(0, startCommentIndex);
                            inMultiLineComment = true;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // Deleting one-line comments
                line = singleLineCommentRegex.Replace(line, "");

                // Deleting attributes
                line = attributeRegex.Replace(line, "");

                if (stringWithRussianTextRegex.IsMatch(line))
                {
                    yield return lineNumber;
                }
            }
        }
    }
}
