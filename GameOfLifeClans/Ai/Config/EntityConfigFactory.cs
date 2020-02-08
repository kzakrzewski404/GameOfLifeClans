using System;
using System.Linq;

using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Ai.Config
{
    public class EntityConfigFactory
    {
        private EntityConfig[] _configs;


        public EntityConfig Create(EntityId entity) => _configs[(int)(entity)];


        public EntityConfigFactory()
        {
            int count = Enum.GetNames(typeof(EntityId)).Length;
            _configs = new EntityConfig[count];

            _configs[(int)EntityId.Headquarter] = new EntityConfig(AiConfig.HEADQUARTER_HEALTH, AiConfig.HEADQUARTER_DAMAGE, AiConfig.HEADQUARTER_DEFENCE);
            _configs[(int)EntityId.Soldier] = new EntityConfig(AiConfig.SOLDIER_HEALTH, AiConfig.SOLDIER_DAMAGE, AiConfig.SOLDIER_DEFENCE);

            if (_configs.Contains(null))
            {
                throw new Exception("_configs[] in EntityConfigFactory contains NULL, not enough defined configs");
            }
        }


        
    }
}