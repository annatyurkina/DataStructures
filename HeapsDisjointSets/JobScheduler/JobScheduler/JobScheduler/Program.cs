using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler
{
    class Program
    {
        static PriorityQueue _pq;
        static Random _rnd = new Random();

        static void Main(string[] args)
        {
            //while (true)
            //{
            //Int64[] nums = Array.ConvertAll(Console.ReadLine().Split(' '), Int64.Parse);
            //Int64[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), Int64.Parse);

            Int64[] nums = new Int64[] { 1, 100000 }; //{ (Int64)_rnd.Next(1, 10000), (Int64)_rnd.Next(1, 10000) };
            Int64[] arr = new Int64[nums[1]];
            for (Int64 i = 0; i < nums[1]; i++)
            {
                arr[i] = 1000000000; //(Int64)_rnd.Next(0, 1000000000);
            }

            _pq = new PriorityQueue(0, nums[0]);

                for (Int64 i = 0; i < nums[0]; i++)
                {
                    _pq.Insert(new KeyValuePair<Int64, Int64>(i, 0));
                }
                for (Int64 i = 0; i < nums[1]; i++)
                {
                    KeyValuePair<Int64, Int64> cur = _pq.ExtractMin();
                    Console.WriteLine(string.Format("{0} {1}", cur.Key, cur.Value));
                    _pq.Insert(new KeyValuePair<Int64, Int64>(cur.Key, cur.Value + arr[i]));
                }
            //}
            Console.ReadLine();
        }
    }

    public class PriorityQueue
    {
        KeyValuePair<Int64, Int64>[] Arr { get; set; }
        Int64 Size { get; set; }
        Int64 MaxSize { get; set; }

        public PriorityQueue(Int64 size, Int64 maxSize)
        {
            Size = size;
            MaxSize = maxSize;
            Arr = new KeyValuePair<Int64, Int64>[MaxSize];
        }

        public KeyValuePair<Int64, Int64> ExtractMin()
        {
            KeyValuePair<Int64, Int64> res = Arr[0];
            Arr[0] = Arr[Size - 1];
            Size--;
            SiftDown(0);
            return res;
        }

        public void Insert(KeyValuePair<Int64, Int64> value)
        {
            if (Size >= MaxSize)
                throw new Exception("ttt");
            Arr[Size] = value;
            Size++;
            SiftUp(Size - 1);
        }

        public void SiftUp(Int64 i)
        {
            if (i > 0)
            {
                Int64 parent = (Int64)Math.Floor(((double)(i - 1))/(double)2.0);
                Int64 child = i;
                while (parent >= 0 && Lesser(Arr[child], Arr[parent]))
                {
                    KeyValuePair<Int64, Int64> temp = Arr[child];
                    Arr[child] = Arr[parent];
                    Arr[parent] = temp;

                    Int64 tempIndex = parent;
                    parent = (Int64)Math.Floor(((double)(child - 1))/(double)2.0);
                    child = tempIndex;
                }
            }
        }


        public void SiftDown(Int64 i)
        {
            while (2 * i + 1 < Size && Lesser(Arr[2 * i + 1], Arr[i]) || 2 * i + 2 < Size && Lesser(Arr[2 * i + 2], Arr[i]))
            {
                Int64 childToSwap = 2 * i + 2 >= Size || Lesser(Arr[2 * i + 2], Arr[2 * i + 1]) ? 2 * i + 2 : 2 * i + 1;
                KeyValuePair<Int64, Int64> temp = Arr[i];
                Arr[i] = Arr[childToSwap];
                Arr[childToSwap] = temp;
                i = childToSwap;
            }
        }

        public bool Lesser(KeyValuePair<Int64, Int64> a, KeyValuePair<Int64, Int64> b)
        {
            return a.Value < b.Value || a.Value == b.Value && a.Key < b.Key;
        }
    }
}
