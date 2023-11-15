using GameRunningDbg.Model;
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
        private Gold golds;
        public Gold Golds
        {
            set
            {
                golds = value;
                need_update_objects.Add(value);
            }
            get
            {
                return golds;
            }
        }

        public abstract void game_update();

        public abstract void init_info();
    }
}
