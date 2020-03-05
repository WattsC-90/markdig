using Markdig.Helpers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markdig.Parsers
{
    public class MarkdownIncludeBlockParser : BlockParser
    {
        public MarkdownIncludeBlockParser()
        {
            base.OpeningCharacters = new[] { ':' };
        }

        //our pattern is
        //:[alternate-text] (https://github.com/yzane/vscode-markdown-pdf/blob/master/relative-path-to-file.md)
        //must be on a single line..
        public override BlockState TryOpen(BlockProcessor processor)
        {
            // If we are in a CodeIndent, early exit
            if (processor.IsCodeIndent)
            {
                return BlockState.None;
            }

            var line = processor.Line;
            var c = line.CurrentChar;
            var matchingChar = c;
            var stringBuilder = new StringBuilder();

            //need to optimise this, its obviously late night coding.. but it works..for the example at least
            bool altName = false, altDone = false, path = false;
            string alt = string.Empty, relPath = string.Empty;

            var lineLength = line.End - line.Start+1;
            for (int i = 0; i < lineLength; i++)
            {
                if (c == '[')
                {
                    altName = true;
                }
                else if (c != ']' && altName == true)
                {
                    stringBuilder.Append(c);
                }
                else if(c == ']')
                {
                    altName = false;
                    altDone = true;
                    alt = stringBuilder.ToString();
                    stringBuilder = new StringBuilder();
                }

                else if (c == '(')
                {
                    path = true;
                }
                else if (c != ')' && path == true)
                {
                    stringBuilder.Append(c);
                } else if (c == ')')
                {
                    relPath = stringBuilder.ToString();
                }
                c = line.NextChar();
            }

            if (alt.Length >0 && relPath.Length > 0 && (c.IsSpaceOrTab() || c == '\0'))
            {
                var includeBlock = new MarkdownIncludeBlock(this)
                {
                    AlternativeName = alt,
                    RelativePath = relPath,
                    Span = {Start = line.Start, End=line.Length},
                    Column = processor.Column,
                };
                processor.NewBlocks.Push(includeBlock);
                return BlockState.Break;
            }
            return BlockState.None;
        }
    }
}
