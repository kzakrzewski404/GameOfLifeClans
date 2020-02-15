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
        protected static EntityFactory _entityFactory = new EntityFactory();
        private int _nextSpawnCounter;
        private int _spawnTreshold;

        protected Dictionary<int, EntityId> _possibleSpawns = new Dictionary<int, EntityId>();


        public Headquarter(IClanInfo myClan, SpawnStats stats, IVisionSense visionSense, int spawnTreshold) : base(myClan, stats, visionSense)
        {
            _spawnTreshold = spawnTreshold;
            InitializePossibleSpawns();
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
            HandleSpawn(visionResult, ref summary);

            return summary;
        }


        protected virtual void InitializePossibleSpawns()
        {
            _possibleSpawns.Add(1, EntityId.Builder);
            _possibleSpawns.Add(100, EntityId.Soldier);
        }

        protected void HandleSpawn(IVisionResult visionResult, ref StepSummary summary)
        {
            if (_nextSpawnCounter < _spawnTreshold)
            {
                _nextSpawnCounter++;
            }
            else if (visionResult.IsFreeTileFound)
            {
                SpawnEntity(visionResult, ref summary);
            }
        }

        protected void SpawnEntity(IVisionResult visionResult, ref StepSummary summary)
        {
            Entity spawned = _entityFactory.Create(GetEntityIdToSpawn(), this.ClanInfo);
            summary.AddSpawnedEntityInfo(spawned);

            visionResult.GetRandomFreeTile().SetAiEntity(spawned);
            _nextSpawnCounter = 0;
        }


        private EntityId GetEntityIdToSpawn()
        {
            int x = _rnd.Next(1, 100);
            foreach (var item in _possibleSpawns)
            {
                if (x <= item.Key)
                {
                    return item.Value;
                }
            }
            
            throw new ArgumentNullException();
        }
    }
}