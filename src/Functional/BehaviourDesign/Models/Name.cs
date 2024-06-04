using System.Linq.Expressions;

namespace BehaviourDesign;

public abstract record NameType;

public sealed record FullNameType(string FirstName, string LastName) : NameType;
public sealed record MononymType(string Name) : NameType;

public static class NameExtensions
{
    public static R Map<R>(this NameType name, Func<FullNameType, R> mapFullName, Func<MononymType, R> mapMononym) =>
        name switch
        {
            FullNameType fullName => mapFullName(fullName),
            MononymType mononym => mapMononym(mononym),
            _ => throw new ArgumentException("Invalid name type."),
        };
}

public static class Name
{
    public static NameType? Create(string firstName, string lastName) =>
        string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)
            ? null
            : new FullNameType(firstName, lastName);
    
    public static NameType? Create(string name) =>
        string.IsNullOrWhiteSpace(name)
            ? null
            : new MononymType(name);
    
    // if a single name cannot be created then we dont want any names.
    public static NameType[]? CreateMany(params NameType?[] names) =>
        names.Any(name => name is null) 
            ? null 
            : (NameType[]?)names!; 
}