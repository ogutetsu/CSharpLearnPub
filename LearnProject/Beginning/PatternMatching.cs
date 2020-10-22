using System;
using System.Collections.Generic;
using System.Text;
using LearnLib;

namespace LearnProject.Beginning
{
    public class PatternMatching : ConsoleMenu
    {
        public override void Run()
        {
            Console.WriteLine(Matching(null));
            Console.WriteLine(Matching('a'));
            Console.WriteLine(Matching($"abcd"));

            
            IEnumerable<object> clist = new object[]{'a','b', "abc", new object[]{'e', "abcd"}, 10, null};
            Console.WriteLine($"=== PatternUsingSwitch ===");
            Console.WriteLine(PatternUsingSwitch(clist));

        }


        public string Matching(object character)
        {
            if (character is null) return $"{nameof(character)}はnull";
            if (character is char) return $"{nameof(character)} はchar型です";
            //string型であった場合、str変数に代入されます
            if (character is string str)
            {
                string res = str + $" {nameof(character)} は文字列です";
                return res;
            }
            
            return $"どのパターンにもマッチングしませんでした。";
        }

        public string PatternUsingSwitch(IEnumerable<object> inputs)
        {
            var res = new StringBuilder();
            foreach (var obj in inputs)
            {
                switch (obj)
                {
                    case char c:
                        res.AppendLine($"{c}はchar型です");
                        break;
                    //when句を使って、特殊なケースも指定可能
                    case string str when str.Contains('a'):
                        res.AppendLine($"{str}には a があります。");
                        break;
                    case IEnumerable<object> list:
                        foreach (var l in list)
                        {
                            res.AppendLine(Matching(l));
                        }
                        break;
                    case null:
                        res.AppendLine($"case文にnullも指定できます。");
                        break;
                    //varはdefaultに近い挙動
                    case var v:
                        res.AppendLine($"{v.GetType().Name}は該当なし");
                        break;
                }
                
            }

            return res.ToString();
        }
        
        
    }
}