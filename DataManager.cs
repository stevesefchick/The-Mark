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
        public Dictionary<string, SkillData> skillData = new Dictionary<string, SkillData>();
        public Dictionary<string, TorsoSpriteData> torsoSpriteData = new Dictionary<string, TorsoSpriteData>();
        public Dictionary<string, HeadSpriteData> headSpriteData = new Dictionary<string, HeadSpriteData>();
        public Dictionary<string, HairSpriteData> hairSpriteData = new Dictionary<string, HairSpriteData>();
        public Dictionary<string, LegSpriteData> legSpriteData = new Dictionary<string, LegSpriteData>();
        public Dictionary<string, ArmSpriteData> armSpriteData = new Dictionary<string, ArmSpriteData>();

        //item data
        public Dictionary<string, Item> itemLootData = new Dictionary<string, Item>();
        public Dictionary<string, ConsumableItem> itemConsumableData = new Dictionary<string, ConsumableItem>();
        public Dictionary<string, EquipmentItem> itemEquipmentData = new Dictionary<string, EquipmentItem>();

        //event Data
        public Dictionary<string, Event> passiveEventData = new Dictionary<string, Event>();

        //random id data
        public List<string> randomGenData = new List<string>();

        public void LoadAllData()
        {
            //JsonSerializer serializer = new JsonSerializer();

            loadNames();
            loadItems();
            loadPersonData();
            loadEventData();
            loadRandomGenData();
            Console.WriteLine("\n\nDataManager info loaded! \nfirstNames: " + firstNames.Count + ". \nlastNames: " + lastNames.Count + ". \ntownNames: " + townNames.Count + ". \nrandomGenData: " +
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

        private void loadEventData()
        {
            passiveEventData = JsonConvert.DeserializeObject<Dictionary<string, Event>>(File.ReadAllText(@"Content/Data/Events/passiveEventData.json"));

        }

        private void loadPersonData()
        {
            traitData = JsonConvert.DeserializeObject<Dictionary<string, TraitData>>(File.ReadAllText(@"Content/Data/Person/traitData.json"));
            skillData = JsonConvert.DeserializeObject<Dictionary<string, SkillData>>(File.ReadAllText(@"Content/Data/Person/skillData.json"));
            //sprite data
            torsoSpriteData = JsonConvert.DeserializeObject<Dictionary<string, TorsoSpriteData>>(File.ReadAllText(@"Content/Data/Person/torsoSpriteData.json"));
            headSpriteData = JsonConvert.DeserializeObject<Dictionary<string, HeadSpriteData>>(File.ReadAllText(@"Content/Data/Person/headSpriteData.json"));
            hairSpriteData = JsonConvert.DeserializeObject<Dictionary<string, HairSpriteData>>(File.ReadAllText(@"Content/Data/Person/hairSpriteData.json"));
            legSpriteData = JsonConvert.DeserializeObject<Dictionary<string, LegSpriteData>>(File.ReadAllText(@"Content/Data/Person/legSpriteData.json"));
            armSpriteData = JsonConvert.DeserializeObject<Dictionary<string, ArmSpriteData>>(File.ReadAllText(@"Content/Data/Person/armSpriteData.json"));
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
        public int endurance;
        public int strength;
        public int charisma;
        public int dexterity;
        public int wit;
        public int wisdom;
        public int health;
        public int stamina;
        public int stress;

        public TraitData(string thedesc, int end, int dex, int str, int wt, int wis, int cha, int hlth, int stm, int strss)
        {
            description = thedesc;
            endurance = end;
            dexterity = dex;
            strength = str;
            wit = wt;
            wisdom = wis;
            charisma = cha;
            health = hlth;
            stamina = stm;
            stress = strss;
        }
    }

    class SkillData
    {
        public string description;
        public int endurance;
        public int strength;
        public int charisma;
        public int dexterity;
        public int wit;
        public int wisdom;
        public int health;
        public int stamina;
        public int stress;

        public SkillData(string thedesc, int end, int dex, int str, int wt, int wis, int cha, int hlth, int stm, int strss)
        {
            description = thedesc;
            endurance = end;
            dexterity = dex;
            strength = str;
            wit = wt;
            wisdom = wis;
            charisma = cha;
            health = hlth;
            stamina = stm;
            stress = strss;
        }
    }

    class TorsoSpriteData
    {
        public Vector2 spriteLoc;
        public Vector2 spriteCenter;
        public Vector2 spriteHeadConnector;
        public Vector2 leftArmConnector;
        public Vector2 rightArmConnector;
        public Vector2 leftLegConnector;
        public Vector2 rightLegConnector;

        public TorsoSpriteData(int sheetx, int sheety, int centerx, int centery,int headx, int heady, int leftarmx, int leftarmy,
            int rightarmx, int rightarmy, int leftlegx, int leftlegy, int rightlegx, int rightlegy)
        {
            spriteLoc = new Vector2(sheetx * 50, sheety * 50);
            spriteCenter = new Vector2(centerx, centery);
            spriteHeadConnector = new Vector2(headx, heady);
            spriteHeadConnector -= spriteCenter;
            leftArmConnector = new Vector2(leftarmx, leftarmy);
            leftArmConnector -= spriteCenter;
            rightArmConnector = new Vector2(rightarmx, rightarmy);
            rightArmConnector -= spriteCenter;
            leftLegConnector = new Vector2(leftlegx, leftlegy);
            leftLegConnector -= spriteCenter;
            rightLegConnector = new Vector2(rightlegx, rightlegy);
            rightLegConnector -= spriteCenter;
    }
    }

    class HeadSpriteData
    {
        public Vector2 spriteLoc;
        public Vector2 spriteCenter;
        public Vector2 spriteBodyConnector;
        public Vector2 spriteHairConnector;

        public HeadSpriteData(int sheetx, int sheety, int centerx, int centery, int bodyx, int bodyy,int hairx,int hairy)
        {
            spriteLoc = new Vector2(sheetx*50, sheety*50);
            spriteCenter = new Vector2(centerx, centery);
            spriteBodyConnector = new Vector2(bodyx, bodyy);
            spriteHairConnector = new Vector2(hairx, hairy);
            spriteHairConnector -= spriteCenter;
        }
    }

    class HairSpriteData
    {
        public Vector2 spriteLoc;
        public Vector2 spriteHeadConnector;

        public HairSpriteData(int sheetx, int sheety, int headx,int heady)
        {
            spriteLoc = new Vector2(sheetx*50, sheety*50);
            spriteHeadConnector = new Vector2(headx, heady);
        }
    }

    class FaceSpriteData
    {
        public Vector2 spriteLoc;
        public Vector2 spriteCenter;

        public FaceSpriteData(int sheetx, int sheety, int centerx, int centery)
        {
            spriteLoc = new Vector2(sheetx, sheety);
            spriteCenter = new Vector2(centerx, centery);
        }
    }

    class LegSpriteData
    {
        public Vector2 spriteLoc;
        public Vector2 spriteBodyConnector;

        public LegSpriteData(int sheetx, int sheety, int bodyx, int bodyy)
        {
            spriteLoc = new Vector2(sheetx*50, sheety*50);
            spriteBodyConnector = new Vector2(bodyx, bodyy);
        }
    }

    class ArmSpriteData
    {
        public Vector2 spriteLoc;
        public Vector2 spriteBodyConnector;

        public ArmSpriteData(int sheetx, int sheety, int bodyx, int bodyy)
        {
            spriteLoc = new Vector2(sheetx*50, sheety*50);
            spriteBodyConnector = new Vector2(bodyx, bodyy);
        }
    }
}
