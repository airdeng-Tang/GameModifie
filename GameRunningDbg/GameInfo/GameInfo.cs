using GameRunningDbg.GameInfo.Game;
using GameRunningDbg.GameInfo.Model;
using GameRunningDbg.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningDbg.GameInfo
{
    public static class GameInfo
    {
        /// <summary>
        /// 怪物猎人: 世界
        /// </summary>
        public static readonly string MonsterHunter_World = "MonsterHunterWorld";
        /// <summary>
        /// 空洞骑士
        /// </summary>
        public static readonly string HollowKnight = "hollow_knight";

        public static string GetGameNameById(int id)
        {
            switch (id)
            {
                case 0:
                    ProcessModel.Instance.game_info = new MonsterHunterWorldInfo();
                    ProcessModel.Instance.game = GAME.MONSTERHUNTERWORLD;
                    return MonsterHunter_World;

                case 1:
                    ProcessModel.Instance.game_info = new HollowKnightInfo();
                    ProcessModel.Instance.game = GAME.HOLLOWKNIGHT;
                    return HollowKnight;
            }
            return null;
        }


    }
}
