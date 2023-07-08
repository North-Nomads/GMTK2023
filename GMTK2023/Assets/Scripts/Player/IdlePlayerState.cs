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
        switch (currentState)
        {
            case StateMode.None:
                var targetBlocks = Player.ScanAreaAroundPlayer<WorkbenchBlock>();
                var availableBlocks = targetBlocks.Where(x => Player.Inventory.Contains((x.PlacedBlock as WorkbenchBlock).CraftResource));
                if (availableBlocks.Any())
                {
                    Target = availableBlocks.OrderBy(x => (x.transform.position - Player.transform.position).sqrMagnitude).FirstOrDefault();
                }
                else
                {
                    Player.SwitchState<SortingPlayerState>();
                    currentState = StateMode.None;
                    break;
                }
                currentState = StateMode.MovingToTarget;
                break;
            case StateMode.MovingToTarget:
                Player.MoveOnPoint(Player.GetNextCoordTowards(targetPosition));
                if ((Player.transform.position - target.transform.position).magnitude < InteractionDistance)
                {
                    if (workbench == null)
                    {
                        Player.SwitchState<SortingPlayerState>();
                        currentState = StateMode.None;
                        break;
                    }
                    int amount = Player.Inventory.CountItems(workbench.CraftResource);
                    workbench.PlaceItems(amount);
                    Player.Inventory.RemoveItems(workbench.CraftResource, amount);
                    currentState = StateMode.Waiting;
                }
                break;
            case StateMode.Waiting:
                if (!workbench.IsCrafting)
                {
                    Player.Inventory.AddItems(workbench.PickItemsUp());
                    Player.SwitchState<SortingPlayerState>();
                    currentState = StateMode.None;
                }
                break;
        }
    }

    private enum StateMode
    {
        None,
        MovingToTarget,
        Waiting,
    }
}