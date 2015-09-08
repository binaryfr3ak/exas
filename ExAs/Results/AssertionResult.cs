namespace ExAs.Results
{
    public class AssertionResult
    {
        public readonly bool succeeded;
        public readonly string log;

        public AssertionResult(bool succeeded, string log)
        {
            this.succeeded = succeeded;
            this.log = log;
        }
    }
}