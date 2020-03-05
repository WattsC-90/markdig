using Markdig;
using System;
using System.IO;

namespace SimpleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("Root.md");

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var result = Markdown.ToHtml(input, pipeline);

            Console.WriteLine(result);
        }
    }
}
