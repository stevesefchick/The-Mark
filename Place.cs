﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class Place
    {
		//TODO: Unique ID


		//place attributes
		public string placeName;
		public Vector2 placeLocation;

		//type
		public enum PlaceType { Castle, Town}
		public PlaceType thisPlaceType;

		//sprites
		Texture2D placeTexture;

		void LoadPlaceTexture(GameMain gamedeets)
        {
			if (thisPlaceType == PlaceType.Town)
			{
				placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/place_mockup");
			}
			else if (thisPlaceType == PlaceType.Castle)
			{
				placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/castle_mockup");
			}
		}

		void getPlaceName(DataManager datamanager, Random rando)
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
				placeName = candidatesname[rando.Next(0, candidatesname.Count)].ToString() +"'s " + candidatesend[rando.Next(0, candidatesend.Count)].ToString();

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

		public void CreateNewPlace(PlaceType theplacetype, Vector2 theplacelocation, GameMain gamedeets,Random rando)
        {
			thisPlaceType = theplacetype;
			placeLocation = theplacelocation;

			//load texture
			LoadPlaceTexture(gamedeets);
			//get name
			getPlaceName(gamedeets.dataManager, rando);


			//console debug
			Console.WriteLine(placeName + " is a wonderful place to live.");

		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(placeTexture, placeLocation, Color.White);
		}
	}
}
