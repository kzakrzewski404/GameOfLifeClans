using System.Drawing;

using GameOfLifeClans.Map;


namespace GameOfLifeClans.Render
{
    public abstract class Renderer
    {
        protected MapContainer _map;


        public virtual void LinkMap(MapContainer map) => _map = map;


        public abstract void Render();
    }
}
