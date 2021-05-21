using System;
using System.Threading.Tasks;

namespace Factory
{
    // It is not possible to call an async method in a constructor
    // If async initialization is required you can instead use a factory method
    class Foo {
        private Foo() {
            // make ctor private so it cannot be created uninitialized
        }

        private async Task InitAsync(){
            await Task.Delay(5);
        }

        public static async Task<Foo> CreateAsync(){
            // factory mehtod
            var result = new Foo();
            await result.InitAsync();
            return result;
        }
    }
    
    class Program
    {
        static async Task Main(string[] args)
        {
            Foo x = await Foo.CreateAsync();
            Console.WriteLine("hello");
        }
    }
}
