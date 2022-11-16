namespace ConcurrentCircularBuffer;

public class CasBasedCircularArray<T> : IConcurrentCircularArray<T>
{
    private readonly T[] _items;
    private volatile int _pointer;
    private int _size;
    private readonly object _lock;
    
    public CasBasedCircularArray(int size)
    {
        _size = size;
        _items = new T[_size];
        _pointer = 0;
        _lock = new();
    }
    
    public void Append(T item)
    {
        int currentPointer, nextPointer;
        do
        {
            currentPointer = _pointer;
            nextPointer = (currentPointer + 1) % _size;
        } while (currentPointer != Interlocked.CompareExchange(ref _pointer, nextPointer, currentPointer));
        
        // TODO im not convinced
        _items[currentPointer] = item;
        Console.WriteLine($"Position {currentPointer} is now {item.ToString()}");
        
        // while (true)
        // {
        //
        // }
        // lock (_lock)
        // {
        //     _items[_pointer] = item;
        //     _pointer++;
        //     _pointer %= _size; // TODO use bitwise & _pointerMask (size - 1)
        //     Console.WriteLine($"Position {_pointer} is now {item.ToString()}");
        // }
    }
}