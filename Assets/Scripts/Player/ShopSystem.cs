using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject screen;
    [SerializeField] private int hpCoin = 10;
    [SerializeField] private int hpBuy = 5;
    [SerializeField] private AudioClip buySound;
    [SerializeField] private AudioClip errorSound;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        if (screen != null && screen.activeSelf && Input.GetKeyDown(KeyCode.B))
        {
          if (ItemsCollectionController.CoinCollected > 0 && GetComponent<PlayerHealth>().CurrentHealth < GetComponent<PlayerHealth>().Health)
            {
                ItemsCollectionController.CoinCollected -= hpCoin;
                SoundManagement.instance.PlaySound(buySound);
                GetComponent<ItemsCollectionController>().txtCoin.text = ItemsCollectionController.CoinCollected + "";
                GetComponent<PlayerHealth>().CurrentHealth += hpBuy;
                // + hp
            }
            else
            {
                SoundManagement.instance.PlaySound(errorSound);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (screen != null && collider2D.gameObject.CompareTag("ShopBuyer"))
        {
            Show();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (screen != null && collision.gameObject.CompareTag("ShopBuyer"))
        {
            Hide();
        }
    }

    private void Show()
    {
        if (screen != null)
        {
            screen.SetActive(true);
        }
    }

    private void Hide()
    {
        if (screen != null)
        {
            screen.SetActive(false);
        }
    }
}
