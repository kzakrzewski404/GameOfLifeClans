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
        public void RemoveAiEntity() => AiEntity = null;
        public void SetTerrain(TileTerrain terrain) => Terrain = terrain;


        public Tile(int x, int y, TileTerrain terrain, MapContainer map)
        {
            LocationX = x;
            LocationY = y;
            Map = map;
            SetTerrain(terrain);
        }


        public void SetAiEntity(Entity aiEntity)
        {
            AiEntity = aiEntity;
            AiEntity.SetOccupiedTile(this);
        }

        public void MoveAiEntityHere(Tile fromTile)
        {
            SetAiEntity(fromTile.AiEntity);
            fromTile.RemoveAiEntity();
        }
    }
}
