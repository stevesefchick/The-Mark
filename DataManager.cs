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

        public void LoadAllData(GameMain gameMain)
        {
            JsonSerializer serializer = new JsonSerializer();

            loadNames(gameMain, serializer);

        }

        public void loadNames(GameMain gameMain, JsonSerializer serializer)
        {
            firstNames = JsonConvert.DeserializeObject<Dictionary<string, NameData>>(File.ReadAllText(@"Content/Data/Person/namedata.json"));

        }

    }


    class NameData
    {
        public string gender;

        public NameData(string thegender)
        {
            gender = thegender;
        }
    }
}
