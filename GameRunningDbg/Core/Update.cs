using GameRunningDbg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.Core
{
    public class Update
    {
        List<Thread> threads;

        public Update()
        {
            threads = new List<Thread>();
            this.Init();
        }

        public void Init()
        {
            //threads.Add(CreateThread(Core_Update));
            threads.Add(CreateThread(update_player_info));
            Start();
            Core_Update();
        }

        public void Start()
        {
            foreach (var thread in threads)
            {
                thread.Start();
            }
        }
        
        public Thread CreateThread(Action action)
        {
            return new Thread(new ThreadStart(action));
        }

        public void update_player_info()
        {
            while(true)
            {
                Thread.Sleep(100);
                ProcessModel.Instance.player_gold.update_golds();
            }
        }

        /// <summary>
        /// 输入
        /// </summary>
        public void Core_Update()
        {
            while (true)
            {
                string i = Console.ReadLine();
                InputProc(i);
                //Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// 输入处理
        /// </summary>
        /// <param name="i"></param>
        void InputProc(string i)
        {
            if (i == "set golds")
            {
                Console.WriteLine("请输入想要更改的金币");
                int.TryParse(Console.ReadLine() , out int new_golds);
                if (ProcessModel.Instance.player_gold.set_golds(new_golds))
                {
                    Console.WriteLine($"修改成功");
                    return;
                }
                else
                {
                    Console.WriteLine($"修改失败");
                    return;
                }
            }
        }
    }
}
