using System;
using System.Collections;
using LearnLib;

namespace LearnProject.Beginning
{
    public class Collection : ConsoleMenu
    {
        public override void Run()
        {
            //非ジェネリック型
            
            Console.WriteLine($"  ArrayList  ");
            
            ArrayList arrayList = new ArrayList();
            ArrayList arrayList1 = new ArrayList {Capacity = 10};

            int count = 10;
            var rand = new Random(count);
            for (int i = 0; i < count; i++)
            {
                arrayList.Add(rand.Next(count));
            }

            Console.WriteLine($"Capacity : {arrayList.Capacity}");
            Console.WriteLine($"Count : {arrayList.Count}");
            Console.Write($"ArrayListの一覧 : ");
            PrintArrayList(arrayList);

            arrayList.Reverse();
            Console.Write($"ArrayList.Reverse : ");
            PrintArrayList(arrayList);
            
            arrayList.Sort();
            Console.Write($"ArrayList.Sort : ");
            PrintArrayList(arrayList);
            
            
            //Hashtable
            Console.WriteLine($"  Hashtable  ");
            Hashtable hashtable = new Hashtable
            {
                {1, "aaa bbb"},
                {2, "ccc"},
                {3, "abcd"},
                {4, "sample"},
                {5, "test"}
            };
            foreach (var key in hashtable.Keys)
            {
                Console.WriteLine($"Key : {key}   Value : {hashtable[key]}");
            }

            //SortedList
            Console.WriteLine($"  SortedList  ");
            SortedList sortedList = new SortedList
            {
                {1, "aaa bbb"},
                {2, "ccc"},
                {3, "abcd"},
                {4, "sample"},
                {5, "test"}
            };
            sortedList.Add(10, "add");
            Console.WriteLine($"Capacity : {sortedList.Capacity}");

            foreach (var key in sortedList.Keys)
            {
                Console.WriteLine($"Key : {key}  Value : {sortedList[key]}");
            }
            
            //Stack
            Console.WriteLine($"  Stack  ");
            Stack stack = new Stack();
            stack.Push($"first");
            stack.Push($"Second");
            stack.Push($"Third");
            stack.Push($"4th");
            Console.WriteLine($"Count : {stack.Count}");
            stack.Push($"5th");
            Console.WriteLine($"Count : {stack.Count}");
            Console.WriteLine($"Stack.Peek : {stack.Peek()}");
            foreach (var value in stack)
            {
                Console.WriteLine($"{value}");
            }

            stack.Pop();
            Console.WriteLine($"Stack.Pop : {stack.Peek()}");

            //Queue
            Console.WriteLine($"  Queue  ");
            Queue queue = new Queue();
            queue.Enqueue($"first");
            queue.Enqueue($"second");
            queue.Enqueue($"third");
            queue.Enqueue($"4th");
            Console.WriteLine($"Cound : {queue.Count}");
            queue.Enqueue($"5th");
            Console.WriteLine($"Cound : {queue.Count}");
            foreach (var value in queue)
            {
                Console.WriteLine($"{value}");
            }
            Console.WriteLine($"Queue.Dequeue : {queue.Dequeue()}");
            
            

        }

        private void PrintArrayList(ArrayList list)
        {
            foreach (var num in list)
            {
                Console.Write($" {num}");
            }
            Console.WriteLine();
        }
        
        
    }
}