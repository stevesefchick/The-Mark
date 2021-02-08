using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Mark
{
    class WorldEvent
    {
        //enums
        public enum WorldEventBase { CreatureEncounter }

        //requirements
        Boolean requiresTerrain = false;
        GridTile.GridTerrain requiredTerrainType;
        Boolean requiresRoad = false;
        Boolean isonroadrequirement;

        //properties
        WorldEventBase thisWorldEventBaseType;
        int eventChance;
        List<String> thisEventTextPossibilities = new List<String>();
        List<EventOption> eventOptions = new List<EventOption>();
        String thisEventText;

        public Boolean CheckForGridRequirements(GridTile.GridTerrain terraintype, Boolean isonroad)
        {
            if (requiresRoad == false && requiresTerrain == false)
            {
                return true;
            }
            else if (requiresRoad == true && isonroadrequirement == isonroad)
            {
                return true;
            }
            else if (requiresTerrain == true && requiredTerrainType == terraintype)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public String ReturnEventText()
        {
            return thisEventText;
        }

        public void DetermineValidText(Random rando)
        {
            thisEventText = thisEventTextPossibilities[rando.Next(0, thisEventTextPossibilities.Count)];
        }

        public int ReturnEventChance()
        {
            return eventChance;
        }

        //world event builder
        public WorldEvent(String thisEventBaseTypeString, int percentChance, String[] eventDescriptionText,
            EventOption[] eventoptions, String requiredterrainString, String requiredroadString)
        {
            thisWorldEventBaseType = (WorldEventBase)Enum.Parse(typeof(WorldEventBase), thisEventBaseTypeString);
            eventChance = percentChance;
            //text
            foreach (String t in eventDescriptionText)
            {
                thisEventTextPossibilities.Add(t);
            }
            //event options
            foreach (EventOption o in eventoptions)
            {
                eventOptions.Add(o);
            }

            //requirements
            if (requiredterrainString != "")
            {
                requiresTerrain = true;
                requiredTerrainType = (GridTile.GridTerrain)Enum.Parse(typeof(GridTile.GridTerrain), requiredterrainString);
            }
            if (requiredroadString != "")
            {
                requiresRoad = true;
                if (requiredroadString == "true")
                {
                    isonroadrequirement = true;
                }
                else if (requiredroadString == "false")
                {
                    isonroadrequirement = false;
                }

            }



        }

    }
}
