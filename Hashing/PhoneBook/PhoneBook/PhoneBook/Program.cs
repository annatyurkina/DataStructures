using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    class Program
    {
        private static Dictionary<int, string> _phoneBook = new Dictionary<int, string>();

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<string> results = new List<string>();
            for(int i = 0; i < n; i++)
            {
                string[] record = Console.ReadLine().Split(' ');
                string res = Process(record[0], int.Parse(record[1]), record.Length == 2 ? string.Empty : record[2]);
                if (!string.IsNullOrEmpty(res))
                    results.Add(res);

            }
            foreach (var res in results)
            {
                Console.WriteLine(res);
            }
            Console.ReadLine();
        }

        static string Process(string command, int number, string name)
        {
            string res = string.Empty;
            switch(command)
            {
                case "add":
                    if (_phoneBook.ContainsKey(number))
                        _phoneBook[number] = name;
                    else
                        _phoneBook.Add(number, name);
                    break;
                case "find":
                    if (_phoneBook.ContainsKey(number))
                        res = _phoneBook[number];
                    else
                        res = "not found";
                    break;
                case "del":
                    if (_phoneBook.ContainsKey(number))
                        _phoneBook.Remove(number);
                    break;
                default:
                    throw new Exception();

            }
            return res;
        } 


    }
}
