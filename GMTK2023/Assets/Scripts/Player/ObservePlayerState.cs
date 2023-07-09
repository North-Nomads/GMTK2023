using System.Collections.Generic;
using UnityEngine;
using World;

internal class ObservePlayerState : BasicPlayerState
{
    private float _scanTimer = .5f;
    private float _currentScanTimer;
    private PlaceableBlock _goalBlock;
    public ObservePlayerState(PlayerBehavior player) : base(player)
    {
        _currentScanTimer = _scanTimer;
    }

    public override void Update()
    {
        _currentScanTimer -= Time.deltaTime;
        if (_currentScanTimer >= 0) return;
        // Reboot timer
        _currentScanTimer = _scanTimer;
        
        if (Player.Inventory.IsFull)
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
                Player.MoveOnPoint(Player.PlayerPosition + new Vector2Int(Random.Range(-1, 1), Random.Range(-1, 1)));
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