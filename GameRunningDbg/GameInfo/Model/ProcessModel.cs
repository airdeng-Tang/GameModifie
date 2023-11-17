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

namespace GameRunningDbg.GameInfo.Model
{
    /// <summary>
    /// 游戏模式
    /// </summary>
    public enum GAME
    {
        /// <summary>
        /// 无启用游戏模式
        /// </summary>
        None = 0,
        /// <summary>
        /// 怪猎世界
        /// </summary>
        MONSTERHUNTERWORLD,
        /// <summary>
        /// 空洞骑士
        /// </summary>
        HOLLOWKNIGHT,
    }


    public class ProcessModel : Singleton<ProcessModel>
    {
        public string name = "";

        public GAME game = GAME.None;

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
            JsonInfo["MonsterHunterWorld"].Add("HrCoinModule", "MonsterHunterWorld.exe");
            int[] HrMemoryOffset = { 0x05011710, 0xA8, 0x90 };
            JsonInfo["MonsterHunterWorld"].Add("HrMemoryOffset", HrMemoryOffset);
            JsonInfo["MonsterHunterWorld"].Add("HrExpCoinModule", "MonsterHunterWorld.exe");
            int[] HrExpMemoryOffset = { 0x05011710, 0xA8, 0x9C };
            JsonInfo["MonsterHunterWorld"].Add("HrExpMemoryOffset", HrExpMemoryOffset);
            JsonInfo["MonsterHunterWorld"].Add("MrCoinModule", "MonsterHunterWorld.exe");
            int[] MrMemoryOffset = { 0x05011710, 0xA8, 0xD4 };
            JsonInfo["MonsterHunterWorld"].Add("MrMemoryOffset", MrMemoryOffset);
            JsonInfo["MonsterHunterWorld"].Add("MrExpCoinModule", "MonsterHunterWorld.exe");
            int[] MrExpMemoryOffset = { 0x05011710, 0xA8, 0xD8 };
            JsonInfo["MonsterHunterWorld"].Add("MrExpMemoryOffset", MrExpMemoryOffset);
            JsonInfo["MonsterHunterWorld"].Add("TimeCoinModule", "MonsterHunterWorld.exe");
            int[] TimeMemoryOffset = { 0x05011710, 0xA8, 0xA0 };
            JsonInfo["MonsterHunterWorld"].Add("TimeMemoryOffset", TimeMemoryOffset);

            int FirstItemBagOffsetOfGold = 0x3897C;
            JsonInfo["MonsterHunterWorld"].Add("FirstItemBagOffsetOfGold", FirstItemBagOffsetOfGold);
        }

        public void SetPlayer(IntPtr jb)
        {
            exe_p = jb;
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
