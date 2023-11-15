using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.JSON
{
    /// <summary>
    /// json信息模拟类
    /// </summary>
    public static class infoSim
    {
        
        public static class hollow_knight
        {
            public static string GoldCoinModule = "UnityPlayer.dll";
            public static int[] GoldsMemoryOffset = { 0x019B8BE0 , 0x10 , 0x88 , 0x28 , 0x20 , 0xA0 , 0x30 , 0x1C4 };
        }

        public static class MonsterHunterWorld
        {
            public static string GoldCoinModule = "MonsterHunterWorld.exe";
            public static int[] GoldsMemoryOffset = { 0x05011710, 0xA8, 0x94 };
        }
    }
}
