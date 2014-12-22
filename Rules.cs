using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TextGenerator.Properties;

namespace TextGenerator
{
    class Rules // public static Dictionary<string, List<string>> _dictionaryRules
    {
        public static Dictionary<string, List<string>> _dictionaryRules = TextToDictionary();

        public static void PrintRules()//not needed
        {
            foreach (KeyValuePair<string, List<string>> pair in Rules.TextToDictionary())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(pair.Key);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" -> ");
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < Rules.TextToDictionary()[pair.Key].Count; i++)
                {
                    if (i == Rules.TextToDictionary()[pair.Key].Count - 1)
                    {
                        Console.Write(pair.Value[i]);
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(pair.Value[i]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" | ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }

        static Dictionary<string, List<string>> TextToDictionary()
        {
            string[] stringRules = Regex.Split(Resources.rules, "\r\n|\r|\n");
            Dictionary<string, List<string>> dictionaryRules = new Dictionary<string, List<string>>();
            string[] separators = new string[] { ">", "->", "-->" };
            foreach (string rule in stringRules)
            {
                string[] currientrule = rule.Split(separators, StringSplitOptions.None);
                if (currientrule.Length != 2)
                    continue;
                string currientKey = currientrule[0].Trim();
                string currientValue = System.Text.RegularExpressions.Regex.Replace(currientrule[1].Trim(), @"\s+", " ");//using System.Text.RegularExpressions;
                if (currientKey.ToCharArray()[0] == '/' && currientKey.ToCharArray()[1] == '/')
                    continue;
                if (dictionaryRules.ContainsKey(currientKey))
                    dictionaryRules[currientKey].Add(currientValue);
                else
                {
                    dictionaryRules.Add(currientKey, new string[] { currientValue }.ToList());
                }
            }
            return dictionaryRules;
        }
    }
}
