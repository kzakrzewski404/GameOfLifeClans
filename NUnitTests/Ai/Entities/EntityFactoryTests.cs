using NUnit.Framework;

using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.UnitTests.Ai.Entities
{
    public class EntityFactoryTests
    {
        private EntityFactory _factory = new EntityFactory();


        [Test]
        public void Create_Headquarter_ObjectIsTypeOfHeadquarter()
        {
            //Arrange

            //Act
            Entity entity = _factory.Create(EntityId.Headquarter, null);

            //Assert
            Assert.IsTrue(entity.GetType() == typeof(Headquarter));
        }

        [Test]
        public void Create_Soldier_ObjectIsTypeOfSoldier()
        {
            //Arrange

            //Act
            Entity entity = _factory.Create(EntityId.Soldier, null);

            //Assert
            Assert.IsTrue(entity.GetType() == typeof(Soldier));
        }
    }
}
