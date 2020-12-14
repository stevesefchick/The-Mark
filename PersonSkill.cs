using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class PersonSkill
    {
        //enum
        public enum SkillType { Blacksmithing, FirstAid, Singing }
        public enum SkillRanking { Novice, Apprentice, Professional, Master }

        SkillType skillType;
        SkillRanking skillRanking;

        public PersonSkill(SkillType type, SkillRanking ranking)
        {
            skillType = type;
            skillRanking = ranking;
        }

    }
}
