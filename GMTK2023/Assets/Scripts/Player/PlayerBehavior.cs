using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private int scanRadius;
    [SerializeField] private WorkbenchBlock furnace;
    [SerializeField] private ChestBlock chest;
    
    private List<BasicPlayerState> _playerStates;
    private BasicPlayerState _currentState;
    private Vector2Int _destinationPoint;
    private Vector2Int _playerPosition;
    private Vector2Int _playerBaseCoords;

    public Vector2Int PlayerBaseCoords => _playerBaseCoords;
    public PlayerInventory Inventory { get; private set; }

    public Vector2Int PlayerPosition => _playerPosition;

    public List<Block> VisibleBlocks
    {
        get
        {
            List<Block> blocks = new();
            var half = scanRadius / 2;
            int playerX = PlayerPosition[0], playerY = PlayerPosition[1];
            for (int i = Mathf.Max(0, playerX - half); i < playerX + half && i < BlockHolder.WorldSize; i++)
                for (int j = Mathf.Max(0, playerX - half); j < playerY + half && j < BlockHolder.WorldSize; j++)
                {
                    blocks.Add(BlockHolder.Blocks[i, j]);
                }
                
            return blocks;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        
        var half = BlockHolder.WorldSize / 2;
        _playerPosition = new Vector2Int(half, half);
        _playerBaseCoords = _playerPosition;
        Inventory = new();
        _playerStates = new()
        {
            new ObservePlayerState(this),
            new BuildingPlayerState(this, furnace, chest),
            new IdlePlayerState(this),
            new SortingPlayerState(this)
        };
        _currentState = _playerStates[0];
    }

    // Update is called once per frame
    private void Update()
    {
        BlockHolder.ProceesEntitiesStress();
        _currentState.Update();
    }

    public void SwitchState<T>() where T : BasicPlayerState
    {
        _currentState.OnStateLeave();
        _currentState = _playerStates.OfType<T>().First();
        _currentState.OnStateEnter();
    }

    /// <summary>
    /// Scans area around player according to his scan radius
    /// </summary>
    /// <typeparam name="T">PlacebleBlock we scan for</typeparam>
    /// <returns>List of blocks that satisfy generic</returns>
    public List<Block> ScanAreaAroundPlayer<T>() where T : PlaceableBlock
    {
        List<Block> blocks = new();
        var half = scanRadius / 2;
        var playerX = _playerPosition[0];
        var playerY = _playerPosition[1];
        for (int i = Mathf.Max(0, playerX - half); i < playerX + half && i < BlockHolder.WorldSize; i++)
        for (int j = Mathf.Max(0, playerX - half); j < playerY + half && j < BlockHolder.WorldSize; j++)
        {
            if (BlockHolder.Blocks[i, j].PlacedBlock is T)
                blocks.Add(BlockHolder.Blocks[i, j]);
        }
            

        return blocks;
    }
    
    /// <summary>
    /// Scans area around player according to his scan radius
    /// </summary>
    /// <typeparam name="T">PlacebleBlock we scan for</typeparam>
    /// <returns>List of blocks that satisfy generic</returns>
    public List<Block> ScanAreaAroundPlayer()
    {
        List<Block> blocks = new(); 
        var half = scanRadius / 2;
        var playerX = _playerPosition[0];
        var playerY = _playerPosition[1];
        for (int i = Mathf.Max(0, playerX - half); i < playerX + half && i < BlockHolder.WorldSize; i++)
        for (int j = Mathf.Max(0, playerX - half); j < playerY + half && j < BlockHolder.WorldSize; j++)
        {
            if (BlockHolder.Blocks[i, j].PlacedBlock is null)
                blocks.Add(BlockHolder.Blocks[i, j]);
        }

        return blocks;
    }

    /// <summary>
    /// Performs movement on new point if it's available
    /// </summary>
    /// <param name="point">Point to walk on</param>
    public void MoveOnPoint(Vector2Int point)
    {
        if (point[0] > BlockHolder.WorldSize || point[1] > BlockHolder.WorldSize) return;

        if (point[0] < 0 || point[1] < 0) return;

        if (Mathf.Abs(_playerPosition[0] - point[0]) > 1 || Mathf.Abs(_playerPosition[1] - point[1]) > 1) return;

        var position = BlockHolder.Blocks[point[0], point[1]].transform.position;
        transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
        _playerPosition = point;
        gameObject.transform.position = BlockHolder.Blocks[point[0], point[1]].transform.position + Vector3.up * 5;
    }
    
    public Block GetClosetBlock(List<Block> blocks)
    {
        var closest = blocks[0];
        var closestDistance = Vector3.Distance(transform.position, closest.transform.position);
        foreach (var block in blocks)
        {
            var newDistance = Vector3.Distance(transform.position, block.transform.position);
            if (newDistance < closestDistance)
            {
                closest = block;
                closestDistance = newDistance;
            }
        }

        return closest;
    }
    
    /// <summary>
    /// Calculates next point to walk towards based on current player coords
    /// </summary>
    /// <param name="destination">Final point</param>
    /// <returns>Coordinates of next block</returns>
    public Vector2Int GetNextCoordTowards(Vector2Int destination)
    {
        var xDistance = destination[0] - _playerPosition[0];
        var yDistance = destination[1] - _playerPosition[1];

        var xSign = Sign(xDistance);
        var ySign = Sign(yDistance);
        
        // Move by X axis if deltaX is wider than deltaY
        if (Mathf.Abs(xDistance) > Mathf.Abs(yDistance))
            return _playerPosition + new Vector2Int(1 * xSign, 0);
        
        // Move by Y axis if deltaY is longer than deltaX
        if (Mathf.Abs(xDistance) < Mathf.Abs(yDistance))
            return _playerPosition + new Vector2Int(0, 1 * ySign);

        // Move diagonally if deltaX == deltaY
        return _playerPosition + new Vector2Int(xSign, ySign);

        int Sign(int value)
        {
            if (value == 0)
                return 0;
            if (value > 0)
                return 1;
            return -1;
        }
    }
    
}
