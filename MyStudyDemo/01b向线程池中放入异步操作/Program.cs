using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using static System.Threading.Thread;

namespace _01b向线程池中放入异步操作
{
    /// <summary>
    /// 该例重点是使用线程池来执行方法，即为 向线程池中放入异步操作，此时方法为异步执行
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //定义常量
            const int x = 1;
            const int y = 2;
            const string lambdaState = "lambda state 2";
            ThreadPool.QueueUserWorkItem(AsyncOperation);
            Sleep(TimeSpan.FromSeconds(1));

            ThreadPool.QueueUserWorkItem(AsyncOperation, "异步状态");
            Sleep(TimeSpan.FromSeconds(1));

            ThreadPool.QueueUserWorkItem(state =>
            {
                WriteLine($"操作状态：{state}");
                WriteLine($"工作线程Id：{CurrentThread.ManagedThreadId}");
                Sleep(TimeSpan.FromSeconds(2));
            }, "lambde state");

            //使用闭包机制
            ThreadPool.QueueUserWorkItem(_ =>
            {
                WriteLine($"操作状态：{x + y},{lambdaState}");
                WriteLine($"工作线程Id：{CurrentThread.ManagedThreadId}");
                Sleep(TimeSpan.FromSeconds(2));
            }, "lambde state");
            Sleep(TimeSpan.FromSeconds(2));
            ReadLine();
        }

        static void AsyncOperation(object state)
        {
            WriteLine($"操作状态：{state ?? "(NULL)"}");
            WriteLine($"工作线程Id：{CurrentThread.ManagedThreadId}");
            //线程休眠
            Sleep(TimeSpan.FromSeconds(2));
        }


    }
}
