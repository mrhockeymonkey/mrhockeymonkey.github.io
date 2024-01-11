using System;
using System.Collections.Generic;
using System.Linq;

namespace BigO;
class Program
{
    // 2^y = N     2^3 = 8
    // y = log N   3 = log(8)  i.e. how many times can you half 8
    
    // so complexity log(n) is the time it takes to half n as many times as possible
    
    static void Main(string[] args)
    {
        int[] array = { 52, 96, 67, 71, 42, 38, 39, 40, 14 };
        Quicksort(array, 0, array.Length - 1);
        Console.WriteLine(string.Join(",", array));
    }
    
    // O(n)
    static void Reverse(int[] arr) {
        for (var i = 0; i < arr.Length / 2; i++) {
            var indexOther = arr.Length -i -1;
            (arr[i], arr[indexOther]) = (arr[indexOther], arr[i]);
        }
    }

    // O(√n)
    //https://en.wikipedia.org/wiki/Trial_division
    static bool IsPrime(int n)
    {
        if (n <= 1){ return false; }
        if (n == 2){ return true; }
        if (n % 2 == 0){ return false; }

        // sqrt(n) is the highest you need to test because if anything bigger divided
        // then it would have to have already been something smaller. 
        var sqrtN = Math.Sqrt(n);
        for (int i = 3; i <= sqrtN; i += 2)
        {
            if (n % i == 0){
                return false;
            }
        }
        return true;
    }
    
    // O(n^2 * n!)
    // We will call this function n times to get to the end result which will be a further n! times to print, so O(n * n!)
    // If you include the printing of each char (which I find odd...) then that explains the n^2 * n!
    // https://www.geeksforgeeks.org/time-complexity-permutations-string/
    static void Permutations(String str, String prefix){
        if (str.Length == 0){
            Console.WriteLine(prefix);
        }
        else {
            for (var i = 0; i < str.Length; i++){
                // here we branch n times, then n-1, n-2, ... i.e. n!
                var rem = str.Substring(0, i) + str.Substring(i + 1);
                Permutations(rem, prefix + str[i]);
            }
        }
    }
    
    // O(2^n) without memo
    // we branch into 2 for every integer up to n
    
    // O(n) with memo
    // lookup previous values is constant time O(n)
    static int nthFib(int n, Dictionary<int, int> memo){
        if (n <= 0) { return 0; }
        if (n == 1) { return 1; }

        var value = memo.GetValueOrDefault(n);
        if (value > 0) return value;

        memo[n] = nthFib(n - 1, memo) + nthFib(n - 2, memo);
        return memo[n];
    }
    
    // O(log n)
    // how many times can we divide by 2 until we reach base case 1, dividing by 2 is log
    // put another way every time n doubles the method is called only once more...
    // if x is the no of calls then 2^x = n which is equivalent to x = log n
    static int powersOf2(int n){ // get powers of 2 between 1 and n
        if (n < 1) return 0;
        if (n == 1) return 1;
        
        var prev = powersOf2(n / 2);
        var curr = prev * 2;
        Console.WriteLine(curr);
        return curr;
    }
    
    // worst case: O(n^2) if every iteration the pivot is smallest or largest
    // best case: O(n log n) when the array is perfectly split in half every iteration
    static void Quicksort(int[] array, int leftIndex, int rightIndex)
    {
        Console.WriteLine($"Sorting {string.Join(',', array[leftIndex..(rightIndex + 1)])}");
        var li = leftIndex;
        var ri = rightIndex;
        
        // the pivot should end up in it final position after the below sorting
        var pivot = array[leftIndex];

        while (li <= ri)
        {
            // find index of larger from left
            while (array[li] < pivot) li++;
            // find index of smaller from right
            while (array[ri] > pivot) ri--;
            
            if (li <= ri) // then larger is on left and smaller on right, so swap
            {
                (array[li], array[ri]) = (array[ri], array[li]);
                li++;
                ri--;
            }
        }
        // so now the cursors have crossed over: ri pivot li
        
        if (leftIndex < ri)
            Quicksort(array, leftIndex, ri);
        if (li < rightIndex)
            Quicksort(array, li, rightIndex);
    }
    
    // O(n log n) we divide the problem in logarithmic time then the work to recombine is O(n)
    // but it has higher space complexity
    public int[] MergeSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = left + (right - left) / 2;

            MergeSort(array, left, middle);
            MergeSort(array, middle + 1, right);

            MergeArray(array, left, middle, right);
        }

        return array;
    }
    
    public void MergeArray(int[] array, int left, int middle, int right)
    {
        var leftArrayLength = middle - left + 1;
        var rightArrayLength = right - middle;
        var leftTempArray = new int[leftArrayLength];
        var rightTempArray = new int[rightArrayLength];
        int i, j;

        for (i = 0; i < leftArrayLength; ++i)
            leftTempArray[i] = array[left + i];
        for (j = 0; j < rightArrayLength; ++j)
            rightTempArray[j] = array[middle + 1 + j];

        i = 0;
        j = 0;
        int k = left;

        while (i < leftArrayLength && j < rightArrayLength)
        {
            if (leftTempArray[i] <= rightTempArray[j])
            {
                array[k++] = leftTempArray[i++];
            }
            else
            {
                array[k++] = rightTempArray[j++];
            }
        }

        while (i < leftArrayLength)
        {
            array[k++] = leftTempArray[i++];
        }

        while (j < rightArrayLength)
        {
            array[k++] = rightTempArray[j++];
        }
    }
}
