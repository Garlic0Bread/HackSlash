using OWL;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class _PlayerAttack : MonoBehaviour
{
    public UIManager UIM;
    public UIManagerPlayer2 UIMP2;
    private Animator anim;
    private RaycastHit2D[] hits;
    private int currentAttack = 0;
    public float timeSinceAttack = 0.0f;
    public bool isAttacking = false;
    public string PlayerTagName;

    [Header("Audio Handling")]
    [SerializeField] private bool bonusOn;
    [SerializeField] private float currentAudioTime;
    [SerializeField] private float timeWindow = 5f; // 5-second window
    [SerializeField] private float targetTime = 20f; // 00:20 in seconds
    [SerializeField] private float bonusDamage = 3f;
    [SerializeField] private AudioSource audioSource; // Your music track

    [Header("Melee Attack")]
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float timeBtwAttacks = 0.25f;
    [SerializeField] private GameObject damageEffect;

    [Header("Shooting Attack")]
    [SerializeField] private float nextFireTime;
    [SerializeField] private float shieldTimout;
    [SerializeField] private Transform playerGun;
    [SerializeField] private int numBinSpread = 3;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float spreadAngle = 15f;
    public ShootMode currentShootmode = ShootMode.Single;

    public enum ShootMode
    {
        Single,
        Spread,
    }
    private void Start()
    {
        SetShootMode(currentShootmode);
        anim = GetComponent<Animator>();
        timeSinceAttack = timeBtwAttacks;
        //audioSource.Play();
    }
    private void Update()
    {
        if (this.gameObject.CompareTag("Player1"))
        {
            
            // currentAudioTime = audioSource.time;
            if (currentAudioTime >= targetTime - timeWindow && currentAudioTime <= targetTime + timeWindow)
            {
                // Player hit within the timing window, apply bonus damage
                if (Input.GetKeyDown(KeyCode.E) && timeSinceAttack >= timeBtwAttacks)
                {
                    print("bonus damage activated");
                    bonusOn = true;
                    Melee_Attack();
                }
            }

            if (_InputManager.isAttacking && timeSinceAttack >= timeBtwAttacks)
            {
                isAttacking = true;
                Melee_Attack();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Projectile_Attack();
                print("shooting");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1)) //changing shooting modes
            {
                SetShootMode(ShootMode.Single);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetShootMode(ShootMode.Spread);
            }
            timeSinceAttack += Time.deltaTime;
        }

        if (this.gameObject.CompareTag("Player2"))
        {
            
            // currentAudioTime = audioSource.time;
            if (currentAudioTime >= targetTime - timeWindow && currentAudioTime <= targetTime + timeWindow)
            {
                // Player hit within the timing window, apply bonus damage
                if (Input.GetKeyDown(KeyCode.E) && timeSinceAttack >= timeBtwAttacks)
                {
                    print("bonus damage activated");
                    bonusOn = true;
                    Melee_Attack();
                }
            }

            if (_InputManager1.isAttacking && timeSinceAttack >= timeBtwAttacks)
            {
                isAttacking = true;
                Melee_Attack();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Projectile_Attack();
                print("shooting");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1)) //changing shooting modes
            {
                SetShootMode(ShootMode.Single);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetShootMode(ShootMode.Spread);
            }
            timeSinceAttack += Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
    private void Melee_Attack()
    {
        //deal damage
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

        for(int i = 0; i < hits.Length; i++)
        {
            IDamageable i_Damageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

            GameObject effect = Instantiate(damageEffect, attackTransform.position, Quaternion.identity);
            Destroy(effect, 5f);

            if (bonusOn && i_Damageable != null)// Player hit within song keyMoment timing-window, apply bonus damage
            {
                i_Damageable.Damage(bonusDamage);
            }

            else if  (i_Damageable != null )
            {
                i_Damageable.Damage(damageAmount);
            }
        }

        //combo & animations
        currentAttack++;
        // Loop back to one after third attack
        if (currentAttack > 3)
            currentAttack = 1;

        // Reset Melee_Attack combo if time since last attack is too large
        if (timeSinceAttack > 1.0f)
            currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        anim.SetTrigger("Melee_Attack" + currentAttack);

        // Reset timer
        timeSinceAttack = 0.0f;
    }
    void Projectile_Attack()
    {
        switch (currentShootmode)
        {
            case ShootMode.Single:
                ShootSingle();
                break;
            case ShootMode.Spread:
                SpreadShooting();
                break;
        }
    }

    void ShootSingle()
    {
        // Instantiate a bullet prefab at the player's gun position and rotation
        GameObject bullet = Instantiate(bulletPrefab, playerGun.position, Quaternion.identity);

        // Apply velocity to the bullet in the forward direction of the gun
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = playerGun.right * bulletSpeed;
    }
    void SpreadShooting()
    {
        for (int i = 0; i < numBinSpread; i++)
        {
            float angle = playerGun.rotation.eulerAngles.z - spreadAngle / 2f + i * (spreadAngle / (numBinSpread - 1));

            // Calculate direction based on the angle
            Vector3 direction = Quaternion.Euler(0f, 0f, angle) * Vector3.right;
            GameObject bullet = Instantiate(bulletPrefab, playerGun.position, Quaternion.identity);

            // Apply velocity to the bullet in the forward direction of the gun
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }
    void SetShootMode(ShootMode newMode)
    {
        currentShootmode = newMode;
        Debug.Log("Switched to " + currentShootmode.ToString() + " mode");
    }
}
