# Array Notes

## Fixed Arrays

Run times:

- **Insertion Random: O(n)** - It’s easy to insert randomly anywhere in the array. Getting to the location of insertion take O(1) time. However, if you want to preserve order, there is a chance you will have to move O(n) elements to put the number there. Therefore, it's O(n).
- **Insertion Front: O(n)** – Inserting in the front of an array will usually take O(n) time, as you have to shift up to n elements backwards to insert properly.  
- **Insertion Back: O(1)** – Nothing has to be shifted, so you can accomplish this in O(1) time.    
- **Deletion Random: O(n)** – Deleting randomly from the array is just like inserting randomly. You can get to and remove the element in O(1) time. However, if you need to delete an element and not create a hole, then this becomes O(n) as you will need to shift everything to cover the hole.     
- **Deletion Front: O(n)** – Same as Insertion, a shift of up to n elements will be required.  
- **Search Unsorted: O(n)** – If the array is unsorted, there is no way of knowing where the element is going to be. So at worst case, it’s going to be a search of the entire array to find the element.  
- **Search Sorted: O(logn)** – If the array is sorted, we can keep cutting the array in half to find the element we are searching for. This means it will take at most logn operations to find our element. (Reverse exponential).  

## Circular Array
Circular behaviour can be acheived using front and back markers and moving them when inserting data. Becuase you do not know if data will be added to the front or back, Modulo is used to to prevent indexing outside of the array. 

```plain
front: (f - 1) mod arraySize
back: (b + 1) mod arraySize

example adding 8 to the front: 
[0,4,3,-,-,-] --> [0,4,3,-,-,8]
```

This improves the following run times becuase no data need to be shifted.
- **Insertion Front: O(1)**
- **Deletion Front: O(1)**

## Dynamic Arrays
When the array capacity has been reached but you wish to insert more data if we were to add just 1 more capacity this would result on O(n) operations creating a new array in memory. Instead we can double the capacity which results in O(log(n)).

As n -> infinity this can be approximated as constant time O(1). This is true because the rate of occurence of an O(n) operation, i.e. when we resize the array, is ever decreasing. If the occurence was constant like 1 O(n) for every 999 O(1) we could not make this approximation.

## Singly Linked Lists
Works by storing data "individually" in nodes. Each node points to the next.

```
Head --> <Q:.>  --> <W:.> --> <E:.> --> <R:.> --> Null
```
The benefit of this approach is memory efficiency. There is no wasted space. As a caveat the runtimes for inserting/deleting at the fron become constant time and the back becomes n-time. One drawback is that indexing is not possible and neither is binary search regardless of if the list is sorted.

-**Insert(Rand): O(n)** - To insert at a particular location, one has to traverse the list up to that point to insert there.
-**Insert(Front): O(1)** – We just move the head pointer to the new node and point the new node at the old head.
-**Insert(Back): O(n)** – We will have to traverse the entire array to get to the back. (We will find a way to improve this a little bit later)
-**Delete(Random/Back): O(n)** – We must traverse the length of the element we want to delete
-**Delete(Front): O(1)** – Just as easy as inserting, just have to remove the first element and repoint the head pointer
-**Search(Sorted): O(n)** – Doesn’t matter if it’s sorted or not. We at worst have to traverse the entirety of the list to find the element. (And if the element isn’t in the list, we have to traverse the entire list to figure that out. )
-**Search(Unsorted): O(n)** – Exactly the same as the Sorted.

## Doubly Linked Lists (with tail pointer)
Singly linked lists can be improved by adding a pointer to the previous node and by introducing a tail pointer.

This improves the below runtimes:
-**Insert(Back): O(n) --> O(1)**
-**Delete(Back): O(n) --> O(1)**

## Stacks (LIFO)
Stacks are used for a variety of operations in computer science. They can do anything from helping to navigate through a maze, to helping traverse a graph. Stacks work by only allow insert/remove to occur at one end, typically an array or linked list, i.e. they are Last-In-First-Out (LIFO)

```c#
pop() // removes the top most element
push() // adds new element to the top of the stack
```

## Queues (FIFO)
A queue is similar to a stack, but with one large difference, insertions and deletions take part from separate ends. They are good for processing data when order is important like CPU instructions or tree traversals.

```c#
Enqueue() // add element to the "back" of the queue
Dequeue() // take element from the "front" of the queue
// "front and "back" are ambiguous, it depends on how the queue is implemented.
```