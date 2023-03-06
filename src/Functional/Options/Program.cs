// See https://aka.ms/new-console-template for more information

using Options;

await ResultExamples.Run();


// var thing = GetThing();
// var g = new Guid();
// var f = 0;
//
// static Option<Thing> GetThing()
// {
//     return Option<Thing>.Some(new Thing("", 2));
//     //return Option<Thing1>.Some(new Thing1());
//     
// }
// public record Thing(string Name, int Value);
//
// public class Thing1
// {
//     
// }



// static Option2<string> GetSome() => new Some<string>("hi");
// static async Task<Option2<string>> GetNone() => await Task.FromResult(new None<string>());
//
// var result1 = GetSome() switch {
//     Some<string> some => some.Content,
//     None<string> => string.Empty,
//     _ => string.Empty
// };

// no compile
// await GetNone() switch
// {
//     Some<string> => Console.WriteLine("yay it worked"),
//     _ => Console.WriteLine("Nope")
// };

// switch (await GetNone()) // ugly
// {
//     case Some<string>: 
//         Console.WriteLine("yay");
//         break;
//     default:
//         Console.WriteLine("yay");
//         break;
//         
// }