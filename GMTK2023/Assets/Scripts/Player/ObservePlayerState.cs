using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using World;

internal class ObservePlayerState : BasicPlayerState
{
    private float _scanTimer = 3f;
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

        // Set goal if none
        if (_goalBlock is null)
        {
            var blocks = Player.ScanAreaAroundPlayer<StoneBlock>();
            if (blocks.Count == 0)
                return;
        
            _goalBlock = GetClosetBlock(blocks).PlacedBlock;    
        }
        
        // Follow current goal
        Debug.Log(_goalBlock.ParentBlock.Position);
        var coords = Player.GetNextCoordTowards(_goalBlock.ParentBlock.Position);
        Player.MoveOnPoint(coords);
        
        // Goal reached check
        if (_goalBlock.ParentBlock.Position == Player.PlayerPosition)
        {
            Player.Inventory.AddItems(_goalBlock.ParentBlock.PlacedBlock.PickItemsUp());
            _goalBlock.ParentBlock.ClearOre();
            _goalBlock = null;
        }
        
        // Reboot timer
        _currentScanTimer = _scanTimer;
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