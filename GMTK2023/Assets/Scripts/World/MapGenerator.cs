using System;
using System.Collections.Generic;
using UnityEngine;
using World;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int mapSize;
    [SerializeField] private Block tile;
    [SerializeField] private StoneBlock stonePrefab_small;
    [SerializeField] private StoneBlock stonePrefab_medium;
    [SerializeField] private StoneBlock stonePrefab_large;
    [SerializeField] private float scale;
    
    private void Start()
    {
        var scaleFactor = tile.ScaleFactor * 2;
        BlockHolder.WorldSize = mapSize;
        BlockHolder.Blocks = new Block[mapSize, mapSize];
        BlockHolder.Entities = new List<PlaceableBlock>();
        var halfMap = mapSize / 2;
        for (int i = -mapSize / 2; i < mapSize / 2; i++)
        {
            for (int j = -mapSize / 2; j < mapSize / 2; j++){
                var block = Instantiate(tile, new Vector3(i * scaleFactor, 0.5f * scaleFactor, j * scaleFactor), new Quaternion());
                block.SetPosition(new Vector2Int(i, j));
                BlockHolder.Blocks[i + halfMap, j + halfMap] = block;
                block.name = $"{i + halfMap}, {j + halfMap}";
            }
        }

        GenerateStones();
    }

    private void GenerateStones()
    {
        var stoneValue = 0.7f;
        var halfMap = mapSize / 2;
        for (int i = -halfMap; i < halfMap; i++)
        {
            for (int j = -halfMap; j < halfMap; j++)
            {
                float seed = Mathf.PerlinNoise((i + halfMap) / (float)mapSize * scale, (j + halfMap) / (float)mapSize * scale);
                if (seed > stoneValue * 1.2f)
                {
                    BlockHolder.Blocks[i + halfMap, j + halfMap].SetPlaceableBlock(stonePrefab_small);
                    // print("hje");
                }
                else if (seed > stoneValue * 1.1f)
                {
                    BlockHolder.Blocks[i + halfMap, j + halfMap].SetPlaceableBlock(stonePrefab_medium);
                    //print("hje");
                }
                else if (seed > stoneValue)
                {
                    BlockHolder.Blocks[i + halfMap, j + halfMap].SetPlaceableBlock(stonePrefab_large);
                   // print("nya");
                }

            }
        }
    }
}
