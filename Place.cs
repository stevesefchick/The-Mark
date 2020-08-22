using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace The_Mark
{
    class Place
    {
		//uniqueID
		public string placeID;


		//place attributes
		public string placeName;
		public Vector2 placeLocation;
		Vector2 placeSize;
		Vector2 placeCenter;

		//type
		public enum PlaceType { Castle, Town}
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
					placeName = candidatesnoun[rando.Next(0, candidatesnoun.Count)].ToString() + candidatesend[rando.Next(0, candidatesend.Count)].ToString();
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


			//console debug
			Console.WriteLine(placeName + " is a wonderful place to live.");

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

		public void Update(GameMain gamedeets)
		{
			isColliding = checkForCollision(gamedeets.mouse.getMousePosition(gamedeets.camera.cameraPosition, gamedeets.backbufferJamz));



		}

		public void Draw(SpriteBatch spriteBatch,SpriteFont displayfont)
		{
			if (isColliding==true)
            {
				spriteBatch.DrawString(displayfont, placeName, new Vector2((int)(placeLocation.X - placeSize.X), (int)(placeLocation.Y-100) ), Color.White);

			}
			spriteBatch.Draw(placeTexture, new Rectangle((int)placeLocation.X,(int)placeLocation.Y,(int)placeSize.X,(int)placeSize.Y),null, Color.White,0,placeCenter,SpriteEffects.None,0);
		}
	}
}
