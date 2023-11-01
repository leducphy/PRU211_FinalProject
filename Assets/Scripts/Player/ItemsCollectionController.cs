﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsCollectionController : MonoBehaviour
{
    public GameObject CoinCollectionEffectPrefab;
    // add sound in here
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Items_Coin"))
        {
            GameObject CoinCollectionEffect = Instantiate(CoinCollectionEffectPrefab, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            StartCoroutine(WaitForAnimationEnd(CoinCollectionEffect, collision.gameObject));
        }
    }

    private IEnumerator WaitForAnimationEnd(GameObject CoinCollectionEffect,GameObject Coin)
    {
        // Đợi cho đến khi animation hoàn thành
        yield return new WaitForSeconds(CoinCollectionEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);  
        Destroy(CoinCollectionEffect);
        Destroy(Coin);
    }
}