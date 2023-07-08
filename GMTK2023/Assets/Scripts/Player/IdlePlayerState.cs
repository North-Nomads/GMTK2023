using System.Linq;
using UnityEngine;

internal class IdlePlayerState : BasicPlayerState
{
    public IdlePlayerState(PlayerBehavior player) : base(player)
    {
    }

    public override void Update()
    {
        var blocks = Player.ScanAreaAroundPlayer<InteractiveBlock>();
        var block = blocks.OrderBy(x => Vector3.Distance(x.transform.position, Player.transform.position)).First();
        var workbench = block.PlacedBlock as InteractiveBlock;
        if (workbench.IsCrafting) return;
        Player.Inventory.AddItems(workbench.PickItemsUp());
        Player.SwitchState<SortingPlayerState>();
    }
}