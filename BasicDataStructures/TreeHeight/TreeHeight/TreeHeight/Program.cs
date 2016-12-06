using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeHeight
{
    class Program
    {
        static int[] _heights;
        static TreeNode[] _nodes;

        static void Main(string[] args)
        {
            //Random rnd = new Random();
            //while (true)
            //{
                int n = int.Parse(Console.ReadLine());
                //Console.WriteLine(n);
                //int[] arr = GetMaxTreeArray(n);
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                _nodes = new TreeNode[arr.Length];
                //Console.WriteLine(string.Join(" ", arr));
                Tree tree = GetTree(arr);
                Console.WriteLine(GetHeight(tree));
                //Console.ReadLine();
            //}
        }

        static Tree GetTree(int[] arr)
        {
            Tree res = null;
            for(int i = 0; i < arr.Length; i++)
            {
                if(_nodes[i] == null)
                {
                    _nodes[i] = new TreeNode { Key = i, Kids = new List<TreeNode>() };
                }
                else
                {
                    _nodes[i].Key = i;
                }
                if(arr[i] == -1)
                {
                    res = new Tree { Root = _nodes[i] };
                }
                else
                {
                    if (_nodes[arr[i]] == null)
                    {
                        _nodes[arr[i]] = new TreeNode { Kids = new List<TreeNode>() };
                    }
                    _nodes[arr[i]].Kids.Add(_nodes[i]);
                }
            }
            return res;
        }

        static int GetHeight(Tree tree)
        {
            int height = 0;
            if (tree.Root == null)
                return height;

            Queue<KeyValuePair<int, int>> queue = new Queue<KeyValuePair<int, int>>();
            queue.Enqueue(new KeyValuePair<int, int>(tree.Root.Key, 1));

            while(queue.Count > 0)
            {
                KeyValuePair<int, int> curNodeIndex = queue.Dequeue();

                foreach(TreeNode kid in _nodes[curNodeIndex.Key].Kids)
                {
                    queue.Enqueue(new KeyValuePair<int, int>(kid.Key, curNodeIndex.Value + 1));
                }

                height = Math.Max(curNodeIndex.Value, height);
            }

            return height;

        }

        static int[] GetRandomTreeArray()
        {
            Random rnd = new Random();
            int n = rnd.Next(1, 10);
            int availSpaces = n;
            int[] arr = new int[n];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = -2;
            }
            arr[rnd.Next(0, n - 1)] = -1;
            availSpaces--;
            for(int i = 0; availSpaces > 0 && i < n; i++)
            {
                int iNum = rnd.Next(1, availSpaces);
                availSpaces -= iNum;
                int index = rnd.Next(0, n - 1);
                while (iNum > 0)
                {
                    if (arr[index] == -2 && index != i)
                    {
                        arr[index] = i;
                        iNum--;
                    }
                    else
                        index = (index + 1) % arr.Length;
                }
            }
            return arr;
        }

        static int[] GetMaxTreeArray(int n)
        {
            int[] res = new int[n];
            for(int i = 0; i < n; i++)
            {
                res[i] = i - 1;
            }
            return res;
        }
    }

    public class TreeNode
    {
        public int Key { get; set; }
        public List<TreeNode> Kids { get; set; }
    }

    public class Tree
    {
        public TreeNode Root { get; set; }
    }
}
