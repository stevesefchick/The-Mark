﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace The_Mark
{
    class TravelHandler
    {
        //location and travel
        public Point currentGridLocation;
        public String currentLocationText;
        public String currentLocationDescription;
        public Point destinationLocation;
        public Boolean displayDestination = false;
        public String currentDestinationText;
        public String currentDestinationDescription;
        public Point travelStartingLocation;
        int safeTilesRemaining = 3;
        int restSafeTilesRemaining = 3;

        //events
        Event currentEvent;
        WorldEvent currentWorldEvent;
        Event currentWakeEvent;

        //paths
        public List<TravelRoute> travelPath = new List<TravelRoute>();

        //travelling animation
        Vector2 currentTravelDistance = Vector2.Zero;
        Vector2 totalTravelDistance = Vector2.Zero;

        //const
        const int mapIconOffset = -32;
        const int worldEventSafeTiles = 5;
        const int passiveEventSafeTiles = 3;
        const int restEventSafeTiles = 3;

        public TravelHandler()
        {


        }

        public String GetWorldEventCreatureName()
        {
            return currentWorldEvent.GetAssociatedCreatureName();
        }

        void UpdateCurrentLocation(WorldMap world)
        {
            currentLocationText = world.returnCurrentLocNameDescription(currentGridLocation).Item1;
            currentLocationDescription = world.returnCurrentLocNameDescription(currentGridLocation).Item2;
        }

        void createDestinationStrings(WorldMap world)
        {
            currentDestinationText = world.returnCurrentLocNameDescription(destinationLocation).Item1;
            currentDestinationDescription = world.returnCurrentLocNameDescription(destinationLocation).Item2;
        }



        Event CheckIfPeopleEligible(Event e,PlayerHandler player)
        {

            //check the mark
            e.IsPersonEligible(player.theMark);


            //check the party
            if (player.partyMembers.Count > 0)
            {
                for (int i = 0; i < player.partyMembers.Count; ++i)
                {
                    e.IsPersonEligible(player.partyMembers[i]);
                }
            }

            return (e);
        }

        void RestSafeTick()
        {
            if (restSafeTilesRemaining>0)
            {
                restSafeTilesRemaining -= 1;
            }
        }

        void RestSafeSet()
        {
            restSafeTilesRemaining = restEventSafeTiles;
        }

        public void CheckForValidSleepEvents(PlayerHandler player, DataManager datamanager, Random rando, GridTile.GridTerrain terraintype, Boolean isonroad, WorldMap world, int hour, UI_Helper uihelper)
        {
            List<Event> possibleEvents = new List<Event>();
            RestSafeTick();

            //check for valid sleep events
            foreach (KeyValuePair<String,Event> e in datamanager.passiveEventData)
            {
                //check if this is a sleep specific event
                if ((e.Value.CanOccurWhenSleeping()==true && e.Value.CanOccurWhenSleeping() == false) 
                    || (e.Value.OccursWhenWakingUp() == true && currentWakeEvent==null))
                {
                    //add any eligible people to event
                    Event newevent = CheckIfPeopleEligible(e.Value, player);
                    //determine the text used
                    newevent.DetermineValidText(rando);
                    //if eligible people exist, add to event
                    if (newevent.IsEligibleExists() == true && newevent.CheckForGridRequirements(terraintype, isonroad) == true &&
                        newevent.CheckForPartySizeRequirements(player.partyMembers.Count) == true)
                    {
                        possibleEvents.Add(newevent);
                    }


                }

            }


            for (int i = 0; i < possibleEvents.Count; ++i)
            {
                int rand = rando.Next(0, 101);
                if (rand < possibleEvents[i].ReturnEventChance())
                {
                    if (possibleEvents[i].OccursWhenWakingUp() == true)
                    {
                        SetCurrentWakeEventEvent(possibleEvents[i], true, rando, player, "");
                    }
                    else if (restSafeTilesRemaining==0)
                    {
                        SetCurrentPassiveEvent(possibleEvents[i], true, rando, player, "");
                        RunCurrentPassiveEvent(player, rando, uihelper);
                        RestSafeSet();
                    }
                    break;
                }

            }

        }


        void CheckForValidEvents(PlayerHandler player, DataManager datamanager, Random rando, GridTile.GridTerrain terraintype,Boolean isonroad, WorldMap world, int hour)
        {

            List<Event> possibleEvents = new List<Event>();
            List<WorldEvent> possibleWorldEvents = new List<WorldEvent>();

            //check for world events
            foreach (KeyValuePair<String, WorldEvent> e in datamanager.worldEventData)
            {
                WorldEvent worldevent = e.Value;
                //check if the mark is eligible to make a decision for options
                //party members aren't checked, but are affected by the mark's choice
                worldevent.DetermineEventOptionAvailability(player);
                //check grid and other reqs, then add to possible events
                if (worldevent.CheckForGridRequirements(terraintype,isonroad)==true &&
                    worldevent.ValidCreatureIfApplicable(world,currentGridLocation,rando, hour) == true)
                {
                    //determine the text used
                    worldevent.DetermineValidText(rando);
                    //add to possible events
                    possibleWorldEvents.Add(worldevent);
                }

            }


            //check for passive events
            foreach (KeyValuePair<String, Event> e in datamanager.passiveEventData)
            {
                //check if can occur when travelling
                if (e.Value.CanOccurWhenTravelling() == true)
                {
                    //add any eligible people to event
                    Event newevent = CheckIfPeopleEligible(e.Value, player);
                    //determine the item
                    newevent.DetermineValidItem(datamanager, rando);
                    //determine the text used
                    newevent.DetermineValidText(rando);
                    //if eligible people exist, add to event
                    if (newevent.IsEligibleExists() == true && newevent.CheckForGridRequirements(terraintype, isonroad) == true &&
                        newevent.CheckForPartySizeRequirements(player.partyMembers.Count) == true)
                    {
                        possibleEvents.Add(newevent);
                    }
                }
            }


            //check against list of posible world events
            for (int i =0; i < possibleWorldEvents.Count;++i)
            {
                int rand = rando.Next(0, 101);
                if (rand < possibleWorldEvents[i].ReturnEventChance())
                {
                    currentWorldEvent = possibleWorldEvents[i];
                    break;
                }

            }


            //check against list of possible passive events, only if a world event isn't going on
            if (currentWorldEvent == null)
            {
                for (int i = 0; i < possibleEvents.Count; ++i)
                {
                    int rand = rando.Next(0, 101);
                    if (rand < possibleEvents[i].ReturnEventChance())
                    {
                        SetCurrentPassiveEvent(possibleEvents[i], true, rando,player,"");
                        break;
                    }

                }
            }

        }

        public void SetCurrentPassiveEvent(Event eventtobeadded, Boolean randomperson, Random rando, PlayerHandler player,String creaturename)
        {
            currentEvent = eventtobeadded;
            if (randomperson == true)
            {
                currentEvent.GetRandomAssociatedPerson(rando);
            }
            else
            {
                currentEvent.AssociateMarkToEvent(player.theMark);
            }
            currentEvent.UpdateTextForName();
            currentEvent.UpdateTextForItem();
            currentEvent.UpdateTextForEnemy(creaturename);
        }

        void SetCurrentWakeEventEvent(Event eventtobeadded, Boolean randomperson, Random rando, PlayerHandler player, String creaturename)
        {
            currentWakeEvent = eventtobeadded;
            if (randomperson == true)
            {
                currentWakeEvent.GetRandomAssociatedPerson(rando);
            }
            else
            {
                currentWakeEvent.AssociateMarkToEvent(player.theMark);
            }
            currentWakeEvent.UpdateTextForName();
            currentWakeEvent.UpdateTextForItem();
            currentWakeEvent.UpdateTextForEnemy(creaturename);
        }

        public void RunCurrentPassiveEvent(PlayerHandler player, Random rando, UI_Helper uihelper)
        {
            safeTilesRemaining = passiveEventSafeTiles;
            currentEvent.PerformPassiveEventActivity(player, rando);
            CreateTravelFeed(currentEvent.ReturnEventText(), uihelper);
            ConsoleLogEvent();
            ClearEvent();
        }

        public void RunCurrentWakeEvent(PlayerHandler player, Random rando, UI_Helper uihelper)
        {
            if (currentWakeEvent != null)
            {
                safeTilesRemaining = passiveEventSafeTiles;
                currentWakeEvent.PerformPassiveEventActivity(player, rando);
                CreateTravelFeed(currentWakeEvent.ReturnEventText(), uihelper);
                ConsoleLogEvent();
                ClearWakeEvent();
            }
        }

        void CreateTravelFeed(String text,UI_Helper uihelper)
        {
            uihelper.CreateTravelFeed(text);

        }

        void ConsoleLogEvent()
        {
            if (currentEvent != null)
            {
                Console.WriteLine(currentEvent.ReturnEventText());
            }
            else if (currentWorldEvent != null)
            {
                Console.WriteLine(currentWorldEvent.ReturnEventTitleText());
            }
            else if (currentWakeEvent != null)
            {
                Console.WriteLine(currentWakeEvent.ReturnEventText());
            }    

        }


        void ClearEvent()
        {
            currentEvent = null;
        }

        void ClearWakeEvent()
        {
            currentWakeEvent = null;
        }

        public void ClearWorldEvent()
        {
            currentWorldEvent = null;
        }

        public Boolean IsPausedForEvent()
        {
            if (currentWorldEvent==null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        

        //returns minutes to tick down
        //this performs all checks/stat changes, etc for travelling
        public int TravelTick(PlayerHandler player, DataManager datamanager, Random rando, UI_Helper uihelper,GridTile.GridTerrain gridTerrainType, Boolean isOnRoad,WorldMap world, int hour)
        {
            int value = 0;

            //if at destination
            if (currentTravelDistance == totalTravelDistance)
            {
                //check for event
                if (currentGridLocation != travelStartingLocation &&
                    safeTilesRemaining == 0)
                {
                    CheckForValidEvents(player, datamanager, rando, gridTerrainType, isOnRoad,world, hour);
                }


                //if world event
                if (currentWorldEvent != null)
                {
                    safeTilesRemaining = worldEventSafeTiles;
                    uihelper.CreateEventUI(currentWorldEvent);
                }
                //if passive event
                else if (currentEvent != null)
                {
                    RunCurrentPassiveEvent(player,rando,uihelper);
                    //safeTilesRemaining = passiveEventSafeTiles;
                    //currentEvent.PerformPassiveEventActivity(player, rando);
                    //CreateTravelFeed(currentEvent.ReturnEventText(), uihelper);
                   // ConsoleLogEvent();
                    //ClearEvent();
                }
                //if no event
                else
                {
                    if (safeTilesRemaining>0)
                    {
                        safeTilesRemaining -= 1;
                    }
                    //player basic ticks
                    player.LoseStamina(PlayerHandler.StaminaDrainType.Travel);
                    player.LoseRested(PlayerHandler.RestedLossType.Travel);

                }


                //get next distance
                    currentGridLocation += new Point((int)(totalTravelDistance.X / 64), (int)(totalTravelDistance.Y / 64));
                    for (int i = 0; i < travelPath.Count; ++i)
                    {
                        if (travelPath[i].routeLocation == currentGridLocation)
                        {
                            totalTravelDistance = travelPath[i].getDirection();
                            currentTravelDistance = Vector2.Zero;
                            if (totalTravelDistance == Vector2.Zero)
                            {
                                value = -1;

                            }
                            else
                            {
                                value = travelPath[i].GetTravelEstimate();
                            }
                            break;
                        }
                    }
                
                
            }

            //move
                currentTravelDistance.X += totalTravelDistance.X / 64;
                currentTravelDistance.Y += totalTravelDistance.Y / 64;
            


            return value;
        }

        public void TravelCleanup(WorldMap world)
        {
            travelPath.Clear();
            UpdateCurrentLocation(world);
            currentTravelDistance = Vector2.Zero;
            totalTravelDistance = Vector2.Zero;
            displayDestination = false;
            destinationLocation = Point.Zero;
            currentDestinationText = "";
            currentDestinationDescription = "";
            travelStartingLocation = Point.Zero;
    }

        public void clearTravelPath()
        {
            travelPath.Clear();
        }

        public void createTravelPath(Point destination,Random rando,WorldMap world)
        {
            Point up = new Point(0, -1);
            Point down = new Point(0, 1);
            Point right = new Point(1, 0);
            Point left = new Point(-1, 0);

            Point start = currentGridLocation;
            travelPath.Add(new TravelRoute(start));

            while (start != destination)
            {
                
                List<Point> possibleDirections = new List<Point>();
                if (destination.X > start.X)
                {
                    possibleDirections.Add(right);
                }
                if (destination.X < start.X)
                {
                    possibleDirections.Add(left);
                }
                if (destination.Y > start.Y)
                {
                    possibleDirections.Add(down);
                }
                if (destination.Y < start.Y)
                {
                    possibleDirections.Add(up);
                }

                start += possibleDirections[rando.Next(0, possibleDirections.Count)];

                travelPath.Add(new TravelRoute(start));
            }

            determineTravelRouteSpriteMapAndDirections();
            GetEstimationsForTravel(world);

        }

        public void GetEstimationsForTravel(WorldMap world)
        {
            for (int i =0;i<travelPath.Count;++i)
            {
                //road
                if (world.IsGridRoad(travelPath[i].routeLocation))
                {
                    AssignEstimateValueToPath(5, i);
                }
                //water
                else if (world.IsGridLakeOrWater(travelPath[i].routeLocation))
                {
                    AssignEstimateValueToPath(30, i);
                }
                //node
                else if (world.IsGridNodeType(travelPath[i].routeLocation) != GridTile.GridNode.None)
                {
                    AssignEstimateValueToPath(10, i);
                }
                //terrains
                else
                {
                    GridTile.GridTerrain terraintype = world.GridNodeTerrainType(travelPath[i].routeLocation);

                    if (terraintype == GridTile.GridTerrain.Beach)
                    {
                        AssignEstimateValueToPath(15, i);
                    }
                    else if (terraintype == GridTile.GridTerrain.Forest)
                    {
                        AssignEstimateValueToPath(20, i);
                    }
                    else if (terraintype == GridTile.GridTerrain.Grass)
                    {
                        AssignEstimateValueToPath(10, i);
                    }
                    else if (terraintype == GridTile.GridTerrain.Hills)
                    {
                        AssignEstimateValueToPath(20, i);
                    }
                    else if (terraintype == GridTile.GridTerrain.Swamp)
                    {
                        AssignEstimateValueToPath(25, i);
                    }
                }
            }
            
        }



        void AssignEstimateValueToPath(int minutes, int pathid)
        {
            travelPath[pathid].AssignEstimate(minutes);
        }

        //used to determine which area on the spritesheet to use
        void determineTravelRouteSpriteMapAndDirections()
        {
            for (int i =0;i < travelPath.Count;++i)
            {
                //first spot, only check for nearby and create endpoint
                if (i==0)
                {
                    if (travelPath[i+1].routeLocation.X < travelPath[i].routeLocation.X)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(1, 0);
                        travelPath[i].AssignNextDirection(new Vector2(-64, 0));
                    }
                    else if (travelPath[i + 1].routeLocation.X > travelPath[i].routeLocation.X)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(0, 0);
                        travelPath[i].AssignNextDirection(new Vector2(64, 0));

                    }
                    else if (travelPath[i + 1].routeLocation.Y < travelPath[i].routeLocation.Y)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(1, 1);
                        travelPath[i].AssignNextDirection(new Vector2(0, -64));

                    }
                    else if (travelPath[i + 1].routeLocation.Y > travelPath[i].routeLocation.Y)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(0, 1);
                        travelPath[i].AssignNextDirection(new Vector2(0, 64));

                    }
                }
                //last spot, only check for last nearby and create endpoint
                else if (i==travelPath.Count-1)
                {
                    if (travelPath[i - 1].routeLocation.X < travelPath[i].routeLocation.X)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(1, 0);
                    }
                    else if (travelPath[i - 1].routeLocation.X > travelPath[i].routeLocation.X)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(0, 0);
                    }
                    else if (travelPath[i - 1].routeLocation.Y < travelPath[i].routeLocation.Y)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(1, 1);
                    }
                    else if (travelPath[i - 1].routeLocation.Y > travelPath[i].routeLocation.Y)
                    {
                        travelPath[i].routeSpriteSheet = new Vector2(0, 1);
                    }

                    travelPath[i].AssignNextDirection(Vector2.Zero);
                }
                //middle, check for both before and after positions and create spritesheet accordingly
                else
                {
                    //sheet
                    if ((travelPath[i - 1].routeLocation.X < travelPath[i].routeLocation.X &&
    travelPath[i + 1].routeLocation.X > travelPath[i].routeLocation.X) ||
    (travelPath[i - 1].routeLocation.X > travelPath[i].routeLocation.X &&
    travelPath[i + 1].routeLocation.X < travelPath[i].routeLocation.X))

                    {
                        travelPath[i].routeSpriteSheet = new Vector2(4, 0);
                    }
                    else if ((travelPath[i - 1].routeLocation.Y < travelPath[i].routeLocation.Y &&
travelPath[i + 1].routeLocation.Y > travelPath[i].routeLocation.Y) ||
(travelPath[i - 1].routeLocation.Y > travelPath[i].routeLocation.Y &&
travelPath[i + 1].routeLocation.Y < travelPath[i].routeLocation.Y))

                    {
                        travelPath[i].routeSpriteSheet = new Vector2(4, 1);
                    }
                    else if ((travelPath[i - 1].routeLocation.Y > travelPath[i].routeLocation.Y &&
travelPath[i + 1].routeLocation.X > travelPath[i].routeLocation.X) ||
(travelPath[i - 1].routeLocation.X > travelPath[i].routeLocation.X &&
travelPath[i + 1].routeLocation.Y > travelPath[i].routeLocation.Y))

                    {
                        travelPath[i].routeSpriteSheet = new Vector2(2, 0);
                    }
                    else if ((travelPath[i - 1].routeLocation.Y > travelPath[i].routeLocation.Y &&
travelPath[i + 1].routeLocation.X < travelPath[i].routeLocation.X) ||
(travelPath[i - 1].routeLocation.X < travelPath[i].routeLocation.X &&
travelPath[i + 1].routeLocation.Y > travelPath[i].routeLocation.Y))

                    {
                        travelPath[i].routeSpriteSheet = new Vector2(3,0);
                    }
                    else if ((travelPath[i - 1].routeLocation.Y < travelPath[i].routeLocation.Y &&
travelPath[i + 1].routeLocation.X > travelPath[i].routeLocation.X) ||
(travelPath[i - 1].routeLocation.X > travelPath[i].routeLocation.X &&
travelPath[i + 1].routeLocation.Y < travelPath[i].routeLocation.Y))

                    {
                        travelPath[i].routeSpriteSheet = new Vector2(2, 1);
                    }
                    else if ((travelPath[i - 1].routeLocation.Y < travelPath[i].routeLocation.Y &&
travelPath[i + 1].routeLocation.X < travelPath[i].routeLocation.X) ||
(travelPath[i - 1].routeLocation.X < travelPath[i].routeLocation.X &&
travelPath[i + 1].routeLocation.Y < travelPath[i].routeLocation.Y))

                    {
                        travelPath[i].routeSpriteSheet = new Vector2(3, 1);
                    }

                    //travelling
                    //left
                    if (travelPath[i + 1].routeLocation.X < travelPath[i].routeLocation.X )
                    {
                        travelPath[i].AssignNextDirection(new Vector2(-64, 0));
                    }
                    //right
                    else if (travelPath[i + 1].routeLocation.X > travelPath[i].routeLocation.X)
                    {
                        travelPath[i].AssignNextDirection(new Vector2(64, 0));

                    }
                    //up
                    else if (travelPath[i + 1].routeLocation.Y < travelPath[i].routeLocation.Y)
                    {
                        travelPath[i].AssignNextDirection(new Vector2(0, -64));

                    }
                    //down
                    else if (travelPath[i + 1].routeLocation.Y > travelPath[i].routeLocation.Y)
                    {
                        travelPath[i].AssignNextDirection(new Vector2(0, 64));

                    }
                }

            }

        }

        public void TravelToDestination(Point start, Point destination, WorldMap world,Random rando)
        {
            createTravelCoords(start, destination);
            createDestinationStrings(world);
        }

        void createTravelCoords(Point start, Point destination)
        {
            travelStartingLocation = start;
            destinationLocation = destination;
            displayDestination = true;


        }



        public void DrawPathOnMap(SpriteBatch spriteBatch, Texture2D pathTexture)
        {
            for (int i =0; i < travelPath.Count;++i)
            {
                spriteBatch.Draw(pathTexture, new Rectangle(travelPath[i].routeLocation.X * 64, travelPath[i].routeLocation.Y * 64, 64, 64), new Rectangle((int)(travelPath[i].routeSpriteSheet.X*64), (int)(travelPath[i].routeSpriteSheet.Y * 64), 64, 64), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.71f);

            }

        }

        public void DrawIconOnMap(SpriteBatch spriteBatch, Texture2D mapLocationIcon)
        {
            spriteBatch.Draw(mapLocationIcon, new Rectangle((int)((currentGridLocation.X * 64) + currentTravelDistance.X), (int)((currentGridLocation.Y * 64) + currentTravelDistance.Y + mapIconOffset), 64, 64), new Rectangle(0, 0, 64, 64), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.71f);

        }

        public void DrawLocationUI(SpriteBatch spriteBatch, SpriteFont worldfont, Vector2 baseposition, Texture2D currentLocIcon, Texture2D destinationIcon)
        {
            spriteBatch.Draw(currentLocIcon, new Rectangle((int)baseposition.X - 40, (int)baseposition.Y + 30, 50, 50), new Rectangle(0, 0, 100, 100), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);

            //location
            spriteBatch.DrawString(worldfont, currentLocationText, new Vector2(baseposition.X + 10, baseposition.Y + 30), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
            spriteBatch.DrawString(worldfont, currentLocationText, new Vector2(baseposition.X + 11, baseposition.Y + 31), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            spriteBatch.DrawString(worldfont, currentLocationText, new Vector2(baseposition.X + 12, baseposition.Y + 32), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            //description
            spriteBatch.DrawString(worldfont, currentLocationDescription, new Vector2(baseposition.X + 35, baseposition.Y + 55), Color.LightGray, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
            spriteBatch.DrawString(worldfont, currentLocationDescription, new Vector2(baseposition.X + 36, baseposition.Y + 56), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
            spriteBatch.DrawString(worldfont, currentLocationDescription, new Vector2(baseposition.X + 37, baseposition.Y + 57), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);

            if (displayDestination == true)
            {
                spriteBatch.Draw(destinationIcon, new Rectangle((int)baseposition.X, (int)baseposition.Y + 100, 50, 50), new Rectangle(0, 0, 100, 100), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.81f);

                //location
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 50, baseposition.Y + 100), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 51, baseposition.Y + 101), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                spriteBatch.DrawString(worldfont, currentDestinationText, new Vector2(baseposition.X + 52, baseposition.Y + 102), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                //description
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 75, baseposition.Y + 125), Color.LightGray, 0, Vector2.Zero, 1, SpriteEffects.None, 0.81f);
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 76, baseposition.Y + 126), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);
                spriteBatch.DrawString(worldfont, currentDestinationDescription, new Vector2(baseposition.X + 77, baseposition.Y + 127), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.8f);



            }
        }
    }
}
