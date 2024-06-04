using BehaviourDesign.Models;

namespace BehaviourDesign.Processes;

public delegate BookType AddAuthor(BookType book, NameType name);

public static class AddAuthorExtensions
{
    public static Func<NameType, BookType> Apply(this AddAuthor strategy, BookType book) =>
        author => strategy(book, author);
}

public static class AddAuthorDefaults
{
    // note they are property getters
    public static AddAuthor AddAnyAuthor => (book, author) =>
        book with { Authors = [..book.Authors, author] };
    
    public static AddAuthor AddUniqueAuthor => (book, author) =>
        book.Authors.Contains(author) ? book
            : book with { Authors = [..book.Authors, author] };
}