using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace The_Mark
{
    class DataManager
    {
        public Dictionary<string, NameData> firstNames = new Dictionary<string, NameData>();
        public Dictionary<string, LastNameData> lastNames = new Dictionary<string, LastNameData>();
        public Dictionary<string, TownNameData> townNames = new Dictionary<string, TownNameData>();

        public void LoadAllData(GameMain gameMain)
        {
            JsonSerializer serializer = new JsonSerializer();

            loadNames(gameMain, serializer);

        }

        public void loadNames(GameMain gameMain, JsonSerializer serializer)
        {
            firstNames = JsonConvert.DeserializeObject<Dictionary<string, NameData>>(File.ReadAllText(@"Content/Data/Person/namedata.json"));
            lastNames = JsonConvert.DeserializeObject<Dictionary<string, LastNameData>>(File.ReadAllText(@"Content/Data/Person/lastnameData.json"));
            townNames = JsonConvert.DeserializeObject<Dictionary<string, TownNameData>>(File.ReadAllText(@"Content/Data/Place/townNameData.json"));

        }

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
}
