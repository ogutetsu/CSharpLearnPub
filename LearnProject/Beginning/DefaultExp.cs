using System;
using LearnLib;

namespace LearnProject.Beginning
{
    public class DefaultExp : ConsoleMenu
    {
        public override void Run()
        {
            int def = default;
            int def1 = default(int);
            var def2 = default(int);
            int? def3 = default;

            Console.WriteLine($"default : {def}");
            Console.WriteLine($"default(int) : {def1}");
            Console.WriteLine($"var : {def2}");
            Console.WriteLine($"int? : {def3}");

            

        }
    }
}