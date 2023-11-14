using System;
using System.Runtime.InteropServices;
using System.Text;

namespace HunterPie.Core.System.Windows.Native;

internal static class Kernel32
{

    /// <summary>
    /// 最高权限,允许对进程的所有资源进行访问。
    /// </summary>
    public const uint PROCESS_ALL_ACCESS = 0x001F0FFF;
    /// <summary>
    /// 线程的全权限访问标志，允许对线程的所有资源进行访问。
    /// </summary>
    public const uint THREAD_ALL_ACCESS = 0x1F0FFF;
    /// <summary>
    /// 内存提交标志，表示分配的内存页将提交到物理内存中。
    /// </summary>
    public const uint MEM_COMMIT = 0x1000;
    /// <summary>
    /// 页面执行读写权限，允许对分配的内存页进行读写操作。
    /// </summary>
    public const uint PAGE_EXECUTE_READWRITE = 4;
    /// <summary>
    /// 内存保留标志，表示在分配的内存页中预留一部分空间，以便后续使用。
    /// </summary>
    public const uint MEM_RESERVE = 0x2000;
    /// <summary>
    /// 页面读/写权限，允许对分配的内存页进行读写操作。
    /// </summary>
    public const uint PAGE_READWRITE = 4;
    /// <summary>
    /// 页面执行读权限，允许对分配的内存页进行读操作。
    /// </summary>
    public const uint PAGE_EXECUTE_READ = 4;
    /// <summary>
    /// 内存释放标志，表示释放分配的内存页。
    /// </summary>
    public const uint MEM_RELEASE = 0x8000;
    /// <summary>
    /// 页面只读权限，允许对分配的内存页进行读操作，不允许写操作。
    /// </summary>
    public const uint PAGE_READONLY = 4;

    [Flags]
    public enum AllocationType : uint
    {
        Commit = 0x1000,
        Reserve = 0x2000,
        Decommit = 0x4000,
        Release = 0x8000,
        Reset = 0x80000,
        Physical = 0x400000,
        TopDown = 0x100000,
        WriteWatch = 0x200000,
        LargePages = 0x20000000
    }

    [Flags]
    public enum MemoryProtection : uint
    {
        Execute = 0x10,
        ExecuteRead = 0x20,
        ExecuteReadWrite = 0x40,
        ExecuteWriteCopy = 0x80,
        NoAccess = 0x01,
        ReadOnly = 0x02,
        ReadWrite = 0x04,
        WriteCopy = 0x08,
        GuardModifierflag = 0x100,
        NoCacheModifierflag = 0x200,
        WriteCombineModifierflag = 0x400
    }

