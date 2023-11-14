
using GameRunningDbg.Core;
using GameRunningDbg.Model;
using HunterPie.Core.System.Windows.Native;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;

public class Program
{

    private static void Main()
    {
        _ = new Program();
        //string game_name = "MonsterHunterWorld";
        string game_name = "hollow_knight";
        // See https://aka.ms/new-console-template for more information
        int PID = ECHelper.GetPIDByProcessName(game_name);
        
        Console.WriteLine(PID);
        ProcessModel.Instance.Init();
        Console.WriteLine(ProcessModel.Instance.name);
        ProcessModule player_module = null;
        if (PID != 0 )
        {
            IntPtr jb = Kernel32.OpenProcess((int)Kernel32.PROCESS_ALL_ACCESS, true, PID);
            Process game_process = Process.GetProcessById(PID);
            if (game_process != null)
            {
                Console.WriteLine($"窗口句柄地址 {game_process.MainWindowHandle}\n" +
                    $"主句柄 :: {jb}");
                if (game_process.MainModule != null)
                {
                    Console.WriteLine(game_process.MainModule.ToString());
                    Console.WriteLine($"主模块内存地址 :: {game_process.MainModule.BaseAddress}");
                }

                //for(int i  = 0; i < game_process.Modules.Count; i++)
                //{
                //    Console.WriteLine($"主模块内存地址 :: {game_process.Modules[i].ModuleName}");
                //}
                Console.WriteLine("===========");
                foreach(ProcessModule module in game_process.Modules)
                {
                    Console.WriteLine($"模块名 :: {module.ModuleName}  ;  " +
                        $"模块内存地址 :: {Convert.ToString(module.BaseAddress.ToInt64(), 16)}  ;  " +
                        $"10进制 :: {module.BaseAddress}");
                    if (module.ModuleName == "UnityPlayer.dll")
                    {
                        player_module = module;   
                    }
                }
                Console.WriteLine("===========");

                Console.WriteLine($"进程 \"{game_name}\" 打开句柄数 {game_process.HandleCount}");

                Console.WriteLine(game_process.MainWindowHandle);

                ProcessModel.Instance.SetPlayer(player_module, jb);

                Update update = new Update();
            }
        }
        
        //获取句柄
        //IntPtr ptr =  ECHelper.OpenProcess(0x1F0FFF, true, PID);
        //IntPtr hprocess = ECHelper.OpenProcess(0x1F0FFF, false, ECHelper.GetPIDByProcessName(game_name));
        //Console.WriteLine($"{ptr.ToString()}  ++++++++  {hprocess.ToString()}");
    }
}