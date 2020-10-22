using System;
using System.Collections.Generic;
using LearnLib;

namespace LearnProject.Beginning
{
    public class Generic : ConsoleMenu
    {
        public override void Run()
        {
            List<string> nameList = new List<string>();
            nameList.Add($"abcd");
            //nameList.Add(11);  ← ビルドエラー 文字列を指定しているので数値を追加出来ない
            
            GenericConstraint<string> stringConstraint = new GenericConstraint<string>();
            //GenericConstraintはクラスに制限しているため値型は使用できない。
            //GenericConstraint<int> intConstraint = new GenericConstraint<int>();
            
            
            //デフォルトのコンストラクタは制限されているのでビルドエラー
            //DefaultConstractor<string> defaultConstractor = new DefaultConstractor<string>();
            
            
            
        }
        
    }

    //whereをつけることで、特定のデータ型のみに使用を制限することが出来る
    //classだとクラス型  structだと値型に制限できる また特定のクラス名も可
    public class GenericConstraint<T> where T : class
    {
        public T GetValue(T value) => value;
    }

    //new()を使用するとデフォルトコンストラクタを制限出来ます。
    public class DefaultConstractor<T> where T : new()
    {
    }


    //インターフェース制約
    public class InterfaceConstraint<T>:IDisposable where T : IDisposable
    {
        public void Dispose()
        {
        }
    }
    
    
}