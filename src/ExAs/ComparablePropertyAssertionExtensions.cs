﻿using System;
using ExAs.Assertions;
using ExAs.Assertions.PropertyAssertions.Numbers;

namespace ExAs
{
    public static class ComparablePropertyAssertionExtensions
    {
        public static ObjectAssertion<T> IsSmallerThan<T, TProperty>(this IAssertMember<T, TProperty> property, TProperty expected)
            where TProperty : IComparable<TProperty>
        {
            return property.SetAssertion(new IsSmallerAssertion<TProperty>(expected));
        }

        public static ObjectAssertion<T> IsBiggerThan<T, TProperty>(this IAssertMember<T, TProperty> property, TProperty expected)
            where TProperty : IComparable<TProperty>
        {
            return property.SetAssertion(new IsBiggerAssertion<TProperty>(expected));
        }

        public static ObjectAssertion<T> IsInRange<T, TProperty>(this IAssertMember<T, TProperty> property, TProperty min, TProperty max)
            where TProperty : IComparable<TProperty>
        {
            return property.SetAssertion(new InRangeAssertion<TProperty>(min, max));
        }
    }
}