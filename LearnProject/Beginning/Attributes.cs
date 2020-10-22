using System;
using LearnLib;

namespace LearnProject.Beginning
{

    
    public class InfoAttribute : Attribute
    {
        public InfoAttribute(string msg)
        {
            Msg = msg;
        }

        public string Msg;

    }
    
    
    [Info("InfoAttributeを呼び出しています")]
    public class Attributes : ConsoleMenu
    {
        public override void Run()
        {
            //WarnMsgはコンパイラが警告を出します
            WarnMsg();

            Print();

        }


        //メソッドを使用すると警告を出します。 [Obsolete(string, tru)]というように最後にtrueを入れると使用禁止になる
        [Obsolete("WarnMsgは非推奨です このテキストはAttributesサンプルです。")]
        void WarnMsg()
        {
            
        }

        void Print()
        {
            //Reflectionを使用して、InfoAttributeへアクセスしています。
            Type type = typeof(Attributes);
            Attribute[] attrs = Attribute.GetCustomAttributes(type);
            foreach (var attr in attrs)
            {
                InfoAttribute i = attr as InfoAttribute;
                Console.WriteLine(i.Msg);
            }
        }
        
    }
}