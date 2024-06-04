namespace BehaviourDesign.Models;

public record TitleType(string Value);

public class Title
{
    public static TitleType? Create(string value) =>
        string.IsNullOrWhiteSpace(value) ? null : new(value);
}