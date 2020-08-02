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
			worldTexture = gamedeets.Content.Load<Texture2D>("Sprites/World/worldmock");
        }

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(worldTexture, new Rectangle(0, 0,800,600), Color.White);

			for (int i = 0; i < terrains.Count;++i)
            {
				terrains[i].Draw(gameTime, spriteBatch);
            }
			for (int i =0; i < places.Count;++i)
            {
				places[i].Draw(gameTime, spriteBatch);
            }
		}

	}

}
