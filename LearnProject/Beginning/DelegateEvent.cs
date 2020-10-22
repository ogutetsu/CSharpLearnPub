using System;
using LearnLib;

namespace LearnProject.Beginning
{
    public class DelegateEvent : ConsoleMenu
    {
        
        //デリゲート型
        public delegate string PrintNumber(int number);
        private PrintNumber _printNumber;
        
        //イベント (イベントを宣言する前にデリゲート型が必要)
        //イベントは、デリゲートの仕組みを使って実行している
        //イベントとデリゲートの違いは、イベントの実行は宣言されているクラス内からしか呼び出せない
        //これは、派生したクラスからも呼び出させない
        private event PrintNumber _printNumEvent;
        
        
        public override void Run()
        {

            _printNumber = Function;
            Console.WriteLine($"{_printNumber(10)} こっちはdelegate");

            _printNumEvent = Function;
            
            Console.WriteLine($"{_printNumber(10)} こっちはevent");

        }

        private string Function(int number)
        {
            return number.ToString();
        }
        
    }
}