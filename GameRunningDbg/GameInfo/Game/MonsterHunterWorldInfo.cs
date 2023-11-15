using GameRunningDbg.GameInfo.Base;
using GameRunningDbg.JSON;
using GameRunningDbg.Manager;
using GameRunningDbg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo.Game
{
    public class MonsterHunterWorldInfo : GameBase
    {
        private Pts pts;
        public Pts Pts { 
            get { return pts; }
            set
            {
                pts = value;
                need_update_objects.Add(value);
            }
        }
        public MonsterHunterWorldInfo() 
        {
            need_update_objects = new List<NeedUpdate>();
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
            Golds = 
                new Gold((int[])ProcessModel.Instance.JsonInfo[ProcessModel.Instance.name]["GoldsMemoryOffset"]);
            Golds.CoinModule =
                ModuleManager.Instance.modules[(string)ProcessModel.Instance.JsonInfo[ProcessModel.Instance.name]["GoldCoinModule"]];
            Golds.InitValue(ProcessModel.Instance.exe_p);

            Pts =
                new Pts((int[])ProcessModel.Instance.JsonInfo[ProcessModel.Instance.name]["PtsMemoryOffset"]);
            Pts.CoinModule =
                ModuleManager.Instance.modules[(string)ProcessModel.Instance.JsonInfo[ProcessModel.Instance.name]["PtsCoinModule"]];
            Pts.InitValue(ProcessModel.Instance.exe_p);
        }
    }
}
