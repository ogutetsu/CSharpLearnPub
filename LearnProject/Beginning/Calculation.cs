using System;
using LearnLib;

namespace LearnProject.Beginning
{
    public class Calculation : ConsoleMenu
    {
        public override void Run()
        {
            int add1 = 1;
            int add2 = 2;
            Console.WriteLine($"{add1} + {add2} = {add1+add2}" );
            int sub1 = 10;
            int sub2 = 2;
            Console.WriteLine($"{sub1} - {sub2} = {sub1-sub2}" );
            uint ui1 = 1;
            uint ui2 = 10;
            Console.WriteLine($"{ui1} - {ui2} = {ui1-ui2}   uintでマイナスになるとオーバーフローを起こします" );

            int mi1 = 10;
            int mi2 = 2;
            Console.WriteLine($"{mi1} * {mi2} = {mi1*mi2}" );
            Console.WriteLine($"{mi1} / {mi2} = {mi1/mi2}" );
            Console.WriteLine($"{mi1} % {mi2} = {mi1%mi2}" );
            
            int i = 1;
            Console.WriteLine($"{i}++ は後に評価される {i++} ");
            i = 1;
            Console.WriteLine($"++{i} は先に評価される {++i} ");


            float m1 = 10.5f;
            float m2 = 1.5f;
            Console.WriteLine($"{m1} * {m2} = {m1*m2}");

            float m3 = 1000000.0f;
            float m4 = 0.0001f;
            Console.WriteLine($"{m3} + {m4} = {m3+m4} 浮動小数点で極端に値が離れている値どうしで計算すると情報落ち(小さい方の情報が失われる)が発生します。");

            float m5 = 123.456f;
            float m6 = 123.444f;
            Console.WriteLine($"{m5} - {m6} = {m5-m6} 不動小数点で値が近いものどうしで計算すると誤差が発生する");
            
            Console.WriteLine($"{float.MaxValue} + {float.MaxValue} = {float.MaxValue+float.MaxValue} 浮動小数点ではオーバーフローはしないで特殊な値になる");
            
            Console.WriteLine($"0.0 / 0.0 = {0.0/0.0} 0除算でも例外はスローしない");
            
            //浮動小数で例外をスローしたい場合は、decimal型を使用します。
            

        }
    }
}