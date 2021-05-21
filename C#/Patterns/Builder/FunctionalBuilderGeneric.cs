using System;
using System.Collections.Generic;
using System.Linq;

// This example builds on FunctionalBuild.cs by creating a base class with generics
namespace FunctionalBuilderGeneric
{
    public class Person
    {
        public string Name, Position;
    }

    public abstract class FunctionalBuilder<TSubject, TSelf> 
        where TSelf : FunctionalBuilder<TSubject, TSelf> // TSelf must implement this base class
        where TSubject : new() // the subject must has a parameterless contructor
    {
        // Here we use Func instead of Action so that we can use System.Linq.Aggregate below
        private readonly List<Func<TSubject, TSubject>> _functions
          = new List<Func<TSubject, TSubject>>();

        public TSubject Build() =>
            // Aggregate iterates over the functions, starting with a new Subject, and applies each function
            _functions.Aggregate(new TSubject(), (subject, func) => func(subject));

        public TSelf AddAction(Action<TSubject> action)
        {
            // function should apply given action and return subject
            // a bit over complicated?
            Func<TSubject, TSubject> newFunction = subject =>
            {
                action(subject);
                return subject;
            };
            _functions.Add(newFunction);
            return (TSelf) this; // fluent api
        }
    }

    public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name) =>
            AddAction(p => p.Name = name);
    }

    public static class PersonBuilderExtensions
    {
        // Because PersonBuilder is sealed we cannot inherit so extension methods can be used to add functionality
        public static PersonBuilder WorksAsA
          (this PersonBuilder builder, string position)
        {
            builder.AddAction(p =>
            {
                p.Position = position;
            });
            return builder;
        }
    }

    public class FunctionalBuilder
    {
        public static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            var person = pb.Called("Dmitri").WorksAsA("Programmer").Build();
            Console.WriteLine($"Person: Name={person.Name}, Position={person.Position}");
        }
    }
}