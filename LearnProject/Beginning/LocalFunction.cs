using System;
using LearnLib;

namespace LearnProject.Beginning
{
    public class LocalFunction : ConsoleMenu
    {
        public override void Run()
        {
            Console.Write($"数値を入力してください -> ");
            var number = Console.ReadLine();

            //ローカル関数は、このスコープ内でしか使用できない。
            bool IsOddNumber(int number)
            {
                return number % 2 != 0;
            }

            string str = IsOddNumber(Convert.ToInt32(number)) ? "奇数です" : "偶数です";
            Console.WriteLine($"{number}は、{str}");

        }
    }
}