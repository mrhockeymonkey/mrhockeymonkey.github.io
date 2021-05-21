using System;
using System.Collections.Generic;

namespace DotNetDesignPatternDemos.Creational.Builder
{
    public class Person
    {
        public string Name, Position;
    }

    public sealed class PersonBuilder
    {
        // This class uses a functional approach by maintaining a list of actions
        // to apply to a person object.
        public readonly List<Action<Person>> Actions
          = new List<Action<Person>>();

        // By returning PersonBuilder we are making a fluent api
        public PersonBuilder Called(string name)
        {
            Actions.Add(p => { p.Name = name; });
            return this;
        }

        public Person Build()
        {
            // Aggregate all actions and return the resulting object
            var p = new Person();
            Actions.ForEach(a => a(p));
            return p;
        }
    }

    public static class PersonBuilderExtensions
    {
        // Because PersonBuilder is seal we cannot inherit so extension methods can be used to add functionality
        public static PersonBuilder WorksAsA
          (this PersonBuilder builder, string position)
        {
            builder.Actions.Add(p =>
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
        }
    }
}