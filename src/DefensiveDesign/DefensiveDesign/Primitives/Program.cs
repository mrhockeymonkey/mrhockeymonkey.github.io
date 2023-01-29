// See https://aka.ms/new-console-template for more information

using Primitives.Enums;
using Primitives.Strings;

Console.WriteLine("Hello, World!");

GradeEnum passed = GradeEnum.B;
GradeEnum failedMiserably = (GradeEnum)(-270); // no checking occurs

// you can guard against this
if (Enum.IsDefined(typeof(GradeEnum), failedMiserably))
{
    // but now you have introduced branching and potentially excessive guard clauses 
}

// Prefer to use an object in your domain over an enum
var gradeA = GradeClass.A;

// Enums may still be useful for serialization/choices on you api but should be mapped to objects immediately. 


// Instead of relying on primitive string use a class to validate a consistent state for a Name.
var name = new PersonalName(null!, "", "Matthews");


