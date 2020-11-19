using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
    public class ThreadReaderWriterLockSlim : ConsoleMenu
    {
        static ReaderWriterLockSlim _readerWriter = new ReaderWriterLockSlim();
        static Dictionary<int, int> _items = new Dictionary<int, int>();

        static void Read()
        {
            Console.WriteLine($"Reading Dic");
            while (true)
            {
                try
                {
                    _readerWriter.EnterReadLock();
                    foreach (var VARIAkeyBLE in _items.Keys)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                    }

                }
                finally
                {
                    _readerWriter.ExitReadLock();
                }
            }
        }

        static void Write(string threadName)
        {
            while (true)
            {
                try
                {
                    int newKey = new Random().Next(250);
                    _readerWriter.EnterUpgradeableReadLock();
                    if (!_items.ContainsKey(newKey))
                    {
                        try
                        {
                            _readerWriter.EnterWriteLock();
                            _items[newKey] = 1;
                            Console.WriteLine($"New key {newKey} by {threadName}");
                        }
                        finally
                        {
                            _readerWriter.ExitWriteLock();
                        }
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }
                finally
                {
                    _readerWriter.ExitUpgradeableReadLock();
                }
            }
        }
        
        public override void Run()
        {
            new Thread(Read){ IsBackground = true}.Start();
            new Thread(Read){ IsBackground = true}.Start();
            new Thread(Read){ IsBackground = true}.Start();
            
            new Thread(() => Write("Threa 1")) { IsBackground = true}.Start();
            new Thread(() => Write("Threa 2")) { IsBackground = true}.Start();

            Thread.Sleep(TimeSpan.FromSeconds(30));
        }
    }
}