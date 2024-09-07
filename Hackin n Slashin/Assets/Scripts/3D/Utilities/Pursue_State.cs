using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue_State : State
{
    public override State Tick()
    {
        Debug.Log("is in idle state");
        return this;
    }
}
