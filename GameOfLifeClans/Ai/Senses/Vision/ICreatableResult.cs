﻿using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public interface ICreatableResult
    {
        void AddFreeTile(Tile tile);
        void AddAlly(Entity ally);
        void AddEnemy(Entity enemy);
    }
}