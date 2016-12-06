using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableMerge
{
    class Program
    {
        static List<KeyValuePair<int, int>> _merges = new List<KeyValuePair<int, int>>();
        static int _maxNumberOfRows; 

        static void Main(string[] args)
        {
            int[] nums = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            _maxNumberOfRows = 0;
            TableSet[] tableSets = new TableSet[arr.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                tableSets[i] = new TableSet(i, arr[i]);
                if (_maxNumberOfRows < arr[i])
                    _maxNumberOfRows = arr[i];
            }
            
            for (int i = 0; i < nums[1]; i++)
            {
                int[] tables = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                _merges.Add(new KeyValuePair<int, int>(tables[0] - 1, tables[1] - 1));
            }
            for (int i = 0; i < nums[1]; i++)
            {
                Merge(tableSets[_merges[i].Key], tableSets[_merges[i].Value]);
                Console.WriteLine(_maxNumberOfRows);
            }
            Console.ReadLine();

        }

        static void Merge(TableSet t1, TableSet t2)
        {
            while (t1.SymbolicLink != null)
            {
                t1 = t1.SymbolicLink;
            }
            while (t2.SymbolicLink != null)
            {
                t2 = t2.SymbolicLink;
            }
            if (t1.Id != t2.Id)
            {
                if (t1.Rank >= t2.Rank)
                {
                    t1.NumberOfRows += t2.NumberOfRows;
                    t2.NumberOfRows = 0;
                    t2.SymbolicLink = t1;
                    if (t1.Rank == t2.Rank)
                        t1.Rank++;
                }
                else
                {
                    t2.NumberOfRows += t1.NumberOfRows;
                    t1.NumberOfRows = 0;
                    t1.SymbolicLink = t2;
                }
            }
            if (_maxNumberOfRows < Math.Max(t1.NumberOfRows, t2.NumberOfRows))
                _maxNumberOfRows = Math.Max(t1.NumberOfRows, t2.NumberOfRows);
        }
    }

    public class TableSet
    {
        public int Id { get; set; }
        public int NumberOfRows { get; set; }
        public TableSet SymbolicLink { get; set; }
        public int Rank { get; set; }

        public TableSet(int id, int numberOfRows)
        {
            Id = id;
            NumberOfRows = numberOfRows;
        }
    }
}
