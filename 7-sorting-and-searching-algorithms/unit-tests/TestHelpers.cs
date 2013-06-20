using System;
using System.Linq;

namespace unit_tests
{
    static class TestHelpers
    {

        static readonly Random rand = new Random();

        public static T ElementFrom<T>(T[] array)
        {
            return array[rand.Next(0, array.Length)];
        }

        public static int ElementNotFrom(int[] array)
        {
            int ret;
            do
            {
                ret = rand.Next();
            } 
            while (array.Contains(ret));
            return ret;
        }

        public static T[] GenerateArray<T>(ArrayOptions option, int length)
        {
            var ret = new T[length];
            for (var ii = 0; ii < length; ++ii)
            {
                ret[ii] = (T)GetRandom(typeof(T));
            }
            if (option == ArrayOptions.Sorted)
            {
                Array.Sort(ret);
            }
            if (option == ArrayOptions.Reversed)
            {
                Array.Sort(ret);
                Array.Reverse(ret);
            }
            return ret;
        }

        public static object GetRandom(Type t)
        {
            if (t == typeof(Double))
            {
                return rand.NextDouble() * long.MaxValue;
            }
            if (t == typeof(Int32))
            {
                return rand.Next();
            }
            if (t == typeof(String))
            {
                return rand.Next() + "";
            }

            throw new ArgumentException();
        }
    }
}