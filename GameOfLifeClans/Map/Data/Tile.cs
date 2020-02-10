﻿using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Map.Data
{
    public class Tile
    {
        public Tile(int x, int y, Terrain terrain, MapContainer map)
        {
            LocationX = x;
            LocationY = y;
            Map = map;
            SetTerrain(terrain);
            ClanOwnership = ClanId.NEUTRAL;
        }


        public int LocationX { get; private set; }
        public int LocationY { get; private set; }
        public Terrain Terrain { get; private set; }
        public Entity AiEntity { get; private set; }
        public MapContainer Map { get; private set; }
        public ClanId ClanOwnership { get; private set; }
        public bool IsOccupied => !(AiEntity == null);


        public void RemoveAiEntity() => AiEntity = null;

        public void SetTerrain(Terrain terrain) => Terrain = terrain;

        public void SetAiEntity(Entity aiEntity)
        {
            AiEntity = aiEntity;
            AiEntity.SetOccupiedTile(this);
            ClanOwnership = AiEntity.Clan;
        }

        public void MoveAiEntityHere(Entity invoker)
        {
            invoker.OccupiedTile.RemoveAiEntity();
            SetAiEntity(invoker);
            ClanOwnership = invoker.Clan;
        }
    }
}
