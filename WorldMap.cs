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
		protected enum roadDirections { Up, Down, Left, Right }
		protected enum riverDirections { Up, Down, Left, Right }

		//content
		protected List<Terrain> terrains = new List<Terrain>();
		protected List<Place> places = new List<Place>();
		protected List<Person> people = new List<Person>();
		protected List<Road> roads = new List<Road>();
		protected List<Water> waterbodies = new List<Water>();
		protected List<Creature> creatures = new List<Creature>();

		//clouds
		protected List<Cloud> clouds = new List<Cloud>();


		public Vector2 gridSize = new Vector2(50, 50);
		Dictionary<Point, GridTile> gridTiles = new Dictionary<Point, GridTile>();

		
		//assets
		protected Texture2D roadTiles;
		protected Texture2D riverTiles;
		//terrains
		protected Texture2D grassTerrainTiles;
		protected Texture2D forestTerrainTiles;
		protected Texture2D beachTerrainTiles;
		//doodads
		protected Texture2D treeTerrainTiles;
		protected Texture2D hillTerrainTiles;
		protected Texture2D beachDoodadTerrainTiles;
		//world elements
		protected Texture2D cloudTiles;


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
			roadTiles = gamedeets.Content.Load<Texture2D>("Sprites/Road/road_tiles");
			riverTiles = gamedeets.Content.Load<Texture2D>("Sprites/Terrain/river_tiles");
			grassTerrainTiles = gamedeets.Content.Load<Texture2D>("Sprites/Terrain/grass_grid_1");
			forestTerrainTiles = gamedeets.Content.Load<Texture2D>("Sprites/Terrain/forest_grid_1");
			treeTerrainTiles = gamedeets.Content.Load<Texture2D>("Sprites/Terrain/tree_grid_1");
			hillTerrainTiles = gamedeets.Content.Load<Texture2D>("Sprites/Terrain/hills_grid_1");
			beachTerrainTiles = gamedeets.Content.Load<Texture2D>("Sprites/Terrain/beach_grid_1");
			beachDoodadTerrainTiles = gamedeets.Content.Load<Texture2D>("Sprites/Terrain/beach_doodad_grid");
			cloudTiles = gamedeets.Content.Load<Texture2D>("Sprites/World/cloud_grid");
		}

		//debug - announce creations
		protected void debugAnnounceCreation()
        {

			Console.WriteLine("creating bodies of water!!");
			for (int i = 0; i < waterbodies.Count;++i)
            {
				Console.WriteLine(waterbodies[i].riverName + " is a body of water!\n" +
	"Type: " + waterbodies[i].thisWaterType + "\n");


			}


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
				int population = getPopulationOfPlace(places[i].placeID);

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
				Console.Write(roads[i].roadName + " is a new road!\n");
			}

			//announce creatures
			Console.WriteLine("creating creatures! coming in hot");
			for (int i = 0; i < creatures.Count; ++i)
			{
				//console debug
				Console.Write("a creature is here! it's a " + creatures[i].thisCreatureType.ToString() + "!\n");
				if (creatures[i].isUnique==true)
                {
					Console.Write("It's unique! Name is " + creatures[i].uniqueCreatureName + "!\n");
				}
			}
		}

		int getPopulationOfPlace(string placeID)
        {
			int pop = 0;

					foreach (Person p in people)
					{
						if (p.placeIDHome == placeID)
						{
							pop += 1;
						}
					}


			return pop;
        }

		//determine roads
		protected void createMajorRoads(Random rando, GameMain gamedeets)
		{
			//do all places
			for (int i = 0; i < places.Count; ++i)
			{
				//get starting place
				if (places[i].isLiveable == true || places[i].thisPlaceLocationType == Place.PlaceLocationType.Castle)
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
				//all grid points, used to undo
				List<Point> allGridPoints = new List<Point>();

				Road newRoad = new Road(gamedeets, rando);
				Boolean abort = false;

				roadDirections thisDirections;
				List<roadDirections> roadOptions = new List<roadDirections>();

				//Initial check, ensures that road doesn't exist in starting location
				if (gridTiles[starting].thisRoadType != GridTile.RoadType.Road)
				{
					gridTiles[starting].thisRoadType = GridTile.RoadType.Road;
					newRoad.roadChunks.Add(new RoadChunk(multiplyBy64(new Vector2(starting.X, starting.Y))));

					allGridPoints.Add(starting);
				}


				while (starting != ending)
				{
					//check for tiles ahead - to avoid crossthatching
					Boolean checkAhead = false;

					roadOptions.Clear();
					if (gridTiles[new Point(starting.X, starting.Y)].thisWaterType == GridTile.WaterType.None)
					{
						if (ending.X > starting.X && 
							(gridTiles[new Point(starting.X + 1, starting.Y)].thisWaterType == GridTile.WaterType.None ||
							(gridTiles[new Point(starting.X + 1, starting.Y)].thisWaterType == GridTile.WaterType.River && gridTiles[new Point(starting.X + 2, starting.Y)].thisWaterType == GridTile.WaterType.None)))
							roadOptions.Add(roadDirections.Right);
						if (ending.X < starting.X && 
							(gridTiles[new Point(starting.X - 1, starting.Y)].thisWaterType == GridTile.WaterType.None ||
							(gridTiles[new Point(starting.X - 1, starting.Y)].thisWaterType == GridTile.WaterType.River && gridTiles[new Point(starting.X - 2, starting.Y)].thisWaterType == GridTile.WaterType.None)))
							roadOptions.Add(roadDirections.Left);
						if (ending.Y > starting.Y &&
							(gridTiles[new Point(starting.X, starting.Y + 1)].thisWaterType == GridTile.WaterType.None ||
							(gridTiles[new Point(starting.X, starting.Y+1)].thisWaterType == GridTile.WaterType.River && gridTiles[new Point(starting.X, starting.Y+2)].thisWaterType == GridTile.WaterType.None)))
							roadOptions.Add(roadDirections.Down);
						if (ending.Y < starting.Y &&
							(gridTiles[new Point(starting.X, starting.Y - 1)].thisWaterType == GridTile.WaterType.None ||
							(gridTiles[new Point(starting.X , starting.Y-1)].thisWaterType == GridTile.WaterType.River && gridTiles[new Point(starting.X, starting.Y-2)].thisWaterType == GridTile.WaterType.None)))
							roadOptions.Add(roadDirections.Up);
					}
					else if (gridTiles[new Point(starting.X, starting.Y)].thisWaterType == GridTile.WaterType.River)
					{
						if (gridTiles[new Point(starting.X - 1, starting.Y)].thisWaterType == GridTile.WaterType.None &&
							gridTiles[new Point(starting.X + 1, starting.Y)].thisRoadType == GridTile.RoadType.Road)
						{
							roadOptions.Add(roadDirections.Left);
						}
						else if (gridTiles[new Point(starting.X + 1, starting.Y)].thisWaterType == GridTile.WaterType.None &&
							gridTiles[new Point(starting.X - 1, starting.Y)].thisRoadType == GridTile.RoadType.Road)
						{
							roadOptions.Add(roadDirections.Right);
						}
						else if (gridTiles[new Point(starting.X, starting.Y-1)].thisWaterType == GridTile.WaterType.None &&
							gridTiles[new Point(starting.X, starting.Y+1)].thisRoadType == GridTile.RoadType.Road)
						{
							roadOptions.Add(roadDirections.Up);
						}
						else if (gridTiles[new Point(starting.X, starting.Y + 1)].thisWaterType == GridTile.WaterType.None &&
							gridTiles[new Point(starting.X, starting.Y-1)].thisRoadType == GridTile.RoadType.Road)
						{
							roadOptions.Add(roadDirections.Down);
						}
					}



					if (roadOptions.Count == 1)
					{
						thisDirections = roadOptions[0];
					}
					else if (roadOptions.Count==0)
                    {
						abort = true;
						break;
                    }
					else
					{
						thisDirections = roadOptions[rando.Next(0, roadOptions.Count)];
					}


					if (thisDirections == roadDirections.Left)
					{
						starting.X -= 1;

                        //check ahead
                        #region check ahead
                        if (gridTiles[new Point(starting.X-1,starting.Y)].thisRoadType == GridTile.RoadType.Road ||
							gridTiles[new Point(starting.X, starting.Y-1)].thisRoadType == GridTile.RoadType.Road ||
							gridTiles[new Point(starting.X, starting.Y+1)].thisRoadType == GridTile.RoadType.Road)
                        {
							checkAhead = true;
                        }
						#endregion
					}
					else if (thisDirections == roadDirections.Right)
					{
						starting.X += 1;

                        //check ahead
                        #region checkahead
                        if (gridTiles[new Point(starting.X + 1, starting.Y)].thisRoadType == GridTile.RoadType.Road ||
							gridTiles[new Point(starting.X, starting.Y - 1)].thisRoadType == GridTile.RoadType.Road ||
							gridTiles[new Point(starting.X, starting.Y + 1)].thisRoadType == GridTile.RoadType.Road)
						{
							checkAhead = true;
						}
						#endregion
					}
					else if (thisDirections == roadDirections.Down)
					{
						starting.Y += 1;

                        //check ahead
                        #region checkahead
                        if (gridTiles[new Point(starting.X - 1, starting.Y)].thisRoadType == GridTile.RoadType.Road ||
							gridTiles[new Point(starting.X + 1, starting.Y)].thisRoadType == GridTile.RoadType.Road ||
							gridTiles[new Point(starting.X, starting.Y + 1)].thisRoadType == GridTile.RoadType.Road)
						{
							checkAhead = true;
						}
						#endregion
					}
					else if (thisDirections == roadDirections.Up)
					{
						starting.Y -= 1;

                        //check ahead
                        #region check ahead
                        if (gridTiles[new Point(starting.X - 1, starting.Y)].thisRoadType == GridTile.RoadType.Road ||
							gridTiles[new Point(starting.X, starting.Y - 1)].thisRoadType == GridTile.RoadType.Road ||
							gridTiles[new Point(starting.X + 1, starting.Y)].thisRoadType == GridTile.RoadType.Road)
						{
							checkAhead = true;
						}
						#endregion
					}

					if (gridTiles[starting].thisRoadType == GridTile.RoadType.Road)
					{
						starting = ending;
					}
					else
					{
						newRoad.roadChunks.Add(new RoadChunk(multiplyBy64(new Vector2(starting.X, starting.Y))));
						gridTiles[starting].thisRoadType = GridTile.RoadType.Road;
						allGridPoints.Add(starting);


						//stop if running in parrallel
						if (checkAhead == true)
						{
							starting = ending;
						}
					}


                }

				if (abort == false)
				{
					roads.Add(newRoad);
				}
				else if (abort==true)
                {
					for (int i =0;i<allGridPoints.Count;++i)
                    {
						gridTiles[allGridPoints[i]].thisRoadType = GridTile.RoadType.None;
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

		void pathRiver(Point starting, Point ending, Random rando,DataManager datamanager)
		{
			Water newRiver = new Water(Water.WaterType.River, datamanager, rando);

			riverDirections thisDirections;
			List<riverDirections> riverOptions = new List<riverDirections>();

			while (starting != ending)
			{

				riverOptions.Clear();
				if (ending.X > starting.X)
					riverOptions.Add(riverDirections.Right);
				if (ending.X < starting.X)
					riverOptions.Add(riverDirections.Left);
				if (ending.Y > starting.Y)
					riverOptions.Add(riverDirections.Down);
				if (ending.Y < starting.Y)
					riverOptions.Add(riverDirections.Up);

				if (riverOptions.Count == 1)
				{
					thisDirections = riverOptions[0];
				}
				else
				{
					thisDirections = riverOptions[rando.Next(0, riverOptions.Count)];
				}

				if (thisDirections == riverDirections.Left)
				{
					starting.X -= 1;

				}
				else if (thisDirections == riverDirections.Right)
				{
					starting.X += 1;

				}
				else if (thisDirections == riverDirections.Down)
				{
					starting.Y += 1;

				}
				else if (thisDirections == riverDirections.Up)
				{
					starting.Y -= 1;

				}

				if ((gridTiles[starting].thisWaterType != GridTile.WaterType.None && newRiver.waterChunks.Count>0) || 
					(starting.X > 22 && starting.X < 28 && starting.Y > 22 && starting.Y < 28))
				{
					starting = ending;
				}
				else if (gridTiles[starting].thisWaterType== GridTile.WaterType.None)
				{
					newRiver.waterChunks.Add(new WaterChunk(multiplyBy64(new Vector2(starting.X, starting.Y))));
					gridTiles[starting].thisWaterType = GridTile.WaterType.River;

				}


			}

				waterbodies.Add(newRiver);
			

			//add child
			/*
			if (newRiver.waterChunks.Count>25 && rando.Next(1,4)==1)
            {
				Point childStarting = new Point();
				Point childEnding = new Point();
				childStarting.X = newRiver.waterChunks[rando.Next(5, newRiver.waterChunks.Count - 10)].rect.X;
				childStarting.Y = newRiver.waterChunks[rando.Next(5, newRiver.waterChunks.Count - 10)].rect.Y;


				//add river ending positions
				for (int x = 0; x < gridSize.X; ++x)
				{
					for (int y = 0; y < gridSize.Y; ++y)
					{
						if (x == 0 || y == 0 || x == gridSize.X - 1 || y == gridSize.Y - 1)
						{
							//riverEndingLocCandidates.Add(new Vector2(x, y));
						}

					}
				}
			}
			*/
		}

		protected void assignTileToForest()
		{
			foreach (KeyValuePair<Point, GridTile> g in gridTiles)
			{
				if (g.Value.thisTerrainType == GridTile.GridTerrain.Forest)
				{
					Point thisForestLoc = g.Key;
					Vector2 thisLoc = new Vector2(thisForestLoc.X, thisForestLoc.Y);
					thisLoc = multiplyBy64(thisLoc);

					for (int i = 0; i < terrains.Count;++i)
                    {
						if (terrains[i].thisTerrainType == Terrain.TerrainType.Forest &&
							terrains[i].returnTerrainLocation() == thisLoc)
                        {
							Boolean above = false;
							Boolean below = false;
							Boolean left = false;
							Boolean right = false;

							//check above
							if (gridTiles[new Point(thisForestLoc.X, thisForestLoc.Y - 1)].thisTerrainType == GridTile.GridTerrain.Forest)
							{
								above = true;
							}
							//check below
							if (gridTiles[new Point(thisForestLoc.X, thisForestLoc.Y + 1)].thisTerrainType == GridTile.GridTerrain.Forest)
							{
								below = true;
							}
							//check left
							if (gridTiles[new Point(thisForestLoc.X - 1, thisForestLoc.Y)].thisTerrainType == GridTile.GridTerrain.Forest)
							{
								left = true;
							}
							//check right
							if (gridTiles[new Point(thisForestLoc.X + 1, thisForestLoc.Y)].thisTerrainType == GridTile.GridTerrain.Forest)
							{
								right = true;
							}


							//left side
							if (above == true && below == true && left == false && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(64, 64));
							}
							//right side
							if (above == true && below == true && left == true && right == false)
							{
								terrains[i].AssignForestTile(new Vector2(192, 64));
							}
							//top side
							if (above == false && below == true && left == true && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(128, 0));
							}
							//bottom side
							if (above == true && below == false && left == true && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(128, 128));
							}
							//top left
							if (above == false && below == true && left == false && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(64, 0));
							}
							//top right
							if (above == false && below == true && left == true && right == false)
							{
								terrains[i].AssignForestTile(new Vector2(192, 0));
							}
							//bottom left
							if (above == true && below == false && left == false && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(64,128));
							}
							//bottom right
							if (above == true && below == false && left == true && right == false)
							{
								terrains[i].AssignForestTile(new Vector2(192, 128));
							}

							//all dirs
							if (above == true && below == true && left == true && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(0, 0));
							}

						}
                    }


				}
			}
		}


		protected void assignTileToBeach()
		{
			foreach (KeyValuePair<Point, GridTile> g in gridTiles)
			{
				if (g.Value.thisTerrainType == GridTile.GridTerrain.Beach)
				{
					Point thisBeachLoc = g.Key;
					Vector2 thisLoc = new Vector2(thisBeachLoc.X, thisBeachLoc.Y);
					thisLoc = multiplyBy64(thisLoc);

					for (int i = 0; i < terrains.Count; ++i)
					{
						if (terrains[i].thisTerrainType == Terrain.TerrainType.Beach &&
							terrains[i].returnTerrainLocation() == thisLoc)
						{
							Boolean above = false;
							Boolean below = false;
							Boolean left = false;
							Boolean right = false;

							//check above
							if (thisBeachLoc.Y==0 ||  gridTiles[new Point(thisBeachLoc.X, thisBeachLoc.Y - 1)].thisTerrainType == GridTile.GridTerrain.Beach)
							{
								above = true;
							}
							//check below
							if (thisBeachLoc.Y==49 || gridTiles[new Point(thisBeachLoc.X, thisBeachLoc.Y + 1)].thisTerrainType == GridTile.GridTerrain.Beach)
							{
								below = true;
							}
							//check left
							if (thisBeachLoc.X==0 ||  gridTiles[new Point(thisBeachLoc.X - 1, thisBeachLoc.Y)].thisTerrainType == GridTile.GridTerrain.Beach)
							{
								left = true;
							}
							//check right
							if (thisBeachLoc.Y==49 || gridTiles[new Point(thisBeachLoc.X + 1, thisBeachLoc.Y)].thisTerrainType == GridTile.GridTerrain.Beach)
							{
								right = true;
							}


							//left side
							if (above == true && below == true && left == false && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(64, 64));
							}
							//right side
							if (above == true && below == true && left == true && right == false)
							{
								terrains[i].AssignForestTile(new Vector2(192, 64));
							}
							//top side
							if (above == false && below == true && left == true && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(128, 0));
							}
							//bottom side
							if (above == true && below == false && left == true && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(128, 128));
							}
							//top left
							if (above == false && below == true && left == false && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(64, 0));
							}
							//top right
							if (above == false && below == true && left == true && right == false)
							{
								terrains[i].AssignForestTile(new Vector2(192, 0));
							}
							//bottom left
							if (above == true && below == false && left == false && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(64, 128));
							}
							//bottom right
							if (above == true && below == false && left == true && right == false)
							{
								terrains[i].AssignForestTile(new Vector2(192, 128));
							}

							//all dirs
							if (above == true && below == true && left == true && right == true)
							{
								terrains[i].AssignForestTile(new Vector2(0, 0));
							}

						}
					}


				}
			}
		}


		Boolean terrainGridCollidesWith(GridTile.GridTerrain checkForTileType, Point startingPoint, int size)
        {
			for (int x=startingPoint.X;x<startingPoint.X+size;++x)
            {
				for (int y =startingPoint.Y;y<startingPoint.Y+size;++y)
                {
					if (gridTiles[new Point(x,y)].thisTerrainType == checkForTileType)
                    {
						return true;
                    }
                }
            }




			return false;
        }

		protected void createHills(DataManager datamanager, Random rando)
        {
			int numberofhills = rando.Next(1, 4);
			for (int i = 0; i < numberofhills; ++i)
			{
				int hillssize = rando.Next(2, 6);
				Vector2 startingArea;
				startingArea.X = rando.Next(6, (int)gridSize.X - 6);
				startingArea.Y = rando.Next(6, (int)gridSize.Y - 6);

				startingArea = new Vector2(startingArea.X - hillssize, startingArea.Y - hillssize);

				if (terrainGridCollidesWith(GridTile.GridTerrain.Forest, new Point((int)startingArea.X, (int)startingArea.Y), hillssize) == false &&
					terrainGridCollidesWith(GridTile.GridTerrain.Beach, new Point((int)startingArea.X, (int)startingArea.Y), hillssize) == false)
                {
						for (int x = (int)startingArea.X; x < (int)(startingArea.X + hillssize); ++x)
						{
							for (int y = (int)startingArea.Y; y < (int)(startingArea.Y + hillssize); ++y)
							{
								//find existing terrain
								for (int t = 0; t < terrains.Count; ++t)
								{
									if (terrains[t].returnTerrainLocation() == multiplyBy64(new Vector2(x, y)))
									{
										Boolean isWaterOrRoads = true;
										if (gridTiles[new Point(x, y)].thisWaterType == GridTile.WaterType.None && gridTiles[new Point(x, y)].thisRoadType == GridTile.RoadType.None)
										{
											isWaterOrRoads = false;
										}

										terrains[t].createNewTerrain(Terrain.TerrainType.Hills, multiplyBy64(new Vector2(x, y)), rando, isWaterOrRoads);
										gridTiles[new Point(x, y)].thisTerrainType = GridTile.GridTerrain.Hills;
										break;
									}
								}



							}
						}

                }

			}

		}


		protected void createBeaches(DataManager datamanager, Random rando,Point starting,int size)
		{


				for (int x = starting.X; x < (starting.X + size); ++x)
				{
					for (int y = starting.Y; y < (starting.Y + size); ++y)
					{

						//find existing terrain
						for (int t = 0; t < terrains.Count; ++t)
						{
							if (terrains[t].returnTerrainLocation() == multiplyBy64(new Vector2(x, y)))
							{
								Boolean isWaterOrRoads = true;
								if (gridTiles[new Point(x, y)].thisWaterType == GridTile.WaterType.None && gridTiles[new Point(x, y)].thisRoadType == GridTile.RoadType.None)
								{
									isWaterOrRoads = false;
								}

							terrains[t].createNewTerrain(Terrain.TerrainType.Beach, multiplyBy64(new Vector2(x, y)), rando, isWaterOrRoads);
								gridTiles[new Point(x, y)].thisTerrainType = GridTile.GridTerrain.Beach;
								break;
							}
						}

					}
				}


			assignTileToBeach();

		}



		protected void createForests(DataManager datamanager, Random rando)
		{
			int numberofforests = rando.Next(1, 5);
			for (int i = 0; i < numberofforests; ++i)
			{
				int forestsize = rando.Next(3, 8);
				Vector2 startingArea;
				startingArea.X = rando.Next(9, (int)gridSize.X - 10);
				startingArea.Y = rando.Next(9, (int)gridSize.Y - 10);

				startingArea = new Vector2(startingArea.X - forestsize, startingArea.Y - forestsize);

				if (terrainGridCollidesWith(GridTile.GridTerrain.Beach, new Point((int)startingArea.X, (int)startingArea.Y), forestsize) == false)
				{

					for (int x = (int)startingArea.X; x < (int)(startingArea.X + forestsize); ++x)
					{
						for (int y = (int)startingArea.Y; y < (int)(startingArea.Y + forestsize); ++y)
						{

							//find existing terrain
							for (int t = 0; t < terrains.Count; ++t)
							{
								if (terrains[t].returnTerrainLocation() == multiplyBy64(new Vector2(x, y)))
								{
									Boolean isWaterOrRoads = true;
									if (gridTiles[new Point(x, y)].thisWaterType == GridTile.WaterType.None && gridTiles[new Point(x, y)].thisRoadType == GridTile.RoadType.None)
									{
										isWaterOrRoads = false;
									}

									terrains[t].createNewTerrain(Terrain.TerrainType.Forest, multiplyBy64(new Vector2(x, y)), rando, isWaterOrRoads);
									gridTiles[new Point(x, y)].thisTerrainType = GridTile.GridTerrain.Forest;
									break;
								}
							}


						}
					}
				}
			}

			assignTileToForest();

		}


		protected void createLakes(DataManager datamanager, Random rando)
        {
			int numberoflakes = rando.Next(1, 4);

			for (int i =0; i < numberoflakes;++i)
            {
				Boolean isCollidingWithABadThing = false;
				List<Point> riverGridPoints = new List<Point>();

				int lakesize = rando.Next(2, 5);
				Vector2 startingArea;
				startingArea.X = rando.Next(5, (int)gridSize.X - 5);
				startingArea.Y = rando.Next(5, (int)gridSize.Y - 5);

				startingArea = new Vector2(startingArea.X - lakesize, startingArea.Y - lakesize);


				//river location options
				List<Vector2> riverStartingLocCandidates = new List<Vector2>();
				List<Vector2> riverEndingLocCandidates = new List<Vector2>();


				Water newLake = new Water(Water.WaterType.Lake, datamanager, rando);

				for (int x = (int)startingArea.X; x < (int)(startingArea.X + lakesize);++x)
                {
					for (int y = (int)startingArea.Y; y < (int)(startingArea.Y + lakesize); ++y)
					{
						if (gridTiles[new Point(x, y)].thisWaterType != GridTile.WaterType.None)
                        {
							isCollidingWithABadThing = true;
                        }
						else if ((x > 22 && x < 28) && (y > 22 && y < 28))
                        {
							isCollidingWithABadThing = true;
                        }

						//add potential river starting locations
						riverStartingLocCandidates.Add(new Vector2(x, y));

						//int randodestruction = rando.Next(1, 12);
						//if (randodestruction != 1)
						//{
							Vector2 position = multiplyBy64(new Vector2(x, y));
							newLake.waterChunks.Add(new WaterChunk(position));
							gridTiles[new Point(x, y)].thisWaterType = GridTile.WaterType.Lake;
						riverGridPoints.Add(new Point(x, y));
						//}
					}
				}


				if (isCollidingWithABadThing == false)
				{
					waterbodies.Add(newLake);
					//create beaches
					if (rando.Next(1, 4) == 1)
					{
						createBeaches(datamanager, rando, new Point((int)(startingArea.X - 1), (int)(startingArea.Y - 1)), lakesize + 2);
					}

					//add river ending positions
					for (int x = 0; x < gridSize.X; ++x)
					{
						for (int y = 0; y < gridSize.Y; ++y)
						{
							if (x == 0 || y == 0 || x == gridSize.X - 1 || y == gridSize.Y - 1)
							{
								riverEndingLocCandidates.Add(new Vector2(x, y));
							}

						}
					}

					Vector2 actualRiverStarting = riverStartingLocCandidates[rando.Next(0, riverStartingLocCandidates.Count)];
					Vector2 actualRiverEnding = riverEndingLocCandidates[rando.Next(0, riverEndingLocCandidates.Count)];



					pathRiver(new Point((int)actualRiverStarting.X, (int)actualRiverStarting.Y), new Point((int)actualRiverEnding.X, (int)actualRiverEnding.Y), rando, datamanager);

				}
				else if (isCollidingWithABadThing==true)
                {
					for (int i2 =0;i2 < riverGridPoints.Count;++i2)
                    {
						gridTiles[riverGridPoints[i2]].thisWaterType = GridTile.WaterType.None;

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


            //bodies of water
            #region water
            //add lakes and rivers
            createLakes(datamanager, rando);
			//assign graphics to water tiles
			createTileAssignmentsforRiversandLakes();
            #endregion


            //Add Terrains
            #region Terrain
            //Add Forests
            createForests(datamanager, rando);
			//Add Hills
			createHills(datamanager, rando);



            #endregion

            //places
            #region places
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
				Vector2 newLocation = getNewGridPlaceLocation(14, castleLoc, rando, 4);
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
						newOrbitalPlace.CreateNewPlace(newOrbitalPlace.determineOrbitalPlaceType(rando, gridTiles[new Point((int)newOrbitalLocation.X, (int)newOrbitalLocation.Y)].thisTerrainType), multiplyBy64(newOrbitalLocation), gamedeets, rando);
						AssignNodeToGrid(GridTile.GridNode.OrbitalLocation, (int)newOrbitalLocation.X, (int)newOrbitalLocation.Y, 1, 1);
						places.Add(newOrbitalPlace);
					}
				}

				//add to liveable places
				liveablePlaces.Add(newPlace.placeID);
			}

            #endregion

            //people
            #region people
            //create a bunch of people
            for (int i=0;i<rando.Next(90,120);++i)
            {
				Person person = new Person();
				person.CreatePerson(datamanager, rando, Person.CreationType.Created);
				person.assignPersonToHome(liveablePlaces,rando);
				people.Add(person);
            }
            #endregion

            //create a bunch of creatures based on terrains and places
            #region creatures
            lookForCreatureAvailability(rando,datamanager);

            #endregion

            //create roads
            #region roads
            createMajorRoads(rando, gamedeets);
			//cleanup orbital locations that intersect with roads
			cleanupOrbitalRoadCollision();
			//assign tile graphics to roads
			createTileAssignmentsForRoads();
            #endregion

            //finalization
            debugAnnounceCreation();

		}

		void lookForCreatureAvailability(Random rando,DataManager datamanager)
        {
			Boolean isBeachAvailable = false;
			Boolean isHillsAvailable = false;
			Boolean isForestAvailable = false;

			//terrain availability
			for (int i=0;i<terrains.Count;++i)
            {
				if (terrains[i].thisTerrainType == Terrain.TerrainType.Beach)
				{
					isBeachAvailable = true;
				}
				else if (terrains[i].thisTerrainType == Terrain.TerrainType.Forest)
				{
					isForestAvailable = true;
				}
				else if (terrains[i].thisTerrainType == Terrain.TerrainType.Hills)
				{
					isHillsAvailable = true;
				}
			}

            //FLAP FLAP
            #region flap flap
            List<string> availableLocationIDs = new List<string>();
			int max = 0;
			for (int i = 0; i <places.Count;++i)
            {
				if (places[i].thisPlaceType == Place.PlaceType.Cave ||
					places[i].thisPlaceType == Place.PlaceType.Graveyard)
                {
					availableLocationIDs.Add(places[i].placeID);
                }
            }

			if (availableLocationIDs.Count>0)
            {
				max = rando.Next(0, availableLocationIDs.Count * 10);
            }

			//create and assign creatures
			for (int i =0; i < max; ++i)
            {
				Creature newCreature = new Creature(Creature.ThisCreatureType.FlapFlap, datamanager,rando);
				newCreature.placeIDHome = availableLocationIDs[rando.Next(0, availableLocationIDs.Count)];
				creatures.Add(newCreature);
            }

			max = 0;
			availableLocationIDs.Clear();

			#endregion

			//BIRB
			#region birb
			for (int i = 0; i < places.Count; ++i)
			{
				if (places[i].thisPlaceType == Place.PlaceType.Ruins)
				{
					availableLocationIDs.Add(places[i].placeID);
				}
			}

			if (availableLocationIDs.Count > 0)
			{
				max = rando.Next(0, availableLocationIDs.Count * 10);
			}

			//create and assign creatures
			for (int i = 0; i < max; ++i)
			{
				int randTerrainOrPlaceCheck = rando.Next(1, 5);
				if (randTerrainOrPlaceCheck == 1)
				{
					Creature newCreature = new Creature(Creature.ThisCreatureType.Birb, datamanager, rando);
					newCreature.placeIDHome = availableLocationIDs[rando.Next(0, availableLocationIDs.Count)];
					creatures.Add(newCreature);
				}
				else if (randTerrainOrPlaceCheck == 2)
                {
					Creature newCreature = new Creature(Creature.ThisCreatureType.Birb, datamanager, rando);
					newCreature.terrainTypeHome = Terrain.TerrainType.Grass;
					creatures.Add(newCreature);
				}
				else if (randTerrainOrPlaceCheck == 3 && isForestAvailable==true)
				{
					Creature newCreature = new Creature(Creature.ThisCreatureType.Birb, datamanager, rando);
					newCreature.terrainTypeHome = Terrain.TerrainType.Forest;
					creatures.Add(newCreature);
				}
				else if (randTerrainOrPlaceCheck == 4 && isBeachAvailable == true)
				{
					Creature newCreature = new Creature(Creature.ThisCreatureType.Birb, datamanager, rando);
					newCreature.terrainTypeHome = Terrain.TerrainType.Beach;
					creatures.Add(newCreature);
				}
			}

			max = 0;
			availableLocationIDs.Clear();



			#endregion

			//LEGGY
			#region leggy
			for (int i = 0; i < places.Count; ++i)
			{
				if (places[i].thisPlaceType == Place.PlaceType.Ruins)
				{
					availableLocationIDs.Add(places[i].placeID);
				}
			}

			if (availableLocationIDs.Count > 0)
			{
				max = rando.Next(0, availableLocationIDs.Count * 10);
			}

			//create and assign creatures
			for (int i = 0; i < max; ++i)
			{
				int randTerrainOrPlaceCheck = rando.Next(1, 3);
				if (randTerrainOrPlaceCheck == 1)
				{
					Creature newCreature = new Creature(Creature.ThisCreatureType.Leggy, datamanager, rando);
					newCreature.placeIDHome = availableLocationIDs[rando.Next(0, availableLocationIDs.Count)];
					creatures.Add(newCreature);
				}
				else if (randTerrainOrPlaceCheck == 2 && isForestAvailable==true)
				{
					Creature newCreature = new Creature(Creature.ThisCreatureType.Birb, datamanager, rando);
					newCreature.terrainTypeHome = Terrain.TerrainType.Forest;
					creatures.Add(newCreature);
				}
			}



			max = 0;
			availableLocationIDs.Clear();
			#endregion

			//STINKHORN
			#region stinkhorn

			max = rando.Next(1,10);


			//create and assign creatures
			for (int i = 0; i < max; ++i)
			{
				int randTerrainOrPlaceCheck = rando.Next(1, 3);
				if (randTerrainOrPlaceCheck == 1 && isHillsAvailable==true)
				{
					Creature newCreature = new Creature(Creature.ThisCreatureType.Stinkhorn, datamanager, rando);
					newCreature.terrainTypeHome = Terrain.TerrainType.Hills;
					creatures.Add(newCreature);
				}
				else if (randTerrainOrPlaceCheck == 2)
				{
					Creature newCreature = new Creature(Creature.ThisCreatureType.Stinkhorn, datamanager, rando);
					newCreature.terrainTypeHome = Terrain.TerrainType.Grass;
					creatures.Add(newCreature);
				}
			}



			max = 0;
			availableLocationIDs.Clear();


			#endregion
		}

        void createTileAssignmentsforRiversandLakes()
		{
			foreach (KeyValuePair<Point, GridTile> g in gridTiles)
			{
				if (g.Value.thisWaterType != GridTile.WaterType.None && waterbodies.Count>0)
                {
					Point thisWaterLoc = g.Key;
					Vector2 thisLoc = new Vector2(thisWaterLoc.X, thisWaterLoc.Y);
					thisLoc = multiplyBy64(thisLoc);
					int thewater = 0;
					int thechunk = 0;

					//get the chunk to be assigned
					for (int i = 0; i < waterbodies.Count; ++i)
					{
						for (int i2 = 0; i2 < waterbodies[i].waterChunks.Count; ++i2)
						{
							if (waterbodies[i].waterChunks[i2].rect.X == thisLoc.X &&
								waterbodies[i].waterChunks[i2].rect.Y == thisLoc.Y)
							{
								thewater = i;
								thechunk = i2;
								break;
							}
						}
					}

					Boolean above = false;
					Boolean below = false;
					Boolean left = false;
					Boolean right = false;
					Boolean topright = false;
					Boolean topleft = false;
					Boolean bottomright = false;
					Boolean bottomleft = false;


					//check above
					if (thisWaterLoc.Y == 0 || gridTiles[new Point(thisWaterLoc.X, thisWaterLoc.Y - 1)].thisWaterType != GridTile.WaterType.None)
					{
						above = true;
					}
					//check below
					if (thisWaterLoc.Y == 49 || gridTiles[new Point(thisWaterLoc.X, thisWaterLoc.Y + 1)].thisWaterType != GridTile.WaterType.None)
					{
						below = true;
					}
					//check left
					if (thisWaterLoc.X == 0 || gridTiles[new Point(thisWaterLoc.X - 1, thisWaterLoc.Y)].thisWaterType != GridTile.WaterType.None)
					{
						left = true;
					}
					//check right
					if (thisWaterLoc.X == 49 ||  gridTiles[new Point(thisWaterLoc.X + 1, thisWaterLoc.Y)].thisWaterType != GridTile.WaterType.None)
					{
						right = true;
					}
					//check topright
					if ((thisWaterLoc.Y > 0 && thisWaterLoc.Y<49 && thisWaterLoc.X>0 && thisWaterLoc.X < 49) && gridTiles[new Point(thisWaterLoc.X+1, thisWaterLoc.Y - 1)].thisWaterType != GridTile.WaterType.None)
					{
						topright = true;
					}
					//check topleft
					if ((thisWaterLoc.Y > 0 && thisWaterLoc.Y < 49 && thisWaterLoc.X > 0 && thisWaterLoc.X < 49) && gridTiles[new Point(thisWaterLoc.X - 1, thisWaterLoc.Y - 1)].thisWaterType != GridTile.WaterType.None)
					{
						topleft = true;
					}
					//check bottomright
					if ((thisWaterLoc.Y > 0 && thisWaterLoc.Y < 49 && thisWaterLoc.X > 0 && thisWaterLoc.X < 49) && gridTiles[new Point(thisWaterLoc.X + 1, thisWaterLoc.Y + 1)].thisWaterType != GridTile.WaterType.None)
					{
						bottomright = true;
					}
					//check bottomleft
					if ((thisWaterLoc.Y > 0 && thisWaterLoc.Y < 49 && thisWaterLoc.X > 0 && thisWaterLoc.X < 49) && gridTiles[new Point(thisWaterLoc.X - 1, thisWaterLoc.Y + 1)].thisWaterType != GridTile.WaterType.None)
					{
						bottomleft = true;
					}




					//left only
					if (above == false && below == false && left == true && right == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(256, 64, 64, 64));
					}
					//right only
					if (above == false && below == false && left == false && right == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(256, 0, 64, 64));
					}
					//up only
					if (above == true && below == false && left == false && right == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(256, 128, 64, 64));
					}
					//down only
					if (above == false && below == true && left == false && right == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(192, 128, 64, 64));
					}
					//left and right
					if (above == false && below == false && left == true && right == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(64, 0, 64, 64));
					}
					//up and down
					if (above == true && below == true && left == false && right == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(0, 0, 64, 64));
					}
					//left up
					if (above == true && below == false && left == true && right == false && topleft==false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(192, 64, 64, 64));
					}
					//up right
					if (above == true && below == false && left == false && right == true && topright==false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(128, 64, 64, 64));
					}
					//right down
					if (above == false && below == true && left == false && right == true && bottomright==false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(128, 0, 64, 64));
					}
					//down left
					if (above == false && below == true && left == true && right == false && bottomleft == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(192, 0, 64, 64));
					}
					//left up + topleft
					if (above == true && below == false && left == true && right == false && topleft==true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(192, 256, 64, 64));
					}

					//up right + topright
					if (above == true && below == false && left == false && right == true && topright == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(64, 256, 64, 64));
					}

					//right down + bottomright
					if (above == false && below == true && left == false && right == true && bottomright == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(64, 192, 64, 64));
					}

					//down left + bottomleft
					if (above == false && below == true && left == true && right == false && bottomleft == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(192, 192, 64, 64));
					}
					//left up right
					if (above == true && below == false && left == true && right == true && topleft==false && topright==false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(0, 128, 64, 64));
					}
					//up right down
					if (above == true && below == true && left == false && right == true && topright==false && bottomright==false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(0, 64, 64, 64));
					}
					//left down right
					if (above == false && below == true && left == true && right == true && bottomleft==false && bottomright==false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(64, 128, 64, 64));
					}
					//up left down
					if (above == true && below == true && left == true && right == false && topleft==false && bottomleft==false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(64, 64, 64, 64));
					}

					//rightmost edge
					if (above == true && below == true && left == true && right == false && topleft==true && bottomleft==true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(256, 192, 64, 64));
					}
					//leftmost edge
					if (above == true && below == true && left == false && right == true && topright == true && bottomright == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(256, 256, 64, 64));
					}
					//topmost edge
					if (above == false && below == true && left == true && right == true && bottomright == true && bottomleft == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(128, 192, 64, 64));
					}
					//bottommost edge
					if (above == true && below == false && left == true && right == true && topright == true && topleft == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(128, 256, 64, 64));
					}

					//river mouths
					//above
					if (above == true && below == true && left == true && right == true && topleft == false && topright == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(64, 320, 64, 64));
					}
					//below
					if (above == true && below == true && left == true && right == true && bottomleft == false && bottomright == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(64, 448, 64, 64));
					}
					//left
					if (above == true && below == true && left == true && right == true && topleft == false && bottomleft == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(0, 384, 64, 64));
					}
					//right
					if (above == true && below == true && left == true && right == true && topright == false && bottomright == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(128, 384, 64, 64));
					}
					//top left go left
					if (above == false && below == true && left == true && right == true && bottomleft == false && bottomright==true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(0, 320, 64, 64));
					}
					//top left go up
					if (above == true && below == true && left == false && right == true  && topright == false && bottomright == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(192, 320, 64, 64));
					}

					//top right go right
					if (above == false && below == true && left == true && right == true && bottomleft == true && bottomright == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(128, 320, 64, 64));
					}
					//top right go up
					if (above == true && below == true && left == true && right == false && bottomleft == true && topleft==false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(256, 320, 64, 64));
					}

					//bottom left go left
					if (above == true && below == false && left == true && right == true  && topleft == false && topright == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(0, 448, 64, 64));
					}
					//bottom left go down
					if (above == true && below == true && left == false && right == true && topright == true && bottomright == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(192, 384, 64, 64));
					}



					//bottom right go right
					if (above == true && below == false && left == true && right == true  && topleft == true && topright == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(128, 448, 64, 64));
					}
					//bottom right go down
					if (above == true && below == true && left == true && right == false && bottomleft == false && topleft == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(256, 384, 64, 64));
					}



					//diagnals
					//only bottom right
					if (above == true && below == true && left == true && right == true && bottomleft == true && topleft == true && topright == true && bottomright==false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(192, 448, 64, 64));
					}
					//only bottom left
					if (above == true && below == true && left == true && right == true && bottomleft == false && topleft == true && topright == true && bottomright == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(256, 448, 64, 64));
					}
					//only top right
					if (above == true && below == true && left == true && right == true && bottomleft == true && topleft == true && topright == false && bottomright == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(192, 512, 64, 64));
					}
					//only top left
					if (above == true && below == true && left == true && right == true && bottomleft == true && topleft == false && topright == true && bottomright == true)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(256, 512, 64, 64));
					}

					//all dir
					//all dir/forked
					if (above == true && below == true && left == true && right == true && (topleft==false && topright==false && bottomleft==false && bottomright==false))
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(128, 128, 64, 64));
					}
					//all dir/deep
					if (above == true && below == true && left == true && right == true && (topleft == true && topright == true && bottomleft == true && bottomright == true))
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(0, 192, 64, 64));
					}
					//no dir
					if (above == false && below == false && left == false && right == false)
					{
						waterbodies[thewater].waterChunks[thechunk].AssignTile(new Rectangle(0, 256, 64, 64));
					}






				}
				
			}
		}

        void createTileAssignmentsForRoads()
        {
			foreach (KeyValuePair<Point, GridTile> g in gridTiles)
			{
				if (g.Value.thisRoadType == GridTile.RoadType.Road && roads.Count>0)
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

					//bridges
					if (g.Value.thisWaterType == GridTile.WaterType.River)
                    {
						//up down bridge
						if (above == false && below == false && left == true && right == true)
						{
							roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(0, 192, 64, 64));
						}
						//left right bridge
						if (above == true && below == true && left == false && right == false)
						{
							roads[theroad].roadChunks[thechunk].AssignTile(new Rectangle(64, 192, 64, 64));
						}
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
					newTerrain.createNewTerrain(Terrain.TerrainType.Grass, multiplyBy64(new Vector2(x,y)),rando,true);
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
						gridTiles[thisLoc].thisWaterType == GridTile.WaterType.None &&
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

		public void Update(GameMain gamedeets,Random rando)
		{
			isDisplayTownAreaFont = false;
			String placeCollision = "";
			String roadCollision = "";
			String waterCollision = "";

			for (int i =0;i<places.Count;++i)
            {
				places[i].Update(gamedeets);
				if (places[i].isColliding==true)
                {
					isDisplayTownAreaFont = true;
					placeCollision = places[i].placeName;
                }
            }
			for (int i =0;i < roads.Count;++i)
            {
				roads[i].Update(gamedeets);
				if (roads[i].isColliding==true)
                {
					roadCollision = roads[i].roadName;
                }
            }
			for (int i = 0; i < waterbodies.Count; ++i)
			{
				waterbodies[i].Update(gamedeets);
				if (waterbodies[i].isColliding==true)
                {
					waterCollision = waterbodies[i].riverName;
                }
			}


			//clouds
			for (int i =0; i < clouds.Count;++i)
            {
				clouds[i].Update();
				if (clouds[i].position.X < -1000 && clouds[i].position.Y<-1000)
                {
					clouds.RemoveAt(i);
					i -= 1;
                }
            }
			addNewClouds(rando);

			gamedeets.mouse.placeText = placeCollision;
			gamedeets.mouse.roadText = roadCollision;
			gamedeets.mouse.waterText = waterCollision;

		}

		void addNewClouds(Random rando)
        {
			if (rando.Next(1,950)==1)
            {
				clouds.Add(new Cloud(rando));
            }
        }

		Texture2D returnDoodadTexture(Terrain.TerrainType thistype)
        {
			if (thistype== Terrain.TerrainType.Forest)
            {
				return treeTerrainTiles;
            }
			else if (thistype == Terrain.TerrainType.Hills)
            {
				return hillTerrainTiles;
            }
			else if (thistype == Terrain.TerrainType.Beach)
            {
				return beachDoodadTerrainTiles;
            }
			else
            {
				return null; 
            }
        }

		public void Draw(SpriteBatch spriteBatch, SpriteFont displayFont)
		{
			//draw terrain
			for (int i = 0; i < terrains.Count;++i)
            {
				terrains[i].Draw(spriteBatch,grassTerrainTiles,forestTerrainTiles,beachTerrainTiles);
			}
			//draw terrain doodads
			for (int i = 0; i < terrains.Count; ++i)
			{
				terrains[i].DrawDoodads(spriteBatch, returnDoodadTexture(terrains[i].thisTerrainType));
			}
			//draw water
			for (int i = 0; i < waterbodies.Count; ++i)
			{
				waterbodies[i].Draw(spriteBatch, riverTiles, displayFont, isDisplayTownAreaFont);
			}
			//draw roads
			for (int i = 0; i < roads.Count;++i)
            {
				roads[i].Draw(spriteBatch,displayFont,isDisplayTownAreaFont,roadTiles);
            }

			//draw places
			for (int i =0; i < places.Count;++i)
            {
				places[i].Draw(spriteBatch,displayFont);
            }

			//draw clouds
			for (int i = 0; i < clouds.Count;++i)
            {
				clouds[i].Draw(spriteBatch, cloudTiles);
            }


		}

	}

}
