using GameModifier.GameInfo.Model.Base;
using GameRunningDbg.GameInfo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModifier.GameInfo.Model.HK
{
    public class HKPlayer : PlayerBase
    {
        public HKPlayer(HollowKnightInfo hollowKnightInfo) 
        { 
            master = hollowKnightInfo;
        }
    }
}
