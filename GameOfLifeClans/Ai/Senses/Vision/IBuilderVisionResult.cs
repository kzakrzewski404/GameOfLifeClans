namespace GameOfLifeClans.Ai.Senses.Vision
{
    public interface IBuilderVisionResult : IVisionResult
    {
        bool IsAwayFromClosestHeadquarter { get; }
    }
}
