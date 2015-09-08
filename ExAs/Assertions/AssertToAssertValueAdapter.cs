using ExAs.Results;

namespace ExAs.Assertions
{
    public class AssertToAssertValueAdapter<T> : IAssertValue<T>
    {
        private readonly IAssert<T> assert;

        public AssertToAssertValueAdapter(IAssert<T> assert)
        {
            this.assert = assert;
        }

        public ValueAssertionResult AssertValue(T actual)
        {
            NewObjectAssertionResult result = assert.NewAssert(actual);
            var intermediateResultPrint = ResultsInterpreter.Interpret(result);
            return new ValueAssertionResult(intermediateResultPrint.succeeded, intermediateResultPrint.actualValue, intermediateResultPrint.expectedValue);
        }
    }
}