namespace ConcurrentCircularBuffer;

public class LockBasedCircularArray<T> : IConcurrentCircularArray<T>
{
    private readonly T[] _items;
    private int _pointer;
    private int _size;
    private readonly object _lock;
    
    public LockBasedCircularArray(int size)
    {
        _size = size;
        _items = new T[_size];
        _pointer = 0;
        _lock = new();
    }
    
    public void Append(T item)
    {
        lock (_lock)
        {
            _items[_pointer] = item;
            _pointer++;
            _pointer %= _size; // TODO use bitwise & _pointerMask (size - 1)
            Console.WriteLine($"Position {_pointer} is now {item.ToString()}");
        }
    }
}