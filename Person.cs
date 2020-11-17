using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Person
    {
		//id
		public string personID;

		//enums
		public enum CreationType { Birthed, Created }
		public enum Gender { Male, Female, Whocares }
		public enum PersonalityType { Calm, Grumpy, Cheerful, Curious, Sly, Guarded}

		//person attributes
		public string personFirstName;
		public string personLastName;
		public int personAge;
		public Gender personGender;
		public PersonalityType personPersonalityType;

		//home
		public string placeIDHome;


		//TODO: appearance attributes

		//TODO: personality trait attributes

		//TODO: skills

		//Stat Attributes
		public int endurance;
		public int strength;
		public int dexterity;
		public int wit;
		public int wisdom;
		public int charisma;

		//Combat Stats
		public int attack;
		public int defense;
		public int ability;

		//Health
		public int maxHealth;
		public int currentHealth;
		public int maxStamina;
		public int currentStamina;
		public int maxStress;
		public int currentStress;

        #region stat string returns
        public String returnHealthString()
        {
			if (currentHealth==maxHealth)
            {
				return "Unharmed";
            }
			else if ((float)currentHealth/(float)maxHealth >= 0.9f)
            {
				return "Stable";
            }
			else if ((float)currentHealth / (float)maxHealth >= 0.8f)
			{
				return "Sore";
			}
			else if ((float)currentHealth / (float)maxHealth >= 0.6f)
			{
				return "Bruised";
			}
			else if ((float)currentHealth / (float)maxHealth >= 0.3f)
			{
				return "Injured";
			}
			else if ((float)currentHealth / (float)maxHealth >= 0.1f)
			{
				return "Maimed";
			}
			else
            {
				return "Dying";
            }

        }

		public String returnStaminaString()
		{
			if (currentStamina == maxStamina)
			{
				return "Well Rested";
			}
			else if ((float)currentStamina / (float)maxStamina >= 0.8f)
			{
				return "Energetic";
			}
			else if ((float)currentStamina / (float)maxStamina >= 0.6f)
			{
				return "Winded";
			}
			else if ((float)currentStamina / (float)maxStamina >= 0.4f)
			{
				return "Tired";
			}
			else if ((float)currentStamina / (float)maxStamina >= 0.1f)
			{
				return "Exhausted";
			}
			else
			{
				return "Collapsing";
			}

		}

		public String returnStressString()
		{
			if ((float)currentStress / (float)maxStress >= 0.9f)
			{
				return "Panicking";
			}
			else if ((float)currentStress / (float)maxStress >= 0.8f)
			{
				return "Distressed";
			}
			else if ((float)currentStress / (float)maxStress >= 0.6f)
			{
				return "Pressured";
			}
			else if ((float)currentStress / (float)maxStress >= 0.4f)
			{
				return "Tense";
			}
			else if ((float)currentStress / (float)maxStress >= 0.2f)
			{
				return "Pensive";
			}
			else
			{
				return "Calm";
			}

		}

		#endregion

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime)
		{

		}

        #region get name info
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

        #endregion

		void getAge(CreationType creationType,Random rando)
        {
			if (creationType == CreationType.Created)
			{
				personAge = rando.Next(14, 60);
			}
			else if (creationType == CreationType.Birthed)
			{
				personAge = 0;
			}
		}

		void getPersonality(CreationType creationType,Random rando)
        {
			if (creationType == CreationType.Created)
            {
				Array values = Enum.GetValues(typeof(PersonalityType));
				PersonalityType randomPersonality = (PersonalityType)values.GetValue(rando.Next(values.Length));
				personPersonalityType = randomPersonality;
			}
        }

		void GetHealthStats()
        {
			maxHealth = 100;
			maxStamina = 100;
			maxStress = 100;

			currentHealth = maxHealth;
			currentStamina = maxStamina;
			currentStress = 0;

        }

		void GetAttributeStats()
        {
			endurance = 1;
			strength = 1;
			dexterity = 1;
			wit = 1;
			wisdom = 1;
			charisma = 1;

			attack = 1;
			defense = 1;
			ability = 1;
        }

        public void CreatePerson(DataManager datamanager, Random random, CreationType creationType)
		{
			personID = datamanager.getRandomID(random);

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
			//get person's age
			getAge(creationType, random);
			//get personality
			getPersonality(creationType, random);
			//TODO: Get Traits

			//TODO:Get Skills

			//get Stat Attributes
			GetAttributeStats();
			//get Health Stats
			GetHealthStats();


        }

		public void assignPersonToHome(List<string> placeOptions,Random rando)
        {
			placeIDHome = placeOptions[rando.Next(0, placeOptions.Count)];
        }
	}
}
