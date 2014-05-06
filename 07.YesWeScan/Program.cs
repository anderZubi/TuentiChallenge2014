using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07.YesWeScan
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            string[] lines = System.IO.File.ReadAllLines(@"..\phone_call.log");
            List<int[]> calls = new List<int[]>();

            foreach (var l in lines)
            {
                calls.Add(new int[] { int.Parse(l.Split()[0]), int.Parse(l.Split()[1]) });
            }

            int result = calls.IndexOf(FindConnectingCall(a, b, calls));

            if (result >= 0)
                Console.WriteLine(String.Format("Connected at {0}", result));
            else
                Console.WriteLine("Not connected");
        }

        static int[] FindConnectingCall(int a, int b, List<int[]> list)
        {

            foreach (var l in list)
            {
                if ((l[0] == a && l[1] == b) || (l[0] == b && l[1] == a))
                    return l;
                else if (l[0] == a)
                {
                    return FindConnectingCall(b, l[1], list.Where(x => !((x[0] == a && x[1] == b) || (x[1] == a && x[0] == b))).ToList());
                }
                else if (l[1] == a)
                {
                    return FindConnectingCall(l[0], b, list.Where(x => !((x[0] == a && x[1] == b) || (x[1] == a && x[0] == b))).ToList());
                }
                else if (l[1] == b)
                {
                    return FindConnectingCall(l[0], a, list.Where(x => !((x[0] == a && x[1] == b) || (x[1] == a && x[0] == b))).ToList());
                }
                else if (l[0] == b)
                {
                    return FindConnectingCall(a, l[1], list.Where(x => !((x[0] == a && x[1] == b) || (x[1] == a && x[0] == b))).ToList());
                }
                else
                {
                    return null;
                }
            }

            return null;
        }


    }
}
