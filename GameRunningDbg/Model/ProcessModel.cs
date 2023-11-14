using GameRunningDbg.Core;
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

namespace GameRunningDbg.Model
{
    public class ProcessModel : Singleton<ProcessModel>
    {
        public string name;
        public Process module;
        bool isRunning = false;

        /// <summary>
        /// 主程序基址
        /// </summary>
        public IntPtr exe_p;

        //UnityPlayer.dll模块基址
        ProcessModule UnityPlayerDll;


        public IntPtr UnityPlayerDll_p;
        /// <summary>
        /// 金币地址
        /// </summary>
        public IntPtr PlayerGolds_p;

        public Gold player_gold;
        public void Init()
        {
            isRunning = true;
        }

        public void SetPlayer(ProcessModule pm , IntPtr jb)
        {
            if (pm == null)
            {
                return;
            }
            this.exe_p = jb;
            UnityPlayerDll = pm;

            UnityPlayerDll_p = UnityPlayerDll.BaseAddress;

            player_gold = new Gold(this);

            player_gold.get_golds(this.exe_p);
        }

        /// <summary>
        /// 根据泛型生成byte数组
        /// </summary>
        /// <returns></returns>
        public static byte[] GenericToByteArray<T>()
        {
            return new byte[Marshal.SizeOf<T>()];
        }
        //public process() { 
            
        //}
    }
}
