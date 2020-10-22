using System;
using LearnLib;

namespace LearnProject.Beginning
{
    public class TupleDeconstruction : ConsoleMenu
    {
        public override void Run()
        {
            Console.Write("数値を入力してください -> ");
            var numStr = Console.ReadLine();
            var element = Even(Convert.ToInt32(numStr));
            
            Console.WriteLine($"num  {element.Item1} は {element.Item2}");
            Console.WriteLine($"タプル名を省略した場合はItem1,Item2でアクセスできます。");

            var elementName = EvenName(Convert.ToInt32(numStr));
            Console.WriteLine($"num  {elementName.number} は {elementName.outstr}");
            Console.WriteLine($"タプル名を入力した際は、その名前でアクセスできます。");

            var res = element.CompareTo(elementName);
            if (res == 0)
            {
                Console.WriteLine("EvenとEvenNameは同じです");
            }

            var (number, outstr) = Even(Convert.ToInt32(numStr));
            Console.WriteLine($"num  {number} は {outstr}");
            Console.WriteLine($"戻り値をタプルで受け取ることもできます。");
            
            var deconst = Even(Convert.ToInt32(numStr));
            var (deNum, deStr) = deconst;
            Console.WriteLine($"num  {deNum} は {deStr}");
            Console.WriteLine($"varで受け取って任意の名前に分解することもできます。");

        }


        public (int, string) Even(int num)
        {
            string even = ((num & 1) == 0) ? "偶数" : "奇数";
            return (num, even);
        }

        public (int number, string outstr) EvenName(int num)
        {
            string even = ((num & 1) == 0) ? "偶数" : "奇数";
            return (number:num, outstr:even);
        }
        
    }
}