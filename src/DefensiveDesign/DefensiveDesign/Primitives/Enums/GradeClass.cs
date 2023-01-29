namespace Primitives.Enums;

public class GradeClass
{
    public static GradeClass A => new GradeClass();
    public static GradeClass B { get; }
    public static GradeClass C { get; }
    public static GradeClass D { get; }
    public static GradeClass F { get; }

    public string Label { get; }
    public bool IsPassing { get; }

    private GradeClass()
    {
        // dont allow construction of invalid grade
    }
}