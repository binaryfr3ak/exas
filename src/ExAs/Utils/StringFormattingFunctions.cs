﻿using System;
using System.Collections.Generic;
using System.Linq;
using ExAs.Utils.SystemExtensions;

namespace ExAs.Utils
{
    public static class StringFormattingFunctions
    {
        public static string HangingIndent(string unindentedPrefix, string indentedBlock)
        {
            int indentation = 0;
            if (unindentedPrefix != null)
                indentation = unindentedPrefix.Length;
            return String.Format("{0}{1}", unindentedPrefix, indentedBlock).HangingIndent(indentation);
        }

        public static string HangingIndent(this string value, int indentation)
        {
            if (value == null)
                return null;
            IReadOnlyCollection<string> lines = value.Split(new[] { Environment.NewLine}, StringSplitOptions.None);
            IEnumerable<string> indentedSubLines = lines.Rest().Select(x => String.Format("{0}{1}", indentation.Spaces(), x));
            IReadOnlyCollection<string> result = SystemFunctions.Cons(lines.First(), indentedSubLines);
            return String.Join(Environment.NewLine, result);
        }

        public static string FillUpWithSpacesToLength(this string input, int length)
        {
            if (length <= input.Length)
                return input;
            int amount = length - input.Length;
            return input.Add(amount.Spaces());
        }

        public static string[] SplitLines(this string multiLineString)
        {
            return multiLineString.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
        }
    }
}