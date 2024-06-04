namespace ClassLibrary1;

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