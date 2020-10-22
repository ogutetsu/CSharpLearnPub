using System;
using System.Reflection;
using LearnLib;

namespace LearnProject.Beginning
{
    public class SingleVariable : ConsoleMenu
    {
        public override void Run()
        {
            sbyte sb = -128;
            Write(typeof(sbyte), $" sbyte sb : {sb}");
            byte b = 255;
            Write(typeof(byte), $" byte b : {b}");
            short sh = -32768;
            Write(typeof(short), $" short sh : {sh}");
            ushort ush = 65535;
            Write(typeof(ushort), $" ushort ush : {ush}");
            int i = -2147483648;
            Write(typeof(int), $" int i : {i}");
            uint ui = 4294967295;
            Write(typeof(uint), $" int ui : {ui}");
            long l = -9_223_372_036_854_775_808;
            Write(typeof(long), $" long l : {l}");
            ulong ul = 18_446_744_073_709_551_615;
            Write(typeof(ulong), $" ulong ul : {ul}");
            float f = 1.23e-4f;
            Write(typeof(float), $" float f : {f}");
            double pi = 0.12345e-2;
            Write(typeof(double), $" double pi : {pi}");
            decimal d = 2.5m;
            Write(typeof(decimal), $" decimal d : {d}");

            char c = 'a';
            Console.WriteLine($"char UTF16  char c : {c}");
            
            bool check = true;
            Console.WriteLine($"boolはfalseかtrueのみ bool check : {check}");



        }

        public void Write(Type type, string value)
        {
            FieldInfo minInfo = type.GetField("MinValue");
            object minValue = minInfo.GetValue(null);
            FieldInfo maxInfo = type.GetField("MaxValue");
            object maxValue = maxInfo.GetValue(null);
            
            Console.WriteLine($"{type.Name} {minValue}～{maxValue} " +
                              value);
        }
        
        
    }
}