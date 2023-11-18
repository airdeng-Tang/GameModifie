using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDemo
{
    /// <summary>
    /// 进度条
    /// </summary>
    public class ProgressBar
    {
        public bool isEnd = false;


        public int x;
        public int y;

        private int new_x;
        private int new_y;

        int c = 1;

        string Head;

        public ProgressBar(string head, Dictionary<string, ProgressBar> bars = null)
        {
            this.Head = head;
            bars?.Add(head, this);
        }
        /// <summary>
        /// 初始化进度条
        /// </summary>
        /// <returns></returns>
        public void Init()
        {
            Console.Write($"{Head}: ");
            Console.Write("[");
            // 获取当前光标位置
            this.x = Console.CursorLeft;
            this.y = Console.CursorTop;
            this.new_x = x;
            this.new_y = y;
            for (int i = 0; i < 100; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine("]");

            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
        }

        /// <summary>
        /// 更新进度条
        /// </summary>
        /// <param name="x">光标列位置</param>
        /// <param name="y">光标行位置</param>
        /// <param name="i">增加的进度(百分制)</param>
        public void UpdateBar(int x, int y, int i = 1)
        {
            while (i>0)
            {
                AddProgress();
                i--;
            }
            Console.SetCursorPosition(x, y);
        }

        public void AddProgress()
        {
            if (!isEnd)
            {
                Console.SetCursorPosition(new_x+c-1, new_y);
                c++;
                Console.Write('#');
                for (int i = 100; i >= c; i--)
                {
                    Console.Write('*');
                }
                Console.WriteLine("]");
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                if (c>100)
                {
                    isEnd = true;
                }
            }
        }
    }
}
