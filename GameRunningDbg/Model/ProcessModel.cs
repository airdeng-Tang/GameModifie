using GameRunningDbg.Core;
using GameRunningDbg.GameInfo;
using GameRunningDbg.GameInfo.Base;
using GameRunningDbg.JSON;
using GameRunningDbg.Manager;
using GameRunningDbg.Tool;
using HunterPie.Core.System.Windows.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static GameRunningDbg.JSON.infoSim;

namespace GameRunningDbg.Model
{
    public class ProcessModel : Singleton<ProcessModel>
    {
        public string name = "";
        public Process module;
        bool isRunning = false;

        public Dictionary<string, Dictionary<string, object>> JsonInfo 
            = new Dictionary<string, Dictionary<string, object>>();

        /// <summary>
        /// 主程序基址
        /// </summary>
        public IntPtr exe_p;

        /// <summary>
        /// 游戏信息
        /// </summary>
        public GameBase game_info;
        public void Init()
        {
            isRunning = true;

            JsonInfo.Add("hollow_knight", new Dictionary<string, object>());
            JsonInfo["hollow_knight"].Add("GoldCoinModule", "UnityPlayer.dll");
            int[] GoldsMemoryOffset = { 0x019B8BE0, 0x10, 0x88, 0x28, 0x20, 0xA0, 0x30, 0x1C4 };
            JsonInfo["hollow_knight"].Add("GoldsMemoryOffset", GoldsMemoryOffset);


            JsonInfo.Add("MonsterHunterWorld", new Dictionary<string, object>());
            JsonInfo["MonsterHunterWorld"].Add("GoldCoinModule", "MonsterHunterWorld.exe");
            int[] GoldsMemoryOffset2 = { 0x05011710, 0xA8, 0x94 };
            JsonInfo["MonsterHunterWorld"].Add("GoldsMemoryOffset", GoldsMemoryOffset2);
            JsonInfo["MonsterHunterWorld"].Add("PtsCoinModule", "MonsterHunterWorld.exe");
            int[] PtsMemoryOffset = { 0x05011710, 0xA8, 0x98 };
            JsonInfo["MonsterHunterWorld"].Add("PtsMemoryOffset", PtsMemoryOffset);
        }

        public void SetPlayer(IntPtr jb)
        {
            this.exe_p = jb;
            game_info.init_info();
        }

        /// <summary>
        /// 根据泛型生成byte数组
        /// </summary>
        /// <returns></returns>
        public static byte[] GenericToByteArray<T>()
        {
            return new byte[Marshal.SizeOf<T>()];
        }
    }
}
