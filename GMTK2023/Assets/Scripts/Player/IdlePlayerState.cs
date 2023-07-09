using System.Collections.Generic;
using System.Linq;

internal class IdlePlayerState : BasicPlayerState
{
    private List<Block> _entities;
    public IdlePlayerState(PlayerBehavior player) : base(player)
    { }

    public override void OnStateEnter()
    {
        _entities = Player.ScanAreaAroundPlayer<WorkbenchBlock>().Where(x => ((WorkbenchBlock)x.PlacedBlock).IsCrafting).ToList();
    }

    public override void Update()
    {
        if (_entities.Count == 0)
        {
            _entities.Clear();
            Player.SwitchState<SortingPlayerState>();
        }
            

        // Get first workbench
        var target = _entities[0];
        var moveTowards = Player.GetNextCoordTowards(target.Position);
        Player.MoveOnPoint(moveTowards);

        // If reached
        if (Player.PlayerPosition == target.Position)
        {
            var workbench = (WorkbenchBlock)target.PlacedBlock;
            // If finished working -> get resources and move towards next workbench
            if (!workbench.IsCrafting)
            {
                workbench.PickItemsUp();
                _entities.Remove(workbench.ParentBlock);
            }
        }

    }
}