using AdventOfCode.Event2016;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        private static IDay[] days = { new Day01(), new Day02(), new Day03(), new Day04(), new Day05(), new Day06()};
        public static void Main(string[] args)
        {
            int selected = -1;
            while (selected != 0)
            {
                selected = -1;
                while (!(selected >= 0 && selected <= days.Length))
                {
                    Console.Write($"Select which day number to run and press {{ENTER}}\nDays (1-{days.Length}), or zero {{0}} to exit: ");
                    string read = Console.ReadLine();
                    if (!int.TryParse(read, out selected))
                    {
                        selected = -1;
                    }
                }

                if (selected == 0)
                    break;
                Console.WriteLine($"Running day {selected} solution...");

                string result = days[selected - 1].Run();
                Console.WriteLine(result);
                Console.WriteLine($"\nCompleted running solution for day {selected}\n");
            }
        }
    }
}
