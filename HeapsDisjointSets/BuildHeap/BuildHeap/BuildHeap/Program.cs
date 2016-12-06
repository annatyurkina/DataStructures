using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildHeap
{
    class Program
    {
        static List<KeyValuePair<int, int>> _swaps = new List<KeyValuePair<int, int>>();
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            //while (true)
            //{
                int n = int.Parse(Console.ReadLine());
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                //int n = rnd.Next(1, 100000);
                //int[] arr = new int[n];
                //for (int i = 0; i < n; i++)
                //{
                //    arr[i] = rnd.Next(0, 1000000000);
                //}

                Console.WriteLine(BuildHeap(arr));
                foreach (var res in _swaps)
                {
                    Console.WriteLine(string.Format("{0} {1}", res.Key, res.Value));
                }
            //}
            //Console.ReadLine();
        }

        static int BuildHeap(int[] arr)
        {
            int res = 0;

            for(int i = (int)Math.Floor((double)arr.Length/2) - 1; i >= 0; i--)
            {
                res += SiftDown(arr, i);
            }

            return res;
        }

        static int SiftDown(int[] arr, int i)
        {
            int res = 0;

            while (2*i + 1 < arr.Length && arr[i] > arr[2*i + 1] || 2*i + 2 < arr.Length && arr[i] > arr[2*i + 2])
            {
                int childToSwap = 2 * i + 2 >= arr.Length || arr[2 * i + 1] <= arr[2 * i + 2] ? 2 * i + 1 : 2 * i + 2; 
                _swaps.Add(new KeyValuePair<int, int>(i, childToSwap));
                res++;
                int temp = arr[i];
                arr[i] = arr[childToSwap];
                arr[childToSwap] = temp;
                i = childToSwap;
            }

            return res;
        }
    }
}
