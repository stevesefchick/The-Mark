using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Mark
{
    class GridTile
    {
        public int XCoord;
        public int YCoord;

        //status
        public int gridSize = 64;
        public Vector2 gridOffset = new Vector2(32, 32);

        //terrain details
        public enum GridTerrain { Grass, Forest, Hills }
        public GridTerrain thisTerrainType;
        //place of interest details
        public enum GridNode { None,Castle, Town, OrbitalLocation }
        public GridNode thisNodeType;
        //road details
        public enum RoadType { None, Road }
        public RoadType thisRoadType;
        //water detail
        public enum WaterType { None, Lake, River }
        public WaterType thisWaterType;

        public GridTile(int x,int y)
        {
            XCoord = x;
            YCoord = y;
            thisTerrainType = GridTerrain.Grass;
            thisRoadType = RoadType.None;
            thisNodeType = GridNode.None;
            thisWaterType = WaterType.None;
        }


    }
}
