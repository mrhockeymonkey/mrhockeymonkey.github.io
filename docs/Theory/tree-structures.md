# Tree Structures
Trees can have a variety of structures. They can have a set number of children or have an infinite amount. It all comes down to how you want to use them. 

## Binary Search Tree
A special type of tree however is known as the Binary Search Tree (BST).

A binary search tree is a tree with these rules:
- Each node can only have at most two children  
- All right children must be greater than  
- All left children must be less than or equal to. (You can put the equal to on the right or left)  

Run Times:
- **Search: O(log n)** – We only have to touch log elements (the height of the tree) to figure out if the element is in the list or not.  
- **Insert: O(log n)** – Same with inserting in to the tree. We ask the same questions as above, and find the empty place in the tree, and insert there.  
- **Delete O(log n)** – Same as the rest, we can delete by just asking the same questions. 

Note however in the worst case, if the order elements added was already sorted, the runtimes above become O(n) because the structure would resemble a linked list rather than a tree. 

## Heaps
Heaps in essence are just stricter trees. They have all the same properties of trees, with the additional “Heap Property” rule added.

The heap property is a rule which gives a relationship between the parent and child nodes within a tree. It states that the parent node must always be either greater, or less than its children. If it has to be greater, then it is a max heap, if less, a min heap. This is acheived by checking the parent value at insertion and swapping if necessary.

Run Times:
- **Insert: O(logn)**
- **Delete: O(logn)**
- **GetMin/Max: O(1)**