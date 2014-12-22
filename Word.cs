using System;
using System.Text.RegularExpressions;
using TextGenerator.Properties;

namespace TextGenerator
{
    class Word // public static string Substitute(string word), public string _value, public Tense _tense
    {
        public string _value;
        public Tense _tense;

        public Word(string value)
        {
            _value = value;
        }
        public override string ToString()
        {
            return _value;
        }

        static string[] noun = Regex.Split(Resources.nouns, "\r\n|\r|\n");//N
        static string[] verb = Regex.Split(Resources.verbs, "\r\n|\r|\n");//V
        static string[] adjective = Regex.Split(Resources.adjectives, "\r\n|\r|\n");//Adj
        static string[] adverb = Regex.Split(Resources.adverbs, "\r\n|\r|\n");//Adv
        static string[] preposition = Regex.Split(Resources.prepositions, "\r\n|\r|\n");//P
        static string[] article = Regex.Split(Resources.articles, "\r\n|\r|\n");//A
        static string[] pronoun = Regex.Split(Resources.pronouns, "\r\n|\r|\n");//PN
        static string[] coordinating_conjunction = Regex.Split(Resources.coordinating_conjunctions, "\r\n|\r|\n"); //CC   !!!nor, yet
        static string[] subordinating_conjunction = Regex.Split(Resources.subordinating_conjunctions, "\r\n|\r|\n");//SC

        static Random r = new Random();
        public static string Substitute(Word word)
        {
            foreach (char c in word._value)
            {
                if (c == '|')
                {
                    string[] s = word._value.Trim().Split('|');
                    return s[r.Next(s.Length)];
                }
            }
            if (word._value == "V")
            {
                if (word._tense == null)
                {
                    word._tense = new Tense();
                    word._tense.time = Tense.RandomTime();
                    word._tense.aspect = Tense.RandomAspect();
                }
                switch (word._tense.time)
                {
                    case Time.Past:
                        switch (word._tense.aspect)
                        {
                            case Aspect.Perfect:
                                return "had " + verb[r.Next(verb.Length)] + "ed";
                            case Aspect.Perfect_Progressive:
                                return "had been " + verb[r.Next(verb.Length)] + "ing";
                            case Aspect.Progressive:
                                return "was " + verb[r.Next(verb.Length)] + "ing";
                            case Aspect.Simple:
                                return verb[r.Next(verb.Length)] + "ed";
                            default:
                                break;
                        }
                        break;
                    case Time.Present:
                        switch (word._tense.aspect)
                        {
                            case Aspect.Perfect:
                                return "have " + verb[r.Next(verb.Length)] + "ed";
                            case Aspect.Perfect_Progressive:
                                return "have been " + verb[r.Next(verb.Length)] + "ing";
                            case Aspect.Progressive:
                                return "is " + verb[r.Next(verb.Length)] + "ing";
                            case Aspect.Simple:
                                return verb[r.Next(verb.Length)];
                            default:
                                break;
                        }
                        break;
                    case Time.Future:
                        switch (word._tense.aspect)
                        {
                            case Aspect.Perfect:
                                return "will have " + verb[r.Next(verb.Length)] + "ed";
                            case Aspect.Perfect_Progressive:
                                return "will have been " + verb[r.Next(verb.Length)] + "ing";
                            case Aspect.Progressive:
                                return "will be " + verb[r.Next(verb.Length)] + "ing";
                            case Aspect.Simple:
                                return "will " + verb[r.Next(verb.Length)];
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            switch (word._value)
            {
                case "N":
                    return noun[r.Next(noun.Length)];
                //case "V":
                //    return verb[r.Next(verb.Length)];
                case "Adj":
                    return adjective[r.Next(adjective.Length)];
                case "Adv":
                    return adverb[r.Next(adverb.Length)];
                case "A":
                    return article[r.Next(article.Length)];
                case "PN":
                    return pronoun[r.Next(pronoun.Length)];
                case "P":
                    return preposition[r.Next(preposition.Length)];
                case "CC":
                    return coordinating_conjunction[r.Next(coordinating_conjunction.Length)];
                case "SC":
                    return subordinating_conjunction[r.Next(subordinating_conjunction.Length)];
                default:
                    return word._value;
            }
        }
    }
}
