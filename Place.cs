using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.AccessControl;

namespace The_Mark
{
    class Place
    {
		//uniqueID
		public string placeID;


		//place attributes
		public string placeName;
		public Vector2 placeLocation;
		public Boolean isLiveable;

		//type specific variables
		private int graveyard_numberOfDead = 0;

		//location info
		public enum PlaceLocationType { Castle, MajorNode,OrbitingNode}
		public Vector2 placeSize;
		public Vector2 placeCenter;
		public PlaceLocationType thisPlaceLocationType;
		float placeDepth;

		//type
		public enum PlaceType { Castle, Town, Graveyard, Cave, Encampment,Ruins,Farm}
		public PlaceType thisPlaceType;

		//sprites
		Texture2D placeTexture;

		//states
		public Boolean isColliding = false;

		void LoadPlaceTexture(GameMain gamedeets,Random rando)
        {
			if (thisPlaceType == PlaceType.Town)
			{
				int rand = rando.Next(1, 4);
				if (rand == 1)
				{
					placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/place_mockup");
					placeSize = new Vector2(64, 64);
					placeCenter = new Vector2(32, 64);
				}
				else if (rand == 2)
				{
					placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/place_mockup2");
					placeSize = new Vector2(64, 64);
					placeCenter = new Vector2(32, 64);
				}
				else if (rand == 3)
				{
					placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/place_mockup3");
					placeSize = new Vector2(64, 64);
					placeCenter = new Vector2(32, 64);
				}
			}
			else if (thisPlaceType == PlaceType.Castle)
			{
				placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/castle_mockup");
				placeSize = new Vector2(128, 128);
				placeCenter = new Vector2(64, 128);
			}
			else if (thisPlaceType== PlaceType.Graveyard)
            {
				placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/place_graveyard");
				placeSize = new Vector2(64, 64);
				placeCenter = new Vector2(16, 32);
			}
			else if (thisPlaceType == PlaceType.Cave)
			{
				placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/place_cave");
				placeSize = new Vector2(64, 64);
				placeCenter = new Vector2(16, 32);
			}
			else if (thisPlaceType == PlaceType.Encampment)
			{
				placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/place_encampment");
				placeSize = new Vector2(64, 64);
				placeCenter = new Vector2(16, 32);
			}
			else if (thisPlaceType == PlaceType.Ruins)
			{
				placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/place_ruins");
				placeSize = new Vector2(64, 64);
				placeCenter = new Vector2(16, 32);
			}
			else if (thisPlaceType == PlaceType.Farm)
			{
				placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/place_farm");
				placeSize = new Vector2(64, 64);
				placeCenter = new Vector2(16, 32);
			}
			placeCenter = new Vector2(0, 0);
		}

		void getPlaceName(DataManager datamanager, Random rando)
        {
            #region castle
			if (thisPlaceType == PlaceType.Castle)
            {
				placeName = "The Castle";
            }
            #endregion

            #region town name
            if (thisPlaceType == PlaceType.Town)
			{
				int placenametype = rando.Next(1, 5);

				//type noun + town ending
				if (placenametype == 1 || placenametype == 2)
				{
					string prefix = "";

					if (rando.Next(1,30)==1)
                    {
						prefix = "New ";
                    }

					List<string> candidatesnoun = new List<string>();
					List<string> candidatesend = new List<string>();
					foreach (KeyValuePair<string, TownNameData> entry in datamanager.townNames)
					{
						if (entry.Value.townnametype == "noun")
						{
							candidatesnoun.Add(entry.Key);
						}
					}
					foreach (KeyValuePair<string, TownNameData> entry in datamanager.townNames)
					{
						if (entry.Value.townnametype == "townend")
						{
							candidatesend.Add(entry.Key);
						}
					}
					placeName = prefix + candidatesnoun[rando.Next(0, candidatesnoun.Count)].ToString() + candidatesend[rando.Next(0, candidatesend.Count)].ToString();
				}
				//type 2 - "person"'s place
				else if (placenametype == 3)
				{
					List<string> candidatesname = new List<string>();
					List<string> candidatesend = new List<string>();
					foreach (KeyValuePair<string, NameData> entry in datamanager.firstNames)
					{
						candidatesname.Add(entry.Key);

					}
					foreach (KeyValuePair<string, TownNameData> entry in datamanager.townNames)
					{
						if (entry.Value.townnametype == "townfancyend")
						{
							candidatesend.Add(entry.Key);
						}
					}
					placeName = candidatesname[rando.Next(0, candidatesname.Count)].ToString() + "'s " + candidatesend[rando.Next(0, candidatesend.Count)].ToString();

				}
				//type 3 - fancy town start, fancy town end
				else if (placenametype == 4)
				{
					List<string> candidatestart = new List<string>();
					List<string> candidatesend = new List<string>();
					foreach (KeyValuePair<string, TownNameData> entry in datamanager.townNames)
					{
						if (entry.Value.townnametype == "townstart")
						{
							candidatestart.Add(entry.Key);
						}
					}
					foreach (KeyValuePair<string, TownNameData> entry in datamanager.townNames)
					{
						if (entry.Value.townnametype == "townfancyend")
						{
							candidatesend.Add(entry.Key);
						}
					}
					placeName = candidatestart[rando.Next(0, candidatestart.Count)].ToString() + " " + candidatesend[rando.Next(0, candidatesend.Count)].ToString();
				}
			}
			#endregion

			#region graveyard
			if (thisPlaceType == PlaceType.Graveyard)
			{
				placeName = "Graveyard";
			}
			#endregion

			#region cave
			if (thisPlaceType == PlaceType.Cave)
			{
				placeName = "Cave";
			}
			#endregion

			#region encampment
			if (thisPlaceType == PlaceType.Encampment)
			{
				placeName = "Encampment";
			}
			#endregion

			#region ruins
			if (thisPlaceType == PlaceType.Ruins)
			{
				placeName = "Ruins";
			}
			#endregion

			#region farm
			if (thisPlaceType == PlaceType.Farm)
			{
				placeName = "Farm";
			}
			#endregion
		}

