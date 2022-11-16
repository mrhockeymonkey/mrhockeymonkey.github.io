namespace ConcurrentCircularBuffer;

public class Cas2BasedCircularArray<T> : IConcurrentCircularArray<T>
{
    private readonly T[] _items;
    private volatile int _pointer;
    private int _size;
    private int _isWriting;
    
    public Cas2BasedCircularArray(int size)
    {
        _size = size;
        _items = new T[_size];
        _pointer = 0;
        _isWriting = 0;
    }
    
    public void Append(T item)
    {
        while (true)
        {
            if (_isWriting == 0) // i.e. nothing is writing
            {
                // var f = Interlocked.CompareExchange(ref _isWriting, 1, 0);
                if (0 == Interlocked.CompareExchange(ref _isWriting, 1, 0))
                {
                    // now we have exclusive write access
                    _items[_pointer] = item;
                    Console.WriteLine($"Position {_pointer} is now {item.ToString()}");
                    _pointer = (_pointer + 1) % _size; // TODO use bitwise &
                    _isWriting = 0;
                    return;
                }
            }
            else
            {
                
            }
        }
        
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