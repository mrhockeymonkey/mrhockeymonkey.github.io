using NUnit.Framework;

namespace ClassLibrary1;

[TestFixture]
public class StringExercises_Tests
{
    
    [Test]
    public void HasUniqueChars()
    {
        var sut = new StringExercises();
        var r = sut.HasUniqueChars("abdegij");
    }
}