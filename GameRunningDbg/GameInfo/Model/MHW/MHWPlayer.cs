using GameModifier.GameInfo.Model.Base;
using GameRunningDbg.GameInfo.Game;
using GameRunningDbg.GameInfo.Model.MHW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModifier.GameInfo.Model.MHW
{
    public class MHWPlayer : PlayerBase
    {
        public MHWPlayer(MonsterHunterWorldInfo monsterHunterWorldInfo)
        {
            master = monsterHunterWorldInfo;
        }

        private Pts pts;
        public Pts Pts
        {
            get { return pts; }
            set
            {
                pts = value;
                master?.need_update_objects.Add(value);
            }
        }

        private HrLevel hr; 
        public HrLevel HR
        {
            get { return hr; }
            set {
                hr = value;
                master?.need_update_objects.Add(value);
            }
        }

        private MrLevel mr;
        public MrLevel MR
        {
            get { return mr; }
            set {
                mr = value;
                master?.need_update_objects.Add(value);
            }
        }

        private Bag bag;
        public Bag Bag
        {
            get { return bag; }
            set
            {
                bag = value;
            }
        }

        public void ShowInfo()
        {
            Console.Write($"HR : {HR.Value}\nMR : {MR.Value}\n金币 : {Golds.Value}\n调查点 : {Pts.Value}\n");
        }
    }
}
