namespace GameOfLifeClans.Ai
{
    public interface IHealable
    {
        bool IsNeedingHealing { get; }
        void Heal(int healPower);
    }
}
