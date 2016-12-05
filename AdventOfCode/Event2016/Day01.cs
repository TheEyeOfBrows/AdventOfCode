using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Event2016
{
    class Day01
    {
        private static string input = @"L4, L3, R1, L4, R2, R2, L1, L2, R1, R1, L3, R5, L2, R5, L4, L3, R2, R2, L5, L1, R4, L1, R3, L3, R5, R2, L5, R2, R1, R1, L5, R1, L3, L2, L5, R4, R4, L2, L1, L1, R1, R1, L185, R4, L1, L1, R5, R1, L1, L3, L2, L1, R2, R2, R2, L1, L1, R4, R5, R53, L1, R1, R78, R3, R4, L1, R5, L1, L4, R3, R3, L3, L3, R191, R4, R1, L4, L1, R3, L1, L2, R3, R2, R4, R5, R5, L3, L5, R2, R3, L1, L1, L3, R1, R4, R1, R3, R4, R4, R4, R5, R2, L5, R1, R2, R5, L3, L4, R1, L5, R1, L4, L3, R5, R5, L3, L4, L4, R2, R2, L5, R3, R1, R2, R5, L5, L3, R4, L5, R5, L3, R1, L1, R4, R4, L3, R2, R5, R1, R2, L1, R4, R1, L3, L3, L5, R2, R5, L1, L4, R3, R3, L3, R2, L5, R1, R3, L3, R2, L1, R4, R3, L4, R5, L2, L2, R5, R1, R2, L4, L4, L5, R3, L4";
        private const int N = 0, E = 1, S = 2, W = 3;
        public string Run()
        {
            string[] inputs = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

            int angle = N;
            int x = 0;
            int y = 0;
            List<Tuple<int, int>> history = new List<Tuple<int, int>>();
            Tuple<int, int> dest = null;

            foreach (string cmd in inputs)
            {
                // Determine new angle, within 0-3
                if (cmd.IndexOf('L') >= 0)
                    angle -= 1;
                else
                    angle += 1;
                while (angle < 0)
                    angle += 4;
                angle %= 4;

                int steps = int.Parse(cmd.Substring(1));

                // Determine new position
                int newX = x;
                int newY = y;
                switch (angle)
                {
                    case N:
                        newY += steps;
                        break;
                    case E:
                        newX += steps;
                        break;
                    case S:
                        newY -= steps;
                        break;
                    case W:
                        newX -= steps;
                        break;
                }

                // Step along route, adding locations to history, and checking if we've crossed our path
                while (x != newX)
                {
                    x += Math.Sign(newX - x);
                    if (history.Any(h => h.Item1 == x && h.Item2 == y))
                    {
                        dest = new Tuple<int, int>(x, y);
                        break;
                    }
                    history.Add(new Tuple<int, int>(x, y));
                }
                while (y != newY && dest == null)
                {
                    y += Math.Sign(newY - y);
                    if (history.Any(h => h.Item1 == x && h.Item2 == y))
                    {
                        dest = new Tuple<int, int>(x, y);
                        break;
                    }
                    history.Add(new Tuple<int, int>(x, y));
                }

                // If we've crossed our path, we're done.
                if (dest != null)
                {
                    break;
                }

            }
            return $" X [{dest.Item1}] Y [{dest.Item2}] Distance [{(Math.Abs(dest.Item1) + Math.Abs(dest.Item2))}]";

        }
    }
}
