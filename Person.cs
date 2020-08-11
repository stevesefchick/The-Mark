using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Person
    {
		//TODO: Unique ID

		enum BirthType { Birthed, Created }
		protected enum Gender { Male, Female, Whocares }

		//person attributes
		protected string personFirstName;
		protected string personLastName;
		protected int personAge;
		protected Gender personGender;
		//TODO: Sexuality

		//TODO: appearance attributes

		//TODO: personality trait attributes

		//TODO: skills

		//TODO: stats
		

		

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime)
		{

		}

		public void CreatePerson(DataManager datamanager,Random random)
        {
			//choose gender randomly
			if (random.Next(1,3)==1)
            {
				personGender = Gender.Male;
            }
			else
            {
				personGender = Gender.Female;
            }

			//get list of names
			List<string> candidates = new List<string>();
			foreach (KeyValuePair<string,NameData> entry in datamanager.firstNames)
            {
				if ((entry.Value.gender== "male" || entry.Value.gender=="whocares") && personGender == Gender.Male)
                {
					candidates.Add(entry.Key);
                }
				else if ((entry.Value.gender == "female" || entry.Value.gender == "whocares") && personGender == Gender.Female)
				{
					candidates.Add(entry.Key);
				}
			}
			//assign first name
			personFirstName = candidates[random.Next(0, candidates.Count)].ToString();
			Console.WriteLine(personFirstName + " is here and they are " + personGender);

        }
	}
}
