using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{

    //base class for future states
    public virtual State Tick()
    { 
        return this;
    }
}
