namespace Primitives.Enums;

// for performance reasons C# does not check for valid values of enum
// underlying type is integral: byte/sbyte short/ushort int/uint long/ulong
// in general enums are evil

public enum GradeEnum
{
    A, // = 0
    B, // = 1
    C, // ..
    D, // ..
    F // = 4
}