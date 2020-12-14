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

        public SkillType skillType;
        public SkillRanking skillRanking;

        public PersonSkill(SkillType type, SkillRanking ranking)
        {
            skillType = type;
            skillRanking = ranking;
        }

        public int getAttackInfluence()
        {
            //BLACKSMITHING
            if (skillType == SkillType.Blacksmithing)
            {
                if (skillRanking== SkillRanking.Novice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Apprentice)
                {
                    return 1;
                }
                else if (skillRanking == SkillRanking.Professional)
                {
                    return 1;
                }
                else if (skillRanking == SkillRanking.Master)
                {
                    return 2;
                }
            }
            //FIRST AID
            else if (skillType == SkillType.FirstAid)
            {
                if (skillRanking == SkillRanking.Novice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Apprentice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Professional)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Master)
                {
                    return 1;
                }
            }
            //SINGING
            else if (skillType == SkillType.Singing)
            {
                if (skillRanking == SkillRanking.Novice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Apprentice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Professional)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Master)
                {
                    return 0;
                }
            }

            return 0;
        }

        public int getDefenseInfluence()
        {

            //BLACKSMITHING
            if (skillType == SkillType.Blacksmithing)
            {
                if (skillRanking == SkillRanking.Novice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Apprentice)
                {
                    return 1;
                }
                else if (skillRanking == SkillRanking.Professional)
                {
                    return 2;
                }
                else if (skillRanking == SkillRanking.Master)
                {
                    return 3;
                }
            }
            //FIRST AID
            else if (skillType == SkillType.FirstAid)
            {
                if (skillRanking == SkillRanking.Novice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Apprentice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Professional)
                {
                    return 1;
                }
                else if (skillRanking == SkillRanking.Master)
                {
                    return 1;
                }
            }
            //SINGING
            else if (skillType == SkillType.Singing)
            {
                if (skillRanking == SkillRanking.Novice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Apprentice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Professional)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Master)
                {
                    return 0;
                }
            }
            return 0;
        }
        public int getAbilityInfluence()
        {

            //BLACKSMITHING
            if (skillType == SkillType.Blacksmithing)
            {
                if (skillRanking == SkillRanking.Novice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Apprentice)
                {
                    return 1;
                }
                else if (skillRanking == SkillRanking.Professional)
                {
                    return 1;
                }
                else if (skillRanking == SkillRanking.Master)
                {
                    return 2;
                }
            }
            //FIRST AID
            else if (skillType == SkillType.FirstAid)
            {
                if (skillRanking == SkillRanking.Novice)
                {
                    return 0;
                }
                else if (skillRanking == SkillRanking.Apprentice)
                {
                    return 1;
                }
                else if (skillRanking == SkillRanking.Professional)
                {
                    return 1;
                }
                else if (skillRanking == SkillRanking.Master)
                {
                    return 1;
                }
            }
            //SINGING
            else if (skillType == SkillType.Singing)
            {
                if (skillRanking == SkillRanking.Novice)
                {
                    return 1;
                }
                else if (skillRanking == SkillRanking.Apprentice)
                {
                    return 2;
                }
                else if (skillRanking == SkillRanking.Professional)
                {
                    return 2;
                }
                else if (skillRanking == SkillRanking.Master)
                {
                    return 3;
                }
            }
            return 0;
        }

    }
}
