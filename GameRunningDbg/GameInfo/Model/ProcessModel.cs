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
        public static readonly string ver = "0.1.15";

        public string name = "";

        public GAME game = GAME.None;

        public Process module;
        bool isRunning = false;

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
