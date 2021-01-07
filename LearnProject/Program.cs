using System;
using LearnLib;
using LearnProject.Beginning;
using LearnProject.Multithread;
using LearnProject.Multithread.Collection;
using LearnProject.Multithread.PLINQ;
using LearnProject.Multithread.Pool;
using LearnProject.Multithread.ReactiveExtensions;
using LearnProject.Multithread.TaskSample;
using LearnProject.Multithread.Version6;

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
            multithread.AddMenu(23, new ThreadPoolWaitHandleAndTimeout());
            multithread.AddMenu(24, new ThreadPoolTimer());
            multithread.AddMenu(25, new ThreadPoolBackGroundWorker());
            multithread.AddMenu(26, new TaskCreate());
            multithread.AddMenu(27, new TaskBasicOperation());
            multithread.AddMenu(28, new TaskCombine());
            multithread.AddMenu(29, new TaskAPMPattern());
            multithread.AddMenu(30, new TaskEAPPattern());
            multithread.AddMenu(31, new TaskCancelOption());
            multithread.AddMenu(32, new TaskException());
            multithread.AddMenu(33, new TaskParallel());
            multithread.AddMenu(34, new AwaitResult());
            multithread.AddMenu(35, new AwaitLambda());
            multithread.AddMenu(36, new AwaitAsync());
            multithread.AddMenu(37, new AwaitParallel());
            multithread.AddMenu(38, new AwaitException());
            multithread.AddMenu(39, new AsyncVoid());
            multithread.AddMenu(40, new AwaitCustomType());
            multithread.AddMenu(41, new ConcurrentDictionary());
            multithread.AddMenu(42, new ConcurrentQueue());
            multithread.AddMenu(43, new ConcurrentStack());
            multithread.AddMenu(44, new ConcurrentBag());
            multithread.AddMenu(45, new BlockingCollection());
            multithread.AddMenu(46, new UsingParallel());
            multithread.AddMenu(47, new ParallelLINQ());
            multithread.AddMenu(48, new ParametersPLINQ());
            multithread.AddMenu(49, new ExceptionsPLINQ());
            multithread.AddMenu(50, new DataPartitioningPLINQ());
            multithread.AddMenu(51, new CustomAggregatorPLINQ());
            multithread.AddMenu(52, new AsyncObservable());
            multithread.AddMenu(53, new CustomObservable());
            multithread.AddMenu(54, new SubjectTypeFamily());
            multithread.AddMenu(55, new ObservableObject());

            multithread.SetBack();

            
            //最後にトップメニューに追加する
            top.AddMenu(1, beginning);
            top.AddMenu(2, multithread);
            top.SetExit();

            top.Run();
        }
    }
}