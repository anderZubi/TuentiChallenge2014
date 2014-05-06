using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.F1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] inputParts = input.Split('#');
            string modifiedInput = "#" + inputParts[1] + inputParts[0];

            int X = 0;
            int Y = 0;
            
            Direction currentDirection = Direction.Right;

            List<Step> steps = new List<Step>();

            foreach (char c in modifiedInput)
            {
                char path;

                if (c == '-' && (currentDirection == Direction.Up || currentDirection == Direction.Down))
                    path = '|';
                else
                    path = c;
                steps.Add(new Step(new Position(X, Y), path));

                if (c == '-' || c == '#')
                {
                    if (currentDirection == Direction.Down)
                        Y++;
                    else if (currentDirection == Direction.Up)
                        Y--;
                    else if (currentDirection == Direction.Right)
                        X++;
                    else if (currentDirection == Direction.Left)
                        X--;
                }
                else if (c == '/')
                {
                    if (currentDirection == Direction.Down)
                    {
                        X--;
                        currentDirection = Direction.Left;
                    }
                    else if (currentDirection == Direction.Up)
                    {
                        X++;
                        currentDirection = Direction.Right;
                    }
                    else if (currentDirection == Direction.Right)
                    {
                        Y--;
                        currentDirection = Direction.Up;
                    }
                    else if (currentDirection == Direction.Left)
                    {
                        Y++;
                        currentDirection = Direction.Down;
                    }
                }
                else if (c == '\\')
                {
                    if (currentDirection == Direction.Down)
                    {
                        X++;
                        currentDirection = Direction.Right;
                    }
                    else if (currentDirection == Direction.Up)
                    {
                        X--;
                        currentDirection = Direction.Left;
                    }
                    else if (currentDirection == Direction.Right)
                    {
                        Y++;
                        currentDirection = Direction.Down;
                    }
                    else if (currentDirection == Direction.Left)
                    {
                        Y--;
                        currentDirection = Direction.Up;
                    }
                }
            }

            int minX = steps.Min(x => x.Position.X);
            int minY = steps.Min(y => y.Position.Y);

            foreach (var s in steps)
            {
                s.Position.X = s.Position.X - minX;
                s.Position.Y = s.Position.Y - minY;
            }

            int columns = steps.Max(x => x.Position.X) + 1;
            int lines = steps.Max(y => y.Position.Y) + 1;

            for (int i = 0; i<lines; i++)
            {
                StringBuilder sb = new StringBuilder(new string(' ', columns));

                var chars = steps.Where(x => x.Position.Y == i).ToList();
                foreach (var c in chars)
                {
                    sb[c.Position.X] = c.StepType;
                }

                Console.WriteLine(sb.ToString());
            }

            Console.ReadLine();
        }       
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Step
    {
        public Position Position { get; set; }
        public char StepType { get; set; }

        public Step (Position pos, char type)
        {
            Position = pos;
            StepType = type;
        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position (int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
