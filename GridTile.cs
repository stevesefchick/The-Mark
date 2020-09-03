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

        //details
        public enum GridTerrain { Grass }
        public GridTerrain thisTerrainType;

        public enum GridNode { Castle, Town }
        public GridNode thisNodeType;


        public GridTile(int x,int y)
        {
            XCoord = x;
            YCoord = y;
            thisTerrainType = GridTerrain.Grass;

        }


    }
}
