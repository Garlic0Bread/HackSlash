using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Enemies : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private int damagePoint;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float attackCooldown;

    [SerializeField] private float nextFireTime; // Time of the next allowed fire
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float lockOnRange = 10f; 
    [SerializeField] private float bulletSpeed = 1.5f;

    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private GameObject shrapnelVFX;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private bool Swarmer = false;
    [SerializeField] private bool Shooter = false;

    private float _lastAttackTime;
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
  
    private void FixedUpdate()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

        if (player.Length > 0)
        {
            // Find the closest enemy
            Transform closestPlayer = GetClosestPlayer(player);

            // Check if the closest enemy is within lock-on range
            if (Swarmer && (transform.position - closestPlayer.position).sqrMagnitude <= lockOnRange) //moving enemies, layer
            {
                Swarm();
            }

            else if (Shooter && (transform.position - closestPlayer.position).sqrMagnitude <= lockOnRange) //shooting layer
            {
                if (Time.time >= nextFireTime)
                {
                    ShootPlayer();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    Transform GetClosestPlayer(GameObject[] players)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = (transform.position - player.transform.position).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = player.transform;
            }
        }
        return closestEnemy;
    }

    private void ShootPlayer()
    {
        GameObject bullet = Instantiate(enemyBullet, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = bulletSpawnPoint.right * bulletSpeed;
    }
    private void Swarm()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.identity;
    }

    public void IncreaseSpeed(float increaseBy)
    {
        speed += increaseBy;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.time - _lastAttackTime < attackCooldown) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            _PlayerHealth dealDamage = collision.gameObject.GetComponent<_PlayerHealth>();
            dealDamage.Damage(damage);
            _lastAttackTime = Time.time;
        }

        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Instantiate(shrapnelVFX, transform.position, Quaternion.identity);
        }
    }
}
