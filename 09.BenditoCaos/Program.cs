using System;
using System.Collections.Generic;
using System.Linq;

namespace _09.BenditoCaos
{
    class Program
    {
        const int SPACE_FOR_CAR = 5;

        static void Main(string[] args)
        {
            int numCases = int.Parse(Console.ReadLine());

            List<City> cities = new List<City>();

            for (int i = 0; i < numCases; i++)
            {
                City city = new City();

                city.Name = Console.ReadLine();
                string[] speeds = Console.ReadLine().Split(' ');
                city.NormalSpeed = int.Parse(speeds[0]);
                city.DirtySpeed = int.Parse(speeds[1]);
                string[] numbers = Console.ReadLine().Split(' ');
                city.Intersections = int.Parse(numbers[0]);

                city.Roads = new List<Road>();

                for (int j = 0; j < int.Parse(numbers[1]); j++)
                {
                    string[] road = Console.ReadLine().Split(' ');
                    city.Roads.Add(new Road { Start = road[0], End = road[1], Type = road[2], Lanes = int.Parse(road[3]) });
                }

                cities.Add(city);
            }

            foreach (var city in cities)
            {
                allPaths = new List<Tuple<string, int>>();
                GetPaths(city, city.Name, "", int.MaxValue);

                int result = 0;

                foreach(var v in allPaths)
                {
                    result = result + v.Item2;
                }

                Console.WriteLine(String.Format("{0} {1}", city.Name, result));
            }
        }

        
        static List<Tuple<string, int>> allPaths = new List<Tuple<string, int>>();

        private static void GetPaths(City city, string currentCity, string path, int min)
        {
            if (currentCity == "AwesomeVille")
            {
                allPaths.Add(new Tuple<string, int>(path + "->AwesomeVille", min));

            }
            else
            {
                if (currentCity == city.Name)
                {
                    path = city.Name;
                }
                else
                {
                    path = path + "->" + currentCity;
                }
                foreach (var r in city.Roads.Where(x => x.Start == currentCity))
                {
                    if (!path.Contains(r.End))
                    {
                        int bandwidth = 0;
                        if (r.Type == "normal")
                        {
                            bandwidth = city.NormalSpeed * 1000 * r.Lanes / SPACE_FOR_CAR;
                        }
                        else
                        {
                            bandwidth = city.DirtySpeed * 1000 * r.Lanes / SPACE_FOR_CAR;
                        }
                        GetPaths(city, r.End, path, Math.Min(min, bandwidth));
                    }
                }
            }
        }

  

        private class City
        {
            public string Name { get; set; }
            public int NormalSpeed { get; set; }
            public int DirtySpeed { get; set; }

            public int Intersections { get; set; }
            public List<Road> Roads { get; set; }

        }

        private class Road
        {
            public string Start { get; set; }
            public string End { get; set; }
            public string Type { get; set; }
            public int Lanes { get; set; }

        }
    }
}
