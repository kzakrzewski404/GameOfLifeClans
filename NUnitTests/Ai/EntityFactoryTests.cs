using NUnit.Framework;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.UnitTests.Ai
{
    public class EntityFactoryTests
    {
        private EntityFactory _factory = new EntityFactory();


        [Test]
        public void Should_ReturnCorrectObject_Typeof_Headquarter()
        {
            //Arrange

            //Act
            Entity entity = _factory.Create(EntityId.Headquarter, 0);

            //Assert
            Assert.IsTrue(entity.GetType() == typeof(Headquarter));
        }

        [Test]
        public void Should_ReturnCorrectObject_Typeof_Soldier()
        {
            //Arrange

            //Act
            Entity entity = _factory.Create(EntityId.Soldier, 0);

            //Assert
            Assert.IsTrue(entity.GetType() == typeof(Soldier));
        }
    }
}
