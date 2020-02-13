using GameOfLifeClans.Ai.Entities;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public interface IVisionSense
    {
        IVisionResult GetResult(Entity visionOwner);
    }
}
