
using System.Collections.Generic;

public static class BlockHolder
{
    public static int WorldSize;
    public static Block[,] Blocks { get; set; }

    public static List<PlaceableBlock> Entities { get; set; }

    public static void ProceesEntitiesStress()
    {
        foreach (var entity in Entities) 
            entity.ProcessTick();
    }
}