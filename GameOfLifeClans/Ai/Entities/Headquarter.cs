using System;
using System.Collections.Generic;

using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Entities.Config;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Entities
{
    public class Headquarter : Entity
    {
        private static Random _rnd = new Random();
        private static EntityFactory _entityFactory = new EntityFactory();
        private int _nextSpawnCounter;
        private int _spawnTreshold;

        protected Dictionary<int, EntityId> _possibleSpawnsPercentages = new Dictionary<int, EntityId>();


        public Headquarter(IClanInfo myClan, SpawnStats stats, IVisionSense visionSense, int spawnTreshold) : base(myClan, stats, visionSense)
        {
            _spawnTreshold = spawnTreshold;
        }


        public override StepSummary CalculateStep()
        {
            StepSummary summary = new StepSummary();
            IVisionResult visionResult = _visionSense.GetResult(this);

            // Attack
            if (visionResult.IsEnemyFound)
            {
                AttackEnemy(visionResult.GetRandomEnemy());
            }

            // Spawn
            if (_nextSpawnCounter < _spawnTreshold)
            {
                _nextSpawnCounter++;
            }
            else if(visionResult.IsFreeTileFound)
            {
                SpawnEntity(visionResult, ref summary);
            }

            return summary;
        }


        protected void SpawnEntity(IVisionResult visionResult, ref StepSummary summary)
        {
            Entity spawned = _entityFactory.Create(GetEntityIdToSpawn(), this.ClanInfo);
            summary.AddSpawnedEntityInfo(spawned);

            visionResult.GetRandomFreeTile().SetAiEntity(spawned);
            _nextSpawnCounter = 0;
        }

        protected virtual void InitializePossibleSpawns()
        {
            _possibleSpawnsPercentages.Add(1, EntityId.Builder);
            _possibleSpawnsPercentages.Add(100, EntityId.Soldier);
        }

        private EntityId GetEntityIdToSpawn()
        {
            int x = _rnd.Next(1, 101);
            foreach (var item in _possibleSpawnsPercentages)
            {
                if (item.Key <= x)
                {
                    return item.Value;
                }
            }
            
            throw new ArgumentNullException();
        }
    }
}
