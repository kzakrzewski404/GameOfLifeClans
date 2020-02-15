using GameOfLifeClans.Ai.Entities;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public interface IBuilderVisionSense : IVisionSense
    {
        IBuilderVisionResult GetResult(Entity visionOwner, int minimalDistanceFromHeadquarter);
    }
}