		void getPlaceAttributes(Random rando)
        {
			if (thisPlaceType == PlaceType.Town)
            {
				isLiveable = true;
				thisPlaceLocationType = PlaceLocationType.MajorNode;
            }
			else if (thisPlaceType == PlaceType.Castle)
            {
				isLiveable = false;
				thisPlaceLocationType = PlaceLocationType.Castle;
            }
			else if (thisPlaceType == PlaceType.Graveyard)
			{
				isLiveable = false;
				thisPlaceLocationType = PlaceLocationType.OrbitingNode;
				graveyard_numberOfDead = rando.Next(2, 10);
			}
			else if (thisPlaceType == PlaceType.Cave)
			{
				isLiveable = false;
				thisPlaceLocationType = PlaceLocationType.OrbitingNode;
			}
			else if (thisPlaceType == PlaceType.Encampment)
			{
				isLiveable = false;
				thisPlaceLocationType = PlaceLocationType.OrbitingNode;
			}
			else if (thisPlaceType == PlaceType.Ruins)
			{
				isLiveable = false;
				thisPlaceLocationType = PlaceLocationType.OrbitingNode;
			}
			else if (thisPlaceType == PlaceType.Farm)
			{
				isLiveable = true;
				thisPlaceLocationType = PlaceLocationType.OrbitingNode;
			}
		}

		public void CreateNewPlace(PlaceType theplacetype, Vector2 theplacelocation, GameMain gamedeets,Random rando)
        {
			thisPlaceType = theplacetype;
			placeLocation = theplacelocation;
			placeID = gamedeets.dataManager.getRandomID(rando);



			//load texture
			LoadPlaceTexture(gamedeets,rando);
			//get name
			getPlaceName(gamedeets.dataManager, rando);
			//get properties based on type
			getPlaceAttributes(rando);

			placeDepth = 0.3f + ((placeLocation.Y + placeSize.Y) * 0.00001f);

		}

		Boolean checkForCollision(Vector2 mouse)
        {
			Boolean collided = false;
			Rectangle thisPlace = new Rectangle((int)(placeLocation.X-placeCenter.X), (int)(placeLocation.Y-placeCenter.Y), (int)placeSize.X, (int)placeSize.Y);
			Rectangle mouseRect = new Rectangle((int)mouse.X, (int)mouse.Y, 32, 32);

			if (thisPlace.Intersects(mouseRect))
            {
				collided = true;
			}


			return collided;
        }


		//determine the random placetype based on available options
		public PlaceType determineOrbitalPlaceType(Random rando, GridTile.GridTerrain gridTerrain)
        {
			List<PlaceType> possiblePlaceTypes = new List<PlaceType>();

			possiblePlaceTypes.AddRange(new PlaceType[] { PlaceType.Graveyard, PlaceType.Cave, PlaceType.Encampment, PlaceType.Ruins, PlaceType.Farm });

			//check for terrain specific possibilities
			if (gridTerrain == GridTile.GridTerrain.Beach)
            {

            }
			else if (gridTerrain == GridTile.GridTerrain.Forest)
			{

			}
			else if (gridTerrain == GridTile.GridTerrain.Hills)
			{

			}
			else if (gridTerrain == GridTile.GridTerrain.Grass)
			{

			}


			PlaceType thisType = possiblePlaceTypes[rando.Next(0, possiblePlaceTypes.Count)];

			return thisType;
        }

		public void Update(GameMain gamedeets)
		{
			isColliding = checkForCollision(gamedeets.mouse.getMousePosition(gamedeets.camera.cameraPosition, gamedeets.backbufferJamz));

		}

		private void DrawFont(SpriteBatch spriteBatch,SpriteFont displayfont)
        {
			spriteBatch.DrawString(displayfont, placeName, new Vector2((int)(placeLocation.X - placeSize.X + 1), (int)(placeLocation.Y - 99)), Color.Black,0,Vector2.Zero,1,SpriteEffects.None,0.71f);
			spriteBatch.DrawString(displayfont, placeName, new Vector2((int)(placeLocation.X - placeSize.X + 2), (int)(placeLocation.Y - 98)), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.71f);
			spriteBatch.DrawString(displayfont, placeName, new Vector2((int)(placeLocation.X - placeSize.X), (int)(placeLocation.Y - 100)), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.75f);

		}

		public void Draw(SpriteBatch spriteBatch,SpriteFont displayfont)
		{
			//if (isColliding==true)
            //{
			//	DrawFont(spriteBatch,displayfont);
			//}
			spriteBatch.Draw(placeTexture, new Rectangle((int)placeLocation.X,(int)placeLocation.Y,(int)placeSize.X,(int)placeSize.Y),null, Color.White,0,placeCenter,SpriteEffects.None, placeDepth);
		}
	}
}
