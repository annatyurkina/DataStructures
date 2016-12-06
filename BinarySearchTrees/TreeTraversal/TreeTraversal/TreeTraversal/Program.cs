using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeTraversal
{
    class Program
    {
        private static TreeNode[] _tree;
        private static int[] _inOrder;
        private static int _inOrderSize;
        private static int[] _preOrder;
        private static int _preOrderSize;
        private static int[] _postOrder;
        private static int _postOrderSize;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            _tree = new TreeNode[n];
            _inOrder = new int[n];
            _preOrder = new int[n];
            _postOrder = new int[n];

            for (int i = 0; i <  n; i++)
            {
                int[] temp = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                _tree[i] = new TreeNode { Key = temp[0], LeftIndex = temp[1], RightIndex = temp[2] };
            }
            InOrder(0);
            PreOrder(0);
            PostOrder(0);
            Console.WriteLine(string.Join(" ", _inOrder));
            Console.WriteLine(string.Join(" ", _preOrder));
            Console.WriteLine(string.Join(" ", _postOrder));
            Console.ReadLine();
        }

        private static void InOrder(int index)
        {
            if (index == -1)
                return;
            InOrder(_tree[index].LeftIndex);
            _inOrder[_inOrderSize] = _tree[index].Key;
            _inOrderSize++;
            InOrder(_tree[index].RightIndex);
        }

        private static void PreOrder(int index)
        {
            if (index == -1)
                return;
            _preOrder[_preOrderSize] = _tree[index].Key;
            _preOrderSize++;
            PreOrder(_tree[index].LeftIndex);
            PreOrder(_tree[index].RightIndex);
        }

        private static void PostOrder(int index)
        {
            if (index == -1)
                return;
            PostOrder(_tree[index].LeftIndex);
            PostOrder(_tree[index].RightIndex);
            _postOrder[_postOrderSize] = _tree[index].Key;
            _postOrderSize++;
        }
    }

    public class TreeNode
    {
        public int Key { get; set; }
        public int LeftIndex { get; set; }
        public int RightIndex { get; set; }
    }
}
