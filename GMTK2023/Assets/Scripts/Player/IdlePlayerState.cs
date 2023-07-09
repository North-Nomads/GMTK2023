using System.Linq;
using UnityEngine;

internal class IdlePlayerState : BasicPlayerState
{
    private const int InteractionDistance = 2;

    private StateMode currentState;
    private Block target;
    private Vector2Int targetPosition;

    public Block Target
    {
        get => target;
        private set
        {
            target = value;
            targetPosition = Vector2Int.FloorToInt(value.transform.position);
        }
    }

    public IdlePlayerState(PlayerBehavior player) : base(player)
    {
    }

    public override void Update()
    {
        var workbench = Target.PlacedBlock as WorkbenchBlock;
        if (!workbench.IsCrafting)
        {
            Player.Inventory.AddItems(workbench.PickItemsUp());
            Player.SwitchState<SortingPlayerState>();
            currentState = StateMode.None;
        }
    }
}