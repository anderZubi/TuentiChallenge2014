using System;
using System.Collections.Generic;
using System.Linq;

namespace _04.ShapeShifters
{
    class Program
    {
        static void Main(string[] args)
        {
            string start = Console.ReadLine();
            string goal = Console.ReadLine();

            List<string> possibles = new List<string>();

            string pos = Console.ReadLine();
            while (!String.IsNullOrEmpty(pos))
            {
                possibles.Add(pos);
                pos = Console.ReadLine();
            }

            //ShapeShift(start, goal, possibles);

            ShapeShiftIterative(start, goal, possibles);

            Console.ReadLine();

        }


        // RECURSIVE SOLUTION

        private static void ShapeShift(string s, string g, List<string> l)
        {
            if (Levenshtein(s, g) == 1)
                Console.Write(String.Format("{0}->{1}", s, g));
            else
            {
                foreach (var v in l)
                {
                    if (Levenshtein(s, v) == 1)
                    {
                        Console.Write(String.Format("{0}->", s));
                        ShapeShift(v, g, l.Where(x => x != s).ToList());
                    }

                }
            }
        }

        private static int Levenshtein(string s1, string s2)
        {
            if (s1.Length == 0 || s2.Length == 0)
            {
                return Math.Max(s1.Length, s2.Length);
            }
            else
            {
                int[] solutions = new int[3];

                solutions[0] = Levenshtein(s1.Remove(0, 1), s2) + 1;
                solutions[1] = Levenshtein(s1, s2.Remove(0, 1)) + 1;
                if (s1[0] == s2[0])
                    solutions[2] = Levenshtein(s1.Remove(0, 1), s2.Remove(0, 1));
                else
                    solutions[2] = Levenshtein(s1.Remove(0, 1), s2.Remove(0, 1)) + 1;

                return solutions.Min();
            }
        }

        // ITERATIVE SOLUTION

        private static void ShapeShiftIterative(string s, string g, List<string> l)
        {
            List<List<string>> solutions = new List<List<string>>();
            bool found = false;

            solutions.Add(new List<string> { s });

            while (!found)
            {
                List<List<string>> helperList = new List<List<string>>();

                foreach (var sol in solutions)
                {
                    if (LevenshteinIterative(sol.Last(), g) == 1)
                    {
                        sol.Add(g);
                        Console.WriteLine(String.Join("->", sol));
                        found = true;
                        return;
                    }
                    else
                    {
                        foreach (var p in l)
                        {
                            if (sol.Count(x => x == p) == 0 && LevenshteinIterative(sol.Last(), p) == 1)
                            {
                                List<string> solHelper = sol.ToList();
                                solHelper.Add(p);
                                helperList.Add(solHelper);
                            }
                        }
                    }
                }

                solutions = helperList.ToList();
            }
        }


        private static int LevenshteinIterative(string s, string t)
        {
            // degenerate cases
            if (s == t) return 0;
            if (s.Length == 0) return t.Length;
            if (t.Length == 0) return s.Length;

            // create two work vectors of integer distances
            int[] v0 = new int[t.Length + 1];
            int[] v1 = new int[t.Length + 1];

            // initialize v0 (the previous row of distances)
            // this row is A[0][i]: edit distance for an empty s
            // the distance is just the number of characters to delete from t
            for (int i = 0; i < v0.Length; i++)
                v0[i] = i;

            for (int i = 0; i < s.Length; i++)
            {
                // calculate v1 (current row distances) from the previous row v0

                // first element of v1 is A[i+1][0]
                //   edit distance is delete (i+1) chars from s to match empty t
                v1[0] = i + 1;

                // use formula to fill in the rest of the row
                for (int j = 0; j < t.Length; j++)
                {
                    var cost = (s[i] == t[j]) ? 0 : 1;
                    var arr = new int[3] { v1[j] + 1, v0[j + 1] + 1, v0[j] + cost };
                    v1[j + 1] = arr.Min();
                }

                // copy v1 (current row) to v0 (previous row) for next iteration
                for (int j = 0; j < v0.Length; j++)
                    v0[j] = v1[j];
            }

            return v1[t.Length];
        }
    }
}
