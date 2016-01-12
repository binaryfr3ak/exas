﻿using System.Collections.Generic;
using System.Linq;
using ExAs.Results;
using ExAs.Utils;

namespace ExAs.Assertions.PropertyAssertions.Enumerables
{
    public class HasCountAssertion<T> : IAssertValue<IEnumerable<T>>
    {
        private readonly int expected;

        public HasCountAssertion(int expected)
        {
            this.expected = expected;
        }

        public ValueAssertionResult AssertValue(IEnumerable<T> actual)
        {
            List<T> actualValues = actual.ToList();
            return new ValueAssertionResult(
                actualValues.Count() == expected, 
                actualValues.ToValueString(), 
                ComposeLog.Expected(ExpectedMessage()));
        }

        private string ExpectedMessage()
        {
            if (expected == 1)
                return $"{expected} Dojo";
            return $"{expected} Dojos";
        }
    }
}