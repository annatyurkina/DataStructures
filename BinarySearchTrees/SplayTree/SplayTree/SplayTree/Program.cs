using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplayTree
{
    class Program
    {
        static Tree _tree = new Tree();
        static long _lastSum = 0;
        static List<string> _lines = new List<string>();
        static Random _rnd = new Random();

        static void Main(string[] args)
        {
            //while (true)
            //{
                int num = int.Parse(Console.ReadLine());
                //int num = _rnd.Next(90000, 100000);
                for (int i = 0; i < num; i++)
                {
                    //string[] op = GenerateRandomOp().Split(' ');
                    string[] op = Console.ReadLine().Split(' ');
                    Do(op);
                }
                for (int i = 0; i < _lines.Count; i++)
                    Console.WriteLine(_lines[i]);
                //Console.WriteLine("finita");
               // _tree = new Tree();
               // _lastSum = 0;
               // _lines = new List<string>();
            //}
            Console.ReadLine();
        }

        private static string GenerateRandomOp()
        {
            var op = GetOp(_rnd.Next(1, 5));
            var l = _rnd.Next(900000000, 1000000000);
            var r = _rnd.Next(l, 1000000000);
            op = op + " " + l.ToString();
            if (op.StartsWith("s"))
                op = op + " " + r.ToString();
            Console.WriteLine(op);
            return op; 
        }

        private static string GetOp(int x)
        {
            switch(x)
            {
                case 1:
                    return "+";
                case 2:
                    return "-";
                case 3:
                    return "?";
                default:
                    return "s";
            }
        }

        public static void Do(string[] op)
        {
            const long M = 1000000001;
            switch(op[0])
            {
                case "+":
                    _tree.STInsert((int)((long.Parse(op[1]) + _lastSum) % M));
                    break;
                case "-":
                    _tree.STDelete((int)((long.Parse(op[1]) + _lastSum) % M));
                    break;
                case "?":
                    var found = _tree.STFind((int)((long.Parse(op[1]) + _lastSum) % M));
                    _lines.Add(found != null ? "Found" : "Not found");
                    break;
                case "s":
                    var res = _tree.STSum((int)((long.Parse(op[1]) + _lastSum) % M), (int)((long.Parse(op[2]) + _lastSum) % M));
                    _lines.Add(res.ToString());
                    _lastSum = res % M;
                    break;
            }
        }
    }

    public class Tree
    {
        private const int M = 1000000001;
        public TreeNode Root { get; set; }
        

        public void Splay(TreeNode nodeToSplay)
        {
            if (nodeToSplay == null)
                return;
            if (nodeToSplay.Parent == null)
            {
                Root = nodeToSplay;
                return;
            }
            if (nodeToSplay.Parent.Parent == null)
                Zig(nodeToSplay);
            else
            {
                var nodePosition = (nodeToSplay.Parent.Key >= nodeToSplay.Key) ^ (nodeToSplay.Parent.Parent.Key < nodeToSplay.Key);
                if (!nodePosition)
                    ZigZag(nodeToSplay);
                else
                    ZigZig(nodeToSplay);
            }
            Splay(nodeToSplay);
        }

        public void Zig(TreeNode nodeToSplay)
        {
            var isRightChild = nodeToSplay.IsRightChild;
            var parent = nodeToSplay.Parent;
            nodeToSplay.Parent = parent.Parent;
            parent.Parent = nodeToSplay;
            if(isRightChild)
            {
                var leftChild = nodeToSplay.LeftChild;
                nodeToSplay.LeftChild = parent;
                if(leftChild != null)
                    leftChild.Parent = parent;
                parent.RightChild = leftChild; 
            }
            else
            {
                var rightChild = nodeToSplay.RightChild;
                nodeToSplay.RightChild = parent;
                if (rightChild != null)
                    rightChild.Parent = parent;
                parent.LeftChild = rightChild;
            }
            parent.UpdateSum();
            nodeToSplay.UpdateSum();
        }

        public void ZigZig(TreeNode nodeToSplay)
        {
            var isLeftChild = nodeToSplay.IsLeftChild;
            var parent = nodeToSplay.Parent;
            var grandParent = parent.Parent;
            nodeToSplay.Parent = grandParent.Parent;
            parent.Parent = nodeToSplay;
            grandParent.Parent = parent;
            if (isLeftChild)
            {
                var rightChild = nodeToSplay.RightChild;
                var parentRightChild = parent.RightChild;
                nodeToSplay.RightChild = parent;
                parent.RightChild = grandParent;
                if(rightChild != null)
                    rightChild.Parent = parent;
                parent.LeftChild = rightChild;
                if(parentRightChild != null)
                    parentRightChild.Parent = grandParent;
                grandParent.LeftChild = parentRightChild;
            }
            else
            {
                var leftChild = nodeToSplay.LeftChild;
                var parentLeftChild = parent.LeftChild;
                nodeToSplay.LeftChild = parent;
                parent.LeftChild = grandParent;
                if(leftChild != null)
                    leftChild.Parent = parent;
                parent.RightChild = leftChild;
                if(parentLeftChild != null)
                    parentLeftChild.Parent = grandParent;
                grandParent.RightChild = parentLeftChild;
            }
            grandParent.UpdateSum();
            parent.UpdateSum();
            nodeToSplay.UpdateSum();
        }

        public void ZigZag(TreeNode nodeToSplay)
        {
            var isRightChild = nodeToSplay.IsRightChild;
            var parent = nodeToSplay.Parent;
            var grandParent = parent.Parent;
            nodeToSplay.Parent = grandParent.Parent;
            parent.Parent = nodeToSplay;
            grandParent.Parent = nodeToSplay;
            if (isRightChild)
            {
                var leftChild = nodeToSplay.LeftChild;
                var rightChild = nodeToSplay.RightChild;
                nodeToSplay.LeftChild = parent;
                nodeToSplay.RightChild = grandParent;
                if(leftChild != null)
                    leftChild.Parent = parent;
                parent.RightChild = leftChild;
                if(rightChild != null)
                    rightChild.Parent = grandParent;
                grandParent.LeftChild = rightChild;
            }
            else
            {
                var leftChild = nodeToSplay.LeftChild;
                var rightChild = nodeToSplay.RightChild;
                nodeToSplay.LeftChild = grandParent;
                nodeToSplay.RightChild = parent;
                if (leftChild != null)
                    leftChild.Parent = grandParent;
                grandParent.RightChild = leftChild;
                if (rightChild != null)
                    rightChild.Parent = parent;
                parent.LeftChild = rightChild;
            }
            grandParent.UpdateSum();
            parent.UpdateSum();
            nodeToSplay.UpdateSum();
        }

        public TreeNode Find(int key)
        {
            var t = Root;
            if (t == null)
                return null;
            while (true)
            {
                if (key == t.Key)
                {
                    return t;
                }
                else if (key < t.Key)
                {
                    if (t.LeftChild == null)
                        return t;
                    t = t.LeftChild;
                }
                else 
                {
                    if (t.RightChild == null)
                        return t;
                    t = t.RightChild;
                }
            }
        }

        public TreeNode STFind(int key)
        {
            var n = Find(key);
            //if(n != null && n.Key == key)
            if (n != null)
            {
                Splay(n);
                return n.Key == key ? n : null;
            }
            return null;
        }

        public void Insert(int key)
        {
            var n = Find(key);
            if (n == null)
            {
                Root = new TreeNode { Key = key, Sum = key };
                return;
            }
            if (n.Key == key)
                return;
            var newNode = new TreeNode { Key = key, Parent = n, Sum = key };
            if (n.Key > key)
                n.LeftChild = newNode;
            else
                n.RightChild = newNode;
        }

        public void STInsert(int key)
        {
            Insert(key);
            STFind(key);
        }

        public TreeNode Next(TreeNode n)
        {
            if(n.RightChild != null)
            {
                var res = n.RightChild;
                while (res.LeftChild != null)
                {
                    res = res.LeftChild;
                }
                return res;
            }
            while(n != null && !n.IsLeftChild)
            {
                n = n.Parent;
            }
            if (n != null)
                return n.Parent;
            return null;
        }

        public void STDelete(int key)
        {
            if (Root == null)
                return; 
            var n = Find(key);
            if (n.Key != key)
                return;
            var next = Next(n);
            Splay(next);
            Splay(n);
            if (next != null)
            {
                next.Parent = null;
                next.LeftChild = n.LeftChild;
                if(n.LeftChild != null)
                    n.LeftChild.Parent = next;
                Root = next;
                next.UpdateSum();
            }
            else
            {
                Root = n.LeftChild;
                if(n.LeftChild != null)
                    n.LeftChild.Parent = null;
            }
        }

        public Tree STSplit(int key)
        {
            if (Root != null)
            {
                var n = Find(key);
                Splay(n);
                if (n.Key < key)
                {
                    if (n.RightChild != null)
                    {
                        var rightChild = n.RightChild;
                        rightChild.Parent = null;
                        n.RightChild = null;
                        n.UpdateSum();
                        return new Tree { Root = rightChild };
                    }
                }
                else
                {
                    var leftChild = n.LeftChild;
                    if(leftChild != null)
                        leftChild.Parent = null;
                    n.LeftChild = null;
                    n.UpdateSum();
                    var root = Root;
                    Root = leftChild;
                    return new Tree { Root = root };
                }
            }
            return new Tree { Root = null};
        }

        public void STMerge(Tree tree)
        {
            if (Root == null)
                Root = tree.Root;
            else if (tree.Root != null)
            {
                var n = Find(int.MaxValue);
                Splay(n);
                tree.Root.Parent = n;
                n.RightChild = tree.Root;
                n.UpdateSum();
            }
        }

        public long STSum(int l, int r)
        {
            var rightTree = STSplit(l);
            var lastTree = rightTree.STSplit(r + 1);
            var res = rightTree.Root != null ? rightTree.Root.Sum : 0;
            STMerge(rightTree);
            STMerge(lastTree);
            return res;
        }
    }

    public class TreeNode
    {
        public int Key { get; set; }
        public TreeNode LeftChild { get; set; }
        public TreeNode RightChild { get; set; }
        public TreeNode Parent { get; set; }
        public long Sum { get; set; }
        public bool IsLeftChild { get { return Parent != null && Parent.Key > Key; } }
        public bool IsRightChild { get { return Parent != null && Parent.Key < Key; } }
        public void UpdateSum()
        {
            var leftSum = LeftChild == null ? 0 : LeftChild.Sum;
            var rightSum = RightChild == null ? 0 : RightChild.Sum;
            Sum = Key + leftSum + rightSum;
            if (Sum < 0)
                throw new Exception("noo");
        }
    }
}
