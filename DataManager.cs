using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace The_Mark
{
    class DataManager
    {
		public List<string> firstNamesMale = new List<string>();

        public void LoadAllData(GameMain gameMain)
        {
            JsonSerializer serializer = new JsonSerializer();

            loadNames(gameMain, serializer);

        }

        public void loadNames(GameMain gameMain, JsonSerializer serializer)
        {
            firstNamesMale = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(@"Content/Data/Person/namedata.json"));

        }

    }
}
