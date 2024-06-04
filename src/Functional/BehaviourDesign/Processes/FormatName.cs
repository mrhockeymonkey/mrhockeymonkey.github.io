namespace BehaviourDesign.Processes;

public delegate string FormatName(NameType name);
public delegate string FormatNamesList(NameType[] names);
public delegate string FormatNamesListExt(FormatName nameFormatter, NameType[] names);

public static class FormatNamesListExtensions
{
    public static FormatNamesList Apply(this FormatNamesListExt formatter, FormatName nameFormatter) =>
        names => formatter(nameFormatter, names);
}

public static class FormatNamesListDefaults
{
    public static FormatNamesListExt CommaSeparatedNames => (nameFormatter, names) =>
        string.Join(", ", names.Select(nameFormatter.Invoke));
}

public static class FormatNameDefaults
{
    public static FormatName FormatFulName => name => name.Map(
        fullName => $"{fullName.FirstName} {fullName.LastName}",
        mononym => mononym.Name);
    
    public static FormatName FormatInitials => name => name.Map(
        fullName => $"{fullName.FirstName[..1]}. {fullName.LastName[..1]}.",
        mononym => $"{mononym.Name[..1]}.");
    
    public static FormatName FormatAcademicName => name => name.Map(
        fullName => $"{fullName.LastName}, {fullName.FirstName[..1]}.",
        mononym => mononym.Name);
}