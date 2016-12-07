using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Event2016
{
    class Day05
    {
        private static string input = @"ffykfhsq";
        private static string test = @"abc";
        public string Run()
        {

            string doorId = input;

            byte[] feed;
            byte[] hash;
            char[] password = new char[] { char.MaxValue, char.MaxValue, char.MaxValue, char.MaxValue, char.MaxValue, char.MaxValue, char.MaxValue, char.MaxValue };
            long index = 0;
            MD5 hasher = MD5.Create();

            // Silly console UI
            Task ui = Task.Run(() => ConsoleUI(password));
            //we use a break in the loop to escape
            while (true)
            {
                do
                {
                    feed = Encoding.UTF8.GetBytes($"{doorId}{index++}");
                    hash = hasher.ComputeHash(feed);

                } while (!(hash[0] == 0 && hash[1] == 0 && hash[2] < 16));
                if (hash[2] < password.Length && password[hash[2]] == char.MaxValue)
                {
                    password[hash[2]] = hash[3].ToString("x2").First();

                    // The break check was put here so we don't have to verify every loop
                    if (!password.Any(x => x == char.MaxValue))
                    {
                        break;
                    }
                }
            }

            ui.Wait();
            //result = hash[2].ToString("x2").Substring(1);

            return $"Door password: " + string.Join("", password); ;
        }

        // Silly console UI
        public void ConsoleUI(char[] password)
        {
            Random rand = new Random();
            char[] lookup = { '0','1','2','3','4','5','6','7','8','9', 'a','b','c','d','e','f'};
            Console.CursorVisible = false;

            while (password.Any(char.MaxValue.Equals))
            {
                Console.SetCursorPosition(3, 1);

                foreach(char c in password)
                {
                    if(c == char.MaxValue)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(lookup[rand.Next(lookup.Length)]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(c);
                    }
                }
                Task.Delay(50).Wait();
            }
            string final = string.Join("", password.Select(x => x == char.MaxValue ? lookup[rand.Next(lookup.Length)] : x)) + " ";
            Console.SetCursorPosition(3, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(final);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
