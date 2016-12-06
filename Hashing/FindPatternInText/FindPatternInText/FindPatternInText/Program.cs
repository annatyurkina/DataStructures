using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindPatternInText
{
    class Program
    {
        static int[] _hashes;

        static void Main(string[] args)
        {
            char[] pattern = Console.ReadLine().ToCharArray();
            char[] text = Console.ReadLine().ToCharArray();
            _hashes = new int[text.Length - pattern.Length + 1];

            int[] res = FindPatternInText();

            Console.WriteLine(string.Join(" ", res));
        }

        static int Hash(char[] s, int start, int end)
        {
            byte[] arr = Encoding.ASCII.GetBytes(s);
            int x = 263;
            UInt64 p = 500009;
            UInt64 curr = 0;
            int m = 1000;

            for (int i = end; i >= start; i--)
            {
                curr = (curr * (UInt64)x + (UInt64)arr[i]) % p;
            }

            UInt64 temp = curr % p;

            UInt64 temp1 = temp % ((UInt64)m);

            int res = ((int)(temp1));

            return res;
        }

        static void AllHashes(char[] text, int pl)
        {
            byte[] arr = Encoding.ASCII.GetBytes(s);
            int x = 263;
            int p = 500009;
            int m = 1000;

            _hashes[text.Length - pl] = Hash(text, text.Length - pl, text.Length - 1);
            for (int i = text.Length - pl - 1 ; i >= 0; i--)
            {
                _hashes[i] = (_hashes[i + 1] * x % p) + (int)text[i] - (Math.Pow((double)x, (double)(i + pl)) % p) * 
            }

            UInt64 temp = curr % p;

            UInt64 temp1 = temp % ((UInt64)m);

            int res = ((int)(temp1));

            return res;
        }
    }
}
