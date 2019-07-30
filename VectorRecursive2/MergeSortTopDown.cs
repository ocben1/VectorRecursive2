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
            int n = sequence.Length; //number of elements

            if (n < 2) return; // if there are only 0 or 1 elements, array is trivially sorted
            int mid = n / 2; //midpoint

            // Partition
            K[] leftSequence = new K[n/2];
            K[] rightSequence = new K[n/2];
            /***TO DO: Partition using Array.Copy method?? Alternatively, could use two separate for loops to
             * copy elements [0] to [mid] to left sequence, then [mid+1] to [n] to right sequence***/
            //Array.Copy(sequence, 0, leftSequence, 0, mid);
            //Array.Copy(sequence, mid, rightSequence, 0, n);

            /**USING LINQ library. Method does the same job as 'Arrays.CopyOfRange' Java method
            from pg. 538 of textbook.
            Reference: https://stackoverflow.com/questions/21269727/how-to-convert-java-arrays-copyofrange-function-to-c**/
            leftSequence = sequence.Take(mid).ToArray(); // TAKEs first half of sequence copying into left sequence
            rightSequence = sequence.Skip(mid).ToArray(); // SKIPS the first half of sequence, copying second half of sequence into right sequence

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
