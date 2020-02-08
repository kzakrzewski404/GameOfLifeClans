﻿using System;

using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Ai.Config;


namespace GameOfLifeClans.Ai
{
    public class Soldier : Entity
    {
        private static Random _rnd = new Random();
        private int _willingnessToAttactk;


        public Soldier(EntityId id, ClanId clan, int health, int damage, int defence) : base(id, clan, health, damage, defence)
        {
            _willingnessToAttactk = _rnd.Next(AiConfig.SOLDIER_MINIMAL_WILLIGNESS_TO_ATTACK, 100);
        }


        public override void CalculateStep()
        {
            Result visionResult = _vision.GetResult(this);

            //Attack
            bool isEnemyAttacked = false;
            if (visionResult.Enemies.IsNotEmpty && (IsWillToAttack || !visionResult.FreeTiles.IsNotEmpty))
            {
                PerformAttackOnRandomEnemy(visionResult);
                isEnemyAttacked = true;
            }

            //Move
            if (!isEnemyAttacked && visionResult.FreeTiles.IsNotEmpty)
            {
                MoveToRandomFreeTile(visionResult);
            }
        }


        private bool IsWillToAttack => _rnd.Next(0, 100) <= _willingnessToAttactk;
    }
}
