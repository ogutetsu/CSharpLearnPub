using System;
using System.Collections.Generic;

namespace LearnLib
{
    public class Exit : ConsoleMenu
    {
        public Exit() : base()
        {
        }

        public override void Run()
        {
            
        }
    }
    public class Back : ConsoleMenu
    {
        public Back() : base()
        {
        }
        public override void Run()
        {
        }
    }
    
    
    public class ConsoleMenuSelector : ConsoleMenu
    {
        public enum Selector
        {
            Submenu,
            Runner
        };
        
        
        private Dictionary<int, ConsoleMenu> dic;
        private int exitNumber = 99;
        private Selector _selector;
        
        public int ExitNumber
        {
            get => exitNumber;
            set => exitNumber = value;
        }

        public ConsoleMenuSelector(string name, Selector selector) : base()
        {
            dic = new Dictionary<int, ConsoleMenu>();
            base.Name = name;
            _selector = selector;
        }
        
        public void AddMenu(int num, ConsoleMenu menu)
        {
            dic.Add(num, menu);
        }

        public void SetExit()
        {
            AddMenu(exitNumber, new Exit());
        }
        public void SetBack()
        {
            AddMenu(exitNumber, new Back());
        }
        
        public override void Run()
        {
            while (true)
            {
                foreach (var menu in dic)
                {
                    Console.WriteLine(menu.Key + " : " + menu.Value.Name);
                }

                Console.Write("Select number -> ");
                var keyInfo = Console.ReadLine();
                if (keyInfo.ToString() == exitNumber.ToString()) break; //ExitNumberが入力されたら終了

                int selectNumber = int.Parse(keyInfo.ToString());

                if (dic.ContainsKey(selectNumber))
                {
                    int option = 0;
                    if (_selector == Selector.Runner)
                    {
                        Console.Write("0...Run 1...Test 9...TestGen -> ");
                        keyInfo = Console.ReadLine();
                        option = int.Parse(keyInfo.ToString());
                    }

                    var menuClass = dic[selectNumber];

                    Console.WriteLine("select : " + menuClass.Name);
                    switch (option)
                    {
                        case 0:
                            menuClass.Run();
                            break;
                        case 1:
                            menuClass.TestRun();
                            break;
                        case 9:
                            menuClass.TestGen();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("存在しないSelectNumberです");
                }
            }
        }
        
        
        
    }
}