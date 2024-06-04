// See https://aka.ms/new-console-template for more information

using Strings;

Console.WriteLine("Hello, World!");

var a = "aceg";
var s = new StringExercises();

Console.WriteLine($"'aceg' {s.HasUniqueChars("acge")}");
Console.WriteLine($"'a' {s.HasUniqueChars("a")}");

Console.WriteLine($"'aba' {s.HasAtMostOneOddCharCounts("aba")}");
Console.WriteLine($"'abab' {s.HasAtMostOneOddCharCounts("abab")}");
Console.WriteLine($"'ababba' {s.HasAtMostOneOddCharCounts("ababba")}");
