using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace The_Mark
{
    class Terrain
    {
		//main variables
		public enum TerrainType { Grass, Forest}
		public TerrainType thisTerrainType;
		public Vector2 terrainLocation;
		public Rectangle textureRect;


		public void createNewTerrain(TerrainType thisType, Vector2 location,GameMain gamedeets,Random rando)
        {
			thisTerrainType = thisType;
			terrainLocation = location;

			if (thisTerrainType == TerrainType.Grass)
            {
				int texturerand = rando.Next(0, 8);
				textureRect = new Rectangle(texturerand*64, 0, 64, 64);
			}
			else if (thisTerrainType == TerrainType.Forest)
			{
				textureRect = new Rectangle(0, 0, 64, 64);
			}
		}


		public void Update(GameTime gameTime)
		{
		}

		public void Draw(SpriteBatch spriteBatch, Texture2D grassTerrain,Texture2D forestTerrain)
		{
			if (thisTerrainType == TerrainType.Forest)
			{
				spriteBatch.Draw(forestTerrain, terrainLocation, textureRect, Color.White);
			}
			else if (thisTerrainType == TerrainType.Grass)
			{
				spriteBatch.Draw(grassTerrain, terrainLocation, textureRect, Color.White);
			}
		}
	}
}
