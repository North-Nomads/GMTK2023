using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int mapSize;
    [SerializeField] private Block tile;

    private void Start()
    {
        var scaleFactor = tile.ScaleFactor * 2;
        BlockHolder.WorldSize = mapSize;
        BlockHolder.Blocks = new Block[mapSize, mapSize];
        BlockHolder.Entities = new List<PlaceableBlock>();

        for (int i = -mapSize / 2; i < mapSize / 2; i++)
        {
            for (int j = -mapSize / 2; j < mapSize / 2; j++){
                var block = Instantiate(tile, new Vector3(i * scaleFactor, 0.5f * scaleFactor, j * scaleFactor), new Quaternion());
                block.SetPosition(new Vector2Int(i, j));
            }
        }
    }
}
