using System;
using ExAs.Assertions;
using ExAs.Results;

namespace ExAs
{
    public static class ExAsExtensions
    {
        public static AssertionResult Evaluate<T>(this T instance, Func<ObjectAssertion<T>, ObjectAssertion<T>> assertion)
        {
            ObjectAssertion<T> exAssertion = assertion(new ObjectAssertion<T>());
            NewObjectAssertionResult result = exAssertion.NewAssert(instance);
            return new AssertionResult(ResultsInterpreter.IsSuccess(result), ResultsInterpreter.PrintOut(result));
        }
    }
}