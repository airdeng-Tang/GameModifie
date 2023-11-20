using GameRunningDbg.JSON.Define.MHW;
using GameRunningDbg.Manager.MHW;
using GameRunningDbg.Tool;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo.Model.MHW
{
    public class Bag
    {
        /// <summary>
        /// 道具箱全部格子
        /// </summary>
        Dictionary<int, Item> AllItem = new Dictionary<int, Item>();
        int ItemCount = 0;

        /// <summary>
        /// 消耗品道具格
        /// </summary>
        Dictionary<int, Item> ItemBag = new Dictionary<int, Item>();
        /// <summary>
        /// 子弹道具格
        /// </summary>
        Dictionary<int, Item> BulletBag = new Dictionary<int, Item>();
        /// <summary>
        /// 素材道具格
        /// </summary>
        Dictionary<int, Item> MatBag = new Dictionary<int, Item>();
        /// <summary>
        /// 饰品道具格
        /// </summary>
        Dictionary<int, Item> DecorBag = new Dictionary<int, Item>();

        /// <summary>
        /// 道具箱首格相对金币地址的内存偏移
        /// </summary>
        public static readonly int FirstItemBagOffsetOfGold = 0x3897C;
        /// <summary>
        /// 道具背包容量
        /// </summary>
        public int ItemBagSize = 0xC80;
        /// <summary>
        /// 道具格首格
        /// </summary>
        public IntPtr ItemFirst = IntPtr.Zero;

        /// <summary>
        /// 子弹背包容量
        /// </summary>
        public int BulletBagSize = 0xC80;
        /// <summary>
        /// 子弹首格
        /// </summary>
        public IntPtr BulletFirst = IntPtr.Zero;

        /// <summary>
        /// 素材背包容量
        /// </summary>
        public int MatBagSize = 0x4E20;
        /// <summary>
        /// 素材首格
        /// </summary>
        public IntPtr MatFirst = IntPtr.Zero;

        /// <summary>
        /// 饰品背包容量
        /// </summary>
        public int DecorBagSize = 0x1F30;
        /// <summary>
        /// 饰品首格
        /// </summary>
        public IntPtr DecorFirst = IntPtr.Zero;

        /// <summary>
        /// 最后一个格子
        /// </summary>
        public IntPtr End = IntPtr.Zero;
        public Bag(IntPtr firstPtr)
        {
            ItemFirst = firstPtr;
            Init();
        }

        private void Init()
        {
            AllItem = new Dictionary<int, Item>();
            ItemCount = 0;

            ItemBag = new Dictionary<int, Item>();
            BulletBag = new Dictionary<int, Item>();
            MatBag = new Dictionary<int, Item>();
            DecorBag = new Dictionary<int, Item>();

            ItemBagSize = 0xC80;
            BulletFirst = IntPtr.Add(ItemFirst, ItemBagSize);
            BulletBagSize = 0xC80;
            MatFirst = IntPtr.Add(BulletFirst, BulletBagSize);
            MatBagSize = 0x4E20;
            DecorFirst = IntPtr.Add(MatFirst, MatBagSize);
            DecorBagSize = 0x1F30;
            End = IntPtr.Add(DecorFirst, DecorBagSize) - 0x10;
            GetItemBag(ItemFirst);
        }

        public void GetAllBag(Item item)
        {
            ItemCount++;
            item.Key = ItemCount;
            AllItem.Add(ItemCount, item);
            //Console.Write($"Key : {ItemCount}  ::  道具id : {item.ItemId}  ::  Id地址 : {Convert.ToString(item.IdMemory.ToInt64(), 16)}  ::  道具名 : {item.name}  ::  道具数量 : {item.Value}  ::  道具地址 : {Convert.ToString(item.ValueMemory.ToInt64(), 16)}  ::  道具类型 : {item.define?.Target}\n");
        }

        public void GetItemBag(IntPtr itemPtr)
        {
            Item item = new Item();
            item.InitValue(ProcessModel.Instance.exe_p, ref itemPtr);
            GetAllBag(item);
            if (!ItemBag.ContainsKey(item.ItemId))
            {
                ItemBag.Add(item.ItemId, item);
            }
            ItemBagSize -= 0x10;
            if (ItemBagSize > 0)
            {
                GetItemBag(itemPtr);
            }
            else
            {
                GetBulletBag(itemPtr);
            }
        }

        public void GetBulletBag(IntPtr itemPtr)
        {
            Item item = new Item();
            item.InitValue(ProcessModel.Instance.exe_p, ref itemPtr);
            GetAllBag(item);
            if (!BulletBag.ContainsKey(item.ItemId))
            {
                BulletBag.Add(item.ItemId, item);
            }
            
            BulletBagSize -= 0x10;
            if (BulletBagSize > 0)
            {
                GetBulletBag(itemPtr);
            }
            else
            {
                GetMatmBag(itemPtr);
            }
        }

        public void GetMatmBag(IntPtr itemPtr)
        {
            Item item = new Item();
            item.InitValue(ProcessModel.Instance.exe_p, ref itemPtr);
            GetAllBag(item);
            if (!MatBag.ContainsKey(item.ItemId))
            {
                MatBag.Add(item.ItemId, item);
            }
            MatBagSize -= 0x10;
            if (MatBagSize > 0)
            {
                GetMatmBag(itemPtr);
            }
            else
            {
                GetDecorBag(itemPtr);
            }
        }

        public void GetDecorBag(IntPtr itemPtr)
        {
            Item item = new Item();
            item.InitValue(ProcessModel.Instance.exe_p, ref itemPtr);
            GetAllBag(item);
            if (!DecorBag.ContainsKey(item.ItemId))
            {
                DecorBag.Add(item.ItemId, item);
            }
            DecorBagSize -= 0x10;
            if (DecorBagSize > 0)
            {
                GetDecorBag(itemPtr);
            }
            else
            {
                return;
            }
        }

        private Item GetNextNullItemInAllItem(int key,ItemTarget target)
        {
            Int64 max = GetBagMax(target);
            if (max < 0)
            {
                Console.WriteLine("未知错误:: 未获取道具箱区域限制");
                return null;
            }
            while ((Int64)AllItem[key].IdMemory<max)
            {
                if (AllItem[key].ItemId==0)
                {
                    return AllItem[key];
                }
                key++;
            }
            Console.WriteLine("该物品分类的道具箱已满");
            return null;
        }

        private Int64 GetBagMax(ItemTarget target)
        {
            switch (target)
            {
                case ItemTarget.物品:
                    return (Int64)BulletFirst;
                case ItemTarget.弹药或瓶:
                    return (Int64)MatFirst;
                case ItemTarget.素材:
                    return (Int64)DecorFirst;
                case ItemTarget.装饰品:
                    return (Int64)End + 0x10;
            }
            return -1;
        }

        public bool TryAddItem(int id, int value)
        {
            UpdateInfo();
            Thread.Sleep(2);
            ItemDefine define;

            DataManager.Instance.itemDefine.TryGetValue(id, out define);
            if(define == null)
            {
                Console.WriteLine($"无此道具,请重新输入");
                return false;
            }
            Console.WriteLine($"想要添加的道具为: \n" +
                $"id : {define.Id}  ::  Name : {define.Name}  ::  数量 : {value}\n" +
                $"是否继续? [Y/n]");
            string s = Console.ReadLine().ToLower();
            if(s == "y")
            {
                
            }
            else if(s == "n") 
            {
                Console.WriteLine("已退出");
                return false;
            }
            else
            {
                Console.WriteLine("输入失败,已退出");
                return false;
            }

            switch (define.Target)
            {
                case ItemTarget.物品:
                    AddItem(ItemBag, define, value);
                    return true;
                case ItemTarget.弹药或瓶:
                    AddItem(BulletBag, define, value);
                    return true;
                case ItemTarget.素材:
                    AddItem(MatBag, define, value);
                    return true;
                case ItemTarget.装饰品:
                    AddItem(DecorBag, define, value);
                    return true;
                default:
                    Console.Write("无法添加, 物品类型不匹配");
                    break;
            }
            return false;
        }

        private bool AddItem(Dictionary<int,Item> bag, ItemDefine define, int value) 
        {
            if (bag.TryGetValue(define.Id, out Item item))
            {
                if (item.SetValue(item.Value+value))
                {
                    item.Value += value;
                }
                else
                {
                    item.SetValue(item.Value);
                }
                item.ShowThisItem();
                return true;
            }
            else
            {
                int key = bag[0].Key;
                if(bag[0].SetItem(define.Id, value))
                {
                    bag[bag[0].ItemId] = bag[0];
                    Item freeItem = GetNextNullItemInAllItem(key, define.Target);
                    if (freeItem == null)
                    {
                        return false;
                    }
                    bag[0] = freeItem;
                    return true;
                }
            }
            return false;
        }

        public bool SetItemValue(int key,int value)
        {
            AllItem.TryGetValue(key, out Item item);
            if (item == null)
            {
                Console.WriteLine($"修改失败, 未找到key值为 {key} 的物品格");
                return false;
            }
            if(item.ItemId == 0)
            {
                Console.WriteLine($"修改失败, 不可修改空白物品的数量");
                return false;
            }
            return item.SetValue(value);
        }

        public void ShowAll()
        {
            UpdateInfo();
            foreach (var v in AllItem)
            {
                v.Value.ShowThisItem();
            }
        }

        public void ShowItems()
        {
            UpdateInfo();
            foreach (var v in ItemBag)
            {
                if (v.Value.ItemId == 0)
                    break;
                v.Value.ShowThisItem();
            }
        }

        public void ShowBullet()
        {
            UpdateInfo();
            foreach (var v in BulletBag)
            {
                if (v.Value.ItemId == 0)
                    break;
                v.Value.ShowThisItem();
            }
        }

        public void ShowMats()
        {
            UpdateInfo();
            foreach (var v in MatBag)
            {
                if (v.Value.ItemId == 0)
                    break;
                v.Value.ShowThisItem();
            }
        }

        public void ShowDecors()
        {
            UpdateInfo();
            foreach (var v in DecorBag)
            {
                if (v.Value.ItemId == 0)
                    break;
                v.Value.ShowThisItem();
            }
        }


        public void UpdateInfo()
        {
            //foreach(var v in AllItem)
            //{
            //    v.Value.Update();
            //}
            //ShowAll();
            Init();
        }
    }
}
