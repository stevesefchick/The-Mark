using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace The_Mark
{
    class DataManager
    {
        //name data
        public Dictionary<string, NameData> firstNames = new Dictionary<string, NameData>();
        public Dictionary<string, LastNameData> lastNames = new Dictionary<string, LastNameData>();
        public Dictionary<string, TownNameData> townNames = new Dictionary<string, TownNameData>();
        public Dictionary<string, RoadNameData> roadNames = new Dictionary<string, RoadNameData>();
        public Dictionary<string, WaterNameData> waterNames = new Dictionary<string, WaterNameData>();
        public Dictionary<string, CreatureNameData> creatureNames = new Dictionary<string, CreatureNameData>();

        //person data
        public Dictionary<string, TraitData> traitData = new Dictionary<string, TraitData>();

        //item data
        public Dictionary<string, Item> itemLootData = new Dictionary<string, Item>();
        public Dictionary<string, ConsumableItem> itemConsumableData = new Dictionary<string, ConsumableItem>();
        public Dictionary<string, EquipmentItem> itemEquipmentData = new Dictionary<string, EquipmentItem>();

        //random id data
        public List<string> randomGenData = new List<string>();

        public void LoadAllData(GameMain gameMain)
        {
            //JsonSerializer serializer = new JsonSerializer();

            loadNames();
            loadItems();
            loadPersonData();
            loadRandomGenData();
            System.Console.WriteLine("\n\nDataManager info loaded! \nfirstNames: " + firstNames.Count + ". \nlastNames: " + lastNames.Count + ". \ntownNames: " + townNames.Count + ". \nrandomGenData: " +
                randomGenData.Count + ".\nroadNames: " + roadNames.Count + ".\nwaterNames: " + waterNames.Count + ".\ncreatureNames: " + creatureNames.Count + ".\n\n\n\n");
        }

        private void loadNames()
        {
            firstNames = JsonConvert.DeserializeObject<Dictionary<string, NameData>>(File.ReadAllText(@"Content/Data/Person/namedata.json"));
            lastNames = JsonConvert.DeserializeObject<Dictionary<string, LastNameData>>(File.ReadAllText(@"Content/Data/Person/lastnameData.json"));
            townNames = JsonConvert.DeserializeObject<Dictionary<string, TownNameData>>(File.ReadAllText(@"Content/Data/Place/townNameData.json"));
            roadNames = JsonConvert.DeserializeObject<Dictionary<string, RoadNameData>>(File.ReadAllText(@"Content/Data/Road/roadNameData.json"));
            waterNames = JsonConvert.DeserializeObject<Dictionary<string, WaterNameData>>(File.ReadAllText(@"Content/Data/Terrain/waterNameData.json"));
            creatureNames = JsonConvert.DeserializeObject<Dictionary<string, CreatureNameData>>(File.ReadAllText(@"Content/Data/Creature/creatureNameData.json"));

        }

        private void loadPersonData()
        {
            traitData = JsonConvert.DeserializeObject<Dictionary<string, TraitData>>(File.ReadAllText(@"Content/Data/Person/traitData.json"));
        }

        private void loadItems()
        {
            itemLootData = JsonConvert.DeserializeObject<Dictionary<string, Item>>(File.ReadAllText(@"Content/Data/Item/itemLootData.json"));
            itemConsumableData = JsonConvert.DeserializeObject<Dictionary<string, ConsumableItem>>(File.ReadAllText(@"Content/Data/Item/itemConsumableData.json"));
            itemEquipmentData = JsonConvert.DeserializeObject<Dictionary<string, EquipmentItem>>(File.ReadAllText(@"Content/Data/Item/itemEquipmentData.json"));
        }

        #region random gen
        private void loadRandomGenData()
        {
            randomGenData.AddRange(new string[] {
                "a","A","b","B","c","C","d","D","e","E","f","F","g","G","h","H","i","I","j","J","k","K","l","L","m","M","n","N","o","O","p","P","q","Q","r","R","s","S","t","T",
                "u","U","v","V","w","W","x","X","y","Y","z","Z","1","2","3","4","5","6","7","8","9","0","!","-","_"
            });
        }

        public string getRandomID(Random rando)
        {
            string gen="";

            for (int i = 0; i < 30;++i)
            {
                gen += randomGenData[rando.Next(0, randomGenData.Count)];
            }

            return gen;

        }
        #endregion

    }


    class NameData
    {
        public string gender;
        public enum lastNameType { Noun }

        public NameData(string thegender)
        {
            gender = thegender;
        }
    }
    class LastNameData
    {
        public string lastnametype;

        public LastNameData(string thetype)
        {
            lastnametype = thetype;
        }
    }
    class TownNameData
    {
        public string townnametype;

        public TownNameData(string thetype)
        {
            townnametype = thetype;
        }
    }

    class RoadNameData
    {
        public string roadnametype;

        public RoadNameData(string thetype)
        {
            roadnametype = thetype;
        }
    }

    class WaterNameData
    {
        public string waternametype;

        public WaterNameData(string thetype)
        {
            waternametype = thetype;
        }
    }

    class CreatureNameData
    {
        public string creaturenametype;

        public CreatureNameData(string thetype)
        {
            creaturenametype = thetype;
        }
    }

    class TraitData
    {
        public string description;

        public TraitData(string thedesc)
        {
            description = thedesc;
        }
    }
}
