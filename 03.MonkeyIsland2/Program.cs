using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.MonkeyIsland2
{
    class Program
    {
        static void Main(string[] args)
        {            
            int numCases = int.Parse(Console.ReadLine());
            List<double> results = new List<double>();

            for (int i = 0; i<numCases; i++)
            {
                string[] input = Console.ReadLine().Split(' ');
                int x = int.Parse(input[0]);
                int y = int.Parse(input[1]);

                // Pitagoras
                results.Add(Math.Round(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)), 2));
            }

            foreach (var v in results)
            {
                Console.WriteLine(v.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}
