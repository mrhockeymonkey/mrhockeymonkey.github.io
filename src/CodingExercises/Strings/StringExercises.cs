namespace Strings;

public class StringExercises
{
    public bool HasAtMostOneOddCharCounts(string str)
    {
        var vector = 0;
        foreach (var c in str.ToCharArray())
        {
            // toggle this char in vector
            byte charByte = (byte)c;
            var mask = 1 << charByte; // this will wrap around i.e. a = 97 but 97 % 32 = 1
            vector ^= mask; // XOR so that we keep flipping
        }
        
        // now that we have flipped bits on and off as many times as chars occur we just need to check there is a single 1
        // 00010000 - 1 = 00001111
        // 00010000 & 00001111 = 00000000 = 0  (so if this > 0 then there must have been another 1 somewhere)
        
        // 00101000 - 1 = 00100111
        // 00101000 & 00100111 = 00100000 = 32  
        var bin = Convert.ToString(vector, 2);
        return (vector & (vector - 1)) == 0;
    }
    
    public bool HasUniqueChars(string str)
    {
        // check precondition ascii chars 
        var vector = 0;

        foreach (var c in str.ToCharArray())
        {
            int startOfAlphabet = 'a'; // in ascii = 97 but marks the start of a-z (assuming lowercase ascii)
            var val = c - startOfAlphabet; // now we have a=0, b=1, ...

            if ((vector & (1 << val)) > 0)
            {
                // AND finds a previous value so not unique
                return false;
            }
            
            vector |= 1 << val;
        }

        var v = Convert.ToString((byte)vector, 2);
        // "aceg" = 1010101
        
        return true;
    }
}

