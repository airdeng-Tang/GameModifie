using GameRunningDbg.GameInfo.Base;
using GameRunningDbg.Manager;
using GameRunningDbg.Model;
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
        public HollowKnightInfo()
        {
            need_update_objects = new List<NeedUpdate>();
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
            Gold golds = new Gold((int[])ProcessModel.Instance.JsonInfo[ProcessModel.Instance.name]["GoldsMemoryOffset"]);

            golds.CoinModule = 
                ModuleManager.Instance.modules[(string)ProcessModel.Instance.JsonInfo[ProcessModel.Instance.name]["GoldCoinModule"]];

            golds.InitValue(ProcessModel.Instance.exe_p);
            ProcessModel.Instance.game_info.Golds = golds;
        }
    }
}
