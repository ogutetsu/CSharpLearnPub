using System;
using LearnLib;

namespace LearnProject.Beginning
{
    public class Ifelse : ConsoleMenu
    {
        public override void Run()
        {
            //簡単なif-else文
            bool check = true;
            if (check)
            {
                Console.WriteLine("Trueの場合");
            }
            else
            {
                Console.WriteLine("Falseの場合");
            }
            
            //入力した文字が小文字か大文字か
            Console.Write($"何か1文字(英字)入力してください -> ");
            char c = (char)Console.Read();
            Console.ReadLine();    //Read直後にReadLineでEnterの入力を挟んでいます。
            if (char.IsLetter(c))
            {
                Console.WriteLine($"入力された文字は英字です");
                if(char.IsLower(c))
                    Console.WriteLine($"入力された文字は小文字です");
                else
                    Console.WriteLine($"入力された文字は大文字です");
            }
            else
            {
                Console.WriteLine($"入力された文字は英字ではありません。");
            }
            
            Console.Write($"何か1文字(英数字)入力してください -> ");
            c = (char)Console.Read();
            Console.ReadLine();
            if (char.IsUpper(c))
            {
                Console.WriteLine($"入力された文字は大文字です");
            }
            else if (char.IsLower(c))
            {
                Console.WriteLine($"入力された文字は小文字です");
            }
            else if (char.IsDigit(c))
            {
                Console.WriteLine($"入力された文字は数値です");
            }
            else
            {
                Console.WriteLine($"入力された文字は英数字ではありません。");
            }
            
            

        }
    }
}