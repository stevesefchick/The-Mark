using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


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

		public void CreateNewPlace(PlaceType theplacetype, Vector2 theplacelocation, GameMain gamedeets)
        {
			thisPlaceType = theplacetype;
			placeLocation = theplacelocation;

			if (thisPlaceType == PlaceType.Town)
            {
				placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/place_mockup");
			}
			else if (thisPlaceType == PlaceType.Castle)
            {
				placeTexture = gamedeets.Content.Load<Texture2D>("Sprites/Place/castle_mockup");
			}
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
