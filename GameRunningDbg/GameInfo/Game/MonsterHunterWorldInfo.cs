﻿using GameModifier.GameInfo.Model.Base;
using GameModifier.GameInfo.Model.MHW;
using GameRunningDbg.GameInfo.Base;
using GameRunningDbg.GameInfo.Model;
using GameRunningDbg.GameInfo.Model.Base;
using GameRunningDbg.GameInfo.Model.MHW;
using GameRunningDbg.JSON;
using GameRunningDbg.Manager;
using GameRunningDbg.Manager.MHW;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo.Game
{
    public class MonsterHunterWorldInfo : GameBase
    {
        public MHWPlayer Player
        {
            get { return (MHWPlayer)player; }
            set { player = value; }
        }

        public MonsterHunterWorldInfo() 
        {
            need_update_objects = new List<NeedUpdate>();
            Player = new MHWPlayer(this);
        }

        public override void game_update()
        {
            foreach(var v in need_update_objects)
            {
                v.Update();
            }
        }

        public override void init_info()
        {
            Player.Golds = 
                new Gold(((JArray)DataManager.Instance.offsets[ProcessModel.Instance.name]["GoldsMemoryOffset"]).Select(x => (int)x).ToArray());
            Player.Golds.CoinModule =
                ModuleManager.Instance.modules[(string)DataManager.Instance.offsets[ProcessModel.Instance.name]["GoldCoinModule"]];
            Player.Golds.InitValue(ProcessModel.Instance.exe_p);

            Player.Pts =
                new Pts(((JArray)DataManager.Instance.offsets[ProcessModel.Instance.name]["PtsMemoryOffset"]).Select(x => (int)x).ToArray());
            Player.Pts.CoinModule =
                ModuleManager.Instance.modules[(string)DataManager.Instance.offsets[ProcessModel.Instance.name]["PtsCoinModule"]];
            Player.Pts.InitValue(ProcessModel.Instance.exe_p);

            Player.HR =
                new HrLevel(((JArray)DataManager.Instance.offsets[ProcessModel.Instance.name]["HrMemoryOffset"]).Select(x => (int)x).ToArray());
            Player.HR.CoinModule =
                ModuleManager.Instance.modules[(string)DataManager.Instance.offsets[ProcessModel.Instance.name]["HrCoinModule"]];
            Player.HR.InitValue(ProcessModel.Instance.exe_p);

            Player.MR =
                new MrLevel(((JArray)DataManager.Instance.offsets[ProcessModel.Instance.name]["MrMemoryOffset"]).Select(x => (int)x).ToArray());
            Player.MR.CoinModule =
                ModuleManager.Instance.modules[(string)DataManager.Instance.offsets[ProcessModel.Instance.name]["MrCoinModule"]];
            Player.MR.InitValue(ProcessModel.Instance.exe_p);

            Player.Bag = new Bag(IntPtr.Add(Player.Golds.p,
                (int)Bag.FirstItemBagOffsetOfGold));
            Player.Bag.ShowAll();
        }

        public override void ShowHelp()
        {
            Console.Write("set golds  ->  修改金币\nadd item  ->  向道具箱中添加道具\nset pts  ->  修改调查点\nshow player info  ->  显示玩家信息\nshow item  ->  展示道具箱中全部道具\nupdate item  -> 更新道具箱中数据\nshow bullet  ->  展示全部子弹\nshow mats  ->  展示全部素材\nshow decors  ->  展示全部饰品\nshow all  ->  展示全部饰品\n");
        }
    }
}
