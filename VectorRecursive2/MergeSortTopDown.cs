using System;
using System.Collections.Generic;
using System.Linq;


namespace Vector
{
    public class MergeSortTopDown : ISorter
    {
        //Merge-sort contents of array sequence. The conventional MergeSort.
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;
            int n = sequence.Length;

            if (n < 2) return; // if there are only 0 or 1 elements, array is trivially sorted
            int mid = n / 2;

            // Partition
            K[] leftSequence = new K[n / 2];
            K[] rightSequence = new K[n / 2];
            /***TO DO: Partition using Array.Copy method??***/
            //Array.Copy(sequence, 0, leftSequence, 0, mid);
            //Array.Copy(sequence, mid, rightSequence, 0, n);

            /**USING LINQ library. Method is as close as I could find to the 'Arrays.CopyOfRange' Java equivalent
            //from pg. 538 of textbook.**/
            leftSequence = sequence.Take(n / 2).ToArray(); // copy first half of sequence into left sequence using LINQ
            rightSequence = sequence.Skip(n / 2).ToArray(); // copy second half of sequence into right sequence using LINQ

            // Recursion
            Sort(leftSequence, comparer); // sort copy of first half
            Sort(rightSequence, comparer); // sort copy of second half

            // Combination
            Merge(leftSequence, rightSequence, sequence, comparer); // merge sorted halves back into original

        }
        //Merge contents of arrays leftSequence and rightSequence into properly sized array sequence
        private void Merge<K>(K[] leftSequence, K[] rightSequence, K[] sequence, IComparer<K> comparer)
        {
            int i = 0;
            int j = 0;

            while (i + j < sequence.Length)
            {
                if (j == rightSequence.Length || (i < leftSequence.Length && comparer.Compare(leftSequence[i], rightSequence[j]) < 0))
                    sequence[i + j] = leftSequence[i++]; // copy ith element of S1 and increment i
                else
                    sequence[i + j] = rightSequence[j++]; // copy jth element of S2 and increment j
            }
        }


    }
}
