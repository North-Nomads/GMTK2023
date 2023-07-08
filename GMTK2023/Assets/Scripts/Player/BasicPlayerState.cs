public abstract class BasicPlayerState
{
    public PlayerBehavior Player { get; }

    public BasicPlayerState(PlayerBehavior player)
    {
        Player = player;
    }

    public virtual void OnStateEnter() 
    {
    }

    public virtual void OnStateLeave() 
    {   
    }

    public virtual void Update()
    {
    }
}