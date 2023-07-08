using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using World;

internal class ObservePlayerState : BasicPlayerState
{
    private float _scanTimer = 3f;
    private float _currentScanTimer;
    public ObservePlayerState(PlayerBehavior player) : base(player)
    {
        _currentScanTimer = _scanTimer;
    }

    public override void Update()
    {
        _currentScanTimer -= Time.deltaTime;
        if (_currentScanTimer >= 0) return;
        
        var blocks = Player.ScanAreaAroundPlayer<StoneBlock>();
        if (blocks.Count == 0)
            return;
        
        var closestBlock = GetClosetBlock(blocks);
        Debug.Log(closestBlock);
        var coords = Player.GetNextCoordTowards(closestBlock.Position);
        Player.MoveOnPoint(coords);
        if (closestBlock.Position == Player.PlayerPosition)
        {
            Player.Inventory.AddItems(closestBlock.PlacedBlock.PickItemsUp());
            closestBlock.ClearOre();
        }
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