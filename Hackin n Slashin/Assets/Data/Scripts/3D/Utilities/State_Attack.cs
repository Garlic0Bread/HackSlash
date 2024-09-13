using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : State
{
    public override State Tick(Enemy_Manager enemy_Manager)
    {
        Debug.Log("Attack!");
        enemy_Manager.anim.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
        return this;
    }
}
