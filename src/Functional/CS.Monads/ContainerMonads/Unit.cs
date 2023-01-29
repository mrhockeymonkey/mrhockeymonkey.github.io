using System.Diagnostics.CodeAnalysis;

namespace CS.Monads.ContainerMonads;

public class Unit<T>
{
    public T Value { get; }

    public Unit([DisallowNull] T value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public static implicit operator Unit<T>(T @this) => @this.ToUnit();
    public static implicit operator T(Unit<T> @this) => @this.Value;
}