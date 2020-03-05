// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.
using Markdig.Syntax;
using System.IO;

namespace Markdig.Renderers.Html
{
    public class IncludeBlockRenderer : HtmlObjectRenderer<MarkdownIncludeBlock>
    {

        protected override void Write(HtmlRenderer renderer, MarkdownIncludeBlock obj)
        {
            //...recursive.. eek
            var fileContent = File.ReadAllText(obj.RelativePath);

            //this seems limited, how do we get round this?
            //I need a pointer or two how to circumvent this restriction,
            //I would want to use the same pipeline that the user passed in/used when they created us initially.
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var result = Markdown.ToHtml(fileContent, pipeline);

            renderer.Write(result);
        }
    }
}