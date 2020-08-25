using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace The_Mark
{
	class WorldMap
    {
		//content
		protected List<Terrain> terrains = new List<Terrain>();
		protected List<Place> places = new List<Place>();
		protected List<Person> people = new List<Person>();

		//assets
		private Texture2D worldTexture;

		public WorldMap(GameMain gamedeets,Random rando,DataManager datamanager)
        {
			
			//if new
			createNewWorld(gamedeets,rando,datamanager);
        }

		//checks to see if the location is near another place
		// if true, it's too close to another
		Boolean isNearOtherPlaces(Vector2 loc)
        {
			Boolean well_is_it = false;

			for (int i = 0; i < places.Count;++i)
            {
				if (MathHelper.Distance(loc.X,places[i].placeLocation.X) < 150 &&
					MathHelper.Distance(loc.Y,places[i].placeLocation.Y) < 150)
                {
					well_is_it = true;
					break;
                }
            }


			return well_is_it;
        }

		Vector2 getMajorPlaceLocation(Random rando)
        {
			Vector2 newloc = Vector2.Zero;

			//check that it's not 0 or near another place
			while (newloc == Vector2.Zero || isNearOtherPlaces(newloc) == true)
            {
				Matrix castleOriginMatrix = Matrix.CreateScale(1, 1, 1f) *
Matrix.CreateRotationZ(0) *
Matrix.CreateTranslation(new Vector3(0, 0, 0f));


				//second value is distance
				Vector2 distance = new Vector2(0, rando.Next(250, 550));
				//rotation around castle origin
				Matrix distancematrix = Matrix.CreateRotationZ(rando.Next(0, 360)) * castleOriginMatrix;
				newloc = Vector2.Transform(distance, distancematrix);
			}

			return newloc;
        }

		//debug - announce creations
		protected void debugAnnounceCreation()
        {

			//announce people
			Console.WriteLine("creating people!");
			for (int i = 0; i < people.Count;++i)
            {
				string locname="";
				foreach (Place p in places)
                {
					if (p.placeID==people[i].placeIDHome)
                    {
						locname = p.placeName;
                    }
                }

				Console.WriteLine(people[i].personFirstName + " " + people[i].personLastName + " is here!\n" +
					"Age: " + people[i].personAge + "\n" +
					"Gender: " + people[i].personGender + "\n" +
					"Home: " + locname + "\n");
			}


			//announce locations
			Console.WriteLine("creating places!");

			for (int i = 0; i < places.Count; ++i)
			{
				int population = 0;
				foreach (Person p in people)
                {
					if (p.placeIDHome == places[i].placeID)
                    {
						population += 1;
                    }
                }



				//console debug
				Console.WriteLine(places[i].placeName + " is a new place!\n" +
					"Type: " + places[i].thisPlaceType + "\n" + 
					"Population: " + population + "\n");
			}




		}


		//create the world
		protected void createNewWorld(GameMain gamedeets, Random rando, DataManager datamanager)
        {
			//create world texture
			worldTexture = gamedeets.Content.Load<Texture2D>("Sprites/World/worldmock");

			//liveable places, for persons
			List<string> liveablePlaces = new List<string>();

			//create terrains
			for (int i =0; i < 5;++i)
            {
				int randX = rando.Next(-250, 251);
				int randY = rando.Next(-250, 251);
				Terrain newTerrain = new Terrain();
				Vector2 newLocation = new Vector2(randX, randY);
				newTerrain.createNewTerrain(Terrain.TerrainType.Grass, newLocation,gamedeets);
				terrains.Add(newTerrain);
            }
			//create normal places
			for (int i = 0; i < 5; ++i)
			{
				Place newPlace = new Place();
				Vector2 newLocation = getMajorPlaceLocation(rando);
				newPlace.CreateNewPlace(Place.PlaceType.Town, newLocation, gamedeets,rando);
				places.Add(newPlace);

				//add to liveable places
				liveablePlaces.Add(newPlace.placeID);
			}

			//test create graveyards
			for (int i = 0; i < 10; ++i)
			{
				Place newPlace = new Place();
				Vector2 newLocation = getMajorPlaceLocation(rando);
				newPlace.CreateNewPlace(Place.PlaceType.Graveyard, newLocation, gamedeets, rando);
				places.Add(newPlace);

			}

			//create castle
			Place newCastle = new Place();
			Vector2 castleLoc = Vector2.Zero;
			newCastle.CreateNewPlace(Place.PlaceType.Castle, castleLoc, gamedeets,rando);
			places.Add(newCastle);


			//create a bunch of people
			for (int i=0;i<100;++i)
            {
				Person person = new Person();
				person.CreatePerson(datamanager, rando, Person.CreationType.Created);
				person.assignPersonToHome(liveablePlaces,rando);
				people.Add(person);
            }

			//finalization
			debugAnnounceCreation();

		}

		public void Update(GameMain gamedeets)
		{
			for (int i =0;i<places.Count;++i)
            {
				places[i].Update(gamedeets);
            }
		}

		public void Draw(SpriteBatch spriteBatch, SpriteFont displayFont)
		{
			spriteBatch.Draw(worldTexture, new Rectangle(0, 0,1600,1200),null, Color.White,0,new Vector2(128,128), SpriteEffects.None,0);

			for (int i = 0; i < terrains.Count;++i)
            {
				terrains[i].Draw(spriteBatch);
            }
			for (int i =0; i < places.Count;++i)
            {
				places[i].Draw(spriteBatch,displayFont);
            }
		}

	}

}
