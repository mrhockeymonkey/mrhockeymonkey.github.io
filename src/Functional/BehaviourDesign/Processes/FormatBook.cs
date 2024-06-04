using BehaviourDesign.Models;

namespace BehaviourDesign.Processes;

public delegate string FormatBook(BookType book);
public delegate string FormatBookExt(FormatNamesList namesFormatter, BookType book);

public static class FormatBookExtensions
{
    public static FormatBook Apply(this FormatBookExt formatter, FormatNamesList namesFormatter) =>
        book => formatter(namesFormatter, book);
}

public static class FormatBookDefaults
{
    public static FormatBookExt NamesThenTitle => (namesFormatter, book) =>
        $"{namesFormatter(book.Authors)}, {book.Title.Value}";
    
    public static FormatBookExt TitleThenNames => (namesFormatter, book) =>
        $"{book.Title.Value} by {namesFormatter(book.Authors)}";
}