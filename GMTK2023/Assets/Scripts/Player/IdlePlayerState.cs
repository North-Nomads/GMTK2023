using System.Linq;

internal class IdlePlayerState : BasicPlayerState
{
    private const int InteractionDistance = 2;


    public IdlePlayerState(PlayerBehavior player) : base(player)
    {
    }

    public override void Update()
    {
        /*var workbench = Target.PlacedBlock as WorkbenchBlock;
        if (!workbench.IsCrafting)
        {
            Player.Inventory.AddItems(workbench.PickItemsUp());
            Player.SwitchState<SortingPlayerState>();
            currentState = StateMode.None;
        }*/
    }
}