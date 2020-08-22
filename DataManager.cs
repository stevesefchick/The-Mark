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

        //random id data
        public List<string> randomGenData = new List<string>();

        public void LoadAllData(GameMain gameMain)
        {
            //JsonSerializer serializer = new JsonSerializer();

            loadNames();
            loadRandomGenData();
            System.Console.WriteLine("DataManager info loaded! \nfirstNames: " + firstNames.Count + ". \nlastNames: " + lastNames.Count + ". \ntownNames: " + townNames.Count + ". \nrandomGenData: " +
                randomGenData.Count + ".");
        }

        private void loadNames()
        {
            firstNames = JsonConvert.DeserializeObject<Dictionary<string, NameData>>(File.ReadAllText(@"Content/Data/Person/namedata.json"));
            lastNames = JsonConvert.DeserializeObject<Dictionary<string, LastNameData>>(File.ReadAllText(@"Content/Data/Person/lastnameData.json"));
            townNames = JsonConvert.DeserializeObject<Dictionary<string, TownNameData>>(File.ReadAllText(@"Content/Data/Place/townNameData.json"));

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
}
