using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace The_Mark
{
	class WorldMap
    {
		//enums
		public enum roadDirections { Up, Down, Left, Right }


		//content
		protected List<Terrain> terrains = new List<Terrain>();
		protected List<Place> places = new List<Place>();
		protected List<Person> people = new List<Person>();
		protected List<Road> roads = new List<Road>();


		public Vector2 gridSize = new Vector2(50, 50);
		//List<GridTile> gridTiles = new List<GridTile>();
		Dictionary<Point, GridTile> gridTiles = new Dictionary<Point, GridTile>();


		//assets
		private Texture2D debugGuideTexture;
		protected Texture2D roadTiles;


		//checks
		Boolean isDisplayTownAreaFont = false;

		public WorldMap(GameMain gamedeets,Random rando,DataManager datamanager)
        {
			
			//if new
			createNewWorld(gamedeets,rando,datamanager);

			//load textures
			LoadTextures(gamedeets);
        }

		void LoadTextures(GameMain gamedeets)
        {
			debugGuideTexture = gamedeets.Content.Load<Texture2D>("Sprites/UI/debugGuide");
			roadTiles = gamedeets.Content.Load<Texture2D>("Sprites/Road/road_tiles");
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


			//announce roads
			Console.WriteLine("creating roads!");
			for (int i = 0; i < roads.Count; ++i)
			{
				//console debug
				Console.WriteLine(roads[i].roadName + " is a new road!\n");
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
					Point starting = new Point((int)divideBy64(places[i].placeLocation).X, (int)divideBy64(places[i].placeLocation).Y);
					for (int i2 = 0; i2 < places.Count; ++i2)
					{
						//get destination
						if (places[i2].isLiveable == true && places[i].placeID != places[i2].placeID && i2 > i)
						{
							Point ending = new Point((int)divideBy64(places[i2].placeLocation).X, (int)divideBy64(places[i2].placeLocation).Y);
							pathRoad(starting, ending);
						}


					}
				}

			}

			void pathRoad(Point starting, Point ending)
            {
				Road newRoad = new Road(gamedeets, rando);

				roadDirections thisDirections;
				List<roadDirections> roadOptions = new List<roadDirections>();
				gridTiles[starting].thisRoadType = GridTile.RoadType.Road;
				newRoad.roadChunks.Add(new RoadChunk(multiplyBy64(new Vector2(starting.X,starting.Y))));

				while (starting != ending)
				{
					roadOptions.Clear();
					if (ending.X > starting.X)
						roadOptions.Add(roadDirections.Right);
					if (ending.X < starting.X)
						roadOptions.Add(roadDirections.Left);
					if (ending.Y > starting.Y)
						roadOptions.Add(roadDirections.Down);
					if (ending.Y < starting.Y)
						roadOptions.Add(roadDirections.Up);

					if (roadOptions.Count == 1)
					{
						thisDirections = roadOptions[0];
					}
					else
					{
						thisDirections = roadOptions[rando.Next(0, roadOptions.Count)];
					}


					if (thisDirections == roadDirections.Left)
					{
						starting.X -= 1;
					}
					else if (thisDirections == roadDirections.Right)
					{
						starting.X += 1;
					}
					else if (thisDirections == roadDirections.Down)
					{
						starting.Y += 1;
					}
					else if (thisDirections == roadDirections.Up)
					{
						starting.Y -= 1;
					}

					if (gridTiles[starting].thisRoadType == GridTile.RoadType.Road)
					{
						starting = ending;
					}
					else
					{
						newRoad.roadChunks.Add(new RoadChunk(multiplyBy64(new Vector2(starting.X, starting.Y))));
						gridTiles[starting].thisRoadType = GridTile.RoadType.Road;
					}
                }

				roads.Add(newRoad);
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
								//remove places
								places.RemoveAt(ol);
								//remove place from grid, assumes is 1x1
								Vector2 rectToVector = new Vector2(roadrect.X, roadrect.Y);
								rectToVector = divideBy64(rectToVector);
								gridTiles[new Point((int)rectToVector.X, (int)rectToVector.Y)].thisNodeType = GridTile.GridNode.None;

							}
						}
                    }
                }
            }

        }



		//create the world
		protected void createNewWorld(GameMain gamedeets, Random rando, DataManager datamanager)
        {

			//liveable places, for persons
			List<string> liveablePlaces = new List<string>();

			//create grid
			createGrid(gamedeets, rando);

			//TODO: Add Terrains

			//add castle
			Place newCastle = new Place();
			int XCenter = (int)(gridSize.X / 2);
			int YCenter = (int)(gridSize.Y / 2);
			//create the castle
			Vector2 castleLoc = multiplyBy64(new Vector2(XCenter, YCenter));
			newCastle.CreateNewPlace(Place.PlaceType.Castle, castleLoc, gamedeets, rando);
			places.Add(newCastle);
			//assign castle to grid
			AssignNodeToGrid(GridTile.GridNode.Castle, XCenter, YCenter, 2, 2);


			//add town nodes
			for (int i = 0; i < 5; ++i)
			{
				Place newPlace = new Place();
				Vector2 newLocation = getNewGridPlaceLocation(12, castleLoc, rando, 4);
				AssignNodeToGrid(GridTile.GridNode.Town, (int)newLocation.X, (int)newLocation.Y, 1, 1);
				newLocation = multiplyBy64(newLocation);

				newPlace.CreateNewPlace(Place.PlaceType.Town, newLocation, gamedeets, rando);
				places.Add(newPlace);

				//create orbital locations
				for (int i2 = 0; i2 < rando.Next(2, 5); ++i2)
				{
					Place newOrbitalPlace = new Place();
					Vector2 newOrbitalLocation = getNewGridPlaceLocation(4, newLocation, rando, 2);
					if (newOrbitalLocation != Vector2.Zero)
					{
						newOrbitalPlace.CreateNewPlace(newOrbitalPlace.determineOrbitalPlaceType(rando), multiplyBy64(newOrbitalLocation), gamedeets, rando);
						AssignNodeToGrid(GridTile.GridNode.OrbitalLocation, (int)newOrbitalLocation.X, (int)newOrbitalLocation.Y, 1, 1);
						places.Add(newOrbitalPlace);
					}
				}

				//add to liveable places
				liveablePlaces.Add(newPlace.placeID);
			}


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
			//assign tile graphics to roads
			createTileAssignmentsForRoads();

			//finalization
			debugAnnounceCreation();

		}

		void createTileAssignmentsForRoads()
        {
			foreach (KeyValuePair<Point, GridTile> g in gridTiles)
			{
				if (g.Value.thisRoadType == GridTile.RoadType.Road)
                {
					Point thisRoadLoc = g.Key;
					Vector2 thisLoc = new Vector2(thisRoadLoc.X, thisRoadLoc.Y);
					thisLoc = multiplyBy64(thisLoc);
					int theroad=0;
					int thechunk=0;

					//get the chunk to be assigned
					for (int i =0; i < roads.Count;++i)
                    {
						for (int i2=0; i2 < roads[i].roadChunks.Count;++i2)
                        {
							if (roads[i].roadChunks[i2].rect.X == thisLoc.X &&
								roads[i].roadChunks[i2].rect.Y == thisLoc.Y)
                            {
								theroad = i;
								thechunk = i2;
								break;
                            }
                        }
                    }


					Boolean above = false;
					Boolean below = false;
					Boolean left = false;
					Boolean right = false;

					//check above
					if (gridTiles[new Point(thisRoadLoc.X,thisRoadLoc.Y-1)].thisRoadType == GridTile.RoadType.Road)
                    {
						above = true;
                    }
					//check below
					if (gridTiles[new Point(thisRoadLoc.X, thisRoadLoc.Y + 1)].thisRoadType == GridTile.RoadType.Road)
					{
						below = true;
					}
					//check left
					if (gridTiles[new Point(thisRoadLoc.X-1, thisRoadLoc.Y)].thisRoadType == GridTile.RoadType.Road)
					{
						left = true;
					}
					//check right
					if (gridTiles[new Point(thisRoadLoc.X + 1, thisRoadLoc.Y)].thisRoadType == GridTile.RoadType.Road)
					{
						right = true;
					}


					//left only
					if (above == false && below == false && left == true && right == false)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(256, 64, 64, 64));
					}
					//right only
					if (above == false && below == false && left == false && right == true)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(256, 0, 64, 64));
					}
					//up only
					if (above==true && below==false && left==false && right==false)
                    {
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(256, 128, 64, 64));
                    }
					//down only
					if (above == false && below == true && left == false && right == false)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(192, 128, 64, 64));
					}
					//left and right
					if (above == false && below == false && left == true && right == true)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(64, 0, 64, 64));
					}
					//up and down
					if (above == true && below == true && left == false && right == false)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(0, 0, 64, 64));
					}
					//left up
					if (above == true && below == false && left == true && right == false)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(192, 64, 64, 64));
					}
					//up right
					if (above == true && below == false && left == false && right == true)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(128, 64, 64, 64));
					}
					//right down
					if (above == false && below == true && left == false && right == true)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(128, 0, 64, 64));
					}
					//down left
					if (above == false && below == true && left == true && right == false)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(192, 0, 64, 64));
					}
					//left up right
					if (above == true && below == false && left == true && right == true)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(0, 128, 64, 64));
					}
					//up right down
					if (above == true && below == true && left == false && right == true)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(0, 64, 64, 64));
					}
					//left down right
					if (above == false && below == true && left == true && right == true)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(64, 128, 64, 64));
					}
					//up left down
					if (above == true && below == true && left == true && right == false)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(64, 64, 64, 64));
					}
					//all dir
					if (above == true && below == true && left == true && right == true)
					{
						roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(128, 128, 64, 64));
					}
				}

			}
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

                }
            }

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

			if (locationCandidates.Count > 0)
			{
				Vector2 newloc = locationCandidates[rando.Next(0, locationCandidates.Count)];
				return newloc;
			}
			else
            {
				return Vector2.Zero;
            }
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

			for (int i = 0; i < terrains.Count;++i)
            {
				terrains[i].Draw(spriteBatch);
            }
			for (int i = 0; i < roads.Count;++i)
            {
				roads[i].Draw(spriteBatch,displayFont,isDisplayTownAreaFont,roadTiles);
            }
			for (int i =0; i < places.Count;++i)
            {
				places[i].Draw(spriteBatch,displayFont);
            }

			/*
			foreach (KeyValuePair<Point, GridTile> g in gridTiles)
			{
				Vector2 loc = new Vector2(g.Key.X, g.Key.Y);
				loc = multiplyBy64(loc);
				spriteBatch.Draw(debugGuideTexture, loc, Color.White);
			}
			*/
		}

	}

}
