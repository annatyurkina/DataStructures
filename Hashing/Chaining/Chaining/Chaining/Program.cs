using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaining
{
    class Program
    {
        static Dictionary<int, List<string>> _s = new Dictionary<int, List<string>>();
        static Random _rnd = new Random();

        static void Main(string[] args)
        {
            //while (true)
            //{
                int m = int.Parse(Console.ReadLine());
                int n = int.Parse(Console.ReadLine());

                //int n = _rnd.Next(1, 10000);
                //Console.WriteLine(n);
                //int m = _rnd.Next((int)Math.Ceiling(((double)n) / 5.0), n);
                //Console.WriteLine(m);

                List<string> results = new List<string>();

                for (int i = 0; i < n; i++)
                {
                    string[] s = Console.ReadLine().Split(' ');
                    //string[] s = GetRandomCommand(m);
                    //Console.WriteLine(string.Join(" ", s));
                    string res = Process(s, m);
                    if (res != null)
                        results.Add(res);
                }
                //Console.ReadLine();

                foreach (string res in results)
                {
                    Console.WriteLine(res);
                }
                Console.ReadLine();
            //}
        }

        static int Hash(string s, int m)
        {
            byte[] arr = Encoding.ASCII.GetBytes(s);
            int x = 263;
            UInt64 p = 1000000007;
            UInt64 curr = 0;

            for(int i = arr.Length -1; i >= 0; i--)
            {
                curr = (curr * (UInt64)x + (UInt64)arr[i]) % p;
            }

            //if (curr < 0)
            //    throw new ApplicationException($"curr {curr} s {s}");

            UInt64 temp = curr % p;

            //if(temp < 0)
            //    throw new ApplicationException($"curr {curr} temp {temp} s {s}");

            UInt64 temp1 = temp % ((UInt64)m);

            //if (temp1 < 0)
            //    throw new ApplicationException($"curr {curr} temp {temp} temp1 {temp1} s {s} m {m}");

            int res = ((int)(temp1));

            //if (temp1 < 0)
            //    throw new ApplicationException($"curr {curr} temp {temp} temp1 {temp1} res {res} s {s} m {m}");

            return res;
        }

        static string Process(string[] s, int m)
        {
            int hash;
            string res = null;
            switch(s[0])
            {
                case "add":
                    hash = Hash(s[1], m);
                    if (_s.ContainsKey(hash))
                    {
                        bool exists = false;
                        foreach(string ss in _s[hash])
                        {
                            if (AreEqual(ss, s[1]))
                                exists = true;
                        }
                        if (!exists)
                            _s[hash].Add(s[1]);
                    }
                    else
                        _s.Add(hash, new List<string>() { s[1] });
                    break;
                case "del":
                    hash = Hash(s[1], m);
                    if (_s.ContainsKey(hash))
                    {
                        foreach (string ss in _s[hash])
                        {
                            if (AreEqual(ss, s[1]))
                            {
                                _s[hash].Remove(ss);
                                break;
                            }

                        }
                    }
                    break;
                case "check":
                    res = string.Empty;
                    int index = int.Parse(s[1]);
                    if(_s.ContainsKey(index))
                        res = string.Join(" ", _s[index].Reverse<string>());
                    break;
                case "find":
                    hash = Hash(s[1], m);
                    res = "no";
                    if (_s.ContainsKey(hash))
                    {
                        foreach (string ss in _s[hash])
                        {
                            if (AreEqual(ss, s[1]))
                            {
                                res = "yes";
                                break;
                            }
                        }
                    }
                    break;
            }
            return res;
        }

        static bool AreEqual(string s1, string s2)
        {
            byte[] arr1 = Encoding.ASCII.GetBytes(s1);
            byte[] arr2 = Encoding.ASCII.GetBytes(s2);

            if (arr1.Length != arr2.Length)
                return false;

            for(int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                    return false;
            }

            return true;
        }

        static string[] GetRandomCommand(int m)
        {
            string[] res = new string[2];
            int rnd = _rnd.Next(1, 4);
            switch(rnd)
            {
                case 1:
                    res[0] = "add";
                    res[1] = GetRandString();
                    break;
                case 2:
                    res[0] = "del";
                    res[1] = GetRandString();
                    break;
                case 3:
                    res[0] = "check";
                    res[1] = _rnd.Next(0, m - 1).ToString();
                    break;
                case 4:
                    res[0] = "find";
                    res[1] = GetRandString();
                    break;
            }
            return res;
        }

        static string GetRandString()
        {
            char[] all = ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ").ToCharArray();
            int l = _rnd.Next(1, 15);
            char[] res = new char[l];
            for(int i = 0; i < l; i++)
            {
                res[i] = all[_rnd.Next(1, 52)];
            }
            return new string(res);
        }
    }
}
