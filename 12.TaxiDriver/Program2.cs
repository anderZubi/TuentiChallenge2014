using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _12.TaxiDriver
{
    class Program2
    {
        static int M;
        static int N;
        static int[] start = new int[2];
        static int[] finish = new int[2];
        static char[,] table;

        static void Main(string[] args)
        {
            int numCases = int.Parse(Console.ReadLine());

            List<int> results = new List<int>();

            for (int i = 0; i < numCases; i++)
            {
                Dictionary<int[], int[][]> graph = new Dictionary<int[], int[][]>(new ArrayEqualityComparer());

                string[] size = Console.ReadLine().Split(' ');

                M = int.Parse(size[0]);
                N = int.Parse(size[1]);

                int from = 0;
                int to = 0;

                table = new char[N, M];

                for (int x = 0; x < N; x++)
                {
                    string input = Console.ReadLine();

                    for (int y = 0; y < input.Length; y++)
                    {
                        if (input[y] == 'S')
                        {
                            start[0] = x;
                            start[1] = y;
                            table[x, y] = '.';
                            from = int.Parse((x + 1).ToString() + (y + 1).ToString());
                        }
                        else if (input[y] == 'X')
                        {
                            finish[0] = x;
                            finish[1] = y;
                            table[x, y] = '.';
                            to = int.Parse((x + 1).ToString() + (y + 1).ToString());
                        }
                        table[x, y] = input[y];
                    }
                }

                for (int x = 0; x < N; x++)
                {
                    for (int y = 0; y < M; y++)
                    {
                        var possiblePhats = GetPossibleTransitions(new int[] { x, y, x + 1, y }, table);

                        int[] point = new int[] { x, y, x + 1, y };
                        int[][] transitions = new int[possiblePhats.Count][];

                        for (int a = 0; a < possiblePhats.Count; a++)
                        {
                            transitions[a] = new int[] { x + 1, y, possiblePhats[a][2], possiblePhats[a][3] };
                        }

                        graph.Add(point, transitions);

                        possiblePhats = GetPossibleTransitions(new int[] { x, y, x, y + 1 }, table);

                        point = new int[] { x, y, x, y + 1 };
                        transitions = new int[possiblePhats.Count][];

                        for (int a = 0; a < possiblePhats.Count; a++)
                        {
                            transitions[a] = new int[] { x, y + 1, possiblePhats[a][2], possiblePhats[a][3] };
                        }

                        graph.Add(point, transitions);

                        possiblePhats = GetPossibleTransitions(new int[] { x, y, x - 1, y }, table);

                        point = new int[] { x, y, x - 1, y };
                        transitions = new int[possiblePhats.Count][];

                        for (int a = 0; a < possiblePhats.Count; a++)
                        {
                            transitions[a] = new int[] { x - 1, y, possiblePhats[a][2], possiblePhats[a][3] };
                        }

                        graph.Add(point, transitions);

                        possiblePhats = GetPossibleTransitions(new int[] { x, y, x, y - 1 }, table);

                        point = new int[] { x, y, x, y - 1 };
                        transitions = new int[possiblePhats.Count][];

                        for (int a = 0; a < possiblePhats.Count; a++)
                        {
                            transitions[a] = new int[] { x, y - 1, possiblePhats[a][2], possiblePhats[a][3] };
                        }

                        graph.Add(point, transitions);
                    }
                }

                var result = ShortestPath(from, to, graph);

                if (result.Contains(null))
                    results.Add(-1);
                else
                    results.Add(result.Count() + 1);

            }

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i] == -1)
                    Console.WriteLine(String.Format("Case #{0}: {1}", i + 1, "ERROR"));
                else
                    Console.WriteLine(String.Format("Case #{0}: {1}", i + 1, results[i]));
            }

            Console.ReadLine();
        }

        private static IEnumerable<int[]> ShortestPath(int fromState, int toState, Dictionary<int[], int[][]> graph)
        {
            var q = new Queue<int[]>();
            var map = new Dictionary<int[], int[]>(new ArrayEqualityComparer());

            var posPaths = graph.Where(x => x.Key[0] == start[0] && x.Key[1] == start[1]);

            foreach (var v in posPaths)
            {
                map.Add(v.Key, new int[] { -1, -1, -1, -1 });
                q.Enqueue(v.Key);
            }

            while (q.Count > 0)
            {
                var current = q.Dequeue();
                foreach (var s in graph[current])
                {
                    if (!map.ContainsKey(s))
                    {
                        map.Add(s, current);
                        if (s[2] == finish[0] && s[3] == finish[1])
                        {
                            var result = new Stack<int[]>();
                            var thisNode = s;
                            do
                            {
                                result.Push(thisNode);
                                thisNode = map[thisNode];
                            } while (thisNode[0] != start[0] || thisNode[1] != start[1]);
                            while (result.Count > 0)
                                yield return result.Pop();
                            yield break;
                        }
                        q.Enqueue(s);
                    }
                }
            }
            yield return null;
        }


        private static List<int[]> GetPossibleTransitions(int[] current, char[,] table)
        {
            List<int[]> result = new List<int[]>();

            int x = current[2];
            int y = current[3];

            if (x != -1 && y != -1 && x < N && y < M)
            {

                if (current[0] == current[2] && current[1] < current[3]) // comes from left
                {
                    if (x + 1 <= N - 1 && table[x + 1, y] != '#')
                        result.Add(new int[] { x, y, x + 1, y });
                    if (y + 1 <= M - 1 && table[x, y + 1] != '#')
                        result.Add(new int[] { x, y, x, y + 1 });
                }
                else if (current[0] == current[2] && current[1] > current[3]) // comes from right
                {
                    if (x - 1 >= 0 && table[x - 1, y] != '#')
                        result.Add(new int[] { x, y, x - 1, y });
                    if (y - 1 >= 0 && table[x, y - 1] != '#')
                        result.Add(new int[] { x, y, x, y - 1 });
                }
                else if (current[0] < current[2] && current[1] == current[3]) // comes from up
                {
                    if (x + 1 <= N - 1 && table[x + 1, y] != '#')
                        result.Add(new int[] { x, y, x + 1, y });
                    if (y - 1 >= 0 && table[x, y - 1] != '#')
                        result.Add(new int[] { x, y, x, y - 1 });
                }
                else if (current[0] > current[2] && current[1] == current[3]) //comes from down
                {
                    if (x - 1 >= 0 && table[x - 1, y] != '#')
                        result.Add(new int[] { x, y, x - 1, y });
                    if (y + 1 <= M - 1 && table[x, y + 1] != '#')
                        result.Add(new int[] { x, y, x, y + 1 });
                }
            }

            return result;
        }

        class ArrayEqualityComparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(int[] obj)
            {
                StringBuilder sb = new StringBuilder("");

                for (int i = 0; i < obj.Length; ++i)
                {
                    sb.Append((obj[i] + 3).ToString());
                }
                return int.Parse(sb.ToString());
            }

        }

    }
}
