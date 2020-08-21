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

		void getFirstName(DataManager datamanager,Random random)
        {
			//get list of names
			List<string> candidates = new List<string>();
			foreach (KeyValuePair<string, NameData> entry in datamanager.firstNames)
			{
				if ((entry.Value.gender == "male" || entry.Value.gender == "whocares") && personGender == Gender.Male)
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
		}

		void getLastName(DataManager datamanager,Random random)
        {
			int lastnametype = random.Next(1, 7);
			string mc = "";
			string ski = "";
			if (random.Next(1,30)==1)
            {
				mc = "Mc";
            }
			else if (random.Next(1,40)==1)
            {
				mc = "O'";
            }
			if (random.Next(1, 30) == 1)
			{
				ski = "ski";
			}


			//last name type 1 = noun + noun
			if (lastnametype == 1)
			{
				List<string> candidates = new List<string>();
				foreach (KeyValuePair<string, LastNameData> entry in datamanager.lastNames)
				{
					if (entry.Value.lastnametype == "noun")
					{
						candidates.Add(entry.Key);
					}
				}

				personLastName = candidates[random.Next(0, candidates.Count)].ToString() + candidates[random.Next(0, candidates.Count)].ToString();
			}
			//last name type 2 = animal + noun
			else if (lastnametype == 2)
            {
				List<string> nouncandidates = new List<string>();
				List<string> animalcandidates = new List<string>();
				foreach (KeyValuePair<string, LastNameData> entry in datamanager.lastNames)
				{
					if (entry.Value.lastnametype == "noun")
					{
						nouncandidates.Add(entry.Key);
					}
				}
				foreach (KeyValuePair<string, LastNameData> entry in datamanager.lastNames)
				{
					if (entry.Value.lastnametype == "animal")
					{
						animalcandidates.Add(entry.Key);
					}
				}
				personLastName = animalcandidates[random.Next(0, animalcandidates.Count)].ToString() + nouncandidates[random.Next(0, nouncandidates.Count)].ToString();
			}
			//last name type 3 = verb + noun
			else if (lastnametype == 3)
			{
				List<string> nouncandidates = new List<string>();
				List<string> verbcandidates = new List<string>();
				foreach (KeyValuePair<string, LastNameData> entry in datamanager.lastNames)
				{
					if (entry.Value.lastnametype == "noun")
					{
						nouncandidates.Add(entry.Key);
					}
				}
				foreach (KeyValuePair<string, LastNameData> entry in datamanager.lastNames)
				{
					if (entry.Value.lastnametype == "verb")
					{
						verbcandidates.Add(entry.Key);
					}
				}
				personLastName = verbcandidates[random.Next(0, verbcandidates.Count)].ToString() + nouncandidates[random.Next(0, nouncandidates.Count)].ToString();
			}
			//last name type 4 = verb + adj
			else if (lastnametype == 4)
			{
				List<string> adjcandidates = new List<string>();
				List<string> verbcandidates = new List<string>();
				foreach (KeyValuePair<string, LastNameData> entry in datamanager.lastNames)
				{
					if (entry.Value.lastnametype == "adj")
					{
						adjcandidates.Add(entry.Key);
					}
				}
				foreach (KeyValuePair<string, LastNameData> entry in datamanager.lastNames)
				{
					if (entry.Value.lastnametype == "verb")
					{
						verbcandidates.Add(entry.Key);
					}
				}
				personLastName = verbcandidates[random.Next(0, verbcandidates.Count)].ToString() + adjcandidates[random.Next(0, adjcandidates.Count)].ToString();
			}
			//last name type 5 = adj + noun
			else if (lastnametype == 5)
			{
				List<string> adjcandidates = new List<string>();
				List<string> nouncandidates = new List<string>();
				foreach (KeyValuePair<string, LastNameData> entry in datamanager.lastNames)
				{
					if (entry.Value.lastnametype == "adj")
					{
						adjcandidates.Add(entry.Key);
					}
				}
				foreach (KeyValuePair<string, LastNameData> entry in datamanager.lastNames)
				{
					if (entry.Value.lastnametype == "noun")
					{
						nouncandidates.Add(entry.Key);
					}
				}
				personLastName = adjcandidates[random.Next(0, adjcandidates.Count)].ToString() + nouncandidates[random.Next(0, nouncandidates.Count)].ToString();
			}
			//last name type 6 = adj + animal
			else if (lastnametype == 6)
			{
				List<string> adjcandidates = new List<string>();
				List<string> animalcandidates = new List<string>();
				foreach (KeyValuePair<string, LastNameData> entry in datamanager.lastNames)
				{
					if (entry.Value.lastnametype == "adj")
					{
						adjcandidates.Add(entry.Key);
					}
				}
				foreach (KeyValuePair<string, LastNameData> entry in datamanager.lastNames)
				{
					if (entry.Value.lastnametype == "animal")
					{
						animalcandidates.Add(entry.Key);
					}
				}
				personLastName = adjcandidates[random.Next(0, adjcandidates.Count)].ToString() + animalcandidates[random.Next(0, animalcandidates.Count)].ToString();
			}

			//capitalize first letter
			personLastName = personLastName.Substring(0, 1).ToUpper() + personLastName.Substring(1);
			//add extras
			personLastName = mc + personLastName + ski;
			
		}


		public void CreatePerson(DataManager datamanager, Random random)
		{
			//choose gender randomly
			if (random.Next(1, 3) == 1)
			{
				personGender = Gender.Male;
			}
			else
			{
				personGender = Gender.Female;
			}

			//get person first name
			getFirstName(datamanager, random);
			//get person last name
			getLastName(datamanager, random);

			//report out
			Console.WriteLine(personFirstName + " " + personLastName + " is here and they are " + personGender);

        }
	}
}
