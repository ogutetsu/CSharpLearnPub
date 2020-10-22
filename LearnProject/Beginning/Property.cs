using System;
using System.Collections.Generic;
using LearnLib;

namespace LearnProject.Beginning
{
    public class Property : ConsoleMenu
    {
        //autoプロパティ
        public int Number { get; set; }
        //autoプロパティの場合、コンパイラはバックアップフィールドを生成して以下のようなコードになる
        /*
        private int _number;
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }
         */

        private int _number2;
        public int Number2
        {
            get => _number2;
            set => _number2 = value;
        }

        //読み取り専用
        public int ReadProp { get; }
        public int ReadProp2 { get; private set; }
        public IEnumerable<string> list { get; } = new List<string>();
        
        //ラムダ式を使用したプロパティ
        public int Add => Number + Number2;
        
        public override void Run()
        {
            Console.WriteLine($"Beginning/Property.cs内にサンプルを記述");
        }
    }
}