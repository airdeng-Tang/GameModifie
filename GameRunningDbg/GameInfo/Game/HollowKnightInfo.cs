using GameModifier.GameInfo.Model.HK;
using GameModifier.GameInfo.Model.MHW;
using GameRunningDbg.GameInfo.Base;
using GameRunningDbg.GameInfo.Model;
using GameRunningDbg.GameInfo.Model.Base;
using GameRunningDbg.Manager;
using GameRunningDbg.Manager.MHW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameRunningDbg.GameInfo.Game
{
    public class HollowKnightInfo : GameBase
    {
        public HKPlayer Player
        {
            get { return (HKPlayer)player; }
            set { player = value; }
        }
        public HollowKnightInfo()
        {
            need_update_objects = new List<NeedUpdate>();
            Player = new HKPlayer(this);
        }

        public override void game_update()
        {
            foreach (var v in need_update_objects)
            {
                v.Update();
            }
        }

        public override void init_info()
        {
            Gold golds = new Gold((int[])DataManager.Instance.offsets[ProcessModel.Instance.name]["GoldsMemoryOffset"]);

            golds.CoinModule = 
                ModuleManager.Instance.modules[(string)DataManager.Instance.offsets[ProcessModel.Instance.name]["GoldCoinModule"]];

            golds.InitValue(ProcessModel.Instance.exe_p);
            Player.Golds = golds;
        }

        public override void ShowHelp()
        {
            Console.Write("set gold  ->  修改金币\n");
        }
    }
}
