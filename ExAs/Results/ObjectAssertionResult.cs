using ToText;

namespace ExAs.Results
{
    public class ObjectAssertionResult
    {
        public readonly bool succeeded;
        public readonly string log;
        public readonly string expectation;

        public ObjectAssertionResult(bool succeeded, string log, string expectation)
        {
            this.succeeded = succeeded;
            this.log = log;
            this.expectation = expectation;
        }

        public override string ToString()
        {
            return this.ToText(x => x.succeeded,
                               x => x.log,
                               x => x.expectation);
        }
    }
}