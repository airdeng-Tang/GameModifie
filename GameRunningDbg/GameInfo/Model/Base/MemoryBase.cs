using HunterPie.Core.System.Windows.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo.Model.Base
{
    /// <summary>
    /// 内存信息基类
    /// </summary>
    public class MemoryBase<T> : NeedUpdate, InitValue<MemoryBase<T>>
    {
        public string Name { get; set; }
        /// <summary>
        /// 内存偏移量列表
        /// </summary>
        public List<int> offsets;

        /// <summary>
        /// 内存地址
        /// </summary>
        public IntPtr p;

        private ProcessModule coinModule;
        /// <summary>
        /// 相关模块 
        /// </summary>
        public ProcessModule CoinModule
        {
            get { return coinModule; }
            set
            {
                coinModule = value;
                CoinModule_p = coinModule.BaseAddress;
            }
        }

        /// <summary>
        /// 相关模块基址
        /// </summary>
        public IntPtr CoinModule_p;


        public int Value = -1;
        public int Value_New = 0;

        public MemoryBase(int[] offset)
        {
            if(offset != null)
            {
                offsets = new List<int>(offset);
            }
            else
            {
                offsets = new List<int>();
            }
        }


        public MemoryBase<T> InitValue(IntPtr jb)
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
                Value_New = BitConverter.ToInt32(pb32);
                if (Value_New != Value)
                {
                    Value = Value_New;
                    Console.WriteLine($"{Name} :: {Value}");
                }
            }
            else
            {
                Console.WriteLine($"{Name} 未成功读取");
            }
        }

        public bool SetValue(int value)
        {
            byte[] pb = BitConverter.GetBytes(value);
            return Kernel32.WriteProcessMemory(ProcessModel.Instance.exe_p, p, pb, sizeof(int), out int _);
        }
    }
}
