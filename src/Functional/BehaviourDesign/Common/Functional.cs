namespace BehaviourDesign.Common;

public static class Functional
{
    public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
    {
        foreach (T obj in sequence) action(obj);
    }
}