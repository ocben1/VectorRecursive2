using System;
using System.Collections.Generic;

namespace Vector
{
    public class MergeSortBottomUp : ISorter
    {
        /*Merge-sort contents of array sequence. BottomUp MergeSort adapted from pg 543 Data Structures and Algorithms
         in Java 6th Edition
         NOTE: Array.Copy method has been used here instead of Take & Skip*/
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;
            int n = sequence.Length; //number of elements

            K[] src = sequence;     //alias for the original 'sequence'
            K[] dest = new K[n];    //make new temporary array
            K[] temp;               //reference used only for swapping

            for(int i = 1; i < n; i*=2)     //each iteration sorts all runs of length i
            {
                for (int j = 0; j < n; j += 2 * i) //each pass merges two runs of length j
                    Merge(src, dest, comparer, j, i);
                /***SWAP: Reverse roles of array***/
                temp = src;
                src = dest;
                dest = temp;
            }
            if(sequence != src) //additional copy to get result to original
            {
                Array.Copy(src, 0, sequence, 0, n);
            }
        }
        //Merges in[start..start+inc-1] and in[start+inc..start+2*inc-1] into out
        private void Merge<K>(K[] in1, K[] out1, IComparer<K> comparer, int start, int inc)
        {
            int end1 = Math.Min(start + inc, in1.Length);       //boundary for run 1
            int end2 = Math.Min(start + 2 * inc, in1.Length);   //boundary for run 2
            int x = start;                                      //index into run 1
            int y = start + inc;                                //index into run 2
            int z = start;                                      //index into output
            while (x < end1 && y < end2)
            {
                if (comparer.Compare(in1[x], in1[y]) < 0)
                {
                    out1[z++] = in1[x++];                       //take next from run 1
                }
                else
                {
                    out1[z++] = in1[y++];                       //take next from run 2
                }
                if (x < end1)
                {
                    Array.Copy(in1, x, out1, z, end1 - x);      //copy rest of run 1
                }
                else if (y < end2)
                {
                    Array.Copy(in1, y, out1, z, end2 - y);      //copy rest of run 2
                }
            }
        }


    }
}
