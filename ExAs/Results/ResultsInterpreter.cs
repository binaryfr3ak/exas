using System;
using System.Collections.Generic;
using System.Linq;
using ExAs.Utils;

namespace ExAs.Results
{
    public class ResultsInterpreter
    {
        public static string PrintOut(NewObjectAssertionResult result)
        {
            var intermediateResultPrint = Interpret(result);
            string[] logLines = intermediateResultPrint.actualValue.SplitLines();
            string[] expectationLines = intermediateResultPrint.expectedValue.SplitLines();
            int longestLogLine = logLines.MaxOrDefault(s => s.Length);
            IReadOnlyList<string> resultingLogLines = logLines.Map(expectationLines,
                                                                   (al, el) => al.FillUpWithSpacesToLength(longestLogLine).Add(" ").Add(el));
            return string.Join(Environment.NewLine, resultingLogLines);
        }

        public static bool IsSuccess(NewObjectAssertionResult result)
        {
            var intermediateResultPrint = Interpret(result);
            return intermediateResultPrint.succeeded;
        }

        public static IntermediateResultPrint Interpret(NewObjectAssertionResult result)
        {
            ValueAssertionResult isNullResult = result.isNullResult;
            ValueAssertionResult isNotNullResult = result.isNotNullResult;
            IReadOnlyCollection<PropertyAssertionResult> propertyResults = result.propertyResults;

            if (isNotNullResult != null)
            {
                if (!isNotNullResult.succeeded)
                    return new IntermediateResultPrint(isNotNullResult.succeeded, isNotNullResult.actualValueString, isNotNullResult.expectationString);
                if (!propertyResults.Any())
                    return new IntermediateResultPrint(isNotNullResult.succeeded, isNotNullResult.actualValueString, isNotNullResult.expectationString);
            }

            if (isNullResult != null)
            {
                return new IntermediateResultPrint(isNullResult.succeeded, isNullResult.actualValueString, isNullResult.expectationString);
            }

            if (!propertyResults.Any())
                return new IntermediateResultPrint(true, "no assertions", " ");

            int lengthOfLongestProperty = propertyResults.Max(x => x.propertyName.Length);
            IReadOnlyCollection<string> propertyActualValues = propertyResults.Map(
                r =>
                {
                    string propertyString = r.propertyName.FillUpWithSpacesToLength(lengthOfLongestProperty).Add(" = ");
                    return StringFunctions.HangingIndent(propertyString, r.childResult.actualValueString);
                });
            string expectations = string.Join(Environment.NewLine, propertyResults.Select(r => r.childResult.expectationString));
            var log = StringFunctions.HangingIndent(result.type, string.Join(Environment.NewLine, propertyActualValues));
            return new IntermediateResultPrint(propertyResults.All(r => r.childResult.succeeded), log, expectations);
        }
    }
}