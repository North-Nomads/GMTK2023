using System.Collections.Generic;
using UnityEngine;
using World;

internal class ObservePlayerState : BasicPlayerState
{
    private const float ScanTimer = .5f;
    private float _currentScanTimer;
    private PlaceableBlock _goalBlock;
    private int _failedScanCounter;
    private bool _isHeadingHome;

    public ObservePlayerState(PlayerBehavior player) : base(player)
    {
        _currentScanTimer = ScanTimer;
    }

    public override void OnStateEnter()
    {
        _isHeadingHome = false;
        _failedScanCounter = 0;
    }

    public override void Update()
    {
        _currentScanTimer -= Time.deltaTime;
        if (_currentScanTimer >= 0) return;
        // Reboot timer
        _currentScanTimer = ScanTimer;

        if (Player.Inventory.IsFull || _isHeadingHome) 
        {
            var baseCoords = Player.GetNextCoordTowards(Player.PlayerBaseCoords); // base
            Player.MoveOnPoint(baseCoords);
            if (Player.PlayerPosition == Player.PlayerBaseCoords)
                Player.SwitchState<BuildingPlayerState>();
            return;
        }

        // Set goal if none
        if (_goalBlock is null)
        {
            var blocks = Player.ScanAreaAroundPlayer<StoneBlock>();
            if (blocks.Count == 0)
            {
                var posOffset = GenerateOffset();
                Player.MoveOnPoint(Player.PlayerPosition + posOffset);
                _failedScanCounter++;
                if (_failedScanCounter == 3)
                {
                    _isHeadingHome = true;
                    _failedScanCounter = 0;
                }
                return;
            }

            _goalBlock = GetClosetBlock(blocks).PlacedBlock;
        }

        // Follow current goal
        var coords = Player.GetNextCoordTowards(_goalBlock.ParentBlock.Position);
        Player.MoveOnPoint(coords);
        
        // Goal reached check
        if (_goalBlock.ParentBlock.Position == Player.PlayerPosition)
        {
            var items = _goalBlock.ParentBlock.PlacedBlock.PickItemsUp();
            switch (items.Item)
            {
                case ItemType.Stone:
                    Player.Inventory.Stones += items.Count;
                    break;
                case ItemType.Iron:
                    Player.Inventory.Iron += items.Count;
                    break;
            }
            _goalBlock.ParentBlock.ClearOre();
            _goalBlock = null;
        }

        Vector2Int GenerateOffset()
        {
            var pos = Player.PlayerPosition;
            int xCoord, yCoord;
            
            if (pos[0] < 3)
                xCoord = 1;
            else if (pos[0] >= BlockHolder.WorldSize - 3)
                xCoord = -1;
            else
                xCoord = Random.Range(-1, 1);

            if (pos[1] < 3)
                yCoord = 1;
            else if (pos[1] >= BlockHolder.WorldSize - 3)
                yCoord = -1;
            else
                yCoord = Random.Range(-1, 1);
            
            return new Vector2Int(xCoord, yCoord);
        }
    }

    private Block GetClosetBlock(List<Block> blocks)
    {
        var closest = blocks[0];
        var closestDistance = Vector3.Distance(Player.transform.position, closest.transform.position);
        foreach (var block in blocks)
        {
            var newDistance = Vector3.Distance(Player.transform.position, block.transform.position);
            if (newDistance < closestDistance)
            {
                closest = block;
                closestDistance = newDistance;
            }
        }

        return closest;
    }
}