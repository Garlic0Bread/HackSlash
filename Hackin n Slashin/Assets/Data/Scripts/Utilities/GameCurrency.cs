using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameCurrency : MonoBehaviour
{
    public int enemiesInLevel;
    [SerializeField] private float exp;
    public int Points; //core is received from shooting enemies <when bullet touches enemye>

    [SerializeField] private Image expBar;
    [SerializeField] private TMP_Text numCoins;

    [SerializeField] private GameObject nextLevelPoint;
    [SerializeField] private GameObject Store;

    [SerializeField] private UI_InventoryPage inventoryUI;
    [SerializeField] private AudioSource expReachedMax;
    [SerializeField] private Button KirinButton;

    public int inventorySize = 10;

    private void Start()
    {
        //nextLevelPoint.SetActive(false);
        //KirinButton.interactable = false;

        inventoryUI.IntializeInventoryUI(inventorySize);
    }

    private void Update()
    {
        UpdateExp();

        //numCoins.text = Points.ToString();
        if(Points <= 0)
        {
            Points = 0;
        }
        // Ability_Spawner spawnItem = FindObjectOfType<Ability_Spawner>(); When player's Exp reaches 100 reward with choice of ability
        // spawnItem.Spawn();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
   
    void UpdateExp()
    {
        /*if (exp >= 100)
        {
            KirinButton.interactable = true;
            exp = 0;
        }*/
    }
    public void IncreaseExp(float expToGive) //call when enemy dies
    {
        exp += expToGive;
        expBar.fillAmount = exp / 100f;
        if (exp >= 100)
        {
            Points += 1;
        }
        // Enemy increaseEnemySpeed = FindObjectOfType<Enemy>();
        // increaseEnemySpeed.IncreaseSpeed(0.05f);
    }
    public void EnableStore(bool storeAvailable = false)
    {
        if(storeAvailable == true)
        {
            Store.SetActive(true);
        }
        if (storeAvailable == false)
        {
            Store.SetActive(false);
        }
    }

    public void EnemiesLeft(int removePoint)
    {
        enemiesInLevel = enemiesInLevel - removePoint;
        if(enemiesInLevel <= 0)
        {
            nextLevelPoint.SetActive(true);
        }
    }
}
