using System.Collections.Generic;
using UnityEngine;
using World;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int mapSize;
    [SerializeField] private Block tile;
    [SerializeField] private StoneBlock stonePrefab;
    [SerializeField] private PlayerBehavior playerBehavior;
    private void Start()
    {
        var scaleFactor = tile.ScaleFactor * 2;
        BlockHolder.WorldSize = mapSize;
        BlockHolder.Blocks = new Block[mapSize, mapSize];
        BlockHolder.Entities = new List<PlaceableBlock>();

        var halfMap = mapSize / 2;
        for (int i = -mapSize / 2; i < mapSize / 2; i++)
        {
            for (int j = -mapSize / 2; j < mapSize / 2; j++)
            {
                var block = Instantiate(tile, new Vector3(i * scaleFactor, 0.5f * scaleFactor, j * scaleFactor), new Quaternion());
                block.SetPosition(new Vector2Int(i, j));
                BlockHolder.Blocks[i + halfMap, j + halfMap] = block;
                block.name = $"{i + halfMap}, {j + halfMap}";
            }
        }
        GenerateStones();

        Instantiate(playerBehavior, Vector3.up * 5, Quaternion.identity);
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
                {
                    BlockHolder.Blocks[i + halfMap, j + halfMap].SetPlaceableBlock(stonePrefab);
                    print($"Stone for {i + halfMap}, {j + halfMap}");
                }
            }
        }
    }
}
