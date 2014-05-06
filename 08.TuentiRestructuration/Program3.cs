using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _08.TuentiRestructuration
{
    class Program3
    {

        static int numCases;

        static Dictionary<int, int[]> graph;

        static void Main(string[] args)
        {
            numCases = int.Parse(Console.ReadLine());

            List<Tuple<int, int>> cases = new List<Tuple<int, int>>();

            Dictionary<int, int[]> possibleStates = new Dictionary<int, int[]>();

            graph = new Dictionary<int, int[]>();

            int[] items = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
            List<int[]> permutationList = Permutation.GetPermutations<int>(items).ToList();
            for (int i = 0; i < permutationList.Count; i++)
            {
                possibleStates.Add(GetHashCode(permutationList[i]), permutationList[i]);
            }

            foreach (var v in possibleStates)
            {
                var pn = GetPosibleNexts(v.Value);
                int[] paths = new int[pn.Count];
                for (int i = 0; i < pn.Count; i++)
                {
                    paths[i] = GetHashCode(pn[i]);
                }
                graph.Add(v.Key, paths);
            }

            for (int i = 0; i < numCases; i++)
            {
                string[] people = new string[8];
                Dictionary<string, int> peopleDictionary = new Dictionary<string, int>();

                Console.ReadLine();
                string[] line1 = Console.ReadLine().Split(',');
                string[] line2 = Console.ReadLine().Split(',');
                string[] line3 = Console.ReadLine().Split(',');
                peopleDictionary.Add(line1[0].Trim(), 1);
                peopleDictionary.Add(line1[1].Trim(), 2);
                peopleDictionary.Add(line1[2].Trim(), 3);
                peopleDictionary.Add(line2[2].Trim(), 4);
                peopleDictionary.Add(line3[2].Trim(), 5);
                peopleDictionary.Add(line3[1].Trim(), 6);
                peopleDictionary.Add(line3[0].Trim(), 7);
                peopleDictionary.Add(line2[0].Trim(), 8);
                peopleDictionary.Add(" ", 0);
                
                List<int> table1 = new List<int>();
                foreach (var v in peopleDictionary)
                {
                    table1.Add(v.Value);
                }

                var state1Array = table1.ToArray();
                int state1 = GetHashCode(state1Array);

                Console.ReadLine();
                line1 = Console.ReadLine().Split(',');
                line2 = Console.ReadLine().Split(',');
                line3 = Console.ReadLine().Split(',');

                List<int> table2 = new List<int>(9);

                foreach (var v in line1)
                    table2.Add(peopleDictionary[v.Trim()]);               
                    
                table2.Add(peopleDictionary[line2[2].Trim()]);
                table2.Add(peopleDictionary[line3[2].Trim()]);
                table2.Add(peopleDictionary[line3[1].Trim()]);
                table2.Add(peopleDictionary[line3[0].Trim()]);

                table2.Add(peopleDictionary[line2[0].Trim()]);
                table2.Add(peopleDictionary[" "]);

                var state2Array = table2.ToArray();
                int state2 = GetHashCode(state2Array);

                cases.Add(new Tuple<int, int>(state1, state2));
            }


            foreach (var c in cases)
            {
                var result = ShortestPath(c.Item1, c.Item2);
                
                if (result.Contains(-1))
                    Console.WriteLine(-1);
                else
                    Console.WriteLine(result.Count());               
            }
          
            Console.ReadLine();
        }

        private static IEnumerable<int> ShortestPath(int fromState, int toState )
        {          
            var map = new Dictionary<int, int>();

            var q = new Queue<int>();
            map.Add(fromState, 12345678);
            q.Enqueue(fromState);
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                foreach (var s in graph[current])
                {
                    if (!map.ContainsKey(s))
                    {
                        map.Add(s, current);
                        if (s == toState)
                        {
                            var result = new Stack<int>();
                            var thisNode = s;
                            do
                            {
                                result.Push(thisNode);
                                thisNode = map[thisNode];
                            } while (thisNode != fromState);
                            while (result.Count > 0)
                                yield return result.Pop(); 
                            yield break;
                        }
                        q.Enqueue(s);
                    }
                }
            }
            yield return -1;
        }

        private static int GetHashCode(int[] array)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < array.Length; ++i)
            {
                sb.Append(array[i].ToString());               
            }
            return int.Parse(sb.ToString());
        }
       
        private static List<int[]> GetPosibleNexts(int[] table1)
        {
            List<int[]> result = new List<int[]>();

            for (int i = 0; i < table1.Length - 2; i++)
            {
                var helper = table1.ToArray();
                helper[i] = table1[i + 1];
                helper[i + 1] = table1[i];
                result.Add(helper);

                //// move 2 positions
                //if (i % 2 == 0 && i < table1.Length - 2)
                //{
                //    var helper3 = table1.ToArray();
                //    helper3[i] = table1[i + 2];
                //    helper3[i + 2] = table1[i];
                //    result.Add(helper3);
                //}

                // swap erdikoarekin
                if (i % 2 == 1 )
                {
                    var helper3 = table1.ToArray();
                    helper3[i] = table1[table1.Length-1];
                    helper3[table1.Length-1] = table1[i];
                    result.Add(helper3);
                }
            }
            var helper2 = table1.ToArray();
            helper2[0] = table1[table1.Length - 2];
            helper2[table1.Length - 2] = table1[0];
            result.Add(helper2);


            var helper7 = table1.ToArray();
            helper7[table1.Length-1] = table1[table1.Length - 2];
            helper7[table1.Length - 2] = table1[table1.Length-1];
            result.Add(helper7);


            //// move 2 positions
            //var helper4 = table1.ToArray();
            //helper4[0] = table1[table1.Length - 2];
            //helper4[table1.Length - 2] = table1[0];
            //result.Add(helper4);

            //// proba egiteko
            //var helper5 = table1.ToArray();
            //helper5[1] = table1[5];
            //helper5[5] = table1[1];
            //result.Add(helper5);

            //var helper6 = table1.ToArray();
            //helper6[3] = table1[7];
            //helper6[7] = table1[3];
            //result.Add(helper6);


            return result;
        }

        public class Permutation
        {
            public static IEnumerable<T[]> GetPermutations<T>(T[] items)
            {
                int[] work = new int[items.Length];
                for (int i = 0; i < work.Length; i++)
                {
                    work[i] = i;
                }
                foreach (int[] index in GetIntPermutations(work, 0, work.Length))
                {
                    T[] result = new T[index.Length];
                    for (int i = 0; i < index.Length; i++) result[i] = items[index[i]];
                    yield return result;
                }
            }

            public static IEnumerable<int[]> GetIntPermutations(int[] index, int offset, int len)
            {
                if (len == 1)
                {
                    yield return index;
                }
                else if (len == 2)
                {
                    yield return index;
                    Swap(index, offset, offset + 1);
                    yield return index;
                    Swap(index, offset, offset + 1);
                }
                else
                {
                    foreach (int[] result in GetIntPermutations(index, offset + 1, len - 1))
                    {
                        yield return result;
                    }
                    for (int i = 1; i < len; i++)
                    {
                        Swap(index, offset, offset + i);
                        foreach (int[] result in GetIntPermutations(index, offset + 1, len - 1))
                        {
                            yield return result;
                        }
                        Swap(index, offset, offset + i);
                    }
                }
            }

            private static void Swap(int[] index, int offset1, int offset2)
            {
                int temp = index[offset1];
                index[offset1] = index[offset2];
                index[offset2] = temp;
            }

        }
    }
}
