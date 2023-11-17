using GameRunningDbg.GameInfo.Base;
using GameRunningDbg.GameInfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModifier.GameInfo.Model.Base
{
    public abstract class PlayerBase
    {
        public GameBase master;

        private Gold golds;
        public Gold Golds
        {
            set
            {
                golds = value;
                master?.need_update_objects.Add(value);
            }
            get
            {
                return golds;
            }
        }
    }
}
