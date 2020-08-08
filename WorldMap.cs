using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace The_Mark
{
	class WorldMap
    {
		//content
		protected List<Terrain> terrains = new List<Terrain>();
		protected List<Place> places = new List<Place>();


		//assets
		private Texture2D worldTexture;

		public WorldMap(GameMain gamedeets)
        {
			
			//if new
			createNewWorld(gamedeets);
        }

		protected void createNewWorld(GameMain gamedeets)
        {
			//create world texture
			worldTexture = gamedeets.Content.Load<Texture2D>("Sprites/World/worldmock");
			//create terrains
			for (int i =0; i < 5;++i)
            {
				Terrain newTerrain = new Terrain();
				Vector2 newLocation = new Vector2(i * 20, 0);
				newTerrain.createNewTerrain(Terrain.TerrainType.Grass, newLocation,gamedeets);
				terrains.Add(newTerrain);
            }
			//create normal places
			for (int i = 0; i < 5; ++i)
			{
				Place newPlace = new Place();
				Vector2 newLocation = new Vector2(50, i*20);
				newPlace.CreateNewPlace(Place.PlaceType.Town, newLocation, gamedeets);
				places.Add(newPlace);
			}

			//create castle
			Place newCastle = new Place();
			Vector2 castleLoc = Vector2.Zero;
			newCastle.CreateNewPlace(Place.PlaceType.Castle, castleLoc, gamedeets);
			places.Add(newCastle);



		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(worldTexture, new Rectangle(0, 0,800,600), Color.White);

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
