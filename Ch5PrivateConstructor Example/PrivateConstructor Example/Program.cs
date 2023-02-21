using System;

namespace NowNew
{
    class NoNew
    {
        private NoNew() { Console.WriteLine("I’m alive!"); }
        public static NoNew CreateInstance() { return new NoNew(); }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            // Toggle between these two lines:
            // First line fais to compile because of private constructor
            // Second line succeeds because the method in the class is public, static, and has
            // permission to create an instance of the class (from within the class)

            // NoNew thisNew = new NoNew();
            NoNew.CreateInstance();
        }
    }
}
