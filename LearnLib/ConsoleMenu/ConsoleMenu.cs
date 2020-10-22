using System;

namespace LearnLib
{
    public abstract class ConsoleMenu
    {
        public ConsoleMenu()
        {
            Name = this.GetType().Name;
        }

        public string Name { get; set; }

        public abstract void Run();
        public virtual void TestRun() { Console.WriteLine("Testは実装されていません"); }
        public virtual void TestGen() { Console.WriteLine("TestGenが実装されていないためテスト生成できませんでした。");}

    }
}