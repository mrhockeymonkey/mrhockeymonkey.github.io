// See https://aka.ms/new-console-template for more information

using CS.Monads.ContainerMonads;
using CS.Monads.MaybeMonad;
using CS.Monads.TaskMonads;

Console.WriteLine("Hello, World!");

Console.WriteLine("### Unit Examples ###");
UnitExamples.Run();

Console.WriteLine("### Maybe Examples ###");
MaybeExamples.Run();

Console.WriteLine("### Task Examples ###");
await TaskExamples.RunAsync();



