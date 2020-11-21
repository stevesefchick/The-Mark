using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Terrain
    {
		//main variables
		public enum TerrainType { Grass, Forest, Hills, Mountain, Beach, Swamp}
		public TerrainType thisTerrainType;
		public Rectangle terrainLocation;
		public Rectangle textureRect;

		//doodads
		public List<TerrainDoodad> terrainDoodads = new List<TerrainDoodad>();


		public void createNewTerrain(TerrainType thisType, Vector2 location,Random rando,Boolean isOnWaterorRoad)
        {
			thisTerrainType = thisType;
			terrainLocation = new Rectangle((int)location.X, (int)location.Y, 64, 64);
			terrainDoodads.Clear();

			if (thisTerrainType == TerrainType.Grass)
            {
				int texturerand = rando.Next(0, 8);
				textureRect = new Rectangle(texturerand*64, 0, 64, 64);

				//add doodads
				if (isOnWaterorRoad == false)
				{
					int randTree = rando.Next(1, 9);
					if (randTree == 1)
					{
						terrainDoodads.Add(new TerrainDoodad(TerrainDoodad.TerrainDoodadType.NormalTree1, new Vector2(location.X + rando.Next(3, 63), location.Y + rando.Next(3, 63)), rando));

					}
				}

			}
			else if (thisTerrainType == TerrainType.Forest)
			{
				textureRect = new Rectangle(0, 0, 64, 64);

				//add doodads
				if (isOnWaterorRoad == false)
				{
					int randTree = rando.Next(3, 9);
					for (int i = 0; i < randTree; ++i)
					{
						terrainDoodads.Add(new TerrainDoodad(TerrainDoodad.TerrainDoodadType.NormalTree1, new Vector2(location.X + rando.Next(3, 63), location.Y + rando.Next(3, 63)), rando));
					}
				}
			}
			else if (thisTerrainType == TerrainType.Hills)
			{
				textureRect = new Rectangle(0, 0, 64, 64);

				//add doodads
				if (isOnWaterorRoad == false)
				{
					int randhill = rando.Next(2, 4);
					for (int i = 0; i < randhill; ++i)
					{
						terrainDoodads.Add(new TerrainDoodad(TerrainDoodad.TerrainDoodadType.Hill1, new Vector2(location.X + rando.Next(-20, 40), location.Y + rando.Next(0, 20) + (randhill*20)), rando));
					}
				}
			}
			else if (thisTerrainType == TerrainType.Beach)
			{
				textureRect = new Rectangle(0, 0, 64, 64);

				//add doodads
				if (isOnWaterorRoad == false)
				{
					int randdoodad = rando.Next(1, 4);
					for (int i = 0; i < randdoodad; ++i)
					{
						terrainDoodads.Add(new TerrainDoodad(TerrainDoodad.TerrainDoodadType.Beach1, new Vector2(location.X + rando.Next(15,49), location.Y + rando.Next(15, 49)), rando));
					}
				}
			}
			else if (thisTerrainType == TerrainType.Swamp)
			{
				textureRect = new Rectangle(0, 0, 64, 64);

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

		public void Draw(SpriteBatch spriteBatch, Texture2D grassTerrain,Texture2D forestTerrain,Texture2D beachTerrain,Texture2D swampTerrain)
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
			else if (thisTerrainType == TerrainType.Beach)
			{
				//grass under
				spriteBatch.Draw(grassTerrain, terrainLocation, new Rectangle(0, 0, 64, 64), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);

				spriteBatch.Draw(beachTerrain, terrainLocation, textureRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.15f);
			}
			else if (thisTerrainType == TerrainType.Hills)
			{
				spriteBatch.Draw(grassTerrain, terrainLocation, textureRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
			}
			else if (thisTerrainType == TerrainType.Swamp)
			{
				//grass under
				spriteBatch.Draw(grassTerrain, terrainLocation, new Rectangle(0, 0, 64, 64), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);

				spriteBatch.Draw(swampTerrain, terrainLocation, textureRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.15f);
			}
		}

		public void DrawDoodads(SpriteBatch spriteBatch,Texture2D thisTerrain)
		{
				for (int i = 0; i < terrainDoodads.Count; ++i)
				{
					terrainDoodads[i].Draw(spriteBatch, thisTerrain);
				}
			
		}
	}
}
