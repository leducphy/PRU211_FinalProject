using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IShopSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject screen;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        if (screen.activeSelf && Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B key pressed!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("ShopBuyer"))
        {
            Show();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ShopBuyer"))
        {
            Hide();
        }
    }

    private void Show()
    {
        screen.SetActive(true);
    }

    private void Hide()
    {
        screen.SetActive(false);
    }
}
