using System;
using LearnLib;

namespace LearnProject.Beginning
{
    public class Unsafe : ConsoleMenu
    {
        //このコードを実行する煮は、unsafeコードの許可が必要です。
        public override void Run()
        {
            //Unsafeなコードは、unsafeブロック内に実装する必要があります。
            unsafe
            {
                int a = 10;
                int b = 20;
                Swap(&a, &b);

                Console.WriteLine($"num1 : {a}  num2 : {b}");
            }
        }

        
        private unsafe void Swap(int* num1, int* num2)
        {
            int temp = *num1;
            *num1 = *num2;
            *num2 = temp;
        }
        
    }
}