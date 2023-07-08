using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private List<BasicPlayerState> playerStates;
    private BasicPlayerState currentState;

    // Start is called before the first frame update
    void Start()
    {
        playerStates = new()
        {
            new IdlePlayerState(this),
            new ObservePlayerState(this),
            new SortingPlayerState(this),
            new BuildingPlayerState(this)
        };
        currentState = playerStates[0];
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update();
    }

    public void SwitchState<T>() where T : BasicPlayerState
    {
        currentState.OnStateLeave();
        currentState = playerStates.OfType<T>().First();
        currentState.OnStateEnter();
    }
}
