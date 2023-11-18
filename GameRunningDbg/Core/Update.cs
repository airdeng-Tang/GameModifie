using GameRunningDbg.GameInfo.Game;
using GameRunningDbg.GameInfo.Model;
using GameRunningDbg.GameInfo.Model.MHW;
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
                Thread.Sleep(2000);
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
            if(i == "help")
            {
                ShowHelp();
                return;
            }
            if(ProcessModel.Instance.game == GAME.MONSTERHUNTERWORLD)
            {
                if (i == "set golds")
                {
                    Console.WriteLine("请输入想要更改的金币 :");
                    int.TryParse(Console.ReadLine(), out int new_value);
                    if (((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Player.Golds.SetValue(new_value))
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
                    if (((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Player.Pts.SetValue(new_value))
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
                else if(i == "add item")
                {
                    Console.WriteLine("请输入想要增加的道具id:");
                    int.TryParse(Console.ReadLine(), out int id);
                    Console.WriteLine("请输入想要增加的道具数量:");
                    int.TryParse(Console.ReadLine(), out int value);
                    ((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Player.Bag.TryAddItem(id, value);
                }
                else if( i == "update item")
                {
                    ((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Player.Bag.UpdateInfo();
                }
                else if(i == "show item")
                {
                    ((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Player.Bag.ShowItems();
                }
                else if(i == "show bullet")
                {
                    ((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Player.Bag.ShowBullet();
                }
                else if(i == "show mats")
                {
                    ((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Player.Bag.ShowMats();
                }
                else if(i == "show decors")
                {
                    ((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Player.Bag.ShowDecors();
                }
                else if(i == "show player info")
                {
                    ((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Player.ShowInfo();
                }
                else if(i == "show all")
                {
                    ((MonsterHunterWorldInfo)ProcessModel.Instance.game_info).Player.Bag.ShowAll();
                }
                else
                {
                    Console.Write("无效命令\n");
                }
            }
            else if (ProcessModel.Instance.game == GAME.HOLLOWKNIGHT)
            {
                if (i == "set golds")
                {
                    Console.WriteLine("请输入想要更改的金币 :");
                    int.TryParse(Console.ReadLine(), out int new_value);
                    if (((HollowKnightInfo)ProcessModel.Instance.game_info).Player.Golds.SetValue(new_value))
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

        private void ShowHelp()
        {
            //switch(ProcessModel.Instance.game)
            //{
            //    case GAME.HOLLOWKNIGHT:
            //        HollowKnightInfo.ShowHelp();
            //        break;
            //    case GAME.MONSTERHUNTERWORLD:
            //        MonsterHunterWorldInfo.ShowHelp();
            //        break;
            //    default:
            //        Console.WriteLine("请先确认游戏");
            //        break;
            //}
            if(ProcessModel.Instance.game_info != null)
            {
                ProcessModel.Instance.game_info.ShowHelp();
            }
            else
            {
                Console.WriteLine("请先确认游戏");
            }
        }
    }
}
