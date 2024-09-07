using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public Idle_State startingState;
    [SerializeField]private State currentState;

    private void Awake()
    {
        currentState = startingState;
    }
    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void HandleStateMachine()
   {
        State nextState;
        if(currentState != null)
        {
            nextState = currentState.Tick();

            if(nextState != null )
            {
                currentState = nextState;
            }
        }
   }
}
