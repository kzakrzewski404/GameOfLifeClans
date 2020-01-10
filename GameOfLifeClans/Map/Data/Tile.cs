using GameOfLifeClans.Ai;


namespace GameOfLifeClans.Map.Data
{
    public class Tile
    {
        public int LocationX { get; private set; }
        public int LocationY { get; private set; }
        public TileTerrain Terrain { get; private set; }
        public Entity AiEntity { get; private set; }
        public MapContainer Map { get; private set; }


        public bool IsOccupied => !(AiEntity == null);
        public void SetTerrain(TileTerrain terrain) => Terrain = terrain;
        public void SetAiEntity(Entity aiEntity) => AiEntity = aiEntity;
        public void RemoveAiEntity() => AiEntity = null;


        public Tile(int x, int y, TileTerrain terrain, MapContainer map)
        {
            LocationX = x;
            LocationY = y;
            Map = map;
            SetTerrain(terrain);
        }


        public void MoveAiEntityHere(Tile fromTile)
        {
            AiEntity = fromTile.AiEntity;
            fromTile.RemoveAiEntity();
        }
    }
}
