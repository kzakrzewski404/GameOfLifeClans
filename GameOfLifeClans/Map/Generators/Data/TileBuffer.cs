namespace GameOfLifeClans.Map.Generators.Data
{
    public class TileBuffer
    {
        private bool[,] _buffer;


        public TileBuffer(int sizeX, int sizeY)
        {
            _buffer = new bool[sizeX, sizeY];
        }


        public bool IsMarked(int x, int y) => _buffer[x, y];
        public void Add(int x, int y) => _buffer[x, y] = true;
    }
}
