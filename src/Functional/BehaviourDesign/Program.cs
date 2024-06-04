// See https://aka.ms/new-console-template for more information

using BehaviourDesign;
using BehaviourDesign.Common;
using BehaviourDesign.Models;
using BehaviourDesign.Processes;
using static BehaviourDesign.Processes.AddAuthorDefaults;
using static BehaviourDesign.Processes.FormatNameDefaults;
using static BehaviourDesign.Processes.FormatNamesListDefaults;

var authors = Name.CreateMany(
    Name.Create("Erich", "Gamma"), Name.Create("Richard", "Helm"),
    Name.Create("Ralph", "Johnson"), Name.Create("John", "Vlissides"),
    Name.Create("Kernighan"), Name.Create("Ritchie"));
if (authors is null) return;

var title = Title.Create("The Missing Book");
if (title is null) return;

var book = Book.Create(title, authors);
if (book is null) return;

// if we want to append a new name
var gamma = Name.Create("Erich", "Gamma");
if (gamma is null) return;

    
AddAuthor strategy = AddUniqueAuthor;
var added = strategy(book, gamma);

var book1 = authors.Aggregate(Book.Create(title), strategy.Invoke);

Func<BookType, NameType, BookType> f = AddUniqueAuthor.Invoke;
Func<NameType, BookType> g = AddUniqueAuthor.Apply(book1);

var alternatives = authors.Select(AddUniqueAuthor.Apply(book));

var altTitle = Title.Create(book.Title.Value.ToUpper());
if (altTitle is null) return;

FormatNamesList namesFormatter = CommaSeparatedNames.Apply(FormatAcademicName);
FormatBook bookFormatter = FormatBookDefaults.NamesThenTitle.Apply(namesFormatter);

authors
    .Select(AddAnyAuthor.Apply(book))
    .Select(book => book with { Title = altTitle })
    .Select(bookFormatter.Invoke)
    .Select((book, offset) => $"{offset + 1}. {book}")
    .ForEach(Console.WriteLine);
    