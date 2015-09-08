namespace ExAs.Results
{
    public class IntermediateResultPrint
    {
        public readonly bool succeeded;
        public readonly string actualValue;
        public readonly string expectedValue;

        public IntermediateResultPrint(bool succeeded, string actualValue, string expectedValue)
        {
            this.succeeded = succeeded;
            this.actualValue = actualValue;
            this.expectedValue = expectedValue;
        }
    }
}