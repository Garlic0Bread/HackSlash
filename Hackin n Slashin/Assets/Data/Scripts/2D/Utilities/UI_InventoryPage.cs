using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryPage : MonoBehaviour
{
    [SerializeField] private UI_InventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;

    List<UI_InventoryItem> ListOfUIItems = new List<UI_InventoryItem>();

    private void Start()
    {
        Hide();
    }
    public void IntializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UI_InventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            ListOfUIItems.Add(uiItem);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
