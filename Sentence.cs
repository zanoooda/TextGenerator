using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGenerator
{
    public class Sentence // public ctor and ToString()
    {
        string _stringSentence;

        public Sentence(string s = "S")
        {
            List<Word> ReadyStructure = MakeReadyStructure(StringToList(s));
            for (int i = 0; i < ReadyStructure.Count; i++)
            {
                ReadyStructure[i]._value = Word.Substitute(ReadyStructure[i]);
            }
            _stringSentence = Beutify(ReadyStructure);
        }
        public override string ToString()
        {
            return _stringSentence;
        }

        static string punctuation_marks = ".!?,:";
        string Beutify(List<Word> ReadyStructure)
        {
            string stringSentence = string.Join(" ", ReadyStructure).Trim();
            if (punctuation_marks.IndexOf(stringSentence[stringSentence.Length - 1]) == -1)
                stringSentence += ".";
            stringSentence = string.Format("{0}{1}", char.ToUpper(stringSentence[0]), stringSentence.Substring(1));
            for (int i = 1; i < stringSentence.Length; i++)
            {
                if (punctuation_marks.IndexOf(stringSentence[i]) >= 0)
                {
                    if (stringSentence[i - 1] == ' ')
                        stringSentence = stringSentence.Remove(i - 1, 1);
                }
            }
            return stringSentence;
        }
        static Random r = new Random();
        static List<Word> MakeReadyStructure(List<Word> sentence)
        {
            List<Word> readySentence = new List<Word>();
            foreach (Word item in sentence)
            {
                if (!Rules._dictionaryRules.ContainsKey(item._value))
                {
                    readySentence.Add(item);
                    continue;
                }
                else
                {
                    List<string> rules = Rules._dictionaryRules[item._value];

                    List<Word> add = StringToList(rules[r.Next(rules.Count)]);
                    for (int i = 0; i < add.Count; i++)
                    {
                        if (add[i]._value == "V")
                        {
                            add[i]._tense = new Tense();
                            add[i]._tense.time = Tense.RandomTime();
                            add[i]._tense.aspect = Tense.RandomAspect();
                        }
                    }
                    readySentence.AddRange(add);
                }
            }
            bool Ready = true;
            foreach (Word item in readySentence)
            {
                if (Rules._dictionaryRules.ContainsKey(item._value))
                    Ready = false;
            }
            if (Ready)
            {
                return readySentence;
            }
            else
                return MakeReadyStructure(readySentence);
        }
        static List<Word> StringToList(string s)
        {
            List<Word> sentence = new List<Word>();
            StringBuilder word = new StringBuilder();
            foreach (char c in s)
            {
                if (punctuation_marks.IndexOf(c) >= 0 || c == ' ')
                {
                    if (word.Length != 0)
                    {
                        sentence.Add(new Word(word.ToString()));
                        word.Clear();
                    }
                    if (c != ' ')
                        sentence.Add(new Word(c.ToString()));
                }
                else
                    word.Append(c);
            }
            if (word.Length != 0)
                sentence.Add(new Word(word.ToString()));
            return sentence;
        }
    }
}
