using Microsoft.Xna.Framework;
using System.Collections.Generic;
using The_Mark;


namespace The_Mark
{
	class WorldMap
    {
		protected List<Terrain> terrains = new List<Terrain>();
		protected List<Place> places = new List<Place>();


		public WorldMap()
        {
			//if new
			createNewWorld();
        }

		protected void createNewWorld()
        {

        }

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime)
		{

		}

	}

}
