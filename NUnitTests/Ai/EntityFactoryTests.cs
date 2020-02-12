﻿using NUnit.Framework;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.UnitTests.Ai
{
    public class EntityFactoryTests
    {
        private EntityFactory _factory = new EntityFactory();


        [Test]
        public void Create_Headquarter_ObjectIsTypeOfHeadquarter()
        {
            //Arrange

            //Act
            Entity entity = _factory.Create(EntityId.Headquarter, 0);

            //Assert
            Assert.IsTrue(entity.GetType() == typeof(Headquarter));
        }

        [Test]
        public void Create_Soldier_ObjectIsTypeOfSoldier()
        {
            //Arrange

            //Act
            Entity entity = _factory.Create(EntityId.Soldier, 0);

            //Assert
            Assert.IsTrue(entity.GetType() == typeof(Soldier));
        }
    }
}
