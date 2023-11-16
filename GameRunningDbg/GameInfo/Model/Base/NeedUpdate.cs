using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo.Model.Base
{
    public interface NeedUpdate
    {
        void Update();
        bool SetValue(int value);
    }
}
