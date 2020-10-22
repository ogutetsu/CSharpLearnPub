using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using LearnLib;

namespace LearnProject.Beginning
{
    public class Reflection : ConsoleMenu
    {
        public override void Run()
        {
            Console.Write($"数値を入力してください -> ");
            var num = Console.ReadLine();
            //クラスのインスタンスを作成
            Object instance = Activator.CreateInstance(typeof(RefSample));
            //StringNumberメソッドを取得
            MethodInfo method = typeof(RefSample).GetMethod("StringNumber");
            //RefSampple.StringNumberメソッドの呼び出し
            object res = method.Invoke(instance, new object?[] {Convert.ToInt32(num)});
            Console.WriteLine($"入力された数値は {res} です");
            
            Console.WriteLine($"==================");
            var type = typeof(Reflection);
            
            //以下でも同等の内容
            //var type = Type.GetType("LearnProject.Beginning.Reflection");
            
            Console.WriteLine($"Assembly:{type.AssemblyQualifiedName}");
            Console.WriteLine($"Name:{type.Name}");
            Console.WriteLine($"Full Name:{type.FullName}");
            Console.WriteLine($"Namespace:{type.Namespace}");
            
            Console.WriteLine($"==================");
            
        }
    }

    public class RefSample
    {
        public string StringNumber(int number) => number.ToString();

        public string StringName(string name)
        {
            return name;
        }
    }
    
    
}