using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskExamples
{
    class Program
    {
        //public static async Task DoWork()
        //{
        //    await Task.Delay(TimeSpan.FromSeconds(3));

        //    Console.WriteLine($"{System.Threading.Thread.CurrentThread.get}");
        //}
        
        static async Task Main(string[] args)
        {
            var tasks = new List<Task>();
            //foreach (var i in new[] { "a", "b", "c", "d"})
            //{
            //    tasks.Add(DoWork());
            //}
            tasks.Add(new Task(() => {
                Console.WriteLine("running");
            }));

            await Task.Delay(3000);

            Console.WriteLine("start");
            

            await Task.WhenAll(tasks);
            //Console.WriteLine("Hello World!");
        }
    }
}
