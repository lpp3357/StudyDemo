using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01递归算法
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 3; i < 10; i++)
            {
                int a = GetNumber(i);
                Console.WriteLine(a.ToString());
            }
            Console.ReadLine();
        }

        /// <summary>
        /// 斐波那契数列   第N(N > 2)开始 等于(N - 1)+(N - 2)
        /// </summary>
        /// <param name="n">第几位数</param>
        /// <returns></returns>
        static int GetNumber(int n)
        {
            if (n <= 0) return 0;
            if (n == 1) return 1;
            return GetNumber(n - 1) + GetNumber(n - 2);
        }

        /// <summary>
        /// 阶乘 
        /// </summary>
        /// <param name="n">第几位数</param>
        /// <returns></returns>
        static int GetNumber_T(int n)
        {
            int sum = 0;
            if (n == 0)
            {
                return 1;
            }
            else
            {
                sum = n * GetNumber_T(n - 1);
            }
            return sum;
        }
    }
}
