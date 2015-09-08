﻿using System;
using System.Collections.Generic;
using System.Linq;
using ExAs.Assertions.ObjectAssertions;
using ExAs.Results;
using ExAs.Utils;

namespace ExAs.Assertions
{
    public class ObjectAssertion<T> : IAssert<T>
    {
        private readonly List<IAssertOnProperty<T>> propertyAssertions; 
        private IsNotNullAssertion<T> isNotNullAssertion;
        private IsNullAssertion<T> isNullAssertion;

        public ObjectAssertion()
        {
            propertyAssertions = new List<IAssertOnProperty<T>>();
        }

        public ObjectAssertion<T> IsNotNull()
        {
            isNotNullAssertion = new IsNotNullAssertion<T>();
            return this;
        }

        public ObjectAssertion<T> IsNull()
        {
            isNullAssertion = new IsNullAssertion<T>();
            return this;
        }

        public void AddPropertyAssertion(IAssertOnProperty<T> propertyAssertion)
        {
            propertyAssertions.Add(propertyAssertion);
        }

        public NewObjectAssertionResult NewAssert(T actual)
        {
            ValueAssertionResult isNotNullResult = null;
            ValueAssertionResult isNullResult = null;
         
            if (isNotNullAssertion != null)
                isNotNullResult = isNotNullAssertion.AssertValue(actual);
            if (isNullAssertion != null)
                isNullResult = isNullAssertion.AssertValue(actual);

            IReadOnlyCollection<PropertyAssertionResult> propertyAssertionResults = propertyAssertions.Map(r => r.Assert(actual));

            return new NewObjectAssertionResult(TypeName(), isNullResult, isNotNullResult, propertyAssertionResults);
        }

        public ObjectAssertionResult Assert(T actual)
        {
            if (isNotNullAssertion != null)
            {
                ValueAssertionResult isNotNullResult = isNotNullAssertion.AssertValue(actual);
                if (!isNotNullResult.succeeded)
                    return new ObjectAssertionResult(isNotNullResult.succeeded, isNotNullResult.actualValueString, isNotNullResult.expectationString);
                if (!propertyAssertions.Any())
                    return new ObjectAssertionResult(true, isNotNullResult.actualValueString, isNotNullResult.expectationString);
            }
            if (isNullAssertion != null)
            {
                ValueAssertionResult isNullResult = isNullAssertion.AssertValue(actual);
                return new ObjectAssertionResult(isNullResult.succeeded, isNullResult.actualValueString, isNullResult.expectationString);
            }

            if (!propertyAssertions.Any())
                return new ObjectAssertionResult(true, "no assertions", "-");

            IReadOnlyCollection<PropertyAssertionResult> results = propertyAssertions.Map(assertion => assertion.Assert(actual));
            int lengthOfLongestProperty = results.Max(x => x.propertyName.Length);
            IReadOnlyCollection<string> propertyResults = results.Map(
                r =>
                {
                    string propertyString = r.propertyName.FillUpWithSpacesToLength(lengthOfLongestProperty).Add(" = ");
                    return StringFunctions.HangingIndent(propertyString, r.childResult.actualValueString);
                });
            string log = StringFunctions.HangingIndent(TypeName(), string.Join(Environment.NewLine, propertyResults));
            return new ObjectAssertionResult(results.All(r => r.childResult.succeeded), log, string.Join(Environment.NewLine, results.Select(r => r.childResult.expectationString)));
        }

        private static string TypeName()
        {
            return typeof(T).Name.Add(": ");
        }
    }
}