using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Terrain
    {
		//main variables
		public enum TerrainType { Grass, Forest}
		public TerrainType thisTerrainType;
		public Rectangle terrainLocation;
		public Rectangle textureRect;

		//doodads
		public List<TerrainDoodad> terrainDoodads = new List<TerrainDoodad>();


		public void createNewTerrain(TerrainType thisType, Vector2 location,Random rando)
        {
			thisTerrainType = thisType;
			terrainLocation = new Rectangle((int)location.X, (int)location.Y, 64, 64);
			terrainDoodads.Clear();

			if (thisTerrainType == TerrainType.Grass)
            {
				int texturerand = rando.Next(0, 8);
				textureRect = new Rectangle(texturerand*64, 0, 64, 64);

				//add doodads

			}
			else if (thisTerrainType == TerrainType.Forest)
			{
				textureRect = new Rectangle(0, 0, 64, 64);

				//add doodads
				int randTree = rando.Next(3, 9);
				for (int i =0; i < randTree;++i)
                {
					terrainDoodads.Add(new TerrainDoodad(TerrainDoodad.TerrainDoodadType.NormalTree1, new Vector2(location.X + rando.Next(3, 63), location.Y + rando.Next(3, 63)),rando));
                }
			}
		}

		public void AssignForestTile(Vector2 vect)
        {
			textureRect.X = (int)vect.X;
			textureRect.Y = (int)vect.Y;

        }

		public Vector2 returnTerrainLocation()
        {
			Vector2 loc = new Vector2(terrainLocation.X, terrainLocation.Y);
			return loc;
        }

		public void Draw(SpriteBatch spriteBatch, Texture2D grassTerrain,Texture2D forestTerrain)
		{
			if (thisTerrainType == TerrainType.Forest)
			{
				//grass under
				spriteBatch.Draw(grassTerrain, terrainLocation, new Rectangle(0,0,64,64), Color.White,0,Vector2.Zero,SpriteEffects.None,0.1f);

				spriteBatch.Draw(forestTerrain, terrainLocation, textureRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.15f);

			}
			else if (thisTerrainType == TerrainType.Grass)
			{
				spriteBatch.Draw(grassTerrain, terrainLocation, textureRect, Color.White,0, Vector2.Zero, SpriteEffects.None, 0.1f);
			}
		}

		public void DrawDoodads(SpriteBatch spriteBatch,Texture2D treeTerrain)
		{
			if (thisTerrainType == TerrainType.Forest)
			{
				for (int i = 0; i < terrainDoodads.Count; ++i)
				{
					terrainDoodads[i].Draw(spriteBatch, treeTerrain);
				}
			}
		}
	}
}
