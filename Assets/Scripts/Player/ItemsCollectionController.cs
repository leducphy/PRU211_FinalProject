using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsCollectionController : MonoBehaviour
{
    private int coin = 0;
    [SerializeField] private Text coinText;
    // add sound in here
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Items_Coin"))
        {

            Destroy(collision.gameObject);
            //coin++;
            //coinText.text = "Coin: " + coin;
        }
    }
}
