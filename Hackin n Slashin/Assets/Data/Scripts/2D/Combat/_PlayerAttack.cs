using OWL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private RaycastHit2D[] hits;
    private int currentAttack = 0;
    private float timeSinceAttack = 0.0f;

    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private float timeBtwAttacks = 0.25f;
    private void Start()
    {
        anim = GetComponent<Animator>();
        timeSinceAttack = timeBtwAttacks;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && timeSinceAttack >= timeBtwAttacks)
        {
            Attack();
        }
        timeSinceAttack += Time.deltaTime;
    }

    private void Attack()
    {
        //deal damage
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

        for(int i = 0; i < hits.Length; i++)
        {
            IDamageable i_Damageable = hits[i].collider.gameObject.GetComponent<IDamageable>();
            if(i_Damageable != null )
            {
                i_Damageable.Damage(damageAmount);
            }
        }

        //combo & animations
        currentAttack++;
        // Loop back to one after third attack
        if (currentAttack > 3)
            currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (timeSinceAttack > 1.0f)
            currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        anim.SetTrigger("Attack" + currentAttack);

        // Reset timer
        timeSinceAttack = 0.0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}
