using GameRunningDbg.Tool;
using HunterPie.Core.System.Windows.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.Model
{
    public class Gold
    {
        public static int p0 = 0x019B8BE0;
        public static int p1 = 0x10;
        public static int p2 = 0x88;
        public static int p3 = 0x28;
        public static int p4 = 0x20;
        public static int p5 = 0xA0;
        public static int p6 = 0x30;
        public static int p7 = 0x1C4;

        /// <summary>
        /// 金币地址
        /// </summary>
        public IntPtr p;

        //public bool isRunning = false;
        public int golds = -1;
        public int golds_new = 0;
        /// <summary>
        /// 进程管理
        /// </summary>
        public ProcessModel ProcessModel { get; set; }

        public Gold(ProcessModel processModel)
        {
            this.ProcessModel=processModel;
        }

        public Gold get_golds(IntPtr jb)
        {
            IntPtr a = IntPtr.Add(ProcessModel.UnityPlayerDll_p, p0);

            // 根据内存地址访问数据
            byte[] pbPtr = ProcessModel.GenericToByteArray<Int64>();
            Kernel32.ReadProcessMemory(jb, a, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
            a = IntPtr.Add((IntPtr)BitConverter.ToInt64(pbPtr), p1);
            Kernel32.ReadProcessMemory(jb, a, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
            a = IntPtr.Add((IntPtr)BitConverter.ToInt64(pbPtr), p2);
            Kernel32.ReadProcessMemory(jb, a, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
            a = IntPtr.Add((IntPtr)BitConverter.ToInt64(pbPtr), p3);
            Kernel32.ReadProcessMemory(jb, a, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
            a = IntPtr.Add((IntPtr)BitConverter.ToInt64(pbPtr), p4);
            Kernel32.ReadProcessMemory(jb, a, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
            a = IntPtr.Add((IntPtr)BitConverter.ToInt64(pbPtr), p5);
            Kernel32.ReadProcessMemory(jb, a, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
            a = IntPtr.Add((IntPtr)BitConverter.ToInt64(pbPtr), p6);
            Kernel32.ReadProcessMemory(jb, a, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
            a = IntPtr.Add((IntPtr)BitConverter.ToInt64(pbPtr), p7);
            p = a;
            update_golds();
            return this;
        }

        public void update_golds()
        {
            byte[] pb32 = ProcessModel.GenericToByteArray<Int32>();
            if (Kernel32.ReadProcessMemory(ProcessModel.exe_p, p, pb32, sizeof(Int32), out int i))
            {
                ProcessModel.PlayerGolds_p = p;
                golds_new = BitConverter.ToInt32(pb32);
                if(golds_new != golds)
                {
                    golds = golds_new;
                    Console.WriteLine($"金币 :: {golds}");
                }
            }
            else
            {
                Console.WriteLine("金币未成功读取");
            }
        }

        public bool set_golds(int golds)
        {
            byte[] pb = BitConverter.GetBytes(golds);
            return Kernel32.WriteProcessMemory(ProcessModel.exe_p,p, pb, sizeof(int), out int _);
        }
    }
}
