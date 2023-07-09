using System.Collections.Generic;
using UnityEngine;
using World;

public class MapGenerator : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float stoneSpawnChance;
    [SerializeField] private MainStats statsHolder;
    [SerializeField] private int mapSize;
    [SerializeField] private PlayerBehavior playerBehavior;
    [SerializeField] private Block tile;
    [SerializeField] private StoneBlock stonePrefab;
    [SerializeField] private float noiseScale;
    private PlayerBehavior _playerBehavior;
    private float _stoneValue;
 
        
    public int MapSize { get => mapSize; }
    public Block Tile { get => tile; }

    public PlayerBehavior Player { get => _playerBehavior; }

    private void Start()
    {
        var scaleFactor = tile.ScaleFactor * 2;
        _stoneValue = 1 - stoneSpawnChance;
        BlockHolder.WorldSize = mapSize;
        BlockHolder.Blocks = new Block[mapSize, mapSize];
        BlockHolder.Entities = new List<PlaceableBlock>();
        BlockHolder.ActiveBlock = new();
        var halfMap = mapSize / 2;
        for (int i = -halfMap; i < halfMap; i++)
        {
            for (int j = -halfMap; j < halfMap; j++){
                var block = Instantiate(tile, new Vector3(i * scaleFactor, 0.5f * scaleFactor, j * scaleFactor), new Quaternion());
                block.SetPosition(new Vector2Int(i+ halfMap, j + halfMap));
                BlockHolder.Blocks[i + halfMap, j + halfMap] = block;
                block.name = $"{i + halfMap}, {j + halfMap}";
                block.Stats = statsHolder;
            }
        }

        GenerateStones();
        _playerBehavior = Instantiate(playerBehavior, Vector3.up * 5, Quaternion.identity);
    }

    private void GenerateStones()
    {
        var halfMap = mapSize / 2;
        for (int i = -halfMap; i < halfMap; i++)
        {
            for (int j = -halfMap; j < halfMap; j++)
            {
                float seed = Mathf.PerlinNoise((i + halfMap) / (float)mapSize * noiseScale, (j + halfMap) / (float)mapSize * noiseScale);
                if (seed > _stoneValue)
                {
                    BlockHolder.Blocks[i + halfMap, j + halfMap].SetPlaceableBlock(stonePrefab);
                }
            }
        }
    }

    public static void InstantiateInteractableObject(WorkbenchBlock interactiveBlock, Block focusBlock)
    {
        focusBlock.SetPlaceableBlock(interactiveBlock);
        BlockHolder.Entities.Add(focusBlock.PlacedBlock);
    }
}
