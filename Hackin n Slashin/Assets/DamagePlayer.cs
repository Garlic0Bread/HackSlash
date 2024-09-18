using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.GetComponent<PlayerStats>();
        if(stats != null )
        {
            stats.TakeDamage(damage);
        }
    }
}