    /// <summary>
    /// 根据PID打开进程并返回句柄
    /// </summary>
    /// <param name="dwDesiredAccess"> 所需访问权限 (最高权限为 0x001F0FFF) </param>
    /// <param name="bInheritHandle"> 是否继承句柄 </param>
    /// <param name="dwProcessId"> PID </param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr OpenProcess(
        int dwDesiredAccess,
        bool bInheritHandle,
        int dwProcessId
    );

    /// <summary>
    /// 根据PID打开进程并返回句柄
    /// </summary>
    /// <param name="processAccess">所需访问权限 (最高权限为 0x001F0FFF)</param>
    /// <param name="bInheritHandle">是否继承句柄</param>
    /// <param name="processId">指定要打开的进程的ID</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr OpenProcess(
        uint processAccess,
        bool bInheritHandle,
        int processId);

    /// <summary>
    /// 读取指定进程的内存数据
    /// </summary>
    /// <param name="hProcess">要读取内存的进程的句柄</param>
    /// <param name="lpBaseAddress">要读取的内存区域的起始地址</param>
    /// <param name="lpBuffer">用于存储读取到的数据的字节数组</param>
    /// <param name="dwSize">要读取的字节数</param>
    /// <param name="lpNumberOfBytesRead">实际读取到的字节数，是一个输出参数</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        int dwSize,
        out int lpNumberOfBytesRead
    );

    /// <summary>
    /// 读取指定进程的内存数据
    /// </summary>
    /// <param name="hProcess">要读取内存的进程的句柄</param>
    /// <param name="lpBaseAddress">要读取的内存区域的起始地址</param>
    /// <param name="lpBuffer">用于存储读取到的数据的对象。使用[Out, MarshalAs(UnmanagedType.AsAny)]特性将对象转换为非托管类型，以便与非托管代码进行交互。</param>
    /// <param name="dwSize">要读取的字节数</param>
    /// <param name="lpNumberOfBytesRead">实际读取到的字节数，是一个输出参数</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        [Out, MarshalAs(UnmanagedType.AsAny)] object lpBuffer,
        int dwSize,
        out int lpNumberOfBytesRead
    );

    /// <summary>
    /// 读取指定进程的内存数据
    /// </summary>
    /// <param name="hProcess">要读取内存的进程的句柄</param>
    /// <param name="lpBaseAddress">要读取的内存区域的起始地址</param>
    /// <param name="lpBuffer">用于存储读取到的数据的指针</param>
    /// <param name="dwSize">要读取的字节数</param>
    /// <param name="lpNumberOfBytesRead">实际读取到的字节数，是一个输出参数</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        IntPtr lpBuffer,
        int dwSize,
        out int lpNumberOfBytesRead
    );

    /// <summary>
    /// 想指定进程的内存中写入数据
    /// </summary>
    /// <param name="hProcess">要写入数据的进程的句柄</param>
    /// <param name="lpBaseAddress">指定要写入数据的内存地址</param>
    /// <param name="lpBuffer">需要写入的数据的字节数组</param>
    /// <param name="nSize">指定要写入的数据大小</param>
    /// <param name="lpNumberOfBytesWritten">用于输出实际写入的字节数</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        int nSize,
        out int lpNumberOfBytesWritten
    );

    /// <summary>
    /// 在指定的进程中分配虚拟内存
    /// </summary>
    /// <param name="hProcess">指定要分配内存的进程的句柄</param>
    /// <param name="lpAddress">指定要分配内存的起始地址</param>
    /// <param name="dwSize">指定要分配的内存大小</param>
    /// <param name="flNewProtect">指定内存分配类型</param>
    /// <param name="lpflOldProtect">指定内存保护标志</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool VirtualProtectEx(
        IntPtr hProcess,
        IntPtr lpAddress,
        UIntPtr dwSize,
        uint flNewProtect,
        out uint lpflOldProtect
    );
    /// <summary>
    /// 关闭句柄
    /// </summary>
    /// <param name="hObject"></param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool CloseHandle(IntPtr hObject);

    /// <summary>
    /// 在一个进程中创建一个新线程，并返回新线程的句柄
    /// </summary>
    /// <param name="hProcess">指定要在其中创建新线程的进程的句柄</param>
    /// <param name="lpThreadAttributes">指定新线程的属性</param>
    /// <param name="dwStackSize">定新线程的堆栈大小</param>
    /// <param name="lpStartAddress">定新线程的入口点地址</param>
    /// <param name="lpParameter">指定传递给新线程的参数的指针</param>
    /// <param name="dwCreateFlags">指定新线程的创建标志</param>
    /// <param name="lpThreadId">定新线程的标识符</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr CreateRemoteThread(
        IntPtr hProcess,
        IntPtr lpThreadAttributes,
        uint dwStackSize,
        IntPtr lpStartAddress,
        IntPtr lpParameter,
        uint dwCreateFlags,
        IntPtr lpThreadId
    );

    /// <summary>
    /// 指定的进程中分配虚拟内存
    /// </summary>
    /// <param name="hProcess">要分配内存的进程的句柄</param>
    /// <param name="lpAddress">要分配的内存区域的起始地址</param>
    /// <param name="dwSize">要分配的内存大小，以字节为单位</param>
    /// <param name="flAllocationType">内存分配类型</param>
    /// <param name="flProtect">内存保护标志</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr VirtualAllocEx(
        IntPtr hProcess,
        IntPtr lpAddress,
        uint dwSize,
        AllocationType flAllocationType,
        MemoryProtection flProtect
    );

    /// <summary>
    /// 通过名称获取指定模块的句柄
    /// </summary>
    /// <param name="lpModuleName">要获取句柄的模块的名称</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    /// <summary>
    /// 用于获取指定模块中指定函数的地址
    /// </summary>
    /// <param name="hModule">包含目标函数的模块的句柄</param>
    /// <param name="procName">要获取地址的函数的名称</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);


    /// <summary>
    /// 获取指定模块的文件名
    /// </summary>
    /// <param name="hProcess">获取文件名的进程的句柄</param>
    /// <param name="hModule">指定要获取文件名的模块的句柄</param>
    /// <param name="lpBaseName">用于存储获取到的文件名</param>
    /// <param name="nSize">指定缓冲区的大小</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern uint GetModuleFileNameExA(
        IntPtr hProcess,
        IntPtr hModule, 
        [Out] StringBuilder lpBaseName,
        uint nSize
        );

    //void test()
    //{
    //    // 打开游戏进程
    //    IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, 1234); // 1234是游戏进程的ID，需要替换为实际值

    //    // 获取游戏模块句柄
    //    StringBuilder moduleName = new StringBuilder(256);
    //    GetModuleFileNameExA(hProcess, IntPtr.Zero, moduleName, (uint)moduleName.Capacity);
    //    string gameModuleName = moduleName.ToString();

    //    // 获取游戏内存地址函数的指针
    //    IntPtr memoryAddressFunctionPointer = GetProcAddress(hProcess, "GetInventoryMemoryAddress");

    //    if (memoryAddressFunctionPointer == IntPtr.Zero)
    //    {
    //        Console.WriteLine("无法找到游戏内存地址函数");
    //        return;
    //    }

    //    // 在游戏进程中分配内存
    //    IntPtr inventoryMemoryAddress = VirtualAllocEx(hProcess, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(List<GameObject>)), MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);

    //    if (inventoryMemoryAddress == IntPtr.Zero)
    //    {
    //        Console.WriteLine("无法在游戏进程中分配内存");
    //        return;
    //    }

    //    // 将分配的内存地址转换为C# List对象
    //    List<GameObject> inventoryItems = Marshal.PtrToStructure<List<GameObject>>(inventoryMemoryAddress);

    //    // 调用游戏内存地址函数，获取背包道具的内存地址
    //    List<int> itemMemoryAddresses = new List<int>();
    //    UIntPtr bytesWritten;
    //    WriteProcessMemory(hProcess, inventoryMemoryAddress, BitConverter.GetBytes((byte)itemMemoryAddresses.Count), (uint)BitConverter.GetBytes((byte)itemMemoryAddresses.Count).Length, out bytesWritten);

    //    for (int i = 0; i < itemMemoryAddresses.Count; i++)
    //    {
    //        itemMemoryAddresses[i] = memoryAddressFunctionPointer.ToInt32();
    //    }

    //    // 释放分配的内存
    //    Marshal.FreeHGlobal(inventoryMemoryAddress);

    //    // 在游戏进程中创建远程线程，执行游戏内存地址函数
    //    IntPtr remoteThreadHandle = CreateRemoteThread(hProcess, IntPtr.Zero, 0, memoryAddressFunctionPointer, (uint)Marshal.SizeOf(typeof(List<GameObject>)), THREAD_ALL_ACCESS, IntPtr.Zero);

    //    if (remoteThreadHandle == IntPtr.Zero)
    //    {
    //        Console.WriteLine("无法在游戏进程中创建远程线程");
    //        return;
    //    }

    //    bool threadCreated = CloseHandle(remoteThreadHandle);
    //    if (!threadCreated)
    //    {
    //        Console.WriteLine("无法关闭远程线程句柄");
    //        return;
    //    }

    //    // 输出背包道具的内存地址
    //    foreach (int memoryAddress in itemMemoryAddresses)
    //    {
    //        Console.WriteLine("背包道具的内存地址： " + memoryAddress);
    //    }
    //}
}
