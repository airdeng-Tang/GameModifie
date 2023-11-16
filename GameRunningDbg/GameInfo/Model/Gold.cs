using GameRunningDbg.GameInfo.Model.Base;
using GameRunningDbg.Tool;
using HunterPie.Core.System.Windows.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo.Model
{
    public class Gold : MemoryBase, NeedUpdate, InitValue<Gold>
    {
        public int Value = -1;
        public int Value_New = 0;
        public Gold(int[] offset) : base(offset)
        {

        }

        public Gold InitValue(IntPtr jb)
        {
            IntPtr a = IntPtr.Add(CoinModule_p, offsets[0]);

            // 根据内存地址访问数据
            byte[] pbPtr = ProcessModel.GenericToByteArray<long>();
            for (int i = 1; i < offsets.Count; i++)
            {
                Kernel32.ReadProcessMemory(jb, a, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
                a = IntPtr.Add((IntPtr)BitConverter.ToInt64(pbPtr), offsets[i]);
            }
            p = a;
            Update();
            return this;
        }

        public void Update()
        {
            byte[] pb32 = ProcessModel.GenericToByteArray<int>();
            if (Kernel32.ReadProcessMemory(ProcessModel.Instance.exe_p, p, pb32, sizeof(int), out int i))
            {
                PlayerGolds_p = p;
                Value_New = BitConverter.ToInt32(pb32);
                if (Value_New != Value)
                {
                    Value = Value_New;
                    Console.WriteLine($"金币 :: {Value}");
                }
            }
            else
            {
                Console.WriteLine("金币未成功读取");
            }
        }

        public bool SetValue(int value)
        {
            byte[] pb = BitConverter.GetBytes(value);
            return Kernel32.WriteProcessMemory(ProcessModel.Instance.exe_p, p, pb, sizeof(int), out int _);
        }
    }
}
