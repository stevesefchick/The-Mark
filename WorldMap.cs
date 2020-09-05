using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace The_Mark
{
	class WorldMap
    {
		//content
		protected List<Terrain> terrains = new List<Terrain>();
		protected List<Place> places = new List<Place>();
		protected List<Person> people = new List<Person>();
		protected List<Road> roads = new List<Road>();

		//***GRID stuff ***
		public Vector2 gridSize = new Vector2(50, 50);
		//List<GridTile> gridTiles = new List<GridTile>();
		Dictionary<Point, GridTile> gridTiles = new Dictionary<Point, GridTile>();


		//*** end GRID stuff ***

		//assets
		private Texture2D worldTexture;

		//size
		private Vector2 worldSize = new Vector2(2300, 2300);

		//checks
		Boolean isDisplayTownAreaFont = false;

		public WorldMap(GameMain gamedeets,Random rando,DataManager datamanager)
        {
			
			//if new
			createNewWorld(gamedeets,rando,datamanager);
        }

		//checks to see if the location is near another place
		// if true, it's too close to another
		Boolean isNearOtherPlaces(Vector2 loc)
        {
			Boolean well_is_it = false;

			for (int i = 0; i < places.Count;++i)
            {
				if (MathHelper.Distance(loc.X,places[i].placeLocation.X) < 150 &&
					MathHelper.Distance(loc.Y,places[i].placeLocation.Y) < 150)
                {
					well_is_it = true;
					break;
                }
            }


			return well_is_it;
        }

		Boolean isNearCastle(Vector2 loc)
        {
			Boolean well_is_it = false;

			if ((MathHelper.Distance(loc.X, 0) < 150) && (MathHelper.Distance(loc.Y, 0) < 150))
			{
				well_is_it = true;
			}



			return well_is_it;

		}

		Vector2 getMajorPlaceLocation(Random rando)
        {
			Vector2 newloc = Vector2.Zero;

			//check that it's not 0 or near another place
			while (newloc == Vector2.Zero || isNearOtherPlaces(newloc) == true)
            {
				Matrix castleOriginMatrix = Matrix.CreateScale(1, 1, 1f) *
Matrix.CreateRotationZ(0) *
Matrix.CreateTranslation(new Vector3(0, 0, 0f));


				//second value is distance
				Vector2 distance = new Vector2(0, rando.Next(350, 800));
				//rotation around castle origin
				Matrix distancematrix = Matrix.CreateRotationZ(rando.Next(0, 360)) * castleOriginMatrix;
				newloc = Vector2.Transform(distance, distancematrix);

			}



			return newloc;
        }

		Vector2 getOrbitalPlaceLocation(Random rando,Vector2 originLocation, Vector2 orbitRange)
		{
			Vector2 newloc = Vector2.Zero;
			int attempts = 0;

			//check that it's not 0 or near another place
			while ((newloc == Vector2.Zero || isNearOtherPlaces(newloc) == true) && attempts<5)
			{
				Matrix originMatrix = Matrix.CreateScale(1, 1, 1f) *
Matrix.CreateRotationZ(0) *
Matrix.CreateTranslation(new Vector3(originLocation.X, originLocation.Y, 0f));


				//second value is distance
				Vector2 distance = new Vector2(0, rando.Next((int)orbitRange.X, (int)orbitRange.Y));
				//rotation around castle origin
				Matrix distancematrix = Matrix.CreateRotationZ(rando.Next(0, 360)) * originMatrix;
				newloc = Vector2.Transform(distance, distancematrix);

				attempts += 1;
			}

			if (attempts >= 5)
			{
				newloc = Vector2.Zero;
			}

			return newloc;
		}

		//debug - announce creations
		protected void debugAnnounceCreation()
        {

			//announce people
			Console.WriteLine("creating people!");
			for (int i = 0; i < people.Count;++i)
            {
				string locname="";
				foreach (Place p in places)
                {
					if (p.placeID==people[i].placeIDHome)
                    {
						locname = p.placeName;
                    }
                }

				Console.WriteLine(people[i].personFirstName + " " + people[i].personLastName + " is here!\n" +
					"Age: " + people[i].personAge + "\n" +
					"Gender: " + people[i].personGender + "\n" +
					"Home: " + locname + "\n" +
					"Personality Type: " + people[i].personPersonalityType + "\n");
			}


			//announce locations
			Console.WriteLine("creating places!");

			for (int i = 0; i < places.Count; ++i)
			{
				int population = 0;
				foreach (Person p in people)
                {
					if (p.placeIDHome == places[i].placeID)
                    {
						population += 1;
                    }
                }



				//console debug
				Console.WriteLine(places[i].placeName + " is a new place!\n" +
					"Type: " + places[i].thisPlaceType + "\n" + 
					"Population: " + population + "\n");
			}




		}

		//determine roads
		protected void createMajorRoads(Random rando, GameMain gamedeets)
		{
			//do all places
			for (int i = 0; i < places.Count; ++i)
			{
				//get starting place
				if (places[i].isLiveable == true)
				{
					Vector2 starting = places[i].placeLocation;
					for (int i2 = 0; i2 < places.Count; ++i2)
					{
						//get destination
						if (places[i2].isLiveable == true && places[i].placeID != places[i2].placeID && i2 > i &&
							MathHelper.Distance(places[i].placeLocation.X, places[i2].placeLocation.X) < 800 && MathHelper.Distance(places[i].placeLocation.Y, places[i2].placeLocation.Y) < 800)
						{
							Vector2 ending = places[i2].placeLocation;
							Road newRoad = new Road(starting, ending, gamedeets, rando);
							roads.Add(newRoad);
						}


					}
				}

			}

			//road to castle
			for (int i = 0; i < places.Count; ++i)
			{
				//get starting place
				if (places[i].isLiveable == true)
				{
					Vector2 starting = places[i].placeLocation;
					//get destination
					for (int i2 = 0; i2 < places.Count; ++i2)
					{
						if (places[i2].thisPlaceLocationType == Place.PlaceLocationType.Castle && rando.Next(1,3) == 1)
						{
							Vector2 ending = places[i2].placeLocation;
							Road newRoad = new Road(starting, ending, gamedeets, rando);
							roads.Add(newRoad);

						}


					}
				}
			}
		}

		//cleanup colliding orbital locations
		protected void cleanupOrbitalRoadCollision()
        {
			for (int i = 0; i < roads.Count;++i)
            {
				for (int rc=0; rc < roads[i].roadChunks.Count;++rc)
                {
					for (int ol=0; ol < places.Count;++ol)
                    {
						if (places[ol].thisPlaceLocationType == Place.PlaceLocationType.OrbitingNode)
						{
							Rectangle placesrect = new Rectangle((int)(places[ol].placeLocation.X - places[ol].placeCenter.X), (int)(places[ol].placeLocation.Y - places[ol].placeCenter.Y), (int)places[ol].placeSize.X, (int)places[ol].placeSize.Y);
							Rectangle roadrect = roads[i].roadChunks[rc].rect;

							if (placesrect.Intersects(roadrect))
							{
								places.RemoveAt(ol);
							}
						}
                    }
                }
            }

        }



		//create the world
		protected void createNewWorld(GameMain gamedeets, Random rando, DataManager datamanager)
        {
			//create world texture
			worldTexture = gamedeets.Content.Load<Texture2D>("Sprites/World/worldmock");

			//liveable places, for persons
			List<string> liveablePlaces = new List<string>();

			//create terrains
			/*
			for (int i =0; i < 5;++i)
            {
				int randX = rando.Next(-250, 251);
				int randY = rando.Next(-250, 251);
				Terrain newTerrain = new Terrain();
				Vector2 newLocation = new Vector2(randX, randY);
				newTerrain.createNewTerrain(Terrain.TerrainType.Grass, newLocation,gamedeets);
				terrains.Add(newTerrain);
            }
			*/
			//create normal places
			for (int i = 0; i < 5; ++i)
			{
				Place newPlace = new Place();
				Vector2 newLocation = getMajorPlaceLocation(rando);
				newPlace.CreateNewPlace(Place.PlaceType.Town, newLocation, gamedeets,rando);
				places.Add(newPlace);

				//add to liveable places
				liveablePlaces.Add(newPlace.placeID);

				//create orbital locations
				for (int i2 = 0; i2 < rando.Next(2,5); ++i2)
				{
					Place newOrbitalPlace = new Place();
					Vector2 newOrbitalLocation = getOrbitalPlaceLocation(rando, newLocation,new Vector2(110,300));
					if (newOrbitalLocation != Vector2.Zero)
					{
						newOrbitalPlace.CreateNewPlace(newOrbitalPlace.determineOrbitalPlaceType(rando), newOrbitalLocation, gamedeets, rando);
						//don't add if it's too close to the castle
						if (isNearCastle(newOrbitalLocation) == false)
						{
							places.Add(newOrbitalPlace);

						}
					}

				}
			}

			//create castle
			Place newCastle = new Place();
			Vector2 castleLoc = Vector2.Zero;
			newCastle.CreateNewPlace(Place.PlaceType.Castle, castleLoc, gamedeets,rando);
			places.Add(newCastle);


			//create a bunch of people
			for (int i=0;i<rando.Next(90,120);++i)
            {
				Person person = new Person();
				person.CreatePerson(datamanager, rando, Person.CreationType.Created);
				person.assignPersonToHome(liveablePlaces,rando);
				people.Add(person);
            }


			//create roads
			createMajorRoads(rando, gamedeets);
			//cleanup orbital locations that intersect with roads
			cleanupOrbitalRoadCollision();

			//finalization
			debugAnnounceCreation();


			//***GRID
			createGrid(gamedeets,rando);

			//*** END GRID
		}

		void createGrid(GameMain gamedeets,Random rando)
        {
			for (int x=0; x < gridSize.X;++x)
            {
				for (int y=0; y < gridSize.Y; ++y)
                {
					GridTile newgridTile = new GridTile(x, y);
					//terrain
					Terrain newTerrain = new Terrain();
					newTerrain.createNewTerrain(Terrain.TerrainType.Grass, new Vector2(x * newgridTile.gridSize, y * newgridTile.gridSize), gamedeets);
					newgridTile.thisTerrainType = GridTile.GridTerrain.Grass;
					terrains.Add(newTerrain);

					gridTiles.Add(new Point(x, y), newgridTile);

					//gridTiles.Add(newgridTile);
                }
            }

			//add castle
			Place newCastle = new Place();
			int XCenter = (int)(gridSize.X / 2);
			int YCenter = (int)(gridSize.Y/2);

			Vector2 castleLoc = multiplyBy64(new Vector2(XCenter, YCenter));
			newCastle.CreateNewPlace(Place.PlaceType.Castle, castleLoc, gamedeets, rando);
			places.Add(newCastle);

			AssignNodeToGrid(GridTile.GridNode.Castle, XCenter, YCenter,2,2);

			//add town nodes
			for (int i = 0; i < 5; ++i)
			{
				Place newPlace = new Place();
				Vector2 newLocation = getNewGridPlaceLocation(12, castleLoc, rando,4);
				AssignNodeToGrid(GridTile.GridNode.Town, (int)newLocation.X, (int)newLocation.Y, 2, 2);
				newLocation = multiplyBy64(newLocation);

				newPlace.CreateNewPlace(Place.PlaceType.Town, newLocation, gamedeets, rando);
				places.Add(newPlace);

				//create orbital locations
				for (int i2 = 0; i2 < rando.Next(2, 5); ++i2)
				{
					Place newOrbitalPlace = new Place();
					Vector2 newOrbitalLocation = getNewGridPlaceLocation(4, newLocation, rando, 2);
					newOrbitalPlace.CreateNewPlace(newOrbitalPlace.determineOrbitalPlaceType(rando), multiplyBy64(newOrbitalLocation), gamedeets, rando);
					AssignNodeToGrid(GridTile.GridNode.Town, (int)newOrbitalLocation.X, (int)newOrbitalLocation.Y, 1, 1);
					places.Add(newOrbitalPlace);

				}
			}

			//TODO Add Roads

		}

		Boolean isGridItemTooClose(Point location, int distance)
        {
			Boolean tellme = false;

			for (int x = (int)(location.X - distance); x < (int)(location.X + distance); ++x)
			{
				for (int y = (int)(location.Y - distance); y < (int)(location.Y + distance); ++y)
				{
					Point thisLoc = new Point(x, y);
					if (gridTiles[thisLoc].thisNodeType != GridTile.GridNode.None)
                    {
						tellme = true;
						break;
                    }

				}
			}

			return tellme;
        }

		Vector2 multiplyBy64(Vector2 number)
        {
			number.X *= 64;
			number.Y *= 64;

			return number;
        }

		Vector2 divideBy64(Vector2 number)
        {
			number.X /= 64;
			number.Y /= 64;

			return number;
		}

		Vector2 getNewGridPlaceLocation(int maxDistanceFromSource,Vector2 origin,Random rando, int maxDistanceFromNearby)
        {
			List<Vector2> locationCandidates = new List<Vector2>();
			origin = divideBy64(origin);


			for (int x = (int)(origin.X- maxDistanceFromSource); x < (int)(origin.X+ maxDistanceFromSource);++x)
            {
				for (int y = (int)(origin.Y - maxDistanceFromSource); y < (int)(origin.Y + maxDistanceFromSource); ++y)
				{
					Point thisLoc = new Point(x, y);
					if (gridTiles[thisLoc].thisNodeType == GridTile.GridNode.None &&
						isGridItemTooClose(thisLoc, maxDistanceFromNearby) ==false)
                    {
						locationCandidates.Add(new Vector2(x,y));

					}

				}

			}

			Vector2 newloc = locationCandidates[rando.Next(0, locationCandidates.Count)];

			return newloc;
        }


		void AssignNodeToGrid(GridTile.GridNode nodetype, int Xpos, int YPos, int XLength, int YLength)
		{

			Point gridLoc = new Point(Xpos, YPos);

			if (XLength > 1 || YLength > 1)
            {
				for (int x = gridLoc.X; x < gridLoc.X + XLength; ++x)
                {
					for (int y = gridLoc.Y; y < gridLoc.Y + YLength; ++y)
					{
						gridTiles[new Point(x,y)].thisNodeType = nodetype;

					}
				}
			}
			else
            {
				gridTiles[gridLoc].thisNodeType = nodetype;

			}


		}

		public void Update(GameMain gamedeets)
		{
			isDisplayTownAreaFont = false;

			for (int i =0;i<places.Count;++i)
            {
				places[i].Update(gamedeets);
				if (places[i].isColliding==true)
                {
					isDisplayTownAreaFont = true;
                }
            }
			for (int i =0;i < roads.Count;++i)
            {
				roads[i].Update(gamedeets);
            }
		}

		public void Draw(SpriteBatch spriteBatch, SpriteFont displayFont)
		{
			spriteBatch.Draw(worldTexture, new Rectangle(0, 0,(int)worldSize.X,(int)worldSize.Y),null, Color.White,0,new Vector2(128,128), SpriteEffects.None,0);

			for (int i = 0; i < terrains.Count;++i)
            {
				terrains[i].Draw(spriteBatch);
            }
			for (int i = 0; i < roads.Count;++i)
            {
				roads[i].Draw(spriteBatch,displayFont,isDisplayTownAreaFont);
            }
			for (int i =0; i < places.Count;++i)
            {
				places[i].Draw(spriteBatch,displayFont);
            }


		}

	}

}
