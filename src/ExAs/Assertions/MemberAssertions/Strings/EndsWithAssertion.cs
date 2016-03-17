﻿using ExAs.Results;
using ExAs.Utils;

namespace ExAs.Assertions.MemberAssertions.Strings
{
    public class EndsWithAssertion : IAssertValue<string>
    {
        private readonly string expectedEnd;

        public EndsWithAssertion(string expectedEnd)
        {
            this.expectedEnd = expectedEnd;
        }

        public ValueAssertionResult AssertValue(string actual)
        {
            return new ValueAssertionResult(
                actual.EndsWith(expectedEnd), 
                actual.ToValueString(), 
                $"(expected: ends with '{expectedEnd}')");
        }
    }
}