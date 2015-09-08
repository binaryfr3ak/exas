using System.Collections.Generic;

namespace ExAs.Results
{
    public class NewObjectAssertionResult
    {
        public readonly string type;
        public readonly ValueAssertionResult isNullResult;
        public readonly ValueAssertionResult isNotNullResult;
        public readonly IReadOnlyCollection<PropertyAssertionResult> propertyResults;

        public NewObjectAssertionResult(
            string type, 
            ValueAssertionResult isNullResult, 
            ValueAssertionResult isNotNullResult, 
            IReadOnlyCollection<PropertyAssertionResult> propertyResults)
        {
            this.type = type;
            this.isNullResult = isNullResult;
            this.isNotNullResult = isNotNullResult;
            this.propertyResults = propertyResults;
        }
    }
}