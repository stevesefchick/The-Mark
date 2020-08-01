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

		public void Draw(GameTime gameTime)
		{

		}

	}

}
