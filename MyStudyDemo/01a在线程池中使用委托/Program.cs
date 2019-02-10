using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using static System.Threading.Thread;

namespace _01a在线程池中使用委托
{
    /// <summary>
    /// 该例重点是使用 BeginInvoke 来使用委托 ，使用 BeginInvoke之后必须使用EndInvoke
    /// </summary>
    class Program
    {
        //定义委托
        private delegate string RunOneThreadPool(out int threadId);
        static void Main(string[] args)
        {
            int threadID = 0;
            //创建线程
            var t = new Thread(() => Test(out threadID));
            t.Start();
            t.Join();
            WriteLine($"线程Id：{threadID}");
            //实例化委托
            RunOneThreadPool poolDelegate = Test;
            //执行委托
            IAsyncResult r = poolDelegate.BeginInvoke(out threadID, Callback, "一个异步回调---001");
            r.AsyncWaitHandle.WaitOne();
            string result = poolDelegate.EndInvoke(out threadID, r);
            WriteLine($"线程池工作线程Id：{threadID}");
            WriteLine(result);
            ReadKey();
        }

        /// <summary>
        /// 定义回调方法
        /// </summary>
        /// <param name="ar"></param>
        static void Callback(IAsyncResult ar)
        {
            WriteLine("开始执行回调");
            WriteLine($"回调状态：{ar.AsyncState}");
            WriteLine($"是否线程池线程：{CurrentThread.IsThreadPoolThread}");
            WriteLine($"工作线程池ID:{CurrentThread.ManagedThreadId}");
        }

        static string Test(out int threadId)
        {
            WriteLine("开始。。。");
            WriteLine($"是否线程池线程：{CurrentThread.IsThreadPoolThread}");
            //线程休眠
            Sleep(TimeSpan.FromSeconds(2));
            threadId = CurrentThread.ManagedThreadId;
            return $"工作线程池ID是：{CurrentThread.ManagedThreadId}";
        }

    }
}
