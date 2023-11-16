using GameRunningDbg.JSON.Define.MHW;
using GameRunningDbg.Manager.MHW;
using GameRunningDbg.Tool;
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
        Dictionary<int, Item> MatmBag = new Dictionary<int, Item>();
        /// <summary>
        /// 饰品道具格
        /// </summary>
        Dictionary<int, Item> DecorBag = new Dictionary<int, Item>();


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
            BulletFirst = IntPtr.Add(ItemFirst, ItemBagSize);
            MatFirst = IntPtr.Add(ItemFirst, BulletBagSize);
            DecorFirst = IntPtr.Add(ItemFirst, MatBagSize);
            End = IntPtr.Add(DecorFirst, DecorBagSize) - 0x10;
            AddItemBag(firstPtr);
        }

        public void AddAllBag(Item item)
        {
            ItemCount++;
            item.Key = ItemCount;
            AllItem.Add(ItemCount, item);
            Console.WriteLine($"Key : {ItemCount}  ::  " +
                $"道具id : {item.ItemId}  ::  " +
                $"Id地址 : {Convert.ToString(item.IdMemory.ToInt64(), 16)}  ::  " +
                $"道具名 : {item.name}  ::  " +
                $"道具数量 : {item.Value}  ::  " +
                $"道具地址 : {Convert.ToString(item.ValueMemory.ToInt64(), 16)}  ::  " +
                $"道具类型 : {item.define?.Target}");
        }

        public void AddItemBag(IntPtr itemPtr)
        {
            Item item = new Item();
            item.InitValue(ProcessModel.Instance.exe_p, ref itemPtr);
            AddAllBag(item);
            if (!ItemBag.ContainsKey(item.ItemId))
            {
                ItemBag.Add(item.ItemId, item);
            }
            ItemBagSize -= 0x10;
            if (ItemBagSize > 0)
            {
                AddItemBag(itemPtr);
            }
            else
            {
                AddBulletBag(itemPtr);
            }
        }

        public void AddBulletBag(IntPtr itemPtr)
        {
            Item item = new Item();
            item.InitValue(ProcessModel.Instance.exe_p, ref itemPtr);
            AddAllBag(item);
            if (!BulletBag.ContainsKey(item.ItemId))
            {
                BulletBag.Add(item.ItemId, item);
            }
            
            BulletBagSize -= 0x10;
            if (BulletBagSize > 0)
            {
                AddBulletBag(itemPtr);
            }
            else
            {
                AddMatmBag(itemPtr);
            }
        }

        public void AddMatmBag(IntPtr itemPtr)
        {
            Item item = new Item();
            item.InitValue(ProcessModel.Instance.exe_p, ref itemPtr);
            AddAllBag(item);
            if (!MatmBag.ContainsKey(item.ItemId))
            {
                MatmBag.Add(item.ItemId, item);
            }
            MatBagSize -= 0x10;
            if (MatBagSize > 0)
            {
                AddMatmBag(itemPtr);
            }
            else
            {
                AddDecorBag(itemPtr);
            }
        }

        public void AddDecorBag(IntPtr itemPtr)
        {
            Item item = new Item();
            item.InitValue(ProcessModel.Instance.exe_p, ref itemPtr);
            AddAllBag(item);
            if (!DecorBag.ContainsKey(item.ItemId))
            {
                DecorBag.Add(item.ItemId, item);
            }
            DecorBagSize -= 0x10;
            if (DecorBagSize > 0)
            {
                AddDecorBag(itemPtr);
            }
            else
            {
                return;
            }
        }

        private Item GetNextNullItemInAllItem()
        {
            return null;
        }

        public bool AddItem(int id, int value)
        {
            //DataManager.Instance.itemDefine[id].Target;
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
    }
}
