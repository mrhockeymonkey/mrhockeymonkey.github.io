# Stack vs Heap

## Stack
Each thread has its own stack memory (could be considered the state of the thread). Value types are stored on the stack and removed once they loose scope, i.e. the method has finished and returned control to the caller. 

Value types are defined using the `struct` keyword 

## Heap
Any thread can access data stored on the heap. Reference Types, i.e. inherit System.Object, are placed on the heap and a reference (or pointer) to them is placed on the stack. When memory pressure occurs the GC stops all threads, removes any data that no longer has references and compacts the remaining data, which can be expensive.

Reference types are defined using the `class` keyword.

## String is a Refernce type that acts like a Value type
In .Net Framework Strings are immutable reference types. All .net datatypes has default size except string and user type. So String is a Reference type, because it does not have default allocation size.

Immutable means, it cannot be changed after it has been created. Every change to a string will create a new string. This is why all of the String manipulation methods return a string.

For an example, an integer (System.Int32 ) has a fixed memory size(4 bytes) of Value range -2,147,483,648 through 2,147,483,647. Hence, an integer can be stored on the Stack (i.e. fixed memory). Alternatively, a String does not have a pre-defined memory size and it can be huge(the value range may be 0 to approximately 2 billion Unicode characters), so it requires dynamic memory allocation.

When a String object is created, the actual value is stored within dynamic memory, or on the Heap. Because strings can be much larger than the size of a pointer, they are designed as reference types but becuase they are immutable they act as if they were value types.