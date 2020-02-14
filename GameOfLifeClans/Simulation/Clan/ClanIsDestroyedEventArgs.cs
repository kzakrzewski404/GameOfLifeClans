using System;


namespace GameOfLifeClans.Simulation.Clan
{
    public class ClanIsDestroyedEventArgs : EventArgs
    {
        public int DestroyedByClan { get; private set; }


        public ClanIsDestroyedEventArgs(int destroyedByClan) => DestroyedByClan = destroyedByClan;
    }
}
