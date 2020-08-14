using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
			//type noun + town ending
			//"person"'s place
			//"The" noun
        }

		public void CreateNewPlace(PlaceType theplacetype, Vector2 theplacelocation, GameMain gamedeets,Random rando)
        {
			thisPlaceType = theplacetype;
			placeLocation = theplacelocation;

			//load texture
			LoadPlaceTexture(gamedeets);
			//get name
			getPlaceName(gamedeets.dataManager, rando);

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
