using MassTransit.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskFactory fac = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(5));


            TaskFactory _customerTaskFactory = new TaskFactory(new TaskSchedulerBase<CustomerScheduler>());

            //Task.Delay(TimeSpan.FromSeconds(10)).Wait();

            //Console.ReadKey();

            _customerTaskFactory.StartNew(a => 
            {
                Console.WriteLine($"task1 executed start,thread id :{Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(50);
                Console.WriteLine($"task1 executed end,thread id :{Thread.CurrentThread.ManagedThreadId}");
            }, "A");
            Thread.Sleep(50);
            _customerTaskFactory.StartNew(a => 
            {
                Console.WriteLine($"task2 executed start,thread id :{Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(50);
                Console.WriteLine($"task2 executed end,thread id :{Thread.CurrentThread.ManagedThreadId}");
            }, "A");
            Thread.Sleep(50); 
            _customerTaskFactory.StartNew(a => 
            {
                Console.WriteLine($"task3 executed start,thread id :{Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(50);
                Console.WriteLine($"task3 executed end,thread id :{Thread.CurrentThread.ManagedThreadId}");
            }, "A");
            //Thread.Sleep(50);
            //_customerTaskFactory.StartNew(a => 
            //{
            //    Console.WriteLine($"task4 executed start,thread id :{Thread.CurrentThread.ManagedThreadId}");
            //    Thread.Sleep(50);
            //    Console.WriteLine($"task4 executed end,thread id :{Thread.CurrentThread.ManagedThreadId}");
            //}, "B");

            Console.ReadKey();
        }

        //static void Main(string[] args)
        //{

        //    Task<Int32> t = new Task<Int32>(i => Sum((Int32)i), 10000);

        //    //可以现在开始，也可以以后开始

        //    t.Start();

        //    Task cwt = t.ContinueWith(task => Console.WriteLine("The sum is:{0}", task.Result));
        //    cwt.Wait();

        //}

        //private static Int32 Sum(Int32 i)
        //{
        //    Int32 sum = 0;
        //    for (; i > 0; i--)
        //    {
        //        checked { sum += i; }
        //    }

        //    return sum;
        //}

    //static void Main(string[] args)
    //{
    //    CancellationTokenSource cts = new CancellationTokenSource();


    //    Task<Int32> t = new Task<Int32>(() => Sum(cts.Token, 10000), cts.Token);

    //    //可以现在开始，也可以以后开始 

    //    t.Start();

    //    //在之后的某个时间，取消CancellationTokenSource 以取消Task

    //    cts.Cancel();//这是个异步请求，Task可能已经完成了。我是双核机器，Task没有完成过


    //    //注释这个为了测试抛出的异常
    //    //Console.WriteLine("This sum is:" + t.Result);
    //    try
    //    {
    //        //如果任务已经取消了，Result会抛出AggregateException

    //        Console.WriteLine("This sum is:" + t.Result);
    //    }
    //    catch (AggregateException x)
    //    {
    //        //将任何OperationCanceledException对象都视为已处理。
    //        //其他任何异常都造成抛出一个AggregateException，其中
    //        //只包含未处理的异常

    //        x.Handle(e => e is OperationCanceledException);
    //        Console.WriteLine("Sum was Canceled");
    //    }

    //}

    //private static Int32 Sum(CancellationToken ct, Int32 i)
    //{
    //    Int32 sum = 0;
    //    for (; i > 0; i--)
    //    {
    //        //在取消标志引用的CancellationTokenSource上如果调用
    //        //Cancel，下面这一行就会抛出OperationCanceledException

    //        ct.ThrowIfCancellationRequested();

    //        checked { sum += i; }
    //    }

    //    return sum;
    //}

    //static void Main(string[] args)
    //{
    //    TaskCreationOptions
    //    Console.WriteLine("主线程启动");
    //    //ThreadPool.QueueUserWorkItem(StartCode,5);
    //    new Task(StartCode, 5).Start();
    //    Console.WriteLine("主线程运行到此！");
    //    Thread.Sleep(1000);
    //}

    //private static void StartCode(object i)
    //{
    //    Console.WriteLine("开始执行子线程...{0}", i);
    //    Thread.Sleep(1000);//模拟代码操作
    //}


    //static void Main(string[] args)
    //{

    //    //1000000000这个数字会抛出System.AggregateException

    //    Task<Int32> t = new Task<Int32>(n => Sum((Int32)n), 1000000000);

    //    //可以现在开始，也可以以后开始

    //    t.Start();

    //    //Wait显式的等待一个线程完成

    //    t.Wait();

    //    Console.WriteLine("The Sum is:" + t.Result);
    //}

    //private static Int32 Sum(Int32 i)
    //{
    //    Int32 sum = 0;
    //    for (; i > 0; i--)
    //        checked { sum += i; }
    //    return sum;
    //}
}
}
