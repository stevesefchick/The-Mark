using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace The_Mark
{
    class Terrain
    {
		//main variables
		public enum TerrainType { Grass}
		public TerrainType thisTerrainType;
		public Vector2 terrainLocation;
		public Rectangle textureRect;

		Texture2D terrainTexture;

		public void createNewTerrain(TerrainType thisType, Vector2 location,GameMain gamedeets,Random rando)
        {
			thisTerrainType = thisType;
			terrainLocation = location;

			if (thisTerrainType == TerrainType.Grass)
            {
				terrainTexture = gamedeets.Content.Load<Texture2D>("Sprites/Terrain/grass_grid_1");
				int texturerand = rando.Next(0, 8);
				textureRect = new Rectangle(texturerand*64, 0, 64, 64);
			}
		}


		public void Update(GameTime gameTime)
		{
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (thisTerrainType != TerrainType.Grass)
			{
				//todo, may need to draw underlying grass
			}
			else
			{
				spriteBatch.Draw(terrainTexture, terrainLocation, textureRect, Color.White);
			}
		}
	}
}
