// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license.
// See the license.txt file in the project root for more information.
using Markdig.Parsers;
using System.Diagnostics;

namespace Markdig.Syntax
{
    [DebuggerDisplay("{GetType().Name} Line: {Line}, {Lines} Level: {Level}")]
    public class MarkdownIncludeBlock : LeafBlock
    {
        public MarkdownIncludeBlock(BlockParser parser) : base(parser)
        {
            ProcessInlines = true;
        }

        /// <summary>
        /// The alternative name provided of the file
        /// </summary>
        public string AlternativeName { get; set; }
        /// <summary>
        /// The relative path to the file that should be included
        /// </summary>
        public string RelativePath { get; set; }
    }
}