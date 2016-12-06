using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brackets
{
    class Program
    {
        static void Main(string[] args)
        {
            //while (true)
            //{
                char[] arr = Console.ReadLine().ToCharArray();
                //char[] arr = GetRandom();
                int res = FindErrors(arr);
                Console.WriteLine(res == -1 ? "Success" : res.ToString());
            //}
        }

        static int FindErrors(char[] arr)
        {
            Stack<KeyValuePair<char, int>> st = new Stack<KeyValuePair<char, int>>();
            string brackets = "{}[]()";
            Dictionary<char, char> pairs = new Dictionary<char, char> { { '}', '{' }, { ']', '[' }, { ')', '(' } };

            for(int i = 0; i < arr.Length; i++)
            {
                if(brackets.Contains(arr[i]))
                {
                    if(pairs.ContainsKey(arr[i]))
                    {
                        if (st.Count > 0 && pairs[arr[i]] == st.Peek().Key)
                        {
                            st.Pop();
                        }
                        else
                            return i + 1;
                    }
                    else
                    {
                        st.Push(new KeyValuePair<char, int>(arr[i], i));
                    }
                }
            }

            return st.Count == 0 ? -1 : st.Peek().Value + 1;
        }

        static char[] GetRandom()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789{}[]()";
            var random = new Random();
            var length = random.Next(1, 100);
            var stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return stringChars;
        }
    }
}
