using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Mark
{
    class Terrain
    {
		//main variables
		public enum TerrainType { Grass}
		public TerrainType thisTerrainType;
		public Vector2 terrainLocation;

		Texture2D terrainTexture;

		public void createNewTerrain(TerrainType thisType, Vector2 location,GameMain gamedeets)
        {
			thisTerrainType = thisType;
			terrainLocation = location;

			if (thisTerrainType == TerrainType.Grass)
            {
				terrainTexture = gamedeets.Content.Load<Texture2D>("Sprites/Terrain/grass_mock");
			}
		}


		public void Update(GameTime gameTime)
		{
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(terrainTexture, terrainLocation, Color.White);
		}
	}
}
