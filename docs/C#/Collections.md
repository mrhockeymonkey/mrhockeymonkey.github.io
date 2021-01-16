# Collections

### When to use what
Note: each interface builds on top of the first

- IEnumerable: When you only care about looping (GetEnumerator)
- ICollection: When you care about size (Count) or modifying (Add/Remove)
- IList: When you want to modify and care about positions of elements (Indexing)