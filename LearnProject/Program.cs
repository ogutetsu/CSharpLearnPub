using System;
using LearnLib;
using LearnProject.Beginning;
using LearnProject.Multithread;
using LearnProject.Multithread.Pool;

namespace LearnProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleMenuSelector top = new ConsoleMenuSelector("Top", ConsoleMenuSelector.Selector.Submenu);
            
            //Beginningメニューの作成
            ConsoleMenuSelector beginning = new ConsoleMenuSelector("Beginning", ConsoleMenuSelector.Selector.Runner);
            beginning.AddMenu(1, new SingleVariable());
            beginning.AddMenu(2, new Calculation());
            beginning.AddMenu(3, new Ifelse());
            beginning.AddMenu(4, new TupleDeconstruction());
            beginning.AddMenu(5, new PatternMatching());
            beginning.AddMenu(6, new LocalFunction());
            beginning.AddMenu(7, new Literal());
            beginning.AddMenu(8, new DefaultExp());
            beginning.AddMenu(9, new Property());
            beginning.AddMenu(10, new FileIO());
            beginning.AddMenu(11, new Reflection());
            beginning.AddMenu(12, new DelegateEvent());
            beginning.AddMenu(13, new Collection());
            beginning.AddMenu(14, new Generic());
            beginning.AddMenu(15, new Attributes());
            beginning.AddMenu(16, new Unsafe());
            beginning.SetBack();
            
            //Multithread用メニュー
            ConsoleMenuSelector multithread = new ConsoleMenuSelector("Multithread", ConsoleMenuSelector.Selector.Runner);
            multithread.AddMenu(1, new ThreadPause());
            multithread.AddMenu(2, new ThreadAbort());
            multithread.AddMenu(3, new ThreadState());
            multithread.AddMenu(4, new ThreadPriority());
            multithread.AddMenu(5, new ThreadForegroundBackGround());
            multithread.AddMenu(6, new ThreadParameters());
            multithread.AddMenu(7, new ThreadLock());
            multithread.AddMenu(8, new ThreadMonitorLock());
            multithread.AddMenu(9, new ThreadException());
            multithread.AddMenu(10, new ThreadAtomic());
            multithread.AddMenu(11, new ThreadMutex());
            multithread.AddMenu(12, new ThreadSemaphoreSlim());
            multithread.AddMenu(13, new ThreadAutoResetEvent());
            multithread.AddMenu(14, new ThreadManualResetEventSlim());
            multithread.AddMenu(15, new ThreadCountDownEvent());
            multithread.AddMenu(16, new ThreadBarrier());
            multithread.AddMenu(17, new ThreadReaderWriterLockSlim());
            multithread.AddMenu(18, new ThreadSpinWait());
            multithread.AddMenu(19, new ThreadPoolDelegate());
            multithread.AddMenu(20, new ThreadPoolAsync());
            multithread.AddMenu(21, new ThreadPoolParallel());
            multithread.AddMenu(22, new ThreadPoolCancel());
            
            multithread.SetBack();

            
            //最後にトップメニューに追加する
            top.AddMenu(1, beginning);
            top.AddMenu(2, multithread);
            top.SetExit();

            top.Run();
        }
    }
}