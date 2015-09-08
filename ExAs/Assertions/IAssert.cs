using ExAs.Results;

namespace ExAs.Assertions
{
    public interface IAssert<in T>
    {
        ObjectAssertionResult Assert(T actual);

        NewObjectAssertionResult NewAssert(T actual);
    }
}