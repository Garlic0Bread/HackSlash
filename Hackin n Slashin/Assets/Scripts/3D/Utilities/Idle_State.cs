using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_State : State
{
    Pursue_State pursueTarget_State;
    [SerializeField] bool hasTarget;

    private void Awake()
    {
        pursueTarget_State = GetComponent<Pursue_State>();
    }
    public override State Tick()
    {
        if (hasTarget)
        {
            Debug.Log("target has been found");
            return pursueTarget_State;
        }
        else
        {
            Debug.Log("no target found");
            return this;
        }
    }
}
