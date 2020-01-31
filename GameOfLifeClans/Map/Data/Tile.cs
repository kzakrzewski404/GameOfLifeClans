﻿using GameOfLifeClans.Ai;


namespace GameOfLifeClans.Map.Data
{
    public class Tile
    {
        public int LocationX { get; private set; }
        public int LocationY { get; private set; }
        public Terrain Terrain { get; private set; }
        public Entity AiEntity { get; private set; }
        public MapContainer Map { get; private set; }


        public bool IsOccupied => !(AiEntity == null);
        public void RemoveAiEntity() => AiEntity = null;
        public void SetTerrain(Terrain terrain) => Terrain = terrain;


        public Tile(int x, int y, Terrain terrain, MapContainer map)
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

        public void MoveAiEntityHere(Entity invoker)
        {
            invoker.OccupiedTile.RemoveAiEntity();
            SetAiEntity(invoker);
        }
    }
}
