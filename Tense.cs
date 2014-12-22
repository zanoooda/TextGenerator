using System;

namespace TextGenerator
{
    enum Time { Past, Present, Future }
    enum Aspect { Progressive, Simple, Perfect, Perfect_Progressive }
    class Tense
    {
        public Time time;
        public Aspect aspect;

        static Random r = new Random();
        public static Time RandomTime()
        {
            int rnd = r.Next(3);
            switch (rnd)
            {
                case 0:
                    return Time.Future;
                case 1:
                    return Time.Past;
                case 2:
                    return Time.Present;
                default:
                    return Time.Present;
            }
        }
        public static Aspect RandomAspect()
        {
            int rnd = r.Next(4);
            switch (rnd)
            {
                case 0:
                    return Aspect.Perfect;
                case 1:
                    return Aspect.Perfect_Progressive;
                case 2:
                    return Aspect.Progressive;
                case 3:
                    return Aspect.Simple;
                default:
                    return Aspect.Simple;
            }
        }
    }
}
