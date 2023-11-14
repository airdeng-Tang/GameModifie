using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.Core
{
    public class ECHelper
    {
        /// <summary>
        /// 根据PID打开进程并返回句柄
        /// </summary>
        /// <param name="desiredAccess"> 所需访问权限 (最高权限为 0x001F0FFF) </param>
        /// <param name="heritHandle"> 是否继承句柄 </param>
        /// <param name="pocessID"> PID </param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int desiredAccess, bool heritHandle, int pocessID);//访问权限（16进制），是否继承句柄，进程ID
        //关闭句柄
        [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
        public static extern void CloseHandle(IntPtr hObject);
        //读取内存
        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr baseadress, IntPtr buffer, int nsize, IntPtr bytesread);
        //写入内存
        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr baseadress, long[] buffer, int nSize, IntPtr byteswrite);

        /// <summary>
        /// 根据进程名获得PID
        /// </summary>
        /// <param name="name"> 进程名 </param>
        /// <returns></returns>
        public static int GetPIDByProcessName(string name)
        {
            Process[] pros = Process.GetProcessesByName(name);
            if (pros.Count() > 0)
            {
                return pros[0].Id;
            }
            else
            {
                return 0;
            }
        }


        public static int ReadMemoryValue(string name, IntPtr baseadress)
        {
            try
            {
                byte[] buffer = new byte[4];
                IntPtr bufferadress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                IntPtr hprocess = OpenProcess(0x1F0FFF, false, ECHelper.GetPIDByProcessName(name));
                ReadProcessMemory(hprocess, baseadress, bufferadress, 4, IntPtr.Zero);
                CloseHandle(hprocess);
                return Marshal.ReadInt32(bufferadress);
            }
            catch
            {
                return 0;
            }
        }


        public static void WriteMemoryValue(string name, IntPtr baseadress, long value)
        {
            IntPtr hprocess = OpenProcess(0x1F0FFF, false, ECHelper.GetPIDByProcessName(name));
            WriteProcessMemory(hprocess, baseadress, new long[] { value }, 4, IntPtr.Zero);
            CloseHandle(hprocess);
        }
    }
}
