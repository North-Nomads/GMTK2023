using System.Collections.Generic;
using UnityEngine;

internal class SortingPlayerState : BasicPlayerState
{
    private float _swarmingTimeLeft;
    private List<Block> _chests;
    private ChestBlock _target;
    
    public SortingPlayerState(PlayerBehavior player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        _chests = Player.ScanAreaAroundPlayer<ChestBlock>();
        _swarmingTimeLeft = 1f;
    }

    public override void Update()
    {
        if (_chests.Count == 0)
        {
            Player.SwitchState<ObservePlayerState>();
            return;
        }

        if (_target is null)
        {
            _target = Player.GetClosetBlock(_chests).PlacedBlock as ChestBlock;
        }

        var newPosition = Player.GetNextCoordTowards(_target.ParentBlock.Position);
        Player.MoveOnPoint(newPosition);

        if (Player.PlayerPosition == _target.ParentBlock.Position)
        {
            _swarmingTimeLeft -= Time.deltaTime;
            if (_swarmingTimeLeft > 0)
                return;
            
            Player.Inventory.Clear();
            _chests.Clear();
        }
        
        
        

        
        
        
    }
}