using GameRunningDbg.GameInfo.Game;
using GameRunningDbg.GameInfo.Model;
using GameRunningDbg.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.Core
{
    public class Update : Singleton<Update>
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
                Thread.Sleep(1000);
                ProcessModel.Instance.game_info.game_update();
            }
        }

        /// <summary>
        /// 输入
        /// </summary>
        public void Core_Update()
        {
            while (true)
            {
                Console.Write(">");
                string i = Console.ReadLine();
                InputProc(i);
            }
        }
        /// <summary>
        /// 输入处理
        /// </summary>
        /// <param name="i"></param>
        void InputProc(string i)
        {
            if(ProcessModel.Instance.name == "MonsterHunterWorld")
            {
                if (i == "set golds")
                {
                    Console.WriteLine("请输入想要更改的金币 :");
                    int.TryParse(Console.ReadLine(), out int new_value);
                    if (((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Golds.SetValue(new_value))
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
                else if(i == "set pts")
                {
                    Console.WriteLine("请输入想要更改的调查点 :");
                    int.TryParse(Console.ReadLine(), out int new_value);
                    if (((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Pts.SetValue(new_value))
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
            else if (ProcessModel.Instance.name == "hollow_knight")
            {
                if (i == "set golds")
                {
                    Console.WriteLine("请输入想要更改的金币 :");
                    int.TryParse(Console.ReadLine(), out int new_value);
                    if (ProcessModel.Instance.game_info.Golds.SetValue(new_value))
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
}
