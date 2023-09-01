using System.Collections;
using System.Collections.Generic;

namespace Labb1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userInput = getUserInput();
            int sum = sumAllNumbers(userInput);

            foreach (string line in highlightConsecutiveNumbers(userInput)) Console.WriteLine(line);

            Console.WriteLine(sum);
        }
        static string getUserInput()
        {
            string str = "";
            // If the user enters nothing it keeps going
            while (str == "")
            {
                Console.WriteLine("Enter a number or else..");
                str = Console.ReadLine();
            }

            return str;
        }
        static IEnumerable<string> highlightConsecutiveNumbers(string str)
        {
            char[] arrFromString = str.ToCharArray();
            HashSet<char> uniqueVals = new HashSet<char>(arrFromString);
            Dictionary<char, int> backup = uniqueVals.ToDictionary(key => key, el => (int)-1);
            Dictionary<char, int> indexes = new Dictionary<char, int>(backup);

            for (int i = 0; i < arrFromString.Length; i++)
            {
                // Not the best conversion, wont pass all tests
                if (!Char.IsDigit(arrFromString[i]))
                {
                    indexes = backup;
                    continue;
                }

                int savedIndex = 0;
                indexes.TryGetValue(arrFromString[i], out savedIndex);

                if (savedIndex >= 0)
                {
                    string oneThird = str.Substring(0, savedIndex);
                    string twoThirds = "\u001b[0m\x1b[31m" + str.Substring(savedIndex, i - savedIndex + 1) + "\x1b[0m";
                    string threeThirds = str.Substring(i + 1);
                    string concat = oneThird + twoThirds + threeThirds;

                    yield return concat;
                }

                indexes[arrFromString[i]] = i;
            }
        }
        static int sumAllNumbers(string str)
        {
            int sum = 0;

            for (int i = 0; i <  str.Length; i++)
            {
                if (!Char.IsDigit(str[i])) break;

                // Guess this is how you convert chars to ints in c# smh my head
                sum += str[i] - '0';

            }

            return sum;
        }
    }
}