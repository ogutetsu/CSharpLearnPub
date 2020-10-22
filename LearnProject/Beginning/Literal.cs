using System;
using LearnLib;

namespace LearnProject.Beginning
{
    public class Literal : ConsoleMenu
    {
        public override void Run()
        {
            //バイナリリテラル
            int bl = 0b010011;
            Console.WriteLine($"{bl}");
            
            //区切り記号
            int num = 1_000_000;
            //区切りは、任意の位置に記述できる
            num = 1000_000;
            Console.WriteLine($"{num}");
            


        }
    }
}