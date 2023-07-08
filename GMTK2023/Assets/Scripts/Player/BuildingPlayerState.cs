using UnityEngine;

internal class BuildingPlayerState : BasicPlayerState
{
    public BuildingPlayerState(PlayerBehavior player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Building state");
    }

    public override void Update()
    {

    }
}