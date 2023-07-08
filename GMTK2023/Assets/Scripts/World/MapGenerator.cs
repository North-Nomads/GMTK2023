using System.Collections.Generic;
using UnityEngine;
using World;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int mapSize;
    [SerializeField] private Block tile;
    [SerializeField] private StoneBlock stonePrefab;
    
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

        GenerateStones();
    }

    private void GenerateStones()
    {
        var stoneValue = 0.1f;
        var halfMap = mapSize / 2;
        for (int i = -halfMap; i < halfMap; i++)
        {
            for (int j = -halfMap; j < halfMap; j++)
            {
                if (Random.value < stoneValue)
                    BlockHolder.Blocks[i + halfMap, j + halfMap].SetPlaceableBlock(stonePrefab);
                
            }
        }
    }
}
