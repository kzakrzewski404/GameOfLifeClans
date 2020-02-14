using GameOfLifeClans.Ai.Entities;


namespace GameOfLifeClans.Map.Data
{
    public class Tile : IOccupiable, ITransferOwnership
    {
        public int LocationX { get; private set; }
        public int LocationY { get; private set; }
        public int ClanOwnershipId { get; private set; }
        public Terrain Terrain { get; private set; }
        public Entity AiEntity { get; private set; }
        public MapContainer Map { get; private set; }


        public bool IsOccupied => !(AiEntity == null);



        public Tile(int x, int y, Terrain terrain, MapContainer map)
        {
            LocationX = x;
            LocationY = y;
            Map = map;
            SetTerrain(terrain);
            ClanOwnershipId = -1;
        }

        public void RemoveAiEntity() => AiEntity = null;

        public void SetTerrain(Terrain terrain) => Terrain = terrain;

        public void ChangeTileOwnership(int targetId) => ClanOwnershipId = targetId;

        public void SetAiEntity(Entity aiEntity)
        {
            AiEntity = aiEntity;
            AiEntity.SetOccupiedTile(this);
            ClanOwnershipId = AiEntity.ClanInfo.Id;
        }

        public void MoveHere(Entity invoker)
        {
            invoker.OccupiedTile.RemoveAiEntity();
            SetAiEntity(invoker);
            ClanOwnershipId = invoker.ClanInfo.Id;
        }
    }
}
