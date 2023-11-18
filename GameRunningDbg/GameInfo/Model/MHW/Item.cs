using GameRunningDbg.GameInfo.Model.Base;
using GameRunningDbg.JSON.Define.MHW;
using GameRunningDbg.Manager.MHW;
using HunterPie.Core.System.Windows.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo.Model.MHW
{
    public class Item :  NeedUpdate, InitValue<Item>
    {
        /// <summary>
        /// 在道具箱中格子对应的key值
        /// </summary>
        public int Key = 0;

        /// <summary>
        /// 道具id
        /// </summary>
        public int ItemId = 0;
        /// <summary>
        /// id地址
        /// </summary>
        public IntPtr IdMemory = IntPtr.Zero;

        /// <summary>
        /// 道具数量
        /// </summary>
        public int Value = 0;
        /// <summary>
        /// 数量地址
        /// </summary>
        public IntPtr ValueMemory = IntPtr.Zero;

        /// <summary>
        /// 内存地址
        /// </summary>
        public IntPtr p;

        /// <summary>
        /// 道具名称
        /// </summary>
        public string? name;

        public ItemDefine define;

        public Item(int[] offset = null) 
        {

        }

        public Item(Item i)
        {
            Key = i.Key;
            define = i.define;
            ItemId = i.ItemId;
            IdMemory = i.IdMemory;
            Value = i.Value;
            ValueMemory = i.ValueMemory;
            p = i.p;
        }

        public Item InitValue(IntPtr jz)
        {
            return this;
        }

        public Item InitValue(IntPtr jz, ref IntPtr next)
        {
            IdMemory = next;

            byte[] pbPtr = ProcessModel.GenericToByteArray<int>();
            Kernel32.ReadProcessMemory(jz, IdMemory, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
            ItemId = BitConverter.ToInt32(pbPtr);
            define = DataManager.Instance.itemDefine[ItemId];
            name = define.Name;
            ValueMemory = IntPtr.Add(IdMemory, 4);
            Kernel32.ReadProcessMemory(jz, ValueMemory, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
            Value = BitConverter.ToInt32(pbPtr);
            next = IntPtr.Add(ValueMemory, 12);
            return this;
        }

        public bool SetId(int id)
        {
            byte[] pb = BitConverter.GetBytes(id);
            return Kernel32.WriteProcessMemory(ProcessModel.Instance.exe_p, IdMemory, pb, sizeof(int), out int _);
        }

        public bool SetValue(int value)
        {
            byte[] pb = BitConverter.GetBytes(value);
            return Kernel32.WriteProcessMemory(ProcessModel.Instance.exe_p, ValueMemory, pb, sizeof(int), out int _);
        }

        public bool SetItem(int id,int value)
        {
            int thisId = ItemId;
            if (SetId(id))
            {
                ItemId = id;
                if(SetValue(value))
                {
                    Value = value;
                    ShowThisItem();
                    return true;
                }
                else
                {
                    SetId(thisId);
                    ItemId = thisId;
                }
            }
            return false;
        }

        public void Update()
        {
            byte[] pbPtr = ProcessModel.GenericToByteArray<int>();
            Kernel32.ReadProcessMemory(ProcessModel.Instance.exe_p, IdMemory, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
            ItemId = BitConverter.ToInt32(pbPtr);
            define = DataManager.Instance.itemDefine[ItemId];
            name = define.Name;
            ValueMemory = IntPtr.Add(IdMemory, 4);
            Kernel32.ReadProcessMemory(ProcessModel.Instance.exe_p, ValueMemory, pbPtr, Marshal.SizeOf<IntPtr>(), out int _);
            Value = BitConverter.ToInt32(pbPtr);
        }

        public void ShowThisItem()
        {
            Console.Write($"Key : {Key}  ::  道具id : {ItemId}  ::  Id地址 : {Convert.ToString(IdMemory.ToInt64(), 16)}  ::  道具名 : {name}  ::  道具数量 : {Value} \n");
        }
    }
}
