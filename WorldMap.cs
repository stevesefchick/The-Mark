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

		Vector2 getMajorPlaceLocation(Random rando)
        {
			Vector2 newloc = Vector2.Zero;

			Matrix castleOriginMatrix = Matrix.CreateScale(1, 1, 1f) *
		   Matrix.CreateRotationZ(0) *
		   Matrix.CreateTranslation(new Vector3(0,0, 0f));


			//second value is distance
			Vector2 localSword = new Vector2(0, rando.Next(300,550));
			//rotation around castle origin
			Matrix swordMatrix = Matrix.CreateRotationZ(rando.Next(0,360)) * castleOriginMatrix;
			newloc = Vector2.Transform(localSword, swordMatrix);

			return newloc;
        }

		protected void createNewWorld(GameMain gamedeets, Random rando, DataManager datamanager)
        {
			//create world texture
			worldTexture = gamedeets.Content.Load<Texture2D>("Sprites/World/worldmock");
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
				person.CreatePerson(datamanager, rando);
				people.Add(person);
            }

		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(worldTexture, new Rectangle(0, 0,1600,1200),null, Color.White,0,new Vector2(128,128), SpriteEffects.None,0);

			for (int i = 0; i < terrains.Count;++i)
            {
				terrains[i].Draw(spriteBatch);
            }
			for (int i =0; i < places.Count;++i)
            {
				places[i].Draw(spriteBatch);
            }
		}

	}

}
