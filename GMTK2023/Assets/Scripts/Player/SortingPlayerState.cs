internal class SortingPlayerState : BasicPlayerState
{
    public SortingPlayerState(PlayerBehavior player) : base(player)
    {
    }

    public override void Update()
    {
        // HACK: remove when implementing.
        Player.Inventory.Clear();
        Player.SwitchState<ObservePlayerState>();
    }
}