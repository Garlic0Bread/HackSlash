using OWL;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Player_Attacker : MonoBehaviour
{
    public static Player_Attacker instance;

    private Animator anim;
    private int hasAttackCounter = Animator.StringToHash("AttackCounter");

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        TryGetComponent(out anim);
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
    }
    public int AttackCount
    {
        get => anim.GetInteger(hasAttackCounter);
        set => anim.SetInteger(hasAttackCounter, value);
    }
}
