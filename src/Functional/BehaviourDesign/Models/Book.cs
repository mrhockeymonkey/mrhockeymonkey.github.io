namespace BehaviourDesign.Models;

public record BookType(TitleType Title, NameType[] Authors);

public static class Book
{
    public static BookType Create(TitleType title, params NameType[] authors) =>
        new(title, authors);
}