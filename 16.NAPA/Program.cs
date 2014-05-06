using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _16.NAPA
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(',');

            int N = int.Parse(input[0]);
            int M = int.Parse(input[1]);

            List<string> lines = File.ReadLines(@"..\points").Skip(N - 1).Take(M).ToList();
            List<Point> points = new List<Point>();

            foreach (var v in lines)
            {
                string[] s = v.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                points.Add(new Point { X = int.Parse(s[0]), Y = int.Parse(s[1]), Radius = int.Parse(s[2]) });
            }

            int result = 0;

            points = points.OrderBy(x => x.X).ToList();

            object monitor = new object();

            Parallel.For(0, points.Count, (i) =>
                {
                    for (int j = i + 1; j < points.Count; j++)
                    {
                        if (points[i].X + points[i].Radius + 500 > points[j].X)  // If it's further we can be break the iteration. Since list is ordered, none will be further
                        {
                            var distance = Math.Sqrt(Math.Pow(points[j].X - points[i].X, 2) + Math.Pow(points[j].Y - points[i].Y, 2));
                            if (points[i].Radius + points[j].Radius > distance)
                                lock (monitor) result++;
                        }
                        else
                            break;
                    }
                });

            Console.WriteLine(result);
        }
    }

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
    }
}
