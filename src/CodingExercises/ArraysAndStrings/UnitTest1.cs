namespace ArraysAndStrings;

public class StringExercises
{
    public bool HasUniqueChars(string str)
    {
        var vector = 0;

        foreach (var c in str.ToCharArray())
        {
            var val = c - 'a';
            vector |= 1 << val;
        }

        return true;
    }
}

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}