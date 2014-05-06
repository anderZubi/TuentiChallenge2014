using System;
using System.Collections.Generic;
using System.Linq;

namespace _05.Tribblemaker
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] currentState = new char[8, 8];

            for (int i = 0; i < 8; i++)
            {
                string line = Console.ReadLine();
                for (int j = 0; j < 8; j++)
                {
                    currentState[i, j] = line[j];
                }
            }

            char[,] nextState = new char[8, 8];

            List<char[,]> states = new List<char[,]>();

            states.Add(currentState);

            bool found = false;

            while (!found)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        nextState[i, j] = CalculateNext(i, j, currentState);

                        

                    }
                }

                // konprobatu dagoeneko existitzen den, eta hala bada, non;

                for (int i = 0; i< states.Count; i++)
                {
                    var equal =
                        nextState.Rank == states[i].Rank &&
                        Enumerable.Range(0, nextState.Rank).All(dimension => nextState.GetLength(dimension) == states[i].GetLength(dimension)) &&
                        nextState.Cast<char>().SequenceEqual(states[i].Cast<char>());

                    if (equal)
                    {
                        found = true;
                        Console.WriteLine(String.Format("{0} {1}", i, states.Count - i));
                        Console.ReadLine();
                        return;
                    }
                    
                }

                char[,] copy1 = new char[8, 8];
                char[,] copy2 = new char[8, 8];

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        copy1[i, j] = nextState[i, j];

                        copy2[i, j] = nextState[i, j];

                    }
                }

                states.Add(copy1);
                currentState = copy2;
            }

            
        }

        private static char CalculateNext(int i, int j, char[,] currentState)
        {

            int count = 0;

            for (int x = i - 1; x <= i + 1; x++)
            {
                for (int y = j - 1; y <= j + 1; y++)
                {
                    if (0 <= x && x < 8 && 0 <= y && y < 8 && (i != x || j != y) && currentState[x, y] == 'X')
                        count++;
                }
            }
            if (count == 3)
            {
                return 'X';
            }
            else if (currentState[i, j] == 'X' && count == 2)
            {
                // inguruan 2 edo 3 bizirik baditu, bizirik
                return 'X';
            }
            else
            {
                return '-';
            }
        }
    }
}
