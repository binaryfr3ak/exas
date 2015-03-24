﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExAs.Utils;

namespace ExAs.Assertions.Generic
{
    public class ObjectAssertion<T> : IAssert<T>
    {
        private readonly List<PropertyAssertion<T>> propertyAssertions; 
        private IsNotNullAssertion isNotNullAssertion;
        private IsNullAssertion isNullAssertion;

        public ObjectAssertion()
        {
            propertyAssertions = new List<PropertyAssertion<T>>();
        }

        public ObjectAssertion<T> IsNotNull()
        {
            isNotNullAssertion = new IsNotNullAssertion();
            return this;
        }

        public ObjectAssertion<T> IsNull()
        {
            isNullAssertion = new IsNullAssertion();
            return this;
        } 

        public PropertyAssertion<T> HasProperty(Expression<Func<T, object>> property)
        {
            var propertyAssertion = new PropertyAssertion<T>(property, this);
            propertyAssertions.Add(propertyAssertion);
            return propertyAssertion;
        } 

        public AssertionResult Assert(T actual)
        {
            if (isNotNullAssertion != null)
            {
                AssertionResult isNotNullResult = isNotNullAssertion.Assert(actual);
                if (!isNotNullResult.succeeded)
                    return isNotNullResult;
                if (!propertyAssertions.Any())
                    return new AssertionResult(true, TypeName().Add(isNotNullResult.log));
            }
            if (isNullAssertion != null)
                return isNullAssertion.Assert(actual);

            if (!propertyAssertions.Any())
                return new AssertionResult(true, "no assertions");

            IReadOnlyCollection<PropertyAssertionResult> results = propertyAssertions.Map(assertion => assertion.Assert(actual));
            int lengthOfLongestProperty = results.Max(x => x.propertyName.Length);
            IReadOnlyCollection<string> propertyResults = results.Map(
                r =>
                {
                    string propertyString = r.propertyName.FillUpWithSpacesToLength(lengthOfLongestProperty).Add(" = ");
                    string valueBlockString = r.childResult.actualValueString.Add(" ").Add(r.childResult.expectationString);
                    return StringFunctions.HangingIndent(propertyString, valueBlockString);
                });
            string log = StringFunctions.HangingIndent(TypeName(), string.Join(Environment.NewLine, propertyResults));
            return new AssertionResult(results.All(r => r.childResult.succeeded), log);
        }

        private static string TypeName()
        {
            return typeof(T).Name.Add(": ");
        }
    }
}