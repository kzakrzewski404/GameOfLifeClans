using GameOfLifeClans.Map;


namespace GameOfLifeClans.Map.Data
{
    public class TileOwnershipTransferer
    {
        /// <summary>
        /// Returns number of transfered terrain
        /// </summary>
        public int TransferOwnership(MapContainer map, int fromClan, int toClan)
        {
            int transferedTerritory = 0;

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.Tiles[x, y].ClanOwnershipId == fromClan)
                    {
                        map.Tiles[x, y].ChangeTileOwnership(toClan);
                        transferedTerritory++;
                    }
                }
            }

            return transferedTerritory;
        }
    }
}
