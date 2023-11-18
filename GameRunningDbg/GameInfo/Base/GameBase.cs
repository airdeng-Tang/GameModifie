using GameModifier.GameInfo.Model.Base;
using GameRunningDbg.GameInfo.Model;
using GameRunningDbg.GameInfo.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo.Base
{
    public abstract class GameBase
    {
        public List<NeedUpdate> need_update_objects;

        protected PlayerBase player;

        public abstract void game_update();

        public abstract void init_info();

        public abstract void ShowHelp();
    }
}
