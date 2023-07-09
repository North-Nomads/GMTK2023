using System.Collections.Generic;
using System.Linq;

internal class IdlePlayerState : BasicPlayerState
{
    private IEnumerable<Block> _entities;
    public IdlePlayerState(PlayerBehavior player) : base(player)
    { }

    public override void OnStateEnter()
    {
        _entities = Player.ScanAreaAroundPlayer<WorkbenchBlock>().Where(x => ((WorkbenchBlock)x.PlacedBlock).IsCrafting);
    }

    public override void Update()
    {
        int count = _entities.Count();
        if (count == 0)
            Player.SwitchState<SortingPlayerState>();
    }
}