using GameOfLifeClans.Ai;


namespace GameOfLifeClans.Map.Data
{
    public class Tile : IOccupiable
    {
        public int LocationX { get; private set; }
        public int LocationY { get; private set; }
        public Terrain Terrain { get; private set; }
        public Entity AiEntity { get; private set; }
        public MapContainer Map { get; private set; }

        private int _clanOwnership;


        public bool IsOccupied => !(AiEntity == null);
        public int ClanOwnershipId => _clanOwnership;


        public Tile(int x, int y, Terrain terrain, MapContainer map)
        {
            LocationX = x;
            LocationY = y;
            Map = map;
            SetTerrain(terrain);
            _clanOwnership = -1;
        }

        public void RemoveAiEntity() => AiEntity = null;

        public void SetTerrain(Terrain terrain) => Terrain = terrain;

        public void SetAiEntity(Entity aiEntity)
        {
            AiEntity = aiEntity;
            AiEntity.SetOccupiedTile(this);
            _clanOwnership = AiEntity.ClanId;
        }

        public void MoveHere(Entity invoker)
        {
            invoker.OccupiedTile.RemoveAiEntity();
            SetAiEntity(invoker);
            _clanOwnership = invoker.ClanId;
        }
    }
}
