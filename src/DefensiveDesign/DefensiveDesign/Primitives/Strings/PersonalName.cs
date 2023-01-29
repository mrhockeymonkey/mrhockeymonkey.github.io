namespace Primitives.Strings;

public class PersonalName : IEquatable<PersonalName>
{
    public string FirstName { get;  }
    public string MiddleName { get;  }
    public string LastName { get; }

    public PersonalName(string firstName, string middleName, string lastName)
    {
        if (IsBadMandatoryPart(firstName) || IsBadOptionalPart(middleName) || IsBadMandatoryPart(lastName))
            throw new ArgumentException();
        
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

    private bool IsBadOptionalPart(string? part) =>
        part is null || 
        (part.Length > 0 && char.IsHighSurrogate(part[-1]));

    private bool IsBadMandatoryPart(string part) => 
        IsBadOptionalPart(part) || 
        part == string.Empty;

    // let this object "know" how to compare equality as ignore case instead of the calling code checking. 
    private bool ArePartsEqual(string part1, string part2) =>
        string.Compare(part1, part2, StringComparison.OrdinalIgnoreCase) == 0;

    public bool Equals(PersonalName? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ArePartsEqual(FirstName, other.FirstName) && 
               ArePartsEqual(MiddleName, other.MiddleName) && 
               ArePartsEqual(LastName, other.LastName);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((PersonalName)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, MiddleName, LastName);
    }
}