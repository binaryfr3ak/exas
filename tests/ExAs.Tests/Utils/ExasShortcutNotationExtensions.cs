﻿using System;
using System.Linq.Expressions;
using ExAs.Assertions;
using ExAs.Assertions.PropertyAssertions.Booleans;
using ExAs.Results;

namespace ExAs.Utils
{
    public static class ExasShortcutNotationExtensions
    {
        public static PropertyAssertion<T, TProperty> p<T, TProperty>(
            this ObjectAssertion<T> objectAssertion, 
            Expression<Func<T, TProperty>> expession)
        {
            var propertyAssertion = new PropertyAssertion<T, TProperty>(expession, objectAssertion);
            objectAssertion.AddPropertyAssertion(propertyAssertion);
            return propertyAssertion;
        }

        public static ObjectAssertion<ObjectAssertionResult> Fullfills(
            this ObjectAssertion<ObjectAssertionResult> instance, 
            bool succeeded, 
            string log, 
            string expectation)
        {
            IAssertValue<bool> succeededAssertion = succeeded ? (IAssertValue<bool>) new IsTrueAssertion() : new IsFalseAssertion();
            return instance.p(x => x.succeeded)  .SetAssertion(succeededAssertion)
                           .p(x => x.log)        .IsEqualTo(log)
                           .p(x => x.expectation).IsEqualTo(expectation);
        } 
    }
}